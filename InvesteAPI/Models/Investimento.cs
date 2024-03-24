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

        public float CalculaEstimado(float aporteInicial, float aporteMensal, int vigencia, string tipo)
        {
            var juros = 0.15 / 12;
            if (tipo == "Renda Fixa")
            {
                return (float)((aporteInicial + (aporteMensal * vigencia)) * juros * vigencia);
            }
            else
            {
                return (float)((aporteInicial + aporteMensal * Math.Pow((1 + juros), vigencia)));
            }
        }
    }
}
