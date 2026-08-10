using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using MovimentosManuais.Domain.Entities;
using MovimentosManuais.Domain.Interfaces;

namespace MovimentosManuais.Infrastructure.Data.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly string _connectionString;

        public ProdutoRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<Produto>> ObterTodosAtivosAsync()
        {
            var produtos = new List<Produto>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var command = new SqlCommand(
                    @"SELECT COD_PRODUTO, DES_PRODUTO, STA_STATUS FROM PRODUTO WHERE STA_STATUS = 'A' ORDER BY DES_PRODUTO",
                    connection);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        produtos.Add(new Produto
                        {
                            Codigo = reader["COD_PRODUTO"].ToString().Trim(),
                            Descricao = reader["DES_PRODUTO"]?.ToString(),
                            Status = reader["STA_STATUS"]?.ToString()
                        });
                    }
                }
            }

            return produtos;
        }
    }
}