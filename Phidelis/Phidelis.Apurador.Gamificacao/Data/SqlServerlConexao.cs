using Phidelis.Criptografia;
using System.Configuration;
using System.Data.SqlClient;

namespace Phidelis.Apurador.Gamificacao.Data
{
    public static class SqlServerlConexao
    {
        public static SqlConnection Obterconexao()
        {
            var connectionString = CriptorFactory.doTipo(PadraoCriptografia.SenhaDaConfiguracao).untransform(ConfigurationManager.ConnectionStrings["PhidelisSqlServer"].ToString());
            return new SqlConnection(connectionString);
        }
    }
}
