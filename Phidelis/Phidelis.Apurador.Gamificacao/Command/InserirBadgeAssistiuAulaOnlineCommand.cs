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
    public class InserirBadgeAssistiuAulaOnlineCommand : ICommand
    {
        private readonly IConquistaServicoAplicacao _APPCONQUISTA;
        private readonly int _USUARIOID = 813;
        private readonly int _CONQUISTAID = 1;
        private readonly string _DATA;
        private readonly string _SQL;
        public InserirBadgeAssistiuAulaOnlineCommand()
        {
            _APPCONQUISTA = new ConquistaServicoAplicacao();
            _DATA = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            _SQL = $@"
                select 
                    username,
                    fullname,
                    videoid AS idMoodle,
                    DATE_FORMAT(STR_TO_DATE(data_conclusao, '%d/%m/%Y %h:%i:%s'), '%Y-%m-%d %h:%i:%s') as dataconclusao
                from student_video_views
                where completionstate = 1
                and viewed = 1
                and DATE_FORMAT(STR_TO_DATE(data_conclusao, '%d/%m/%Y'), '%Y-%m-%d') >= cast('{_DATA}' as date)
            ";
        }

        public void Execute()
        {
            IEnumerable<AssistiuAulaOnline> dadosMoodle = null;

            using (var conexao = MySqlConexao.Obterconexao())
                dadosMoodle = conexao.Query<AssistiuAulaOnline>(_SQL);

            if (dadosMoodle.Any())
            {
                foreach(var item in dadosMoodle)
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
                            item.dataconclusao,
                            item.idMoodle
                        );
                    }
                }
            }
        }
    }
}
