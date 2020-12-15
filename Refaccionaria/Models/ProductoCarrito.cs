using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Refaccionaria.Models
{
    public class ProductoCarrito
    {
        [Key]
        public int ProductoCarritoID { get; set; }

        [Display(Name = "# Producto")]
        [ForeignKey("ProductoID")]
        public Producto Producto { get; set; }
        public int ProductoID { get; set; }

        public int Cantidad { get; set; } = 1;

        public decimal PrecioUnitario { get; set; } = 0;

        public decimal SubTotal { get; set; } = 0;

        public void CalcularSubtotal()
        {
            SubTotal = Cantidad * Producto.PrecioVenta;
        }
    }
}