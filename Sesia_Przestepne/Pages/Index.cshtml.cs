using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Sesia_Przestepne.Models;
using System;

namespace Sesia_Przestepne.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;


        [BindProperty]
        public Person NewPerson { get; set; }

        public IndexModel(ILogger<IndexModel> logger, ApplicationDbContext context, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _logger = logger;
            _signInManager = signInManager;
            _userManager = userManager;
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
                
                NewPerson.IsLeap();

                People.Add(NewPerson);

                if (_signInManager.IsSignedIn(User))
                {
                    NewPerson.UserId = _userManager.GetUserId(User);
                    NewPerson.User = (IdentityUser?)_userManager.Users.Where(x=>x.Id == NewPerson.UserId).First();
                }

                NewPerson.SearchTime = DateTime.Now;

                _context.People.Add(NewPerson);
                _context.SaveChanges();

                HttpContext.Session.SetString("People",
                JsonConvert.SerializeObject(People));
            }
            return Page();

        }
    }
}