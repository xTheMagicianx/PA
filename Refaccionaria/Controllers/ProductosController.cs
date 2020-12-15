using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Refaccionaria.DAL;
using Refaccionaria.Models;

namespace Refaccionaria.Controllers
{
    public class ProductosController : Controller
    {
        private RefaccionariaContext db = new RefaccionariaContext();

        // GET: Productos
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.OrdenNombre = String.IsNullOrEmpty(sortOrder) ? "nombre_desc" : "";
            ViewBag.OrdenPrecioVenta = sortOrder == "PrecioVenta" ? "precioventa_desc" : "PrecioVenta";
            ViewBag.OrdenProductoID = sortOrder == "ProductoID" ? "productoid_desc" : "ProductoID";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var productos = db.Productoes.AsEnumerable();

            if (!String.IsNullOrEmpty(searchString))
            {
                productos = productos.Where(c => c.Descripcion.Contains(searchString)
                                              || c.Nombre.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "nombre_desc":
                    productos = productos.OrderByDescending(c => c.Nombre);
                    break;
                case "PrecioVenta":
                    productos = productos.OrderBy(c => c.PrecioVenta);
                    break;
                case "precioventa_desc":
                    productos = productos.OrderByDescending(c => c.PrecioVenta);
                    break;
                case "ProductoID":
                    productos = productos.OrderBy(c => c.ProductoID);
                    break;
                case "productoid_desc":
                    productos = productos.OrderByDescending(c => c.ProductoID);
                    break;
                default:
                    productos = productos.OrderBy(c => c.Nombre);
                    break;
            }
            int pageSize = 5;
            int pageNumber = (page ?? 1);

            return View(productos.ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        public ActionResult AgregarCarrito(FormCollection form)
        {
            Producto producto = db.Productoes.Find(int.Parse(Request["item.ProductoID"].ToString()));

            ProductoCarrito item = new ProductoCarrito();
            item.ProductoCarritoID = Carrito.listaProductos.Count + 1;
            item.Producto = producto;
            item.ProductoID = producto.ProductoID;
            item.Cantidad = int.Parse(Request["Cantidad"].ToString());
            item.CalcularSubtotal();
            item.PrecioUnitario = item.Producto.PrecioVenta;

            //Agregamos el artículo al carrito(lista)
            Carrito.listaProductos.Add(item);


            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // GET: Productos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Productoes.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // GET: Productos/Create
        public ActionResult Create()
        {
            ViewBag.CategoriaID = new SelectList(db.Categorias, "CategoriaID", "Nombre");
            ViewBag.ProveedorID = new SelectList(db.Proveedors, "ProveedorID", "Nombre");
            return View();
        }

        // POST: Productos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductoID,CategoriaID,ProveedorID,Nombre,Descripcion,PrecioCompra,PrecioVenta")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                db.Productoes.Add(producto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoriaID = new SelectList(db.Categorias, "CategoriaID", "Nombre", producto.CategoriaID);
            ViewBag.ProveedorID = new SelectList(db.Proveedors, "ProveedorID", "Nombre", producto.ProveedorID);
            return View(producto);
        }

        // GET: Productos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Productoes.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoriaID = new SelectList(db.Categorias, "CategoriaID", "Nombre", producto.CategoriaID);
            ViewBag.ProveedorID = new SelectList(db.Proveedors, "ProveedorID", "Nombre", producto.ProveedorID);
            return View(producto);
        }

        // POST: Productos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductoID,CategoriaID,ProveedorID,Nombre,Descripcion,PrecioCompra,PrecioVenta")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(producto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoriaID = new SelectList(db.Categorias, "CategoriaID", "Nombre", producto.CategoriaID);
            ViewBag.ProveedorID = new SelectList(db.Proveedors, "ProveedorID", "Nombre", producto.ProveedorID);
            return View(producto);
        }

        // GET: Productos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Productoes.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Producto producto = db.Productoes.Find(id);
            db.Productoes.Remove(producto);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
