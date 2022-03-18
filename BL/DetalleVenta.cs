using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class DetalleVenta
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
                            ML.DetalleVenta detalleVenta = new ML.DetalleVenta();
                            //sucursalProducto.IdSucursalProducto = obj.IdSucursalProducto;
                            detalleVenta.Sucursal = new ML.Sucursal();
                            detalleVenta.Sucursal.IdSucursal = obj.IdSucursal;
                            detalleVenta.Sucursal.Nombre = obj.SucursalNombre;
                            detalleVenta.Sucursal.Direccion = obj.Direccion;
                            detalleVenta.Sucursal.Telefono = obj.Telefono;
                            detalleVenta.Producto = new ML.Producto();
                            detalleVenta.Producto.IdProducto = obj.IdProducto;
                            detalleVenta.Producto.Nombre = obj.ProductoNombre;
                            detalleVenta.Producto.Descripcion = obj.Descripcion;
                            detalleVenta.Producto.Precio = obj.Precio;
                            detalleVenta.Producto.Imagen = obj.Imagen;
                            detalleVenta.Producto.Stock = obj.Stock;
                            result.Objects.Add(detalleVenta);
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
    }
}
