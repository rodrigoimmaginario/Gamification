using MySqlConnector;
using Phidelis.Criptografia;
using System.Configuration;

namespace Phidelis.Apurador.Gamificacao.Data
{
    public static class MySqlConexao
    {
        public static MySqlConnection Obterconexao()
        {
            var connectionString = CriptorFactory.doTipo(PadraoCriptografia.SenhaDaConfiguracao).untransform(ConfigurationManager.ConnectionStrings["PhidelisMySql"].ToString());
            return new MySqlConnection(connectionString);
        }
    }
}
