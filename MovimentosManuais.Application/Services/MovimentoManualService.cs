using MovimentosManuais.Application.Interfaces;
using MovimentosManuais.Domain.Entities;
using MovimentosManuais.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovimentosManuais.Application.Services
{
    public class MovimentoManualService : IMovimentoManualService
    {
        private readonly IMovimentoManualRepository _repository;

        public MovimentoManualService(IMovimentoManualRepository repository)
        {
            _repository = repository;
        }

        public async Task IncluirAsync(MovimentoManual movimento)
        {
            Validar(movimento);

            movimento.NumeroLancamento = await _repository.ObterProximoNumeroLancamentoAsync(movimento.Mes, movimento.Ano);

            movimento.CodigoUsuario = "SCOTT_TIGER";
            movimento.DataMovimento = DateTime.Now;

            await _repository.IncluirAsync(movimento);
        }
        public async Task<IEnumerable<MovimentoManualListagem>> ListarAsync()
        {
            return await _repository.ListarAsync();
        }

        private void Validar(MovimentoManual movimento)
        {
            if (movimento.Mes < 1 || movimento.Mes > 12)
                throw new ArgumentException("Mês inválido. Informe um valor entre 1 e 12.");

            if (movimento.Ano < 1900 || movimento.Ano > 2100)
                throw new ArgumentException("Ano inválido.");

            if (string.IsNullOrWhiteSpace(movimento.CodigoProduto))
                throw new ArgumentException("Selecione um Produto.");

            if (string.IsNullOrWhiteSpace(movimento.CodigoCosif))
                throw new ArgumentException("Selecione um Cosif.");

            if (string.IsNullOrWhiteSpace(movimento.Descricao))
                throw new ArgumentException("Informe a Descrição.");

            if (movimento.Descricao.Length > 50)
                throw new ArgumentException("A Descrição não pode ultrapassar 50 caracteres.");

            if (movimento.Valor <= 0)
                throw new ArgumentException("O Valor deve ser maior que zero.");
        }
    }
}