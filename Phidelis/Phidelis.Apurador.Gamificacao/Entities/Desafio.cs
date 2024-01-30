using System;
using System.Linq;

namespace Phidelis.Apurador.Gamificacao.Entities
{
    public class Desafio
    {
        public string username { get; set; }
        public string fullname { get; set; }
        public string dates_viewed { get; set; }
        public string idMoodle => dates_viewed
            .Split(',')
            .Select(x => Convert.ToDateTime(x))
            .OrderByDescending(x => x)
            .Select(x => x.ToString("ddMMyyyyHHmmss"))
            .FirstOrDefault();
    }
}
