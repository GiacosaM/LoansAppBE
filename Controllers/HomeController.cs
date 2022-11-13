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
            return View("Crear");
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
        [HttpPost]
        public IActionResult EditarRegistro(int? id, string Description) {

            if (!ModelState.IsValid)
            {
                return View("Editar");
            }

            if (id == null)
            {
                return NotFound();
            }

            //var thing = _context.Things.FirstOrDefault(x => x.Id == id);
            var thing = _context.Things
                        .FirstOrDefault(x => x.Id == id);
            thing.Description = Description;
            _context.SaveChanges();
            TempData["Mensaje"] = "El Objeto se Edito Correctamente";
            return RedirectToAction("Index");
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
       
        public async Task<IActionResult> Crear(string? Description, string? Category)
        {
            if (!ModelState.IsValid || Description is null || Category is null)
            {
                TempData["Mensaje"] = "No puede Haber campos Vacios";
                return View("Crear");
            } 

                var existeCategory = await _context.Categories.AnyAsync(categoryDB => categoryDB.Description == Category);
                if (!existeCategory)

                {
                    var newCategory = _context.Categories.Add(new Category
                    {
                        Description = Category
                    });

                    _context.SaveChanges();
                    int idCat = newCategory.Entity.Id;

                    _context.Things.Add(new Thing
                        {
                            Description = Description,
                            CategoryId = idCat,
                        }
                    );
                    await _context.SaveChangesAsync(); 
                }
                else {
                  
                     ModelState.AddModelError(String.Empty, "Ya existe una Categoria Que intenta Crear");
                     return View("Crear");
                }

                TempData["Mensaje"] = "El Objeto Fue creado con Exito";
                return RedirectToAction("Index");

               
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