using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ControlDeFinanzas.Models;
using System.Numerics;
using ControlDeFinanzas.Data;

namespace ControlDeFinanzas.Controllers
{
    public class IngresoController : Controller
    {
        private readonly FinanzasDbContext _context;

        public IngresoController(FinanzasDbContext context)
        {
            _context = context;
        }

        // GET: Ingreso
        public async Task<IActionResult> Index(string searchString,string ordenActual, int? numpag, string filtroActual)
        {
            var sumaSalario = _context.Ingresos.Where(i => i.Categoria == "Salario").Sum(i => i.Monto);
            var sumaVenta = _context.Ingresos.Where(i => i.Categoria == "Venta").Sum(i => i.Monto);
            var sumaOtro = _context.Ingresos.Where(i => i.Categoria == "Otro").Sum(i => i.Monto);
            var total = sumaSalario + sumaVenta + sumaOtro;

            ViewData["SumaSalario"] = sumaSalario;
            ViewData["SumaVenta"] = sumaVenta;
            ViewData["SumaOtro"] = sumaOtro;
            ViewData["Total"] = total;

          
            ViewData["FIltroActual"] = filtroActual;

            var ingresos = from categoria in _context.Ingresos select categoria;

            if(searchString!=null)
            {
                numpag=1;
            }
            else
            {
                searchString=filtroActual;
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                ingresos = ingresos.Where(s => s.Categoria!.Contains(searchString));
            }


            int cantidadRegistros = 6;
            return View(await Paginacion<Ingreso>.CrearPaginacion(ingresos.AsNoTracking(), numpag?? 1, cantidadRegistros));
            //return View(await ingresos.ToListAsync());
        }



        // GET: Ingreso/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingreso = await _context.Ingresos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ingreso == null)
            {
                return NotFound();
            }

            return View(ingreso);
        }

        // GET: Ingreso/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ingreso/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Monto,Fecha,Descripcion,Categoria")] Ingreso ingreso)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ingreso);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ingreso);
        }

        // GET: Ingreso/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingreso = await _context.Ingresos.FindAsync(id);
            if (ingreso == null)
            {
                return NotFound();
            }
            return View(ingreso);
        }

        // POST: Ingreso/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Monto,Fecha,Descripcion,Categoria")] Ingreso ingreso)
        {
            if (id != ingreso.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ingreso);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IngresoExists(ingreso.Id))
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
            return View(ingreso);
        }

        // GET: Ingreso/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingreso = await _context.Ingresos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ingreso == null)
            {
                return NotFound();
            }

            return View(ingreso);
        }

        // POST: Ingreso/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ingreso = await _context.Ingresos.FindAsync(id);
            if (ingreso != null)
            {
                _context.Ingresos.Remove(ingreso);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IngresoExists(int id)
        {
            return _context.Ingresos.Any(e => e.Id == id);
        }
    }
}
