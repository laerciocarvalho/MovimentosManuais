using System.Collections.Generic;
using System.Threading.Tasks;
using MovimentosManuais.Domain.Entities;

namespace MovimentosManuais.Application.Interfaces
{
    public interface IProdutoCosifService
    {
        Task<IEnumerable<ProdutoCosif>> ObterPorProdutoAsync(string codigoProduto);
    }
}