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
    public class VentasController : Controller
    {
        private RefaccionariaContext db = new RefaccionariaContext();

        // GET: Ventas
        public ActionResult Index()
        {
            var ventas = db.Ventas.Include(v => v.Cliente).Include(v => v.Producto);
            return View(ventas.ToList());
        }

        // GET: Ventas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venta venta = db.Ventas.Find(id);
            if (venta == null)
            {
                return HttpNotFound();
            }
            return View(venta);
        }

        // GET: Ventas/Create
        public ActionResult Create()
        {
            ViewBag.ClienteID = new SelectList(db.Clientes, "ClienteID", "Nombre");
            ViewBag.ProductoID = new SelectList(db.Productoes, "ProductoID", "Nombre");
            return View();
        }

        // POST: Ventas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ClienteID")] Venta venta)
        {
            if (ModelState.IsValid)
            {
                int ventaID = db.Ventas.Include(t => t.Cliente).Include(t => t.Producto).ToList().Count + 1;

                foreach (ProductoCarrito item in Carrito.listaProductos)
                {
                    Venta miVenta = new Venta();
                    miVenta.VentaID = ventaID;
                    miVenta.ClienteID = venta.ClienteID;
                    miVenta.ProductoID = item.ProductoID;
                    miVenta.Fecha = DateTime.Now;
                    miVenta.Cantidad = item.Cantidad;
                    miVenta.PrecioUnitario = db.Productoes.Find(item.ProductoID).PrecioCompra;
                    miVenta.Total = item.SubTotal;
                    db.Ventas.Add(miVenta);
                }
                db.SaveChanges();
                Carrito.listaProductos = new List<ProductoCarrito>();
                Carrito.Cliente = db.Clientes.Find(venta.ClienteID).Nombre;
                //return Redirect("ConfirmarPago");
            }

            ViewBag.ClienteID = new SelectList(db.Clientes, "ClienteID", "Nombre", venta.ClienteID);
            ViewBag.ProductoID = new SelectList(db.Productoes, "ProductoID", "Nombre", venta.ProductoID);
            return View(venta);
        }

        // GET: Ventas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venta venta = db.Ventas.Find(id);
            if (venta == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClienteID = new SelectList(db.Clientes, "ClienteID", "Nombre", venta.ClienteID);
            ViewBag.ProductoID = new SelectList(db.Productoes, "ProductoID", "Nombre", venta.ProductoID);
            return View(venta);
        }

        // POST: Ventas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VentaID,ClienteID,ProductoID,Fecha,Cantidad,PrecioUnitario,Total")] Venta venta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(venta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClienteID = new SelectList(db.Clientes, "ClienteID", "Nombre", venta.ClienteID);
            ViewBag.ProductoID = new SelectList(db.Productoes, "ProductoID", "Nombre", venta.ProductoID);
            return View(venta);
        }

        // GET: Ventas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venta venta = db.Ventas.Find(id);
            if (venta == null)
            {
                return HttpNotFound();
            }
            return View(venta);
        }

        // POST: Ventas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Venta venta = db.Ventas.Find(id);
            db.Ventas.Remove(venta);
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
