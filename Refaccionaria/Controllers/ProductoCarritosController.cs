using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Refaccionaria.DAL;
using Refaccionaria.Models;

namespace Refaccionaria.Controllers
{
    public class ProductoCarritosController : Controller
    {
        private RefaccionariaContext db = new RefaccionariaContext();

        // GET: ProductoCarritos
        public ActionResult Index()
        {
            return View(Carrito.listaProductos);
        }

        // GET: ProductoCarritos/Details/5
        public ActionResult Details(Producto prod)
        {
            return View(prod);
        }

        // GET: ProductoCarritos/Create
        public ActionResult Create()
        {
            ViewBag.ProductoID = new SelectList(db.Productoes, "ProductoID", "Nombre");
            return View();
        }

        // POST: ProductoCarritos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductoCarritoID,ProductoID,Cantidad,PrecioUnitario,SubTotal")] ProductoCarrito productoCarrito)
        {
            productoCarrito.PrecioUnitario = productoCarrito.Producto.PrecioVenta;
            if (ModelState.IsValid)
            {
                db.ProductoCarritoes.Add(productoCarrito);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductoID = new SelectList(db.Productoes, "ProductoID", "Nombre", productoCarrito.ProductoID);
            return View(productoCarrito);
        }

        // GET: ProductoCarritos/Edit/5
        public ActionResult Edit(ProductoCarrito prod)
        {
            if (prod == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            foreach (ProductoCarrito item in Carrito.listaProductos)
            {
                if (item.ProductoID.Equals(prod.ProductoID))
                {
                    item.Producto = prod.Producto;
                }
            }

            if (prod == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductoID = new SelectList(db.Productoes, "ProductoID", "Nombre", prod.ProductoID);
            return View(prod);
        }

        // GET: ProductoCarritos/Delete/5
        public ActionResult Delete(ProductoCarrito prod)
        {
            foreach (ProductoCarrito item in Carrito.listaProductos)
            {
                if (item.ProductoID.Equals(prod.ProductoID))
                {
                    prod = item;
                }
            }
            return View(prod);
        }

        // POST: ProductoCarritos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductoCarrito productoCarrito = db.ProductoCarritoes.Find(id);
            db.ProductoCarritoes.Remove(productoCarrito);
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
