using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class VentasController : Controller
    {
        [HttpGet]
        public ActionResult GetEmpresa()
        {
            ML.DetalleVenta detalleVenta = new ML.DetalleVenta();
            detalleVenta.Sucursal = new ML.Sucursal();
            detalleVenta.Producto = new ML.Producto();
            detalleVenta.Producto.Productos = new List<object>();
            ML.Result resultSucursal = BL.Sucursal.GetAll();
            if (resultSucursal.Correct)
            {
                detalleVenta.Sucursal.Sucursales = new List<object>();
                detalleVenta.Sucursal.Sucursales = resultSucursal.Objects;
                return View(detalleVenta);
            }
            else
            {
                ViewBag.Mensaje = "Error en la lista de sucursales: " + resultSucursal.ErrorMessage;
            }
            return View();
        }

        [HttpPost]
        public ActionResult GetEmpresa(ML.DetalleVenta IdSucursal)
        {
            ML.DetalleVenta detalleVenta = new ML.DetalleVenta();
            //ML.SucursalProducto sucursalProducto = new ML.SucursalProducto();
            detalleVenta.Producto = new ML.Producto();
            detalleVenta.Producto.Productos = new List<object>();
            detalleVenta.Sucursal = new ML.Sucursal();
            ML.Result resultSucursal = BL.Sucursal.GetAll();
            if (resultSucursal.Correct)
            {
                detalleVenta.Sucursal = new ML.Sucursal();
                detalleVenta.Sucursal.Sucursales = resultSucursal.Objects;
                ML.Result result = BL.DetalleVenta.GetProductosByIdSucursal(IdSucursal.Sucursal.IdSucursal);
                if (result.Correct)
                {
                    detalleVenta.Producto.Productos = result.Objects;
                }
                else
                {
                    ViewBag.Mensaje = "No se han podido cargar los productos";
                    return PartialView("Modal");
                }
            }
            return View(detalleVenta);
        }

        [HttpGet]
        public ActionResult Carrito(int? IdProducto)
        {
            if (IdProducto == null)
            {
                IdProducto = 0;
            }
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            if (IdProducto != null)
            {
                if (Session["Carrito"] == null)
                {
                    ML.DetalleVenta detalleVenta = new ML.DetalleVenta();
                    detalleVenta.Producto = new ML.Producto();
                    detalleVenta.Producto.IdProducto = IdProducto.Value;
                    detalleVenta.Cantidad = 1;
                    ML.Result resultProducto = BL.Producto.GetById(IdProducto.Value);
                    if (resultProducto.Correct)
                    {
                        detalleVenta.Producto = (ML.Producto)resultProducto.Object;
                        result.Objects.Add(detalleVenta);
                    }
                    Session["Carrito"] = result.Objects;
                }
                else
                {
                    result.Objects = (List<Object>)Session["Carrito"];
                    bool Existe = false;
                    var indice = 0;
                    foreach (ML.DetalleVenta detalleVenta in result.Objects)
                    {
                        if (detalleVenta.Producto.IdProducto == IdProducto)
                        {
                            Existe = true;
                            indice = result.Objects.IndexOf(detalleVenta);
                        }
                    }
                    if (Existe==true)
                    {
                        foreach (ML.DetalleVenta detalleVenta in result.Objects)
                        {
                            detalleVenta.Cantidad = detalleVenta.Cantidad + 1;
                        }
                    }
                    else
                    {
                        ML.DetalleVenta detalleVenta = new ML.DetalleVenta();
                        detalleVenta.Producto = new ML.Producto();
                        detalleVenta.Producto.IdProducto = IdProducto.Value;
                        detalleVenta.Cantidad = 1;
                        ML.Result resultProducto = BL.Producto.GetById(IdProducto.Value);
                        if (resultProducto.Correct)
                        {
                            detalleVenta.Producto = (ML.Producto)resultProducto.Object;
                            result.Objects.Add(detalleVenta);
                        }
                        Session["Carrito"] = result.Objects;
                    }
                }
            }
            else
            {
                ViewBag.Mensaje = "Ocurrio un error al obtener la información " + result.ErrorMessage;
                return PartialView("Modal");
            }
            return View(result);
        }

        public ActionResult Sumar(int IdProducto)
        {
            ML.Result result = new ML.Result();
            result.Objects = (List<Object>)Session["Carrito"];
            foreach (ML.DetalleVenta detalleVenta in result.Objects)
            {
                if (detalleVenta.Producto.IdProducto == IdProducto)
                {
                    detalleVenta.Cantidad += 1;
                }
            }
            return View("Carrito", result);
        }

        public ActionResult Restar(int IdProducto)
        {
            ML.Result result = new ML.Result();
            result.Objects = (List<Object>)Session["Carrito"];
            foreach (ML.DetalleVenta detalleVenta in result.Objects)
            {
                if (detalleVenta.Producto.IdProducto==IdProducto)
                {
                    detalleVenta.Cantidad -= 1;
                }
            }
            return View("Carrito", result);
        }

        public ActionResult Eliminar(int IdProducto)
        {
            ML.Result result = new ML.Result();
            result.Objects = (List<Object>)Session["Carrito"];
            var indice = 0;
            foreach (ML.DetalleVenta detalleVenta in result.Objects)
            {
                if (detalleVenta.Producto.IdProducto == IdProducto)
                {
                    indice = result.Objects.IndexOf(detalleVenta);
                }
            }
            result.Objects.RemoveAt(indice);
            Session["Carrito"] = result.Objects;

            return View("Carrito", result);
        }

        public decimal? GetTotal(List<object> Objects)
        {
            decimal? Total = 0;
            foreach (ML.DetalleVenta detalleVenta in Objects)
            {
                Total += detalleVenta.Producto.Precio * detalleVenta.Cantidad;
            }
            return Total;
        }

        public ActionResult ModalCompra()
        {
            ViewBag.Mensaje = "Compra realizada con exito";
            Session.Clear();
            return PartialView("Modal");
        }
    }
}