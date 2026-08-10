<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MovimentosManuaisHome.aspx.cs" Inherits="MovimentosManuais.Web.MovimentosManuaisHome" Async="true" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Movimentos Manuais</title>
    <style type="text/css">
        body {
            font-family: Arial, Helvetica, sans-serif;
            margin: 20px;
            font-size: 14px;
        }

        h1 {
            margin-bottom: 20px;
        }

        fieldset {
            border: 1px solid #999;
            padding: 15px 20px 10px 20px;
            margin-bottom: 25px;
        }

        legend {
            font-weight: bold;
            padding: 0 8px;
        }

        .form-row {
            margin-bottom: 12px;
        }

            .form-row label {
                display: inline-block;
                width: 90px;
                vertical-align: top;
                padding-top: 4px;
            }

            .form-row input[type="text"],
            .form-row select,
            .form-row textarea {
                padding: 4px 6px;
                border: 1px solid #aaa;
            }

            .form-row .campo-curto {
                width: 80px;
            }

            .form-row .campo-medio {
                width: 220px;
            }

            .form-row textarea {
                width: 450px;
                height: 90px;
                resize: vertical;
            }

        .botoes {
            margin-top: 18px;
            margin-left: 90px;
        }

            .botoes input {
                margin-right: 10px;
                padding: 5px 16px;
                min-width: 80px;
            }

        .grid {
            width: 100%;
            border-collapse: collapse;
            margin-top: 5px;
        }

            .grid th {
                background-color: #e8e8e8;
                border: 1px solid #bbb;
                padding: 6px 8px;
                text-align: left;
                font-weight: bold;
            }

            .grid td {
                border: 1px solid #bbb;
                padding: 5px 8px;
            }

            .grid tr:nth-child(even) {
                background-color: #f5f5f5;
            }
    </style>
    <script type="text/javascript">
        function somenteNumeros(e) {
            var charCode = e.which ? e.which : e.keyCode;

            if (charCode == 8 || charCode == 9 || charCode == 46 ||
                (charCode >= 37 && charCode <= 40)) {
                return true;
            }

            if (charCode < 48 || charCode > 57) {
                return false;
            }

            return true;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Movimentos Manuais</h1>

            <!-- Seção Movimento -->
            <fieldset>
                <legend>Movimento</legend>

                <div class="form-row">
                    <label>Mês:</label>
                    <asp:TextBox ID="txtMes" runat="server" CssClass="campo-curto" MaxLength="2" onkeypress="return somenteNumeros(event);" oninput="this.value = this.value.replace(/[^0-9]/g, '');" />
                    &nbsp;&nbsp;&nbsp;
                    <label style="width: 40px;">Ano:</label>
                    <asp:TextBox ID="txtAno" runat="server" CssClass="campo-curto" MaxLength="4" onkeypress="return somenteNumeros(event);" oninput="this.value = this.value.replace(/[^0-9]/g, '');" />
                </div>

                <div class="form-row">
                    <label>Produto:</label>
                    <asp:DropDownList ID="ddlProduto" runat="server" CssClass="campo-medio" AutoPostBack="true" OnSelectedIndexChanged="ddlProduto_SelectedIndexChanged" />
                    &nbsp;&nbsp;&nbsp;
                    <label style="width: 50px;">Cosif:</label>
                    <asp:DropDownList ID="ddlCosif" runat="server" CssClass="campo-medio" />
                </div>

                <div class="form-row">
                    <label>Valor:</label>
                    <asp:TextBox ID="txtValor" runat="server" CssClass="campo-curto" />
                </div>

                <div class="form-row">
                    <label>Descrição:</label>
                    <asp:TextBox ID="txtDescricao" runat="server" TextMode="MultiLine" Rows="4" />
                </div>

                <div class="botoes">
                    <asp:Button ID="btnLimpar" runat="server" Text="Limpar" OnClick="btnLimpar_Click" />
                    <asp:Button ID="btnNovo" runat="server" Text="Novo" OnClick="btnNovo_Click" />
                    <asp:Button ID="btnIncluir" runat="server" Text="Incluir" OnClick="btnIncluir_Click" />
                </div>
            </fieldset>
            <fieldset>
                <legend>Lista</legend>

                <asp:GridView ID="gvMovimentos" runat="server"
                    AutoGenerateColumns="False"
                    CssClass="grid"
                    GridLines="Both"
                    EmptyDataText="Nenhum movimento encontrado.">

                    <Columns>
                        <asp:BoundField DataField="Mes" HeaderText="Mês" ItemStyle-Width="50px" />
                        <asp:BoundField DataField="Ano" HeaderText="Ano" ItemStyle-Width="60px" />
                        <asp:BoundField DataField="CodigoProduto" HeaderText="Código do Produto" ItemStyle-Width="110px" />
                        <asp:BoundField DataField="DescricaoProduto" HeaderText="Descrição do Produto" />
                        <asp:BoundField DataField="NumeroLancamento" HeaderText="NR Lançamento" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="DescricaoMovimentoManual" HeaderText="Descrição" />
                        <asp:BoundField DataField="Valor" HeaderText="Valor"
                            DataFormatString="{0:C}"
                            ItemStyle-HorizontalAlign="Right"
                            ItemStyle-Width="110px" />
                    </Columns>
                </asp:GridView>
            </fieldset>
        </div>
        <div><a href="readme.html">Leia-me!</a></div>
    </form>
</body>
</html>
