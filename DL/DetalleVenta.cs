//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DL
{
    using System;
    using System.Collections.Generic;
    
    public partial class DetalleVenta
    {
        public int IdDetalleProducto { get; set; }
        public Nullable<int> IdSucursal { get; set; }
        public Nullable<int> Cantidad { get; set; }
        public Nullable<int> IdProducto { get; set; }
    
        public virtual Producto Producto { get; set; }
        public virtual Sucursal Sucursal { get; set; }
    }
}
