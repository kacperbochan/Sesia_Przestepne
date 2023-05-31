using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace Sesia_Przestepne.Models
{
    public class Search
    {
        public Search() { }

        public Search(int year) 
        {
            Year = year;
            Leap = (((year % 4 == 0) && (year % 100 != 0)) || (year % 400 == 0));
        }
        
        public Search(string name, int year)
        {
            Name = name;
            Year = year;
            Leap = (((year % 4 == 0) && (year % 100 != 0)) || (year % 400 == 0));
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [
            Display(Name = "Imie"),
            //Required, 
            MaxLength(100, ErrorMessage = "{0} nie powinno przekraczać {1} liter."), 
            MinLength(1, ErrorMessage ="{0} powinno mieć długość conajmniej 1 znaku")]
        public string? Name { get; set; }

        [Display(Name = "Rok urodzenia")]
        [Required, Range(1899, 2022, ErrorMessage = "Oczekiwana wartość {0} z zakredu {1} i {2}."),]
        public int? Year { get; set; }

        [Display(Name = "Czy przestępny")]
        public bool? Leap { get; set; }

        public bool? IsLeap()
        {
            Leap = (((Year % 4 == 0) && (Year % 100 != 0)) || (Year % 400 == 0));
            return Leap;
        }

        [Display(Name = "Kiedy Sprawdzano")]
        public DateTime? SearchTime { get; set; }

        public string? UserId { get; set; }

        public IdentityUser? User { get; set; }
    }
}