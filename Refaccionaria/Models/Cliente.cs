using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Refaccionaria.Models
{
    [Table("Clientes")]
    public class Cliente
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClienteID { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "ERROR: Cantidad de caracteres excedida. Cantidad permitida: 100")]
        [Display(Name = "Nombre completo")]
        [Column(TypeName = "varchar")]
        public string Nombre { get; set; }

        [Required]
        [Phone]
        public string Telefono { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; }
    }
}