//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ReservationDemo.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Train
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Train()
        {
            this.Coaches = new HashSet<Coach>();
            this.Train_Schedules = new HashSet<Train_Schedules>();
        }
    
        public int train_id { get; set; }
        public string train_name { get; set; }
        public string train_type { get; set; }
        public Nullable<int> start_station_id { get; set; }
        public Nullable<int> end_station_id { get; set; }
        public decimal base_fare { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Coach> Coaches { get; set; }
        public virtual Station Station { get; set; }
        public virtual Station Station1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Train_Schedules> Train_Schedules { get; set; }
    }
}
