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
    
    public partial class Activity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Activity()
        {
            this.AssessmentModuleActivities = new ObservableListSource<AssessmentModuleActivity>();
            this.CurriculumCourseModules = new ObservableListSource<CurriculumCourseModule>();
        }
    
        public int ActivityID { get; set; }
        public string ActivityCode { get; set; }
        public int ActvityCategoryID { get; set; }
        public string ActivitiyDescription { get; set; }
    
        public virtual LookupActivityCategory LookupActivityCategory { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ObservableListSource<AssessmentModuleActivity> AssessmentModuleActivities { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ObservableListSource<CurriculumCourseModule> CurriculumCourseModules { get; set; }
    }
}