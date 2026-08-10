using System;

namespace MovimentosManuais.Domain.Entities
{
    public class MovimentoManual
    {
        public int Mes { get; set; }                 
        public int Ano { get; set; }                 
        public long NumeroLancamento { get; set; }   
        public string CodigoProduto { get; set; }    
        public string CodigoCosif { get; set; }      
        public string Descricao { get; set; }        
        public DateTime DataMovimento { get; set; }  
        public string CodigoUsuario { get; set; }    
        public decimal Valor { get; set; }           
    }
}