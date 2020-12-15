using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Refaccionaria.Models
{
    public abstract class Carrito
    {
        public static List<ProductoCarrito> listaProductos = new List<ProductoCarrito>();
        public static string Cliente = "";
    }
}