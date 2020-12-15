using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Refaccionaria.Models
{
    [Table("Productos")]
    public class Producto
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductoID { get; set; }

        [ForeignKey("CategoriaID")]
        public Categoria Categoria { get; set; }

        [Display(Name = "Categoría ID")]
        [Required(ErrorMessage = "Seleccione la categoria del producto")]
        public int CategoriaID { get; set; }

        [ForeignKey("ProveedorID")]
        public Proveedor Proveedor { get; set; }

        [Display(Name = "Proveedor")]
        [Required(ErrorMessage = "Seleccione el proveedor del producto")]
        public int ProveedorID { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "ERROR: Cantidad de caracteres excedida. Cantidad permitida: 50")]
        [Display(Name = "Nombre")]
        [Column(TypeName = "varchar")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Capture la descripción del producto")]
        [MaxLength(300, ErrorMessage = "ERROR: Cantidad de caracteres excedida. Cantidad permitida: 300")]
        [Display(Name = "Descripción")]
        [Column(TypeName = "varchar")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Capture el precio de compra")]
        [Display(Name = "Precio de compra")]
        [Range(0.0, double.MaxValue, ErrorMessage = "ERROR: Considere que es un valor númerico y el rango es positivo")]
        public decimal PrecioCompra { get; set; }

        [Required(ErrorMessage = "Capture el precio venta")]
        [Display(Name = "Precio de venta")]
        [Range(0.0, double.MaxValue, ErrorMessage = "ERROR: Considere que es un valor númerico y el rango es positivo")]
        public decimal PrecioVenta { get; set; }
    }
}
