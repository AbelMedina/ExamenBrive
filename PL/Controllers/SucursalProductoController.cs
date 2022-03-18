using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class SucursalProductoController : Controller
    {
        // GET: SucursalProducto
        [HttpGet]
        public ActionResult SucursalGetAll()
        {
            ML.Result result = BL.Sucursal.GetAll();
            ML.SucursalProducto sucursalProducto = new ML.SucursalProducto();
            sucursalProducto.Sucursal = new ML.Sucursal();
            if (result.Correct)
            {
                sucursalProducto.Sucursal.Sucursales = result.Objects;
            }
            else
            {
                ViewBag.Mensaje = "Error al obtener las sucursales" + result.ErrorMessage;
            }
            return View(sucursalProducto);
        }

        [HttpGet]
        public ActionResult GetProductosByIdSucursal(int? IdSucursal)
        {
            if (IdSucursal == null)
            {
                ViewBag.Mensaje = "Error al obtener los productos";
            }
            ML.SucursalProducto sucursalProducto = new ML.SucursalProducto();
            ML.Result result = new ML.Result();
            result = BL.Sucursal.GetById(IdSucursal.Value);
            if (result.Correct)
            {
                sucursalProducto.Sucursal = new ML.Sucursal();
                //sucursalProducto.Sucursal = ((ML.Sucursal)result.Object);
                sucursalProducto.Sucursal.IdSucursal = ((ML.Sucursal)result.Object).IdSucursal;
                sucursalProducto.Sucursal.Nombre = ((ML.Sucursal)result.Object).Nombre;
                sucursalProducto.Sucursal.Direccion = ((ML.Sucursal)result.Object).Direccion;
                sucursalProducto.Sucursal.Telefono = ((ML.Sucursal)result.Object).Telefono;
                //----------------------------------------------------//
                result = BL.SucursalProducto.GetProductosByIdSucursal(IdSucursal.Value);
                sucursalProducto.SucursalProductos = new List<object>();
                sucursalProducto.SucursalProductos = result.Objects;
                return View(sucursalProducto);
            }
            else
            {
                ViewBag.Mensaje = "Error " + result.ErrorMessage;
                return PartialView("Modal");
            }
        }

        [HttpGet]
        public ActionResult GetProductosNoByIdSucursal(int? IdSucursal)
        {
            ML.SucursalProducto sucursalProducto = new ML.SucursalProducto();
            ML.Result result = new ML.Result();
            result = BL.Sucursal.GetById(IdSucursal.Value);
            if (result.Correct)
            {
                sucursalProducto.Sucursal = new ML.Sucursal();
                sucursalProducto.Sucursal.IdSucursal = ((ML.Sucursal)result.Object).IdSucursal;
                sucursalProducto.Sucursal.Nombre = ((ML.Sucursal)result.Object).Nombre;
                sucursalProducto.Sucursal.Direccion = ((ML.Sucursal)result.Object).Direccion;
                sucursalProducto.Sucursal.Telefono = ((ML.Sucursal)result.Object).Telefono;
                result = BL.SucursalProducto.GetProductosNoByIdSucursal(IdSucursal.Value);
                sucursalProducto.SucursalProductos = new List<object>();
                sucursalProducto.SucursalProductos = result.Objects;
                return View(sucursalProducto);
            }
            else
            {
                ViewBag.Mensaje = "Error " + result.ErrorMessage;
                return PartialView("Modal");
            }
        }

        [HttpPost]
        public ActionResult GetProductosNoByIdSucursal(ML.SucursalProducto sucursalProducto)
        {
            if (sucursalProducto.SucursalProductos!=null)
            {
                foreach (string IdProducto in sucursalProducto.SucursalProductos)
                {
                    ML.SucursalProducto sucursalProductoItem = new ML.SucursalProducto();
                    sucursalProductoItem.Sucursal = new ML.Sucursal();
                    sucursalProductoItem.Sucursal.IdSucursal = sucursalProducto.Sucursal.IdSucursal;
                    sucursalProductoItem.Producto = new ML.Producto();
                    sucursalProductoItem.Producto.IdProducto = int.Parse(IdProducto);
                    ML.Result result = BL.SucursalProducto.Add(sucursalProductoItem);
                }
            }
            else
            {
                ViewBag.Mensaje = "Error al ingresar los productos";
            }
            return RedirectToAction("GetProductosByIdSucursal", sucursalProducto.Sucursal);
        }

        [HttpGet]
        public ActionResult Delete(int IdSucursalProducto, int IdSucursal)
        {
            ML.SucursalProducto sucursalProducto = new ML.SucursalProducto();
            sucursalProducto.IdSucursalProducto = IdSucursalProducto;
            ML.Result result = BL.SucursalProducto.Delete(sucursalProducto);
            ViewBag.SucursalProducto = true;
            ViewBag.IdSucursal = IdSucursal;

            if (result.Correct)
            {
                ViewBag.Mensaje = "Registro eliminado con exito";
            }
            else
            {
                ViewBag.Mensaje = "No se ha podido eliminar el registro";
            }
            return PartialView("Modal");
        }
    }
}