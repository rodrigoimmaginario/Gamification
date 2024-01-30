using System;

namespace Phidelis.Apurador.Gamificacao.Entities
{
    public class QtdLogins
    {
        public string username { get; set; }
        public string fullname { get; set; }
        public int ano_referencia { get; set; }
        public int mes_referencia { get; set; }
        public int dia_referencia { get; set; }
        public DateTime data_referencia => new DateTime(ano_referencia, mes_referencia, dia_referencia).Date;
        public int totallogins { get; set; }
    }
}
