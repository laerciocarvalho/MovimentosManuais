using System.Collections.Generic;
using System.Threading.Tasks;
using MovimentosManuais.Application.Interfaces;
using MovimentosManuais.Domain.Entities;
using MovimentosManuais.Domain.Interfaces;

namespace MovimentosManuais.Application.Services
{
    public class ProdutoCosifService : IProdutoCosifService
    {
        private readonly IProdutoCosifRepository _repository;

        public ProdutoCosifService(IProdutoCosifRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ProdutoCosif>> ObterPorProdutoAsync(string codigoProduto)
        {
            if (string.IsNullOrWhiteSpace(codigoProduto))
                return new List<ProdutoCosif>();

            return await _repository.ObterPorProdutoAsync(codigoProduto);
        }
    }
}