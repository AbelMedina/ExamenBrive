using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Sucursal
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AMedinaBriveEntities context = new DL.AMedinaBriveEntities())
                {
                    var sucursales = context.SucursalGetAll().ToList();
                    result.Objects = new List<object>();
                    if (sucursales!=null)
                    {
                        foreach (var obj in sucursales)
                        {
                            ML.Sucursal sucursal = new ML.Sucursal();
                            sucursal.IdSucursal = obj.IdSucursal;
                            sucursal.Nombre = obj.Nombre;
                            sucursal.Direccion = obj.Direccion;
                            sucursal.Telefono = obj.Telefono;
                            result.Objects.Add(sucursal);
                        }
                        result.Correct = true;    
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontraron registros en la tabla de sucursales";
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
        public static ML.Result GetById(int IdSucursal)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AMedinaBriveEntities context = new DL.AMedinaBriveEntities())
                {
                    var query = context.SucursalGetById(IdSucursal).FirstOrDefault();
                    result.Object = new List<object>();
                    if (query != null)
                    {
                        ML.Sucursal sucursal = new ML.Sucursal();
                        sucursal.IdSucursal = query.IdSucursal;
                        sucursal.Nombre = query.Nombre;
                        sucursal.Direccion = query.Direccion;
                        sucursal.Telefono = query.Telefono;
                        result.Object = sucursal;
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontraron registros en la tabla de sucursales";
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
