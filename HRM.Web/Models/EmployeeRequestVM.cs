using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace HRM.Web.Models
{
    public class EmployeeRequestVM
    {
        public long Id { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Please enter name")]
        public string Name { get; set; }

        [Display(Name = "Department")]
        [Required(ErrorMessage = "Please select department")]
        public string Department { get; set; }

        [Display(Name = "Designation")]
        [Required(ErrorMessage = "Please enter designation")]
        public string Designation { get; set; }

        [Display(Name = "Salary")]
        public decimal Salary { get; set; }

        public bool IsManager { get; set; }

        [Display(Name = "Manager")]
        [Required(ErrorMessage = "Please enter namanager name")]
        public ManagerEnum Manager { get; set; }

        [Display(Name = "Phone number")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Invalid phone number")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string Phone { get; set; }

        [Display(Name = "Email address")]
        [Required(ErrorMessage = "Please enter email address")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid email address format")]
        [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Invalid email address")]
        public string Email { get; set; }
    }

    public enum ManagerEnum
    {
        Neel,
        Pavish
    }
}
