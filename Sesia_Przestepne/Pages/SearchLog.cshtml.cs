using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Sesia_Przestepne.Models;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Sesia_Przestepne.Pages
{
    [Authorize]
    public class SearchLogModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public int TotalRecords { get; set; } = 0;

        public SearchLogModel(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public List<Search> People { get; set; } = default!;

        public async Task<IActionResult> OnPostRemoveLogAsync(int id, int currentPage = 1)
        {
            //sprawdzamy, czy baza danych zawiera jakiekolwiek wartości
            if (_context.People != null)
            {
                //jeśli istnieje zapis o takim id
                if (_context.People.Any(x=>x.Id == id)){

                    //pobieramy wyszukanie
                    Search person = await _context.People.Where(x => x.Id == id).FirstAsync();

                    //drugie sprawdzenie po stronie serwera
                    //jeśli id usuwanego elementu należy do użytkownika wykonującego usuwanie
                    if (_userManager.GetUserId(User) == person.UserId)
                    {
                        //usuwamy z bazy danych
                        _context.People.Remove(person);
                        _context.SaveChanges();
                    }
                }

                //sprawdzamy ile jest danych w DB
                TotalRecords = await _context.People.CountAsync();

                //jeśli wybrany numer strony przekracza ich ilość, pobieramy ostatnią
                currentPage = (currentPage > TotalRecords / PageSize) ? TotalRecords / PageSize : currentPage;

                //zapisujemy obecną stronę
                CurrentPage = currentPage;

                //pobieramy zapisy z obecnej strony
                People = await _context.People
                    .Skip((CurrentPage - 1) * PageSize)
                    .Take(PageSize)
                    .ToListAsync();
            }
            return Page();
        }

        public async Task OnGetAsync(int currentPage = 1)
        {
            if (_context.People != null)
            {

                CurrentPage = currentPage;
                TotalRecords = await _context.People.CountAsync();

                People = await _context.People
                    .Skip((CurrentPage - 1) * PageSize)
                    .Take(PageSize)
                    .ToListAsync();
            }
        }

        public string GetUserName(string id)
        { 
            return (_userManager.Users.Any(x => x.Id == id)) ? _userManager.Users.Where(x => x.Id == id).FirstOrDefault().UserName : "";
        }
    }
}
