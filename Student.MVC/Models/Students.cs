using System.ComponentModel.DataAnnotations;

namespace Student.MVC.Models
{
    public class Students
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public int Class { get; set; }
        [Required]
        public string City { get; set; }
    }
}


