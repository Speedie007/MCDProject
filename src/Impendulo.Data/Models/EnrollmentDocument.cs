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
    
    public partial class EnrollmentDocument
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EnrollmentDocument()
        {
            this.ScheduleConfirmationDocumentations = new ObservableListSource<ScheduleConfirmationDocumentation>();
        }
    
        public int EnrollmentDocumentID { get; set; }
        public int EnrollmentID { get; set; }
        public int FileID { get; set; }
        public int LookupEnrollmentDocumentTypeID { get; set; }
    
        public virtual File File { get; set; }
        public virtual LookupEnrollentDocumentType LookupEnrollentDocumentType { get; set; }
        public virtual Enrollment Enrollment { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ObservableListSource<ScheduleConfirmationDocumentation> ScheduleConfirmationDocumentations { get; set; }
    }
}
