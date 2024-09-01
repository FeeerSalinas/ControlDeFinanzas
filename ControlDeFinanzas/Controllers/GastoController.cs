using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ControlDeFinanzas.Models;
using ControlDeFinanzas.Data;

namespace ControlDeFinanzas.Controllers
{
    public class GastoController : Controller
    {
        private readonly FinanzasDbContext _context;

        public GastoController(FinanzasDbContext context)
        {
            _context = context;
        }

        // GET: Gasto
        public async Task<IActionResult> Index(string searchString, string ordenActual, int? numpag, string filtroActual)
        {
            var sumaEducacion = _context.Gastos.Where(i => i.Categoria == "Educacion").Sum(i => i.Monto);
            var sumaSuscripcion = _context.Gastos.Where(i => i.Categoria == "Suscripcion").Sum(i => i.Monto);
            var sumaEmergencia = _context.Gastos.Where(i => i.Categoria == "Emergencia").Sum(i => i.Monto);
            var sumaEntretenimiento = _context.Gastos.Where(i => i.Categoria == "Entretenimiento").Sum(i => i.Monto);
            var sumaSalud = _context.Gastos.Where(i => i.Categoria == "Salud").Sum(i => i.Monto);
            var total = sumaEducacion + sumaSuscripcion + sumaEmergencia + sumaEntretenimiento + sumaSalud;

            ViewData["SumaEducacion"] = sumaEducacion;
            ViewData["SumaSuscripcion"] = sumaSuscripcion;
            ViewData["SumaEmergencia"] = sumaEmergencia;
            ViewData["SumaEntretenimiento"] = sumaEntretenimiento;
            ViewData["SumaSalud"] = sumaSalud;
            ViewData["Total"] = total;

            ViewData["FIltroActual"] = filtroActual;

            var gastos = from categoria in _context.Gastos select categoria;

            if (searchString != null)
            {
                numpag = 1;
            }
            else
            {
                searchString = filtroActual;
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                gastos = gastos.Where(s => s.Categoria!.Contains(searchString));
            }

            int cantidadRegistros = 6;

            return View(await Paginacion<Gasto>.CrearPaginacion(gastos.AsNoTracking(), numpag?? 1, cantidadRegistros));
        }

        // GET: Gasto/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gasto = await _context.Gastos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gasto == null)
            {
                return NotFound();
            }

            return View(gasto);
        }

        // GET: Gasto/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Gasto/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Monto,Fecha,Descripcion,Categoria")] Gasto gasto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gasto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gasto);
        }

        // GET: Gasto/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gasto = await _context.Gastos.FindAsync(id);
            if (gasto == null)
            {
                return NotFound();
            }
            return View(gasto);
        }

        // POST: Gasto/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Monto,Fecha,Descripcion,Categoria")] Gasto gasto)
        {
            if (id != gasto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gasto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GastoExists(gasto.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(gasto);
        }

        // GET: Gasto/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gasto = await _context.Gastos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gasto == null)
            {
                return NotFound();
            }

            return View(gasto);
        }

        // POST: Gasto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gasto = await _context.Gastos.FindAsync(id);
            if (gasto != null)
            {
                _context.Gastos.Remove(gasto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GastoExists(int id)
        {
            return _context.Gastos.Any(e => e.Id == id);
        }
    }
}
