using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using CL_DSW_CARRITO.Models;

namespace CL_DSW_CARRITO.Controllers
{
    public class HomeController : Controller
    {
        FUENTESODAEntities fe = new FUENTESODAEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult HacerPedido()
        {
            ViewBag.vendedor = new SelectList(fe.VENDEDOR, "IDE_VEN", "NOM_VEN");
            ViewBag.cliente = new SelectList(fe.CLIENTE, "IDE_CLI", "NOM_CLI");
            return View();
        }

        [HttpPost]
        public ActionResult HacerPedido(VENDEDOR ven, CLIENTE cli)
        {
            ViewBag.vendedor = new SelectList(fe.VENDEDOR, "IDE_VEN", "NOM_VEN");
            ViewBag.cliente = new SelectList(fe.CLIENTE, "IDE_CLI", "NOM_CLI");
            var objV = fe.VENDEDOR.Where(v => v.IDE_VEN.Equals(ven.IDE_VEN)).FirstOrDefault();
            var objC = fe.CLIENTE.Where(c => c.IDE_CLI.Equals(cli.IDE_CLI)).FirstOrDefault();
            if (objC != null && objV != null)
            {
                Session["Cliente"] = objC.IDE_CLI;
                Session["Vendedor"] = objV.IDE_VEN;
                return RedirectToAction("../Producto/Producto");
            }
            return View();
        }
    }
}