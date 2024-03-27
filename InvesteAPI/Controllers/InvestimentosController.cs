using InvesteAPI.Context;
using InvesteAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InvesteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvestimentosController : ControllerBase
    {
        private readonly AppDbContext _context;

        string msgNotFound = "Não há investimentos cadastrados nesta categoria.";

        //método GET para retornar os 100 investimentos mais recentes
        [HttpGet("/todosinvesimentos")]
        public ActionResult<IEnumerable<Investimento>> GetInvestimentos()
        {
            var investimentos = _context.Investimentos.
                    Take(100).
                    AsNoTracking().
                    OrderByDescending(i => i.investimentoId).
                    ToList();
            if (investimentos is null)
            {
                return NotFound(msgNotFound);
            }
            return investimentos;
        }

        //método GET para retonar os 100 investimentos de renda fixa mais recentes
        [HttpGet("/rendafixa")]
        public ActionResult<IEnumerable<Investimento>> GetInvestimentosRF()
        {
            var investimentosRF = _context.Investimentos.
                Take(100).
                Where(i => i.tipo == "Renda Fixa").
                OrderByDescending(i => i.investimentoId).
                AsNoTracking().
                ToList();
            if(investimentosRF is null)
            {
                return NotFound(msgNotFound);
            }

            return investimentosRF;
        }

        //método GET para retornar os 100 primeiros investimentos de renda variável mais recentes
        [HttpGet("/rendavariavel")]
        public ActionResult<IEnumerable<Investimento>> GetInvestimentosRV()
        {
            var investimentosRV = _context.Investimentos.
                Take(100).
                Where(i => i.tipo == "Renda Variavel").
                OrderByDescending(i => i.investimentoId).
                AsNoTracking().
                ToList();
            if( investimentosRV is null)
            {
                return NotFound(msgNotFound);
            }
            return investimentosRV;
        }

        //método GET para retornar um investimento através do id
        [HttpGet("/investimento/{id:int}", Name ="ObterInvestimento")]
        public ActionResult<Investimento> GetInvestimento (int id)
        {
            var investimento = _context.Investimentos.FirstOrDefault(i => i.investimentoId == id);
            if(investimento == null)
            {
                return NotFound("Produto não encontrado");
            }
            return investimento;
        }

        //método GET para retornar as regras de investimentos
        [HttpGet("/regras")]
        public string GetRegras()
        {
            return "Regras de investimento: " +
                "1: O tipo de investimento deve ser: Renda Fixa ou Renda Variavel;" +
                "2: Um aporte inicial não precisa ser informado, porém o aporte mensal do primeiro mês será considerado o aporte inicial;"+
                "3: Um investimento não pode ser resgato com menos de 1 mês de rendimento"+
                "4: Ao resgatar, o IR será descontado da seguinte maneira: 20% até 6 meses; 15% até 1 ano; 5% se for na data de resgate;"+
                "5: Todos os invesmentos tem rendimento de 15% ao ano.";
        }
    }
}
