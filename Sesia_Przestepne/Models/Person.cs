using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Sesia_Przestepne.Models
{
    public class Person
    {
        public Person() { }
        
        public Person(string name, int year)
        {
            Name = name;
            Year = year;
            Leap = (((year % 4 == 0) && (year % 100 != 0)) || (year % 400 == 0));
        }

        [
            Display(Name = "Imie"),
            Required, 
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
    }
}