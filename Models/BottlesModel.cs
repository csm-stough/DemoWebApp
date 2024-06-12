using System.ComponentModel.DataAnnotations;

namespace DemoWebApp.Models
{
    public class BottlesModel
    {
        [Key]
        public int Id { get; set; } //Id in our local db
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Value for {0} must be positive")]
        public int NumberOfBottles { get; set; } //number of bottles produced
        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = false)]
        public DateTime Date { get; set; } //date bottles were produced
    }
}
