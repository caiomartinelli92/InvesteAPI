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

        string msgNotFound = "Nenhum registro encontrado.";

        //método GET para retornar os 100 investimentos mais recentes
        [HttpGet("/todosinvesimentos")]
        public ActionResult<IEnumerable<Investimento>> GetInvestimentos()
        {
            var investimentos = _context.Investimentos.
                    Take(100).
                    Where(i => i.Resgatado != true).
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
                Where(i => i.tipo == "F" && i.Resgatado != true).
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
                Where(i => i.tipo == "V" && i.Resgatado != true).
                OrderByDescending(i => i.investimentoId).
                AsNoTracking().
                ToList();
            if( investimentosRV is null)
            {
                return NotFound(msgNotFound);
            }
            return investimentosRV;
        }

        //método GET para retornar os investimentos que já foram resgatados
        [HttpGet("/investimentosresgatados")]
        public ActionResult<IEnumerable<Investimento>> GetResgatados()
        {
            var investimentos = _context.Investimentos.
                Take(100).
                Where(i => i.Resgatado == true).
                OrderByDescending(i => i.investimentoId).
                AsNoTracking().
                ToList();

            if(investimentos is null)
            {
                return NotFound(msgNotFound);
            }

            return investimentos;
        }

        //método GET para retornar um investimento através do id
        [HttpGet("/investimento/{id:int}", Name ="ObterInvestimento")]
        public ActionResult<Investimento> GetInvestimento (int id)
        {
            var investimento = _context.Investimentos.FirstOrDefault(i => i.investimentoId == id);
            if(investimento == null)
            {
                return NotFound(msgNotFound);
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

        //método POST para adicionar um novo investimento
        [HttpPost]
        public ActionResult CriarInvestimento(Investimento i)
        {
            if(i is null)
            {
                return BadRequest("Investimento inválido");
            }

            bool camposValidados = i.ValidacaoDeCampos(i);
            if(camposValidados == false)
            {
                return BadRequest();
            }
            
            //chamada da funcao de calculo de valor estimado de resgate
            i.CalculaEstimado(i);
            i.AlteraDataFinal(i.dataInicial, i.dataFinal, i.tempoVigencia);

            _context.Investimentos.Add(i);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObterInvesimento",
                new { id = i.investimentoId}, i);
        }
        
        //método PUT para alterar os dados do investimento 
        [HttpPut("/edit/{id:int}")]
        public ActionResult AlterarInvestimento(int id, Investimento i)
        {
            if(id != i.investimentoId)
            {
                return BadRequest();
            }

            bool camposValidados = i.ValidacaoDeCampos(i);
            if(camposValidados == false)
            {
                return BadRequest();
            }

            i.AlteraDataFinal(i.dataInicial, i.dataFinal, i.tempoVigencia);
            i.CalculaEstimado(i);

            _context.Investimentos.Entry(i).State = EntityState.Modified;
            _context.SaveChanges();
            return Ok(i);
        }

        //método PUT para resgatar um investimento (DELETE logico)
        [HttpPut("/resgatar/{id:int}")]
        public ActionResult ResgatarInvestimento(int id, Investimento i)
        {
            if(i.investimentoId != id)
            {
                return BadRequest();
            }

            i.dataFinal = DateTime.Now;
            i.tempoVigencia = (i.dataFinal - i.dataInicial).Days / 30;

            if(i.tempoVigencia < 1)
            {
                BadRequest("O investimento não pode ser resgatado com menos de 1 mês.");
            }

            i.CalculaEstimado(i);
            i.Resgatado = true;

            return Ok("Investimento resgatado com sucesso! O valor resgato foi de " + i.valorFinal);
        }
    }
}
