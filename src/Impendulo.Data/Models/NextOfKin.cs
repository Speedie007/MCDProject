//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Impendulo.Data.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class NextOfKin
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NextOfKin()
        {
            this.Students = new ObservableListSource<Student>();
        }
    
        public int IndividualID { get; set; }
        public int TypeOfRelationID { get; set; }
    
        public virtual LookupTypeOfRelation LookupTypeOfRelation { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ObservableListSource<Student> Students { get; set; }
        public virtual Individual Individual { get; set; }
    }
}
