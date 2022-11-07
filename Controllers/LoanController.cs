﻿using AutoMapper;
using BE_LoansApp.DataAccess;
using BE_LoansApp.DTOs;
using BE_LoansApp.Entities;
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
                        
                        .ToListAsync();
            return mapper.Map<List<LoanDTO>>(loans);
        }


        [HttpPost("{personId:int}/{thingIg:int}/loans")]
        public async Task<ActionResult> Post(int personId,  int thingId, LoanCreationDTO loanCreationDTO)
        {
            var ExistePersona = await context.People.AnyAsync(personDB => personDB.Id == personId);
            if (!ExistePersona)
            {
                return NotFound();
            }

            var loan = mapper.Map<Loan>(loanCreationDTO);
            loan.PersonId = personId;
            loan.ThingId = thingId;
            context.Add(loan);
            await context.SaveChangesAsync();
            return Ok();

        }

    }
}
