using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Refaccionaria.Models
{
    [Table("Proveedores")]
    public class Proveedor
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProveedorID { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "ERROR: Cantidad de caracteres excedida. Cantidad permitida: 100")]
        [Display(Name = "Nombre completo")]
        [Column(TypeName = "varchar")]
        public string Nombre { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; }

        [EmailAddress]
        [Display(Name = "Email")]
        [Required]
        public string Email { get; set; }
    }
}