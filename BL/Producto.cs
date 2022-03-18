using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Producto
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AMedinaBriveEntities context = new DL.AMedinaBriveEntities())
                {
                    var productos = context.ProductoGetAll().ToList();
                    result.Objects = new List<object>();
                    if (productos != null)
                    {
                        foreach (var obj in productos)
                        {
                            ML.Producto producto = new ML.Producto();
                            producto.IdProducto = obj.IdProducto;
                            producto.Nombre = obj.Nombre;
                            producto.Descripcion = obj.Descripcion;
                            producto.Precio = obj.Precio.Value;
                            producto.Imagen = obj.Imagen;
                            producto.Stock = obj.Stock.Value;
                            result.Objects.Add(producto);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se han encontrado resgitros de los productos";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static ML.Result GetById(int IdProducto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AMedinaBriveEntities context = new DL.AMedinaBriveEntities())
                {
                    var query = context.ProductoGetById(IdProducto).FirstOrDefault();
                    result.Object = new List<object>();
                    if (query != null)
                    {
                        ML.Producto producto = new ML.Producto();
                        producto.IdProducto = query.IdProducto;
                        producto.Nombre = query.Nombre;
                        producto.Descripcion = query.Descripcion;
                        producto.Precio = query.Precio.Value;
                        producto.Imagen = query.Imagen;
                        producto.Stock = query.Stock.Value;
                        result.Object = producto;
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontro ningun registro";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static ML.Result Add(ML.Producto producto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AMedinaBriveEntities context = new DL.AMedinaBriveEntities())
                {
                    var query = context.ProductoAdd(producto.Nombre, producto.Descripcion, producto.Precio, producto.Imagen, producto.Stock);
                    if (query>=1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrio un error al momento de agregar el producto";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static ML.Result Update(ML.Producto producto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AMedinaBriveEntities context = new DL.AMedinaBriveEntities())
                {
                    var query = context.ProductoUpdate(producto.IdProducto, producto.Nombre, producto.Descripcion, producto.Precio, producto.Imagen, producto.Stock);
                    if (query>=1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se actualizo el producto";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static ML.Result Delete(ML.Producto producto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AMedinaBriveEntities context = new DL.AMedinaBriveEntities())
                {
                    var query = context.ProductoDelete(producto.IdProducto);
                    if (query>=1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se ha eliminado al registro correctamente";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex =ex;
            }
            return result;
        }
    }
}
