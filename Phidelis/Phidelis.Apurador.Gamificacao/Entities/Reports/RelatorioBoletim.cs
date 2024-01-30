using System;

namespace Phidelis.Apurador.Gamificacao.Entities.Reports
{
    public class RelatorioBoletim
    {
        public string id_aluno { get; set; }
        public string nome_aluno { get; set; }
        public string sexo { get; set; }
        public DateTime data_nascimento { get; set; }
        public string nome_curso { get; set; }
        public string nome_disciplina { get; set; }
        public string tipo_disciplina => nome_disciplina.Contains("Contabilidade Gerencial") || nome_disciplina.Contains("Finanças") ? "Hard" : "Soft";
        public decimal? tempo { get; set; }
        public decimal? final { get; set; }
        public int? pontuacao { get; set; }
    }
}
