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
    
    public partial class grups_has_horaris
    {
        public int id_grups { get; set; }
        public int id_horari { get; set; }
        public int id_dias { get; set; }
    
        public virtual dias dias { get; set; }
        public virtual grups grups { get; set; }
        public virtual horari horari { get; set; }
    }
}
