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
    
    public partial class LookupTheoriticalAssesmentStatus
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LookupTheoriticalAssesmentStatus()
        {
            this.AssessmentModules = new ObservableListSource<AssessmentModule>();
        }
    
        public int TheoriticalAssessmentStatusID { get; set; }
        public string TheoriticalAssesmentStatus { get; set; }
        public string TheoriticalAssessmentStatusCode { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ObservableListSource<AssessmentModule> AssessmentModules { get; set; }
    }
}
