using System.Collections.Generic;
using System.Threading.Tasks;
using MovimentosManuais.Domain.Entities;

namespace MovimentosManuais.Application.Interfaces
{
    public interface IMovimentoManualService
    {
        Task IncluirAsync(MovimentoManual movimento);
        Task<IEnumerable<MovimentoManualListagem>> ListarAsync();
    }
}