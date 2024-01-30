using Phidelis.Apurador.Gamificacao.Command;
using System;
using System.Collections.Generic;

namespace Phidelis.Apurador.Gamificacao
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var commands = new List<ICommand>()
                {
                    //new RelatorioEngajamentoCommand(),
                    new RelatorioMediaCommand(),
                    //new InserirBadgeAssistiuAulaOnlineCommand(),
                    //new InserirBadgeDesafioCommand(),
                    //new InserirBadgeConclusaoTopicoCommand(),
                    //new InserirBadgeParticipativoCommand()
                };

                commands.ForEach(command => command.Execute());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.ReadLine();
            }
        }
    }
}
