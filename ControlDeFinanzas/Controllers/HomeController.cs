using ControlDeFinanzas.Data;
using ControlDeFinanzas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ControlDeFinanzas.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly FinanzasDbContext _context;

        public HomeController(ILogger<HomeController> logger, FinanzasDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Obtener el último ingreso basado en la posición (último en la tabla)
            var ultimoIngreso = await _context.Ingresos.OrderByDescending(i => i.Id).FirstOrDefaultAsync();
            var ultimoGasto = await _context.Gastos.OrderByDescending(i => i.Id).FirstOrDefaultAsync();

            //para saber cuanto dinero tengo disponible
            var totalIngresos = await _context.Ingresos.SumAsync(i => i.Monto);
            var totalGastos = await _context.Gastos.SumAsync(i => i.Monto);
            var totalDisponible = totalIngresos - totalGastos;

            // Verificar si hay algún ingreso
            if (ultimoIngreso != null)
            {
                ViewData["UltimoIngresoMonto"] = ultimoIngreso.Monto;
                ViewData["UltimoIngresoCategoria"] = ultimoIngreso.Categoria;
            }
            else
            {
                ViewData["UltimoIngresoMonto"] = 0;
                ViewData["UltimoIngresoCategoria"] = "No hay ingresos registrados";
            }

            // Verificar si hay algún gasto
            if (ultimoGasto != null)
            {
                ViewData["UltimoGastoMonto"] = ultimoGasto.Monto;
                ViewData["UltimoGastoCategoria"] = ultimoGasto.Categoria;
            }
            else
            {
                ViewData["UltimoGastoMonto"] = 0;
                ViewData["UltimoGastoCategoria"] = "No hay gastos registrados";
            }

            //verificar si hay dinero disponible
            if(totalDisponible > 0)
            {
                ViewData["TotalDisponible"] = totalDisponible;
            }
            else
            {
                ViewData["TotalDisponible"] = 0;
            }

            //ultima actualizacion
            ViewData["UltimaActualizacion"] = DateTime.Now;
            //total de ingresos y gastos
            ViewData["TotalIngresos"] = totalIngresos;
            ViewData["TotalGastos"] = totalGastos;

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
