//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebSeries.EntityFramework
{
    using System;
    using System.Collections.Generic;
    
    public partial class Archive
    {
        public int ArchiveId { get; set; }
        public int VideoId { get; set; }
    
        public virtual Video Video { get; set; }
    }
}
