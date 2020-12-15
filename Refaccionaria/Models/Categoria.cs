using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Refaccionaria.Models
{
    [Table("Categorias")]
    public class Categoria
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoriaID { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "ERROR: Cantidad de caracteres excedida. Cantidad permitida: 50")]
        [Display(Name = "Categoria de producto")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Capture la descripción del producto")]
        [MaxLength(300, ErrorMessage = "ERROR: Cantidad de caracteres excedida. Cantidad permitida: 300")]
        [Display(Name = "Descripción de categoría")]
        public string Descripcion { get; set; }
    }
}