//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplicationApi3.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class imatge_usuari
    {
        public int id { get; set; }
        public int id_usuari { get; set; }
        public string imatge_nom { get; set; }
    
        public virtual usuaris usuaris { get; set; }
    }
}
