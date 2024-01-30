using Npoi.Mapper;
using Phidelis.AppService.Interfaces.Gamificacao;
using Phidelis.AppService.Servicos.Entidade.Gamificacao;
using Phidelis.AppService.ViewModel.Gamificacao;
using Phidelis.Apurador.Gamificacao.Entities.Reports;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Phidelis.Apurador.Gamificacao.Command
{
    public class RelatorioMediaCommand : ICommand
    {
        private readonly IConquistaServicoAplicacao _APPCONQUISTA;
        private readonly int _ANOLETIVO = 2023;
        private IEnumerable<PublicoAlvoGamificacao> _ALUNOS = null;
        private List<RelatorioBoletim> _RESULT = null;
        public RelatorioMediaCommand()
        {
            _APPCONQUISTA = new ConquistaServicoAplicacao();
            _RESULT = new List<RelatorioBoletim>();
            if (_ANOLETIVO == 2022) _ALUNOS = _APPCONQUISTA.ObterAlunosGamificacaoAnoLetivo2022();
            else _ALUNOS = _APPCONQUISTA.ObterAlunosGamificacaoAnoLetivo2023();
        }

        public void Execute()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            var mapperReader = new Mapper($"C:\\caminho-do-arquivo-{_ANOLETIVO}.xlsx");
            var dedicacoes = mapperReader.Take<ResultExcelDedicacao>("Aba");

            _RESULT = _ALUNOS.Select(a => new RelatorioBoletim
            {
                id_aluno = a.Matricula,
                nome_aluno = a.NomeCompleto,
                sexo = a.Sexo,
                data_nascimento = a.DataDeNascimento,
                nome_curso = a.NomeCurso,
                nome_disciplina = a.NomeDisciplina,
                final = a.Final,
                pontuacao = a.Pontuacao,
                tempo = dedicacoes.FirstOrDefault(d => $"{d.Value.firstname} {d.Value.lastname}" == a.NomeCompleto && d.Value.nome_disciplina == a.NomeDisciplina)?.Value?.tempo
            }).ToList();

            var mapperWriter = new Mapper();
            mapperWriter.Save($"C:\\caminho-do-arquivo-{_ANOLETIVO}.xlsx", _RESULT, $"Boletim - {_ANOLETIVO}", overwrite: true);

            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;

            // Format and display the TimeSpan value.
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);

            Console.WriteLine("Media Elapsed " + elapsedTime);
            Console.ReadLine();
        }
    }   

    public class ResultExcelDedicacao
    {
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string sigla { get; set; }
        public decimal? tempo { get; set; }
        public string nome_disciplina
        {
            get
            {
                return sigla == "posN_finAnç" ? "Finanças" :
                    sigla == "posN_intComMer" ? "Inteligência Competitiva de Mercado" :
                    sigla == "posN_gesVen" ? "Gestão de Vendas" :
                    sigla == "posN_conGer" ? "Contabilidade Gerencial" : string.Empty;
            }
        }
    }
}
