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
    public class InserirBadgeParticipativoCommand : ICommand
    {
        private readonly IConquistaServicoAplicacao _APPCONQUISTA;
        private readonly int _USUARIOID = 813;
        private readonly int _CONQUISTAID = 4;
        private readonly string _SQL;
        public InserirBadgeParticipativoCommand()
        {
            _APPCONQUISTA = new ConquistaServicoAplicacao();
            _SQL = $@"
                SELECT 
	                username,
                    fullname,
                    id as idMoodle
                FROM view_forum_posts
                where totalposts >= 2
            ";
        }

        public void Execute()
        {
            IEnumerable<Participativo> dadosMoodle = null;

            using (var conexao = MySqlConexao.Obterconexao())
                dadosMoodle = conexao.Query<Participativo>(_SQL);

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
