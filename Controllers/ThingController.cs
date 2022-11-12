using AutoMapper;
using BE_LoansApp.DataAccess;
using BE_LoansApp.DTOs;
using BE_LoansApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace BE_LoansApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThingController : ControllerBase
    {
        private readonly ThingsContext thingsContext;
        private readonly IMapper mapper;

        public ThingController(ThingsContext context, IMapper mapper) 
        {
            thingsContext = context;
            this.mapper = mapper;
        }

        [HttpGet("getall")]
        public async Task<List<ThingDTO>> GetAll()
        {
            var things = await thingsContext.Things.ToListAsync();
            return mapper.Map<List<ThingDTO>>(things);

        }

        [HttpGet("{categoryId:int}/things")]
        public async Task<ActionResult<List<ThingDTO>>> Get(int categoryId)
        {
            var things = await thingsContext.Things
                .Where(thingDB => thingDB.CategoryId == categoryId).ToListAsync();
            return mapper.Map<List<ThingDTO>>(things);
        }

        [HttpGet("{id:int}", Name ="obtenerThing")]
        public async Task<ActionResult<ThingDTO>> GetById(int id)
        { 
            var thing = await thingsContext.Things.FirstOrDefaultAsync(thingBD=> thingBD.Id == id);
            if (thing == null)
            {
                return NotFound();
            }

            return mapper.Map<ThingDTO>(thing);
        }

        [HttpGet("{description}")]
        public async Task<ActionResult<List<ThingDTO>>> Get([FromRoute] string description)
        {
            var things = await thingsContext.Things.Where(thingBD => thingBD.Description.Contains(description)).ToListAsync();

            
            return mapper.Map<List<ThingDTO>>(things);
        }

        [HttpPost("{categoryId:int}/things")]
        public async Task<ActionResult> Post(int categoryId, ThingCreationDTO thingDTO)
        {
            var existeCategory = await thingsContext.Categories.AnyAsync(categoryDB => categoryDB.Id == categoryId);
            if (!existeCategory)
            {
                return NotFound();
            }
            var thing = mapper.Map<Thing>(thingDTO);
            thing.CategoryId = categoryId;
            thingsContext.Add(thing);
            await thingsContext.SaveChangesAsync();

            var NthingDTO = mapper.Map<ThingDTO>(thing);
            return CreatedAtRoute("obtenerThing", new { id = thing.Id }, NthingDTO);


        }

        [HttpPut("{categoryId:int}/things/{id}")]
        public async Task<ActionResult> Put(int categoryId, int id, ThingCreationDTO thingCreationDTO)
        {
            var existeCategory = await thingsContext.Categories.AnyAsync(categoryDB => categoryDB.Id == categoryId);
            if (!existeCategory)
            {
                return NotFound();
            }

            var existeThing = await thingsContext.Things.AnyAsync(thingDB => thingDB.Id == id);
            if (!existeThing)
            {
                return NotFound();
            }

            var thing= mapper.Map<Thing>(thingCreationDTO);
            thing.Id = id;
            thing.CategoryId = categoryId;
            thingsContext.Update(thing);
            await thingsContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await thingsContext.Things.AnyAsync(x => x.Id == id);


                if (!existe) 
                {
                    return NotFound();
                }
            thingsContext.Remove(new Thing() { Id = id });
            await thingsContext.SaveChangesAsync();
            return NoContent();

        }
       
        //[HttpPost]
        //public async Task<ActionResult> Create([FromBody] ThingCreationDTO thingDTO)
        //{
        //    var yaExiste = await thingsContext.Things.AnyAsync( x=> x.Description == thingDTO.description);
        //    if (yaExiste)
        //    {
        //        return BadRequest($"Ya existe una Cosa con el nombre {thingDTO.description}");
        //    }

        //    var thing = mapper.Map<Thing>(thingDTO);

        //    thingsContext.Things.Add(thing);
        //    await thingsContext.SaveChangesAsync();
        //    return Ok(thing);

        //}

    }
}
