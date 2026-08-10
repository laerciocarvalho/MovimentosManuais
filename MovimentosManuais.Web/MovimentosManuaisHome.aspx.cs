using MovimentosManuais.Application.Services;
using MovimentosManuais.Domain.Entities;
using MovimentosManuais.Infrastructure.Data.Repositories;
using MovimentosManuais.Web.Presenters;
using MovimentosManuais.Web.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MovimentosManuais.Web
{
    public partial class MovimentosManuaisHome : Page, IMovimentosManuaisHomeView
    {
        private MovimentosManuaisHomePresenter _presenter;

        protected void Page_Load(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MovimentosManuaisConnection"].ConnectionString;

            var produtoRepository = new ProdutoRepository(connectionString);
            var produtoService = new ProdutoService(produtoRepository);

            var produtoCosifRepository = new ProdutoCosifRepository(connectionString);
            var produtoCosifService = new ProdutoCosifService(produtoCosifRepository);

            var movimentoManualRepository = new MovimentoManualRepository(connectionString);
            var movimentoManualService = new MovimentoManualService(movimentoManualRepository);

            _presenter = new MovimentosManuaisHomePresenter(this, produtoService, produtoCosifService, movimentoManualService);

            if (!IsPostBack)
            {
                RegisterAsyncTask(new PageAsyncTask(async () =>
                {
                    await _presenter.InicializarAsync();
                }));
            }
        }

        public string Mes
        {
            get => txtMes.Text;
            set => txtMes.Text = value;
        }

        public string Ano
        {
            get => txtAno.Text;
            set => txtAno.Text = value;
        }
        public string Descricao
        {
            get => txtDescricao.Text;
            set => txtDescricao.Text = value;
        }
        public string Valor
        {
            get => txtValor.Text;
            set => txtValor.Text = value;
        }
        public string CodigoProdutoSelecionado
        {
            get => ddlProduto.SelectedValue;
            set => ddlProduto.SelectedValue = value;
        }
        public string CodigoCosifSelecionado
        {
            get => ddlCosif.SelectedValue;
            set => ddlCosif.SelectedValue = value;
        }
        public void CarregarProdutos(IEnumerable<KeyValuePair<string, string>> produtos)
        {
            ddlProduto.Items.Clear();
            ddlProduto.Items.Add(new ListItem("-- Selecione --", ""));

            foreach (var item in produtos)
            {
                ddlProduto.Items.Add(new ListItem(item.Value, item.Key));
            }
        }

        public void CarregarCosifs(IEnumerable<KeyValuePair<string, string>> cosifs)
        {
            ddlCosif.Items.Clear();
            ddlCosif.Items.Add(new ListItem("-- Selecione --", ""));

            foreach (var item in cosifs)
            {
                ddlCosif.Items.Add(new ListItem(item.Value, item.Key));
            }
        }

        protected void ddlProduto_SelectedIndexChanged(object sender, EventArgs e)
        {
            string codigoProduto = ddlProduto.SelectedValue;

            RegisterAsyncTask(new PageAsyncTask(async () =>
            {
                await _presenter.ProdutoSelecionadoAsync(codigoProduto);
            }));
        }

        public void LimparFormulario()
        {
            txtMes.Text = string.Empty;
            txtAno.Text = string.Empty;
            txtValor.Text = string.Empty;
            txtDescricao.Text = string.Empty;
            
            if (ddlProduto.Items.Count > 0)
                ddlProduto.SelectedIndex = 0;

            ddlCosif.Items.Clear();
            ddlCosif.Items.Add(new ListItem("-- Selecione --", ""));
        }
        public void DesabilitarFormulario()
        {
            txtMes.Enabled = false;
            txtAno.Enabled = false;
            ddlProduto.Enabled = false;
            ddlCosif.Enabled = false;
            txtValor.Enabled = false;
            txtDescricao.Enabled = false;

            btnIncluir.Enabled = false;
            btnLimpar.Enabled = false;
        }
        public void HabilitarFormulario()
        {
            txtMes.Enabled = true;
            txtAno.Enabled = true;
            ddlProduto.Enabled = true;
            ddlCosif.Enabled = true;
            txtValor.Enabled = true;
            txtDescricao.Enabled = true;

            btnIncluir.Enabled = true;
            btnLimpar.Enabled = true;
        }
        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            _presenter.Limpar();
        }
        protected void btnIncluir_Click(object sender, EventArgs e)
        {
            RegisterAsyncTask(new PageAsyncTask(async () =>
            {
                await _presenter.IncluirAsync();
            }));
        }
        protected void btnNovo_Click(object sender, EventArgs e)
        {
            _presenter.Novo();
        }
        public void CarregarMovimentos(IEnumerable<MovimentoManualListagem> movimentos)
        {
            gvMovimentos.DataSource = movimentos;
            gvMovimentos.DataBind();
        }

        #region Alerts de JavaScript
        public void ExibirMensagemSucesso(string mensagem)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "sucesso", $"alert('{mensagem}');", true);
        }

        public void ExibirMensagemErro(string mensagem)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "erro", $"alert('Erro: {mensagem.Replace("'", "\\'")}');", true);
        }
        #endregion
    }
}