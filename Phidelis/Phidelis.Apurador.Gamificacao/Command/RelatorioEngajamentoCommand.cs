using Dapper;
using Npoi.Mapper;
using Phidelis.AppService.Interfaces.Gamificacao;
using Phidelis.AppService.Servicos.Entidade.Gamificacao;
using Phidelis.AppService.ViewModel.Gamificacao;
using Phidelis.Apurador.Gamificacao.Data;
using Phidelis.Apurador.Gamificacao.Entities;
using Phidelis.Apurador.Gamificacao.Entities.Reports;
using Repositorio.Dominios.Dashboard;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Phidelis.Apurador.Gamificacao.Command
{
    public class RelatorioEngajamentoCommand : ICommand
    {
        private readonly IConquistaServicoAplicacao _APPCONQUISTA;
        private readonly bool ADICIONAR_ALUNOS_ZERADOS = true;
        private readonly int _ANOLETIVO = 2022;
        private IEnumerable<PublicoAlvoGamificacao> _ALUNOS = null;
        private List<RelatorioEngajamento> _RESULT = null;
        public RelatorioEngajamentoCommand()
        {            
            _APPCONQUISTA = new ConquistaServicoAplicacao();
            _RESULT = new List<RelatorioEngajamento>();
            if (_ANOLETIVO == 2022) _ALUNOS = _APPCONQUISTA.ObterAlunosGamificacaoAnoLetivo2022();
            else _ALUNOS = _APPCONQUISTA.ObterAlunosGamificacaoAnoLetivo2023();
        }

        public void Execute()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            AdicionarAlunosComAulasAssistidas();
            AdicionarAlunosComTopicosConcluidos();
            AdicionarAlunosComDesafiosConcluidos();
            AdicionarAlunosComParticipacoesForum();
            //AdicionarAlunosComQuantidadeDeLogins();
            //AdicionarAlunosComLivedClicks();

            if (ADICIONAR_ALUNOS_ZERADOS)
                AdicionarAlunosZerados();

            var mapper = new Mapper();
            mapper.Save($"C:\\caminho-do-arquivo{_ANOLETIVO}.xlsx", _RESULT, $"Ano Letivo - {_ANOLETIVO}", overwrite: true);

            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;

            // Format and display the TimeSpan value.
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);

            Console.WriteLine("Engajamento Elapsed " + elapsedTime);
            Console.ReadLine();
        }

        private void AdicionarAlunosComAulasAssistidas()
        {
            var sql = $@"
                    select distinct
                        username,
                        fullname,
                        data_clicou as data_referencia,
                        videoid
                    from `moodle-pos`.`view_01_QUANTIDADE DE AULAS ONLINE ASSISTIDAS`
                    where viewed = 1
                    and year(data_clicou) = {_ANOLETIVO}
                    {(_ANOLETIVO == 2022 ? "and month(data_clicou) in (7,8,9,10,11,12)" : "and month(data_clicou) in (1,2,3,4,5,6)")}
                ";

            IEnumerable<QtdAulasAssisitidas> dados = null;
            using (var conexao = MySqlConexao.Obterconexao())
                dados = conexao.Query<QtdAulasAssisitidas>(sql);

            foreach (var aluno in dados.GroupBy(a => new { a.username, a.fullname, a.data_referencia }))
            {
                var aluno_phidelis = _ALUNOS.FirstOrDefault(a => a.Matricula == aluno.Key.username && a.NomeDisciplina == aluno.Key.fullname);
                if (aluno_phidelis == null) continue;

                var linha = _RESULT.FirstOrDefault(r => r.id_aluno == aluno.Key.username && r.nome_disciplina == aluno.Key.fullname && r.data_referencia == aluno.Key.data_referencia);
                if (linha != null)
                {
                    linha.qtd_aulas_online = aluno.Count();
                }
                else
                {
                    _RESULT.Add(new RelatorioEngajamento
                    {
                        id_aluno = aluno.Key.username,
                        sexo = aluno_phidelis.Sexo,
                        data_nascimento = aluno_phidelis.DataDeNascimento,
                        nome_curso = aluno_phidelis.NomeCurso,
                        nome_disciplina = aluno.Key.fullname,
                        data_referencia = aluno.Key.data_referencia,
                        qtd_aulas_online = aluno.Count()
                    });
                }
            }
        }
        private void AdicionarAlunosComTopicosConcluidos()
        {
            var sql = $@"
                    select distinct
                        username,
                        fullname,
                        dates_viewed,
                        section_name,
                        resources_viewed
                    from `moodle-pos`.`view_02_QUANTIDADE DE TÓPICOS CONCLUÍDOS`
                    where dates_viewed is not null
                ";

            IEnumerable<QtdTopicosConcluidos> dados = null;
            using (var conexao = MySqlConexao.Obterconexao())
                dados = conexao.Query<QtdTopicosConcluidos>(sql);

            foreach (var aluno in dados.GroupBy(a => new { a.username, a.fullname, a.dates_viewed }))
            {
                var aluno_phidelis = _ALUNOS.FirstOrDefault(a => a.Matricula == aluno.Key.username && a.NomeDisciplina == aluno.Key.fullname);
                if (aluno_phidelis == null) continue;

                var alunosComDatas = aluno.Key.dates_viewed.Split(',').Distinct().ToList();
                foreach(var alunoData in alunosComDatas)
                {
                    var data_referencia = Convert.ToDateTime(alunoData);
                    if (data_referencia.Year != _ANOLETIVO) continue;

                    var listaMeses2022 = new List<int> { 7, 8, 9, 10, 11, 12 };
                    var listaMeses2023 = new List<int> { 1, 2, 3, 4, 5, 6 };
                    if (!(_ANOLETIVO == 2022 ? listaMeses2022.Any(m => m == data_referencia.Month) : listaMeses2023.Any(m => m == data_referencia.Month))) continue;

                    var linha = _RESULT.FirstOrDefault(r => r.id_aluno == aluno.Key.username && r.nome_disciplina == aluno.Key.fullname && r.data_referencia == data_referencia);
                    if (linha != null)
                    {
                        linha.qtd_topicos_concluidos = aluno.Sum(a => a.resources_viewed) / alunosComDatas.Count;
                    }
                    else
                    {
                        _RESULT.Add(new RelatorioEngajamento
                        {
                            id_aluno = aluno.Key.username,
                            sexo = aluno_phidelis.Sexo,
                            data_nascimento = aluno_phidelis.DataDeNascimento,
                            nome_curso = aluno_phidelis.NomeCurso,
                            nome_disciplina = aluno.Key.fullname,
                            data_referencia = data_referencia,
                            qtd_topicos_concluidos = aluno.Sum(a => a.resources_viewed) / alunosComDatas.Count
                        });
                    }
                }
            }
        }
        private void AdicionarAlunosComDesafiosConcluidos()
        {
            var sql = $@"
                    SELECT 
	                    user_username as username,
                        course_fullname as fullname,
                        data_conclusao as data_referencia,
                        assign_id
                    FROM `moodle-pos`.`view_03_QUANTIDADE DE DESAFIOS CONCLUIDOS`
                    where submitted = 'Sim'
                    and year(data_conclusao) = {_ANOLETIVO}
                    {(_ANOLETIVO == 2022 ? "and month(data_conclusao) in (7,8,9,10,11,12)" : "and month(data_conclusao) in (1,2,3,4,5,6)")}
                ";

            IEnumerable<QtdDesafiosConcluidos> dados = null;
            using (var conexao = MySqlConexao.Obterconexao())
                dados = conexao.Query<QtdDesafiosConcluidos>(sql);

            foreach (var aluno in dados.GroupBy(a => new { a.username, a.fullname, a.data_referencia }))
            {
                var aluno_phidelis = _ALUNOS.FirstOrDefault(a => a.Matricula == aluno.Key.username && a.NomeDisciplina == aluno.Key.fullname);
                if (aluno_phidelis == null) continue;

                var linha = _RESULT.FirstOrDefault(r => r.id_aluno == aluno.Key.username && r.nome_disciplina == aluno.Key.fullname && r.data_referencia == aluno.Key.data_referencia);
                if (linha != null)
                {
                    linha.qtd_desafios_concluidos = aluno.Count();
                }
                else
                {
                    _RESULT.Add(new RelatorioEngajamento
                    {
                        id_aluno = aluno.Key.username,
                        sexo = aluno_phidelis.Sexo,
                        data_nascimento = aluno_phidelis.DataDeNascimento,
                        nome_curso = aluno_phidelis.NomeCurso,
                        nome_disciplina = aluno.Key.fullname,
                        data_referencia = aluno.Key.data_referencia,
                        qtd_desafios_concluidos = aluno.Count()
                    });
                }
            }
        }
        private void AdicionarAlunosComParticipacoesForum()
        {
            var sql = $@"
                    SELECT 
	                    username,
                        fullname,
                        ano_referencia,
                        mes_referencia,
                        dia_referencia,
                        totalposts
                    FROM `moodle-pos`.`view_04_QUANTIDADE DE PARTICIPAÇÕES NO FORUM`
                    where ano_referencia = {_ANOLETIVO}
                    {(_ANOLETIVO == 2022 ? "and mes_referencia in (7,8,9,10,11,12)" : "and mes_referencia in (1,2,3,4,5,6)")}
                ";

            IEnumerable<QtdParticipacoesForum> dados = null;
            using (var conexao = MySqlConexao.Obterconexao())
                dados = conexao.Query<QtdParticipacoesForum>(sql);

            foreach (var aluno in dados.GroupBy(a => new { a.username, a.fullname, a.data_referencia }))
            {
                var aluno_phidelis = _ALUNOS.FirstOrDefault(a => a.Matricula == aluno.Key.username && a.NomeDisciplina == aluno.Key.fullname);
                if (aluno_phidelis == null) continue;

                var totalParticipacoes = aluno.Where(a => a.data_referencia == aluno.Key.data_referencia).Sum(a => a.totalposts);
                var linha = _RESULT.FirstOrDefault(r => r.id_aluno == aluno.Key.username && r.nome_disciplina == aluno.Key.fullname && r.data_referencia == aluno.Key.data_referencia);
                if (linha != null)
                {
                    linha.qtd_participacoes_forum = totalParticipacoes;
                }
                else
                {
                    _RESULT.Add(new RelatorioEngajamento
                    {
                        id_aluno = aluno.Key.username,
                        sexo = aluno_phidelis.Sexo,
                        data_nascimento = aluno_phidelis.DataDeNascimento,
                        nome_curso = aluno_phidelis.NomeCurso,
                        nome_disciplina = aluno.Key.fullname,
                        data_referencia = aluno.Key.data_referencia,
                        qtd_participacoes_forum = totalParticipacoes
                    });
                }
            }
        }
        private void AdicionarAlunosComQuantidadeDeLogins()
        {
            var sql = $@"
                    SELECT DISTINCT
	                    username,
                        fullname,
                        ano_referencia,
                        mes_referencia,
                        dia_referencia,
                        totallogins
                    FROM `moodle-pos`.`view_05_QUANTIDADE TOTAL DE VEZES QUE ACESSOU A DISCIPLINA`  
                    where ano_referencia = {_ANOLETIVO}
                    {(_ANOLETIVO == 2022 ? "and mes_referencia in (7,8,9,10,11,12)" : "and mes_referencia in (1,2,3,4,5,6)")}
                ";

            IEnumerable<QtdLogins> dados = null;
            using (var conexao = MySqlConexao.Obterconexao())
                dados = conexao.Query<QtdLogins>(sql);

            foreach (var aluno in dados.GroupBy(a => new { a.username, a.fullname, a.data_referencia }))
            {
                var aluno_phidelis = _ALUNOS.FirstOrDefault(a => a.Matricula == aluno.Key.username && a.NomeDisciplina == aluno.Key.fullname);
                if (aluno_phidelis == null) continue;

                var totalLogins = aluno.Where(a => a.data_referencia == aluno.Key.data_referencia).Sum(a => a.totallogins);
                var linha = _RESULT.FirstOrDefault(r => r.id_aluno == aluno.Key.username && r.nome_disciplina == aluno.Key.fullname && r.data_referencia == aluno.Key.data_referencia);
                if (linha != null)
                {
                    linha.qtd_acesso_disciplina = totalLogins;
                }
                else
                {
                    _RESULT.Add(new RelatorioEngajamento
                    {
                        id_aluno = aluno.Key.username,
                        sexo = aluno_phidelis.Sexo,
                        data_nascimento = aluno_phidelis.DataDeNascimento,
                        nome_curso = aluno_phidelis.NomeCurso,
                        nome_disciplina = aluno.Key.fullname,
                        data_referencia = aluno.Key.data_referencia,
                        qtd_acesso_disciplina = totalLogins
                    });
                }
            }
        }
        private void AdicionarAlunosComLivedClicks()
        {
            var sql = $@"
                    SELECT DISTINCT
	                    username,
                        fullname,
                        clicked_live_case,
                        last_click_date as data_referencia
                    FROM `moodle-pos`.`student_live_case_clicks`  
                    where clicked_live_case > 0
                    and year(last_click_date) = {_ANOLETIVO}
                    {(_ANOLETIVO == 2022 ? "and month(last_click_date) in (7,8,9,10,11,12)" : "and month(last_click_date) in (1,2,3,4,5,6)")}
                ";

            IEnumerable<QtdLiveClick> dados = null;

            if(_ANOLETIVO == 2023)
            {
                using (var conexao = MySqlConexao.Obterconexao())
                    dados = conexao.Query<QtdLiveClick>(sql, commandTimeout: 99999);
            }
            else
            {
                var mapper = new Mapper($"C:\\caminho-do-arquivo-{_ANOLETIVO}.xlsx");
                dados = mapper.Take<QtdLiveClick>("Aba").Select(q => q.Value).Where(a => a.data_referencia.Year == _ANOLETIVO);
            }

            foreach (var aluno in dados.GroupBy(a => new { a.username, a.fullname, a.data_referencia }))
            {
                var aluno_phidelis = _ALUNOS.FirstOrDefault(a => a.Matricula == aluno.Key.username && a.NomeDisciplina == aluno.Key.fullname);
                if (aluno_phidelis == null) continue;

                var totalLiveClicks = aluno.Where(a => a.data_referencia == aluno.Key.data_referencia).Sum(a => a.clicked_live_case);
                var linha = _RESULT.FirstOrDefault(r => r.id_aluno == aluno.Key.username && r.nome_disciplina == aluno.Key.fullname && r.data_referencia == aluno.Key.data_referencia);
                if (linha != null)
                {
                    linha.qtd_live_clicks = totalLiveClicks;
                }
                else
                {
                    _RESULT.Add(new RelatorioEngajamento
                    {
                        id_aluno = aluno.Key.username,
                        sexo = aluno_phidelis.Sexo,
                        data_nascimento = aluno_phidelis.DataDeNascimento,
                        nome_curso = aluno_phidelis.NomeCurso,
                        nome_disciplina = aluno.Key.fullname,
                        data_referencia = aluno.Key.data_referencia,
                        qtd_live_clicks = totalLiveClicks
                    });
                }
            }
        }
        private void AdicionarAlunosZerados()
        {
            _ALUNOS.ForEach(aluno =>
            {
                if (!_RESULT.Any(r => r.id_aluno == aluno.Matricula && r.nome_disciplina == aluno.NomeDisciplina))
                {
                    _RESULT.Add(new RelatorioEngajamento
                    {
                        id_aluno = aluno.Matricula,
                        sexo = aluno.Sexo,
                        data_nascimento = aluno.DataDeNascimento,
                        nome_curso = aluno.NomeCurso,
                        nome_disciplina = aluno.NomeDisciplina,
                        data_referencia = null,
                        qtd_aulas_online = 0,
                        qtd_acesso_disciplina = 0,
                        qtd_desafios_concluidos = 0,
                        qtd_participacoes_forum = 0,
                        qtd_topicos_concluidos = 0,
                        qtd_live_clicks = 0
                    });
                }
            });
        }
    }
}
