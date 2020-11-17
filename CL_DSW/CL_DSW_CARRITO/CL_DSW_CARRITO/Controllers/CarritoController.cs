using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using CL_DSW_CARRITO.Models;

namespace CL_DSW_CARRITO.Controllers
{
    public class CarritoController : Controller
    {
        FUENTESODAEntities fe = new FUENTESODAEntities();

        private int getIndex(int id)
        {
            List<CarritoItem> compra = (List<CarritoItem>)Session["carrito"];
            for (int i = 0; i < compra.Count; i++)
            {
                if (compra[i].Producto.IDE_PRO == id)
                {
                    return i;
                }
            }
            return -1;
        }
        [HttpPost]
        public JsonResult AgregarCarrito(int id, int cantidad)
        {
            if (Session["carrito"] == null)
            {
                List<CarritoItem> compra = new List<CarritoItem>();
                compra.Add(new CarritoItem(fe.PRODUCTO.Find(id), cantidad));
                Session["carrito"] = compra;
            }
            else
            {
                List<CarritoItem> compra = (List<CarritoItem>)Session["carrito"];
                int IndexExistente = getIndex(id);
                if (IndexExistente == -1)
                {
                    compra.Add(new CarritoItem(fe.PRODUCTO.Find(id), cantidad));
                }
                else
                {
                    compra[IndexExistente].Cantidad += cantidad;
                }
                Session["carrito"] = compra;
            }
            return Json(new { Response = true}, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AgregarCarrito()
        {
            return View();
        }

        public ActionResult Eliminar(int id)
        {
            List<CarritoItem> compra = (List<CarritoItem>)Session["carrito"];
            compra.RemoveAt(getIndex(id));
            return View("AgregarCarrito");
        }

        public ActionResult FinalizarCompra()
        {
            List<CarritoItem> compra = (List<CarritoItem>)Session["carrito"];
            if (compra != null && compra.Count > 0)
            {
                BOLETA nuevaBoleta = new BOLETA();
                nuevaBoleta.FEC_BOL = DateTime.Now;
                nuevaBoleta.IDE_CLI = Convert.ToInt32(Session["Cliente"]);
                nuevaBoleta.IDE_VEN = Convert.ToInt32(Session["Vendedor"]);
                nuevaBoleta.DETALLEBOLETA = (from producto in compra
                                        select new DETALLEBOLETA
                                        {
                                            IDE_PRO = producto.Producto.IDE_PRO,
                                            CAN_ART = producto.Cantidad,

                                        }).ToList();
                fe.BOLETA.Add(nuevaBoleta);
                fe.SaveChanges();
                Session["carrito"] = new List<CarritoItem>();
                Session["Cliente"] = null;
                Session["Vendedor"] = null;
                Session.Remove("Cliente");
                Session.Remove("Vendedor");
            }
            return View();
        }
    }
}