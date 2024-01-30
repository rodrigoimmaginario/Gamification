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
    public class InserirBadgeDesafioCommand : ICommand
    {
        private readonly IConquistaServicoAplicacao _APPCONQUISTA;
        private readonly int _USUARIOID = 813;
        private readonly int _CONQUISTAID = 3;
        private readonly string _DATA;
        private readonly string _SQL;
        public InserirBadgeDesafioCommand()
        {
            _APPCONQUISTA = new ConquistaServicoAplicacao();
            _DATA = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            _SQL = $@"
                SELECT 
	                user_username as username,
                    course_fullname as fullname,
                    assign_id as idMoodle,
                    submission_date
                FROM view_desafio
                where submitted = 'Sim'
                and submission_date >= cast('{_DATA}' as date)
            ";
        }

        public void Execute()
        {
            IEnumerable<ConclusaoTopico> dadosMoodle = null;

            using (var conexao = MySqlConexao.Obterconexao())
                dadosMoodle = conexao.Query<ConclusaoTopico>(_SQL);

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
                            item.submission_date,
                            item.idMoodle
                        );
                    }
                }
            }
        }
    }
}
