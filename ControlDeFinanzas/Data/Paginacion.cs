using Microsoft.EntityFrameworkCore;

namespace ControlDeFinanzas.Data
{
    public class Paginacion<T>: List<T>
    {
        public int PaginaInicio { get; private set; }
        public int PaginasTotales { get; private set; }

        public Paginacion(List<T> items, int contador, int paginaInicio, int cantidadRegistros)
        {
            PaginaInicio = paginaInicio;
            PaginasTotales = (int)Math.Ceiling(contador / (double)cantidadRegistros);

            this.AddRange(items);
        }

        public bool PaginasAnteriores => PaginaInicio > 1;
        public bool PaginasSiguientes => PaginaInicio < PaginasTotales;

        public static async Task<Paginacion<T>> CrearPaginacion(IQueryable<T> fuente, int paginaInicio, int cantidadRegistros)
        {
            var contador = await fuente.CountAsync();
            var items = await fuente.Skip((paginaInicio - 1) * cantidadRegistros).Take(cantidadRegistros).ToListAsync();
            return new Paginacion<T>(items, contador, paginaInicio, cantidadRegistros);
        }

    }
}
