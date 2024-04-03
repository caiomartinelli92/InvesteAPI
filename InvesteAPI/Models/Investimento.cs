using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace InvesteAPI.Models
{
    public class Investimento
    {
        [Key]
        public int investimentoId { get; set; }

        [Required]
        public string? tipo { get; set; }

        [Precision(8,2)] 
        public decimal aporteInicial { get; set; }

        [Precision(8, 2)]
        [Required]
        public decimal aporteMensal { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "dd/mm/yyyy")]
        public DateTime dataInicial { get; set; }

        [Required]
        public int tempoVigencia { get; set; }

        [Precision(8, 2)]
        public decimal valorFinal { get; set; }

        [DisplayFormat(DataFormatString ="dd/mm/yyy")]
        public DateTime dataFinal { get; set; }

        public bool Resgatado { get; set; }

        public Investimento()
        {
            Resgatado = false;
        }

        //funcao para calcular o valor final estimado
        internal void CalculaEstimado(Investimento i)
        {
            if(i.aporteInicial == 0)
            {
                i.aporteInicial = i.aporteMensal;
            }

            decimal juros = (decimal)(0.15 / 12);

            if (i.tipo == "F")
            {
                i.valorFinal = i.aporteInicial + (i.aporteMensal * i.tempoVigencia) * 1 + juros;
            }

            if (i.tipo == "V")
            {
                var valorAtt = 0;
                var valorPrxMes = 0;
                for (int j = 0; j < i.tempoVigencia; j++)
                {
                    valorPrxMes = ((int)(valorAtt + juros * (valorAtt + i.aporteMensal) + i.aporteMensal));
                    valorAtt = valorPrxMes;
                }
                i.valorFinal = (i.aporteInicial + valorAtt);
            }
        }

        public void AlteraDataFinal(DateTime dataInicio, DateTime dataFinal, int vigencia)
        {
            dataFinal = dataInicio.AddMonths(vigencia);
        }

        public bool ValidacaoDeCampos (Investimento i)
        {
            if (i.dataInicial > i.dataFinal)
            {
                return false;
            }

            if (i.aporteMensal <= 0)
            {
                return false;
            }
            if (i.tipo != "F" && i.tipo != "V")
            {
                return false;
            }
            return true;
        }
    }
}
