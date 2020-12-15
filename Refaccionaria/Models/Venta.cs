using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Refaccionaria.Models
{
    [Table("Ventas")]
    public class Venta
    {
        [Key]
        [Column(Order = 0)]
        public int VentaID { get; set; }

        [Key]
        [ForeignKey("ClienteID")]
        [Display(Name = "Cliente")]
        [Column(Order = 1)]
        public Cliente Cliente { get; set; }
        public int ClienteID { get; set; }

        [Key]
        [ForeignKey("ProductoID")]
        [Display(Name = "Producto")]
        [Column(Order = 2)]
        public Producto Producto { get; set; }
        public int ProductoID { get; set; }

        [Display(Name = "Fecha de la venta")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime Fecha { get; set; }

        public int Cantidad { get; set; } = 1;

        [Column(TypeName = "money")]
        public decimal PrecioUnitario { get; set; } = 0;

        [Column(TypeName = "money")]
        public decimal Total { get; set; } = 0;
    }
}