using Dapper;
using Phidelis.AppService.Interfaces.Gamificacao;
using Phidelis.AppService.Servicos.Entidade.Gamificacao;
using Phidelis.Apurador.Gamificacao.Data;
using Phidelis.Apurador.Gamificacao.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Phidelis.Apurador.Gamificacao.Command
{
    public class InserirBadgeConclusaoTopicoCommand : ICommand
    {
        private readonly IConquistaServicoAplicacao _APPCONQUISTA;
        private readonly int _USUARIOID = 813;
        private readonly int _CONQUISTAID = 2;
        private readonly string _SQL;
        public InserirBadgeConclusaoTopicoCommand()
        {
            _APPCONQUISTA = new ConquistaServicoAplicacao();
            _SQL = $@"
                SELECT 
	                user_username as username,
                    course_fullname as fullname,
                    dates_viewed
                FROM moodle_section_resources
                where resources_viewed >= total_resources
                and dates_viewed is not null
            ";
        }

        public void Execute()
        {
            IEnumerable<Desafio> dadosMoodle = null;

            using (var conexao = MySqlConexao.Obterconexao())
                dadosMoodle = conexao.Query<Desafio>(_SQL);

            if (dadosMoodle.Any())
            {
                foreach (var item in dadosMoodle)
                {
                    var enturmacao = _APPCONQUISTA.ObterAlunoEnturmadoPelaMatriculaNomeDisciplina(item.username, item.fullname, _CONQUISTAID, item.idMoodle);
                    if (enturmacao != null)
                    {
                        _APPCONQUISTA.ProcessarConquistaDoAluno(
                            enturmacao.IdCursosAluno,
                            enturmacao.Ano,
                            enturmacao.Semestre,
                            _CONQUISTAID,
                            (int)enturmacao.IdDisciplinasOferta,
                            _USUARIOID,
                            DateTime.Now,
                            item.idMoodle
                        );
                    }
                }
            }
        }
    }
}
