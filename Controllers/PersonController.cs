using AutoMapper;
using BE_LoansApp.DataAccess;
using BE_LoansApp.DTOs;
using BE_LoansApp.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BE_LoansApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PersonController : ControllerBase
    {
        private readonly ThingsContext peoplecontext;
        private readonly IMapper mapper;
        private readonly ILogger<PersonController> logger;

        public PersonController(ThingsContext context,IMapper mapper, ILogger<PersonController> logger)
            
        {
            peoplecontext = context;
            this.mapper = mapper;
            this.logger = logger;
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
            var person = await peoplecontext.People.FirstOrDefaultAsync(x => x.Id == id); 
            

            if (person == null)
            {
                logger.LogWarning($" No se encontro La persona con el id {id}");
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
            logger.LogInformation(" Se creo un nuevo registro de Persona");
            return CreatedAtRoute("obtenerPerson", new { id = person.Id }, personDTO);

        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(PersonCreationDTO personCreationDTO, int id)
        {
            var existePersona = await peoplecontext.People.AnyAsync(x => x.Id == id);
            if (!existePersona)
            {
                logger.LogWarning($" NO existe la persona con el id {id}, que se desea modificar");
                return NotFound();
            }

            var people = mapper.Map<Person>(personCreationDTO);
            people.Id = id;
            logger.LogInformation($" Se modifico el registro de la Persona con el id {id}");
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
                logger.LogWarning($" NO existe la persona con el id {id}, que se desea Eliminar");
                return NotFound();
            }
            peoplecontext.Remove(new Person() { Id = id });
            await peoplecontext.SaveChangesAsync();
            logger.LogInformation($" Se Elimino el registro de la Persona con el id {id}");
            return NoContent();

        }

    }
}
