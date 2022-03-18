using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class SucursalProducto
    {
        public static ML.Result GetProductosByIdSucursal(int IdSucursal)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AMedinaBriveEntities context = new DL.AMedinaBriveEntities())
                {
                    var query = context.ProductosGetAsignadosByIdSucursal(IdSucursal).ToList();
                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.SucursalProducto sucursalProducto = new ML.SucursalProducto();
                            sucursalProducto.IdSucursalProducto = obj.IdSucursalProducto;
                            sucursalProducto.Sucursal = new ML.Sucursal();
                            sucursalProducto.Sucursal.IdSucursal = obj.IdSucursal;
                            sucursalProducto.Sucursal.Nombre = obj.SucursalNombre;
                            sucursalProducto.Sucursal.Direccion = obj.Direccion;
                            sucursalProducto.Sucursal.Telefono = obj.Telefono;
                            sucursalProducto.Producto = new ML.Producto();
                            sucursalProducto.Producto.IdProducto = obj.IdProducto;
                            sucursalProducto.Producto.Nombre = obj.ProductoNombre;
                            sucursalProducto.Producto.Descripcion = obj.Descripcion;
                            sucursalProducto.Producto.Precio = obj.Precio;
                            sucursalProducto.Producto.Imagen = obj.Imagen;
                            sucursalProducto.Producto.Stock = obj.Stock;
                            result.Objects.Add(sucursalProducto);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontraron registros sobre la tabla de SucursalProducto";
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
        public static ML.Result GetProductosNoByIdSucursal(int IdSucursal)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AMedinaBriveEntities context = new DL.AMedinaBriveEntities())
                {
                    var query = context.ProductosGetNoAsignadosByIdSucursal(IdSucursal).ToList();
                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.SucursalProducto sucursalProducto = new ML.SucursalProducto();
                            sucursalProducto.Producto = new ML.Producto();
                            sucursalProducto.Producto.IdProducto = obj.IdProducto;
                            sucursalProducto.Producto.Nombre = obj.ProductoNombre;
                            sucursalProducto.Producto.Descripcion = obj.Descripcion;
                            sucursalProducto.Producto.Precio = obj.Precio;
                            sucursalProducto.Producto.Imagen = obj.Imagen;
                            sucursalProducto.Producto.Stock = obj.Stock;
                            result.Objects.Add(sucursalProducto);
                        }
                        result.Correct = true;

                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontraron registros en la tabla";
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
        public static ML.Result Add(ML.SucursalProducto sucursalProducto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AMedinaBriveEntities context = new DL.AMedinaBriveEntities())
                {
                    var query = context.SucursalProductoAdd(sucursalProducto.Sucursal.IdSucursal, sucursalProducto.Producto.IdProducto);
                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se ha podido registrar el producto";
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
        public static ML.Result Delete(ML.SucursalProducto sucursalProducto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AMedinaBriveEntities context = new DL.AMedinaBriveEntities())
                {
                    var query = context.SucursalProductoDelete(sucursalProducto.IdSucursalProducto);
                    if (query >=1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se ha podido eliminar al producto";
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
    }
}
