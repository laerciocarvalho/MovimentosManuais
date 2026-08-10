using System.Collections.Generic;
using System.Threading.Tasks;
using MovimentosManuais.Domain.Entities;

namespace MovimentosManuais.Domain.Interfaces
{
    public interface IMovimentoManualRepository
    {
        Task<long> ObterProximoNumeroLancamentoAsync(int mes, int ano);
        Task IncluirAsync(MovimentoManual movimento);
        Task<IEnumerable<MovimentoManualListagem>> ListarAsync();
    }
}