using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using CL_DSW_CARRITO.Models;

namespace CL_DSW_CARRITO.Controllers
{
    public class ProductoController : Controller
    {
        FUENTESODAEntities fe = new FUENTESODAEntities();
        // GET: Producto
        public ActionResult Producto()
        {
            return View(fe.PRODUCTO.ToList());
        }
    }
}