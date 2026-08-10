using MovimentosManuais.Domain.Entities;
using System.Collections.Generic;

namespace MovimentosManuais.Web.Views
{
    public interface IMovimentosManuaisHomeView
    {
        string Mes { get; set; }
        string Ano { get; set; }
        string CodigoProdutoSelecionado { get; set; }
        string CodigoCosifSelecionado { get; set; }
        string Valor { get; set; }
        string Descricao { get; set; }
        void CarregarProdutos(IEnumerable<KeyValuePair<string, string>> produtos);
        void CarregarCosifs(IEnumerable<KeyValuePair<string, string>> cosifs);
        void CarregarMovimentos(IEnumerable<MovimentoManualListagem> movimentos);
        void ExibirMensagemSucesso(string mensagem);
        void ExibirMensagemErro(string mensagem);
        void LimparFormulario();
        void DesabilitarFormulario();
        void HabilitarFormulario();
    }
}