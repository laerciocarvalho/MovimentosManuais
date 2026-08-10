using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using MovimentosManuais.Domain.Entities;
using MovimentosManuais.Domain.Interfaces;

namespace MovimentosManuais.Infrastructure.Data.Repositories
{
    public class ProdutoCosifRepository : IProdutoCosifRepository
    {
        private readonly string _connectionString;

        public ProdutoCosifRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<ProdutoCosif>> ObterPorProdutoAsync(string codigoProduto)
        {
            var lista = new List<ProdutoCosif>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var command = new SqlCommand(
                    @"SELECT COD_PRODUTO, COD_COSIF, COD_CLASSIFICACAO, STA_STATUS
                      FROM PRODUTO_COSIF
                      WHERE COD_PRODUTO = @CodigoProduto
                        AND (STA_STATUS = 'A' OR STA_STATUS IS NULL)  
                      ORDER BY COD_COSIF", connection);

                command.Parameters.AddWithValue("@CodigoProduto", codigoProduto);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        lista.Add(new ProdutoCosif
                        {
                            CodigoProduto = reader["COD_PRODUTO"].ToString().Trim(),
                            CodigoCosif = reader["COD_COSIF"].ToString().Trim(),
                            CodigoClassificacao = reader["COD_CLASSIFICACAO"]?.ToString()?.Trim(),
                            Status = reader["STA_STATUS"]?.ToString()
                        });
                    }
                }
            }

            return lista;
        }
    }
}