using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Xml.Linq;

namespace EmployeeWorkScheduler.Web.Models
{
    [Table(name: "Employees")]
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Employee ID")]
        public int EmpId { get; set; }

        [Required(ErrorMessage = "Please enter First Name !")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter Last Name !")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please select Designation !")]
        [Display(Name = "Designation")]
        public string Designation { get; set; }


        [Display(Name = "Gender")]
        [Required(ErrorMessage = "Please Select the Gender !")]
        public string Gender { get; set; }


        public string Email { get; set; }

        [Required(ErrorMessage = "Please add the required field !")]
        [Display(Name = "Profile Photo")]
        public string ImageUrl { get; set; } = null;

        #region navigation to AssignedTasks model

        public ICollection<AssignedTask> AssignedTasks { get; set; }

        #endregion
    }
}

