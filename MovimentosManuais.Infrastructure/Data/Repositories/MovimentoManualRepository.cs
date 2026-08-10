using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using MovimentosManuais.Domain.Entities;
using MovimentosManuais.Domain.Interfaces;

namespace MovimentosManuais.Infrastructure.Data.Repositories
{
    public class MovimentoManualRepository : IMovimentoManualRepository
    {
        private readonly string _connectionString;

        public MovimentoManualRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<long> ObterProximoNumeroLancamentoAsync(int mes, int ano)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var command = new SqlCommand(
                    @"SELECT ISNULL(MAX(NUM_LANCAMENTO), 0) + 1
                      FROM MOVIMENTO_MANUAL
                      WHERE DAT_MES = @Mes AND DAT_ANO = @Ano", connection);

                command.Parameters.AddWithValue("@Mes", mes);
                command.Parameters.AddWithValue("@Ano", ano);

                var resultado = await command.ExecuteScalarAsync();
                return Convert.ToInt64(resultado);
            }
        }

        public async Task IncluirAsync(MovimentoManual movimento)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var command = new SqlCommand(
                    @"INSERT INTO MOVIMENTO_MANUAL
                        (DAT_MES, DAT_ANO, NUM_LANCAMENTO, COD_PRODUTO, COD_COSIF,
                         DES_DESCRICAO, DAT_MOVIMENTO, COD_USUARIO, VAL_VALOR)
                      VALUES
                        (@Mes, @Ano, @NumeroLancamento, @CodigoProduto, @CodigoCosif,
                         @Descricao, @DataMovimento, @CodigoUsuario, @Valor)", connection);

                command.Parameters.AddWithValue("@Mes", movimento.Mes);
                command.Parameters.AddWithValue("@Ano", movimento.Ano);
                command.Parameters.AddWithValue("@NumeroLancamento", movimento.NumeroLancamento);
                command.Parameters.AddWithValue("@CodigoProduto", movimento.CodigoProduto);
                command.Parameters.AddWithValue("@CodigoCosif", movimento.CodigoCosif);
                command.Parameters.AddWithValue("@Descricao", movimento.Descricao);
                command.Parameters.AddWithValue("@DataMovimento", movimento.DataMovimento);
                command.Parameters.AddWithValue("@CodigoUsuario", movimento.CodigoUsuario);
                command.Parameters.AddWithValue("@Valor", movimento.Valor);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task<IEnumerable<MovimentoManualListagem>> ListarAsync()
        {
            var lista = new List<MovimentoManualListagem>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("usp_ListarMovimentosManuais", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            lista.Add(new MovimentoManualListagem
                            {
                                Mes = Convert.ToInt32(reader["Mes"]),
                                Ano = Convert.ToInt32(reader["Ano"]),
                                CodigoProduto = reader["CodigoProduto"].ToString().Trim(),
                                DescricaoProduto = reader["DescricaoProduto"]?.ToString(),
                                NumeroLancamento = Convert.ToInt64(reader["NumeroLancamento"]),
                                DescricaoMovimentoManual = reader["DescricaoMovimentoManual"]?.ToString(),
                                Valor = Convert.ToDecimal(reader["Valor"])
                            });
                        }
                    }
                }
            }

            return lista;
        }
    }
}