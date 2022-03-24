using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Final_Entity.Models;

namespace Final_Entity.Data
{
    public class Final_EntityContext : DbContext
    {
        public Final_EntityContext (DbContextOptions<Final_EntityContext> options)
            : base(options)
        {
        }

        public DbSet<Final_Entity.Models.Colectivo> colectivos { get; set; }
        public DbSet<Datos> datos { get; set; }
        public DbSet<Final_Entity.Models.Chofer> choferes { get; set; }
        public DbSet<Final_Entity.Models.Linea> lineas { get; set; }
        public DbSet<Final_Entity.Models.Tipo> tipos { get; set; }
        public DbSet<Final_Entity.Models.Estado> estados { get; set; }
       
    }
}
