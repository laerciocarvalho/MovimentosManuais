using System.Collections.Generic;
using System.Threading.Tasks;
using MovimentosManuais.Domain.Entities;

namespace MovimentosManuais.Application.Interfaces
{
    public interface IProdutoService
    {
        Task<IEnumerable<Produto>> ObterTodosAtivosAsync();
    }
}