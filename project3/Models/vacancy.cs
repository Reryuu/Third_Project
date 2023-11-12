//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace project3.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Runtime.Serialization;


    public partial class vacancy : IValidatableObject
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public vacancy()
        {
            this.details_interview = new HashSet<details_interview>();
        }
        [Key, Column(Order = 1)]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Vacancy ID")]
        public string id { get; set; }
        [Required]
        [Display(Name = "Title")]
        public string title { get; set; }
        [Required]
        [Display(Name = "Date end")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddThh:mm}", ApplyFormatInEditMode = true)]
        public System.DateTime endAt { get; set; }
        [Display(Name = "Status")]
        public string status { get; set; }
        [Display(Name = "Salary")]
        public string salary { get; set; }
        [Required]
        [Display(Name = "Required")]
        [DisplayFormat(ApplyFormatInEditMode = true)]
        public int required { get; set; }
        [Required]
        [Display(Name = "Description")]
        public int descriptionId { get; set; }
        [Required]
        [Display(Name = "Department")]
        public string departmentId { get; set; }
        [Display(Name = "Hired")]
        public string applicants_Id { get; set; }

        public virtual department department { get; set; }
        public virtual description description { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<details_interview> details_interview { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            //if (endAt < DateTime.Now)
            //{
            //    yield return new ValidationResult(
            //        "Vacancy must have a closed day later than now.",
            //        new[] { nameof(endAt) });
            //}
            if (required < 0)
            {
                yield return new ValidationResult(
                    "Required must more than 0.",
                    new[] { nameof(required) });
            }
        }
    }
}