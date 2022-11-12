using AutoMapper;
using BE_LoansApp.DataAccess;
using BE_LoansApp.DTOs;
using BE_LoansApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Diagnostics;

namespace BE_LoansApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ThingsContext _context;
        private readonly IMapper mapper;

        public HomeController(ThingsContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task <IActionResult> Index()
        {
            return View(await _context.Things
                .Include(categoryDB => categoryDB.Category)
                .ToListAsync()); 
        }

       
        [HttpGet]
        public IActionResult Crear()
        {
            //var category = await _context.Categories.ToListAsync();
            //return View(mapper.Map<List<CategoryViewModel>>(category));
            return View();
        }

        
        [HttpGet]
        public IActionResult Editar(int? id)
        {
            if (id == null) {
                return NotFound();
            }

            //var thing = _context.Things.FirstOrDefault(x => x.Id == id);
            var thing = _context.Things
                        .Include(categoryDB => categoryDB.Category)
                        .FirstOrDefault(x => x.Id == id);
             
                       
            if (thing == null) {
                return NotFound();
            }

            return View(mapper.Map<ThingViewModel>(thing));
        }

        [HttpGet]
        public IActionResult Borrar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var thing = _context.Things.FirstOrDefault(x => x.Id == id);
            var thing = _context.Things
                        .Include(categoryDB => categoryDB.Category)
                        .FirstOrDefault(x => x.Id == id);


            if (thing == null)
            {
                return NotFound();
            }

            return View(mapper.Map<ThingViewModel>(thing));
        }

        [HttpPost]
        
        public async Task<IActionResult> BorrarRegistro(int? id)
        {
            var thing = await _context.Things.FindAsync(id);
            if (thing == null) 
            {
                return NotFound();
            }
            _context.Things.Remove(thing);
            await _context.SaveChangesAsync();
            TempData["Mensaje"] = "El Objeto se Borro Correctamente";
            return RedirectToAction("Index");
        }


        [HttpPost]
       
        public async Task<IActionResult> Crear(ThingViewModelDTO thingVMDTO)
        {
            

            if (ModelState.IsValid)
            {

                var existeCategory = await _context.Categories.AnyAsync(categoryDB => categoryDB.Description == thingVMDTO.Category);
                if (!existeCategory)

                {
                    
                    var category = mapper.Map<Category>(thingVMDTO);
                    _context.Categories.Add(category);
                    await _context.SaveChangesAsync();
                    
                }
                else {
                    
                        //ModelState.AddModelError(String.Empty, "Ya existe una Categoria Que intenta Crear");
                        //return View("Crear");

                    }       

                
                
                return RedirectToAction("Index");

            }

         return View();

        }
        public IActionResult Privacy()
        {
            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}