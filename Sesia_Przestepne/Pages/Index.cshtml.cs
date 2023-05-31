using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Sesia_Przestepne.Models;
using Sesia_Przestepne.Services.Interfaces;
using System;

namespace Sesia_Przestepne.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        
        private readonly ISearchLogService _searchLogService;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;


        [BindProperty]
        public Search NewSearch { get; set; }

        public IndexModel(ISearchLogService searchLogService ,ILogger<IndexModel> logger, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _searchLogService = searchLogService;
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
                List<Search> People = new List<Search>();
                if (Data != null)
                    People = JsonConvert.DeserializeObject<List<Search>>(Data);
                
                NewSearch.IsLeap();

                People.Add(NewSearch);

                if (_signInManager.IsSignedIn(User))
                {
                    NewSearch.UserId = _userManager.GetUserId(User);
                    NewSearch.User = (IdentityUser?)_userManager.Users.Where(x=>x.Id == NewSearch.UserId).First();
                }

                NewSearch.SearchTime = DateTime.Now;

                _searchLogService.AddSearch(NewSearch);

                HttpContext.Session.SetString("People",
                JsonConvert.SerializeObject(People));
            }
            return Page();

        }
    }
}