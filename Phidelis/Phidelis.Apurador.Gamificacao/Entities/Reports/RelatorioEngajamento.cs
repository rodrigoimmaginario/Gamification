using System;

namespace Phidelis.Apurador.Gamificacao.Entities.Reports
{
    public class RelatorioEngajamento
    {
        public string id_aluno { get; set; }
        public string sexo { get; set; }
        public DateTime data_nascimento { get; set; }
        public string nome_curso { get; set; }
        public string nome_disciplina { get; set; }
        public string tipo_disciplina => nome_disciplina.Contains("Contabilidade Gerencial") || nome_disciplina.Contains("Finanças") ? "Hard" : "Soft";
        public DateTime? data_referencia { get; set; }
        public int qtd_aulas_online { get; set; }
        public int qtd_topicos_concluidos { get; set; }
        public int qtd_desafios_concluidos { get; set; }
        public int qtd_participacoes_forum { get; set; }
        public int qtd_acesso_disciplina { get; set; }
        public int qtd_live_clicks { get; set; }
    }
}
