using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace qproject.Models
{
    [MetadataType(typeof(EmployeeMetadata))]
    public partial class Employee
    {

    }
    public class EmployeeMetadata
    {
        [Required (AllowEmptyStrings =false,ErrorMessage ="Please provide email")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide name")]
        public string Name { get; set; }
    }
}