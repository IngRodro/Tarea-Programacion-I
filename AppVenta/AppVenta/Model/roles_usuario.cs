//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AppVenta.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class roles_usuario
    {
        public int id_Rol_usuario { get; set; }
        public int id_usuario { get; set; }
        public string tipo_rol { get; set; }
    
        public virtual tb_usuarios tb_usuarios { get; set; }
    }
}
