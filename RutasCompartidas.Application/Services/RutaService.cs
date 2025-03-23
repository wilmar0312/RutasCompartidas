using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using RutasCompartidas.Domain.Entities;
using RutasCompartidas.Infrastructure.Persistence;

namespace RutasCompartidas.Application.Services
{
    public class RutaService
    {
        private readonly AppDbContext _context;

        public RutaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Ruta>> ObtenerRutasAsync()
        {
            return await _context.Rutas.Include(r => r.Conductor).ToListAsync();
        }

        public async Task<Ruta?> ObtenerRutaPorIdAsync(int id)
        {
            return await _context.Rutas.FindAsync(id);
        }

        public async Task AgregarRutaAsync(Ruta ruta)
        {
            _context.Rutas.Add(ruta);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarRutaAsync(Ruta ruta)
        {
            _context.Rutas.Update(ruta);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarRutaAsync(int id)
        {
            var ruta = await _context.Rutas.FindAsync(id);
            if (ruta != null)
            {
                _context.Rutas.Remove(ruta);
                await _context.SaveChangesAsync();
            }
        }
    }
}

