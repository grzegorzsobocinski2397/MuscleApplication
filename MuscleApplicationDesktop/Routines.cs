//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MuscleApplication.Desktop
{
    using System;
    using System.Collections.Generic;
    
    public partial class Routines
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Routines()
        {
            this.id = "";
            this.RoutineExercises = new HashSet<RoutineExercises>();
            this.UserWorkouts = new HashSet<UserWorkouts>();
        }
    
        public string id { get; set; }
        public System.DateTimeOffset createdAt { get; set; }
        public string routineName { get; set; }
        public string userId { get; set; }
        public Nullable<System.DateTime> lastUsed { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RoutineExercises> RoutineExercises { get; set; }
        public virtual Users Users { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserWorkouts> UserWorkouts { get; set; }
    }
}
