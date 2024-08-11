using Dominio;
using MySqlConnector;
using Repositorio.Implementacao;
using Repositorio.Interface;
using System;
using System.Collections.Generic;

public class AdvogadoRepositorio : MySQLRepositorio, IAdvogadoRepositorio
{
    public void IncluirAdvogado(Advogado advogado)
    {
        AbrirConexao();
        MySqlCommand comando = Conexao.CreateCommand();
        MySqlTransaction transacao = Conexao.BeginTransaction();
        comando.Transaction = transacao;

        try
        {
            comando.CommandText = @"INSERT INTO enderecos (Logradouro, Bairro, Estado, CEP, Numero, Complemento)
                                VALUES (@Logradouro, @Bairro, @Estado, @CEP, @Numero, @Complemento)";
            comando.Parameters.AddWithValue("@Logradouro", advogado.Endereco.Logradouro);
            comando.Parameters.AddWithValue("@Bairro", advogado.Endereco.Bairro);
            comando.Parameters.AddWithValue("@Estado", advogado.Endereco.Estado);
            comando.Parameters.AddWithValue("@CEP", advogado.Endereco.Cep);
            comando.Parameters.AddWithValue("@Numero", advogado.Endereco.Numero);
            comando.Parameters.AddWithValue("@Complemento", advogado.Endereco.Complemento);
            comando.ExecuteNonQuery();

            int enderecoId = (int)comando.LastInsertedId;

            comando.CommandText = @"INSERT INTO advogados (Nome, Senioridade, EnderecoId)
                                VALUES (@Nome, @Senioridade, @EnderecoId)";
            comando.Parameters.AddWithValue("@Nome", advogado.Nome);
            comando.Parameters.AddWithValue("@Senioridade", (int)advogado.Senioridade);
            comando.Parameters.AddWithValue("@EnderecoId", enderecoId);
            comando.ExecuteNonQuery();

            transacao.Commit();
        }
        catch (Exception e)
        {
            transacao.Rollback();
            throw e;
        }
        finally
        {
            FecharConexao();
        }
    }

    public Advogado ObterAdvogado(int id)
    {
        AbrirConexao();
        MySqlCommand comando = Conexao.CreateCommand();

        try
        {
            comando.CommandText = @"SELECT a.Id, a.Nome, a.Senioridade, e.Id as EnderecoId, e.Logradouro, e.Bairro, e.Estado, e.CEP, e.Numero, e.Complemento
                                FROM advogados a
                                INNER JOIN enderecos e ON a.EnderecoId = e.Id
                                WHERE a.Id = @Id";
            comando.Parameters.AddWithValue("@Id", id);

            using (var reader = comando.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new Advogado
                    {
                        Id = reader.GetInt32("Id"),
                        Nome = reader.GetString("Nome"),
                        Senioridade = (Senioridade)reader.GetInt32("Senioridade"),
                        Endereco = new Endereco
                        {
                            Id = reader.GetInt32("EnderecoId"),
                            Logradouro = reader.GetString("Logradouro"),
                            Bairro = reader.GetString("Bairro"),
                            Estado = reader.GetString("Estado"),
                            Cep = reader.GetString("CEP"),
                            Numero = reader.GetString("Numero"),
                            Complemento = reader.GetString("Complemento")
                        }
                    };
                }
            }

            return null;
        }
        finally
        {
            FecharConexao();
        }
    }

    public void AtualizarAdvogado(Advogado advogado)
    {
        AbrirConexao();
        MySqlCommand comando = Conexao.CreateCommand();
        MySqlTransaction transacao = Conexao.BeginTransaction();
        comando.Transaction = transacao;

        try
        {
            comando.CommandText = @"UPDATE enderecos 
                                SET Logradouro = @Logradouro, Bairro = @Bairro, Estado = @Estado, CEP = @CEP, Numero = @Numero, Complemento = @Complemento
                                WHERE Id = @EnderecoId";
            comando.Parameters.AddWithValue("@Logradouro", advogado.Endereco.Logradouro);
            comando.Parameters.AddWithValue("@Bairro", advogado.Endereco.Bairro);
            comando.Parameters.AddWithValue("@Estado", advogado.Endereco.Estado);
            comando.Parameters.AddWithValue("@CEP", advogado.Endereco.Cep);
            comando.Parameters.AddWithValue("@Numero", advogado.Endereco.Numero);
            comando.Parameters.AddWithValue("@Complemento", advogado.Endereco.Complemento);
            comando.Parameters.AddWithValue("@EnderecoId", advogado.Endereco.Id);
            comando.ExecuteNonQuery();

            comando.CommandText = @"UPDATE advogados 
                                SET Nome = @Nome, Senioridade = @Senioridade
                                WHERE Id = @Id";
            comando.Parameters.AddWithValue("@Nome", advogado.Nome);
            comando.Parameters.AddWithValue("@Senioridade", (int)advogado.Senioridade);
            comando.Parameters.AddWithValue("@Id", advogado.Id);
            comando.ExecuteNonQuery();

            transacao.Commit();
        }
        catch (Exception e)
        {
            transacao.Rollback();
            throw e;
        }
        finally
        {
            FecharConexao();
        }
    }

    public void ExcluirAdvogado(int id)
    {
        using (var comando = new MySqlCommand())
        {
            try
            {
                AbrirConexao();
                comando.Connection = Conexao;
                comando.CommandText = @"
            DELETE FROM advogados
            WHERE Id = @Id";
                comando.Parameters.AddWithValue("@Id", id);

                comando.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                FecharConexao();
            }
        }
    }

    public List<Advogado> ListarAdvogados()
    {
        AbrirConexao();
        MySqlCommand comando = Conexao.CreateCommand();

        try
        {
            comando.CommandText = @"SELECT a.Id, a.Nome, a.Senioridade, e.Id as EnderecoId, e.Logradouro, e.Bairro, e.Estado, e.CEP, e.Numero, e.Complemento
                                FROM advogados a
                                INNER JOIN enderecos e ON a.EnderecoId = e.Id";

            List<Advogado> advogados = new List<Advogado>();

            using (var reader = comando.ExecuteReader())
            {
                while (reader.Read())
                {
                    var advogado = new Advogado
                    {
                        Id = reader.GetInt32("Id"),
                        Nome = reader.GetString("Nome"),
                        Senioridade = (Senioridade)reader.GetInt32("Senioridade"),
                        Endereco = new Endereco
                        {
                            Id = reader.GetInt32("EnderecoId"),
                            Logradouro = reader.GetString("Logradouro"),
                            Bairro = reader.GetString("Bairro"),
                            Estado = reader.GetString("Estado"),
                            Cep = reader.GetString("CEP"),
                            Numero = reader.GetString("Numero"),
                            Complemento = reader.GetString("Complemento")
                        }
                    };

                    advogados.Add(advogado);
                }
            }

            return advogados;
        }
        finally
        {
            FecharConexao();
        }
    }
}