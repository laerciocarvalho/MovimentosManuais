using System.Collections.Generic;
using System.Threading.Tasks;
using MovimentosManuais.Application.Interfaces;
using MovimentosManuais.Domain.Entities;
using MovimentosManuais.Domain.Interfaces;

namespace MovimentosManuais.Application.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoService(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task<IEnumerable<Produto>> ObterTodosAtivosAsync()
        {
            return await _produtoRepository.ObterTodosAtivosAsync();
        }
    }
}