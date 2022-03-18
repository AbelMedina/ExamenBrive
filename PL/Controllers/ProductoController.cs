using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class ProductoController : Controller
    {
        // GET: Producto
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Result result = BL.Producto.GetAll();
            ML.Producto producto = new ML.Producto();
            if (result.Correct)
            {
                producto.Productos = result.Objects;
            }
            else
            {
                ViewBag.Mensaje = "Error al momento de obtener los productos " + result.ErrorMessage;
            }
            return View(producto);
        }

        [HttpGet]
        public ActionResult Form(int? IdProducto)
        {
            ML.Producto producto = new ML.Producto();
            if (IdProducto == null)
            {
                return View(producto);
            }
            else
            {
                ML.Result result = BL.Producto.GetById(IdProducto.Value);
                if (result.Correct)
                {
                    producto = ((ML.Producto)result.Object);
                    return View(producto);
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult Form(ML.Producto producto)
        {
            ML.Result result = new ML.Result();
            HttpPostedFileBase file = Request.Files["ImagenData"];
            if (file.ContentLength > 0)
            {
                producto.Imagen = ConvertToBytes(file);
            }
            if (producto.IdProducto == 0)
            {
                result = BL.Producto.Add(producto);
                if (result.Correct)
                {
                    ViewBag.Mensaje = "Se ha agregado al producto correctamente";
                }
                else
                {
                    ViewBag.Mensaje = "Ha ocurrido un error al ingresar a producto " + result.ErrorMessage;
                }
            }
            else
            {
                result = BL.Producto.Update(producto);
                if (result.Correct)
                {
                    ViewBag.Mensaje = "Se ha actualizado al producto correctamente";
                }
                else
                {
                    ViewBag.Mensaje = "Ha ocurrido un error al actualizar el producto " + result.ErrorMessage;
                }
            }
            return PartialView("Modal");
        }

        public ActionResult Sumar(int IdProducto)
        {
            ML.Producto producto = new ML.Producto();
            ML.Result result = BL.Producto.GetById(IdProducto);
            if (result.Correct)
            {
                producto = ((ML.Producto)result.Object);
                producto.Stock += 1;
                ML.Result resultUpdate = BL.Producto.Update(producto);
                if (resultUpdate.Correct)
                {
                    return Redirect("GetAll");
                }
            }
            return View();
        }

        public ActionResult Restar(int IdProducto)
        {
            ML.Producto producto = new ML.Producto();
            ML.Result result = BL.Producto.GetById(IdProducto);
            if (result.Correct)
            {
                producto = ((ML.Producto)result.Object);
                producto.Stock -= 1;
                ML.Result resultUpdate = BL.Producto.Update(producto);
                if (resultUpdate.Correct)
                {
                    return Redirect("GetAll");
                }
            }
            return View();
        }


        [HttpGet]
        public ActionResult Delete(int IdProducto)
        {
            ML.Producto producto = new ML.Producto();
            producto.IdProducto = IdProducto;
            ML.Result result = BL.Producto.Delete(producto);
            if (result.Correct)
            {
                ViewBag.Mensaje = "El producto ha sido eliminado correctamente";
            }
            else
            {
                ViewBag.Mensaje = "EL producto no ha sido eliminado correctamente " + result.ErrorMessage;
            }
            return PartialView("Modal");
        }

        public byte[] ConvertToBytes(HttpPostedFileBase Imagen)
        {
            byte[] data = null;
            System.IO.BinaryReader reader = new System.IO.BinaryReader(Imagen.InputStream);
            data = reader.ReadBytes((int)Imagen.ContentLength);
            return data;
        }
    }
}