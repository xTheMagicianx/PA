using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Refaccionaria.DAL
{
    public class RefaccionariaContext : DbContext
    {
        public RefaccionariaContext() : base("RefaccionariaContext")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public System.Data.Entity.DbSet<Refaccionaria.Models.Categoria> Categorias { get; set; }

        public System.Data.Entity.DbSet<Refaccionaria.Models.Proveedor> Proveedors { get; set; }

        public System.Data.Entity.DbSet<Refaccionaria.Models.Cliente> Clientes { get; set; }

        public System.Data.Entity.DbSet<Refaccionaria.Models.Producto> Productoes { get; set; }

        public System.Data.Entity.DbSet<Refaccionaria.Models.ProductoCarrito> ProductoCarritoes { get; set; }

        public System.Data.Entity.DbSet<Refaccionaria.Models.Venta> Ventas { get; set; }
    }
}