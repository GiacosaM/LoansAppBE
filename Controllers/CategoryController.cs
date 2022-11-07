using AutoMapper;
using BE_LoansApp.DataAccess;
using BE_LoansApp.DTOs;
using BE_LoansApp.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BE_LoansApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController: ControllerBase
    {
        private readonly ThingsContext categorycontext;
        private readonly IMapper mapper;

        public CategoryController(ThingsContext context, IMapper mapper)
        {
            this.categorycontext = context;
            this.mapper = mapper;
        }

        [HttpGet("{id:int}", Name ="ObtenerCategory")]
        public async Task<ActionResult<CategoryDTO>> GetById(int id)
        {
            var category = await categorycontext.Categories
                .Include(thingDB => thingDB.Things).FirstOrDefaultAsync(x=> x.Id == id);

            if (category == null) 
            {
                return NotFound();
            }

            return mapper.Map<CategoryDTO>(category);
        }

        [HttpPost]

        public async Task<ActionResult> Post(CategoryCreationDTO categoryCreationDTO)
        {
            var category = mapper.Map<Category>(categoryCreationDTO);
            categorycontext.Categories.Add(category);
            await categorycontext.SaveChangesAsync();

            var categoryDTO = mapper.Map<CategoryDTO>(category);
            return CreatedAtRoute("ObtenerCategory", new { id = category.Id }, categoryDTO);

        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(CategoryCreationDTO categoryCreationDTO, int id)
        {
            var existe = await categorycontext.Categories.AnyAsync(x => x.Id == id);

            if (!existe)
            {
                return NotFound();
            }

            var category = mapper.Map<Category>(categoryCreationDTO);
            category.Id = id;

            categorycontext.Update(category);
            await categorycontext.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await categorycontext.Things.AnyAsync(x => x.Id == id);


            if (!existe)
            {
                return NotFound();
            }
            categorycontext.Remove(new Category() { Id = id });
            await categorycontext.SaveChangesAsync();
            return NoContent();

        }

    }
}
