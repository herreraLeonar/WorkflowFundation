﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApp.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class CoreToWorkflowEntities : DbContext
    {
        public CoreToWorkflowEntities()
            : base("name=CoreToWorkflowEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<tableEstatus> tableEstatus { get; set; }
        public virtual DbSet<tableTasks> tableTasks { get; set; }
        public virtual DbSet<tableTracks> tableTracks { get; set; }
        public virtual DbSet<tableWorkflow> tableWorkflow { get; set; }
    }
}
