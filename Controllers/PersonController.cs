using AutoMapper;
using BE_LoansApp.DataAccess;
using BE_LoansApp.DTOs;
using BE_LoansApp.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BE_LoansApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly ThingsContext peoplecontext;
        private readonly IMapper mapper;

        public PersonController(ThingsContext context, IMapper mapper)
        {
            peoplecontext = context;
            this.mapper = mapper;
        }

        [HttpGet("getall")]

        public async Task<List<PersonDTO>> GetAll()
        {
            Thread.Sleep(1000); // Agrego tiempo para que se vea el spinner
            var people = await peoplecontext.People.ToListAsync();
            return mapper.Map<List<PersonDTO>>(people);
        }


        [HttpGet("{id:int}", Name = "obtenerPerson")]
        public async Task<ActionResult<PersonDTO>> GetById(int id)
        {
            var person = await peoplecontext.People.FirstOrDefaultAsync(x => x.Id == id); // Incluye los prestamos que tiene la persona.
            

            if (person == null)
            {
                return NotFound();
            }

            return mapper.Map<PersonDTO>(person);
        }

        [HttpPost]
        public async Task<ActionResult> Post(PersonCreationDTO personCreationDTO)
        {
            var person = mapper.Map<Person>(personCreationDTO);
            peoplecontext.People.Add(person);
            await peoplecontext.SaveChangesAsync();

            var personDTO = mapper.Map<PersonDTO>(person);
            return CreatedAtRoute("obtenerPerson", new { id = person.Id }, personDTO);

        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(PersonCreationDTO personCreationDTO, int id)
        {
            var existePersona = await peoplecontext.People.AnyAsync(x => x.Id == id);
            if (!existePersona)
            {
                return NotFound();
            }

            var people = mapper.Map<Person>(personCreationDTO);
            people.Id = id;

            peoplecontext.Update(people);
            await peoplecontext.SaveChangesAsync();
            return NoContent();
              
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await peoplecontext.People.AnyAsync(x => x.Id == id);


            if (!existe)
            {
                return NotFound();
            }
            peoplecontext.Remove(new Person() { Id = id });
            await peoplecontext.SaveChangesAsync();
            return NoContent();

        }

    }
}
