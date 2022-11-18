using AutoMapper;
using BE_LoansApp.DataAccess;
using BE_LoansApp.DTOs;
using BE_LoansApp.Entities;
using BE_LoansApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace BE_LoansApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanController: ControllerBase
    {
        private readonly ThingsContext context;
        private readonly IMapper mapper;

        public LoanController(ThingsContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        [HttpGet("{peopleId:int}")]
        public async Task<ActionResult<List<LoanDTO>>> Get(int peopleId)
        {
            var ExistePersona = await context.People.AnyAsync(personDB => personDB.Id == peopleId);
            if (!ExistePersona)
            {
                return NotFound();
            }
            var loans = await context.Loans
                        .Where(x => x.PersonId == peopleId)
                        .Include(thingDB => thingDB.Thing)
                        .Include(PersonDB => PersonDB.Person)
                        
                        .ToListAsync();
            return mapper.Map<List<LoanDTO>>(loans);
        }


        [HttpPost]
        public async Task<ActionResult> Post( LoanCreationDTO loanCreationDTO)
        {
            var ExistePersona = await context.People.AnyAsync(personDB => personDB.Id == loanCreationDTO.PersonId);
            if (!ExistePersona)
            {
                return BadRequest($"No existe persona registrada bajo el ID Nro. {loanCreationDTO.PersonId}");
            }

            var ExisteObjeto = await context.Things.AnyAsync(thingDB => thingDB.Id == loanCreationDTO.ThingId);
            if (!ExisteObjeto)
            {
                return BadRequest($"No existe Objeto registrado bajo el ID Nro. {loanCreationDTO.ThingId}");
            }

            var loan = mapper.Map<Loan>(loanCreationDTO);
                     
            context.Add(loan);
            await context.SaveChangesAsync();
            return Ok();

        }

    }
}
