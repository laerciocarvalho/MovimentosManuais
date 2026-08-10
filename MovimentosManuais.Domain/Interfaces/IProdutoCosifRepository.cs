using System.Collections.Generic;
using System.Threading.Tasks;
using MovimentosManuais.Domain.Entities;

namespace MovimentosManuais.Domain.Interfaces
{
    public interface IProdutoCosifRepository
    {
        Task<IEnumerable<ProdutoCosif>> ObterPorProdutoAsync(string codigoProduto);
    }
}