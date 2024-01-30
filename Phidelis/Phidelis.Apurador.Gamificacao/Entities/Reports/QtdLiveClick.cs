using Npoi.Mapper.Attributes;
using System;

namespace Phidelis.Apurador.Gamificacao.Entities
{
    public class QtdLiveClick
    {
        public QtdLiveClick() { }
        public QtdLiveClick(string username, string fullname, int clicked_live_case, DateTime data_referencia)
        {
            this.username = username;
            this.fullname = fullname;
            this.clicked_live_case = clicked_live_case;
            this.data_referencia = data_referencia;
        }

        public string username { get; set; }
        public string fullname { get; set; }
        public int clicked_live_case { get; set; }
        public DateTime data_referencia { get; set; }
    }
}
