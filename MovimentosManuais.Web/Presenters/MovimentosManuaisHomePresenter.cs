using MovimentosManuais.Application.Interfaces;
using MovimentosManuais.Domain.Entities;
using MovimentosManuais.Web.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovimentosManuais.Web.Presenters
{
    public class MovimentosManuaisHomePresenter
    {
        private readonly IMovimentosManuaisHomeView _view;

        private readonly IProdutoService _produtoService;
        private readonly IProdutoCosifService _produtoCosifService;
        private readonly IMovimentoManualService _movimentoManualService;

        public MovimentosManuaisHomePresenter(
            IMovimentosManuaisHomeView view,
            IProdutoService produtoService,
            IProdutoCosifService produtoCosifService,
            IMovimentoManualService movimentoManualService)
        {
            _view = view;
            _produtoService = produtoService;
            _produtoCosifService = produtoCosifService;
            _movimentoManualService = movimentoManualService;
        }

        public async Task InicializarAsync()
        {
            await CarregarProdutosAsync();
            
            _view.CarregarCosifs(new List<KeyValuePair<string, string>>());
            _view.DesabilitarFormulario();

            await CarregarMovimentosAsync();

        }

        private async Task CarregarProdutosAsync()
        {
            var produtos = await _produtoService.ObterTodosAtivosAsync();

            var lista = produtos.Select(p => new KeyValuePair<string, string>(p.Codigo, p.Descricao));

            _view.CarregarProdutos(lista);
        }

        public async Task ProdutoSelecionadoAsync(string codigoProduto)
        {
            if (string.IsNullOrWhiteSpace(codigoProduto))
            {
                _view.CarregarCosifs(new List<KeyValuePair<string, string>>());
                return;
            }

            var cosifs = await _produtoCosifService.ObterPorProdutoAsync(codigoProduto);

            var lista = cosifs.Select(c =>
                new KeyValuePair<string, string>(
                    c.CodigoCosif,
                    $"{c.CodigoCosif} - {c.CodigoClassificacao}"
                ));

            _view.CarregarCosifs(lista);
        }

        public void Limpar()
        {
            _view.LimparFormulario();
        }
        public void Novo()
        {
            _view.LimparFormulario();
            _view.HabilitarFormulario();
        }

        public async Task IncluirAsync()
        {
            try
            {
                if (!int.TryParse(_view.Mes, out int mes))
                    throw new ArgumentException("Mês inválido. Informe apenas números.");

                if (mes < 1 || mes > 12)
                    throw new ArgumentException("Mês inválido. Informe um valor entre 1 e 12.");

                if (!int.TryParse(_view.Ano, out int ano))
                    throw new ArgumentException("Ano inválido. Informe apenas números.");

                if (ano < 1900 || ano > 2100)
                    throw new ArgumentException("Ano inválido. Informe um ano entre 1900 e 2100.");

                var movimento = new MovimentoManual
                {
                    Mes = mes,
                    Ano = ano,
                    CodigoProduto = _view.CodigoProdutoSelecionado,
                    CodigoCosif = _view.CodigoCosifSelecionado,
                    Descricao = _view.Descricao?.Trim(),
                    Valor = ParseValorMonetario(_view.Valor)
                };

                await _movimentoManualService.IncluirAsync(movimento);

                _view.ExibirMensagemSucesso("Movimento incluído com sucesso!");
                _view.LimparFormulario();
                _view.DesabilitarFormulario();

                await CarregarMovimentosAsync();
            }
            catch (Exception ex)
            {
                _view.ExibirMensagemErro(ex.Message);
            }
        }
        public async Task CarregarMovimentosAsync()
        {
            var movimentos = await _movimentoManualService.ListarAsync();
            _view.CarregarMovimentos(movimentos);
        }

        private decimal ParseValorMonetario(string valorTexto)
        {
            if (string.IsNullOrWhiteSpace(valorTexto))
                throw new ArgumentException("Informe o Valor.");

            valorTexto = valorTexto.Replace("R$", "")
                                   .Replace("r$", "")
                                   .Replace(" ", "")
                                   .Trim();

            if (valorTexto.Contains(","))
            {
                valorTexto = valorTexto.Replace(".", "")
                                       .Replace(",", ".");
            }
            else
            {
                int quantidadePontos = valorTexto.Count(c => c == '.');

                if (quantidadePontos > 1)
                {
                    valorTexto = valorTexto.Replace(".", "");
                }
            }

            if (!decimal.TryParse(
                    valorTexto,
                    System.Globalization.NumberStyles.Number,
                    System.Globalization.CultureInfo.InvariantCulture,
                    out decimal valor))
            {
                throw new ArgumentException("Valor inválido. Use o formato monetário brasileiro (ex: 1.234,56 ou 1234,56).");
            }

            if (valor <= 0)
                throw new ArgumentException("O Valor deve ser maior que zero.");

            valor = Math.Round(valor, 2);

            return valor;
        }
    }
}