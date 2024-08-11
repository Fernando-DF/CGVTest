using MySqlConnector;
using Repositorio.Interface;
using System.Configuration;

namespace Repositorio.Implementacao
{
    public class MySQLRepositorio : IMySQLRepositorio
    {
        protected MySqlConnection Conexao;

        public void AbrirConexao()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            Conexao = new MySqlConnection(connectionString);
            Conexao.Open();
        }

        public void FecharConexao()
        {
            if (Conexao != null && Conexao.State == System.Data.ConnectionState.Open)
            {
                Conexao.Close();
            }
        }
    }
}