namespace MovimentosManuais.Domain.Entities
{
    public class MovimentoManualListagem
    {
        public int Mes { get; set; }
        public int Ano { get; set; }
        public string CodigoProduto { get; set; }
        public string DescricaoProduto { get; set; }
        public long NumeroLancamento { get; set; }
        public string DescricaoMovimentoManual { get; set; }
        public decimal Valor { get; set; }
    }
}