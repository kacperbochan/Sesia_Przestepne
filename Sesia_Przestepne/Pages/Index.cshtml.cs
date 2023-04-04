using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Sesia_Przestepne.Models;

namespace Sesia_Przestepne.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        [BindProperty]
        public Person NewPerson { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet() {
        }

        public IActionResult OnPost()
        {

            if (ModelState.IsValid)// ciekawostka musiałem dodać ? po typie string dla zmiennej Name, jest to dla tego, że jeśli nie podało się nazwy urztkownika 
            {
                var Data = HttpContext.Session.GetString("People");
                List<Person> People = new List<Person>();
                if (Data != null)
                    People = JsonConvert.DeserializeObject<List<Person>>(Data);

                People.Add(NewPerson);

                HttpContext.Session.SetString("People",
                JsonConvert.SerializeObject(People));
                return RedirectToPage("./SavedInSession");
            }
            return Page();

        }
    }
}