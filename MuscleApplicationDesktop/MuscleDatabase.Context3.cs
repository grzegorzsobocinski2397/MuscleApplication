﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MuscleAppWPF
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class muscleapplication_dbEntities : DbContext
    {
        public muscleapplication_dbEntities()
            : base("name=muscleapplication_dbEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Exercises> Exercises { get; set; }
        public virtual DbSet<RoutineExercises> RoutineExercises { get; set; }
        public virtual DbSet<Routines> Routines { get; set; }
        public virtual DbSet<Sets> Sets { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<UserWorkouts> UserWorkouts { get; set; }
    }
}
