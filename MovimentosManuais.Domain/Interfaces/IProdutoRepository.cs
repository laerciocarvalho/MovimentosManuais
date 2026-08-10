using System.Collections.Generic;
using System.Threading.Tasks;
using MovimentosManuais.Domain.Entities;

namespace MovimentosManuais.Domain.Interfaces
{
    public interface IProdutoRepository
    {
        Task<IEnumerable<Produto>> ObterTodosAtivosAsync();
    }
}