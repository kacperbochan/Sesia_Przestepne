using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Sesia_Przestepne.Models;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Sesia_Przestepne.Services.Interfaces;

namespace Sesia_Przestepne.Pages
{
    [Authorize]
    public class SearchLogModel : PageModel
    {

        private readonly ISearchLogService _searchLogService;
        private readonly UserManager<IdentityUser> _userManager;


        public int CurrentPage { get; set; } = 1;
        public int PageAmount { get; set; } = 1;
        public int PageSize { get; set; } = 20;

        public SearchLogModel(ISearchLogService searchLogService, UserManager<IdentityUser> userManager)
        {
            _searchLogService = searchLogService;
            _userManager = userManager;
        }

        public List<Search> People { get; set; } = default!;

        public async Task<IActionResult> OnPostRemoveLogAsync(int id, int currentPage = 1)
        {
            //sprawdzamy, czy baza danych zawiera jakiekolwiek wartości
            if (await _searchLogService.IsPeopleNotNull())
            {
                //usuwamy wyszukanie o podanym id
                await _searchLogService.RemoveSearch(id, _userManager.GetUserId(User));

                PageAmount = await _searchLogService.GetPageAmount();

                currentPage = (currentPage > PageAmount) ? PageAmount : currentPage;

                await _searchLogService.GetCurrentPage(currentPage);
            }
            return Page();
        }

        public async Task OnGetAsync(int currentPage = 1)
        {
            if (await _searchLogService.IsPeopleNotNull())
            {
                PageAmount = await _searchLogService.GetPageAmount();

                currentPage = (currentPage > PageAmount) ? PageAmount : currentPage;

                People = await _searchLogService.GetCurrentPage(currentPage);
            }
        }

        public string GetUserName(string id)
        { 
            return (_userManager.Users.Any(x => x.Id == id)) ? _userManager.Users.Where(x => x.Id == id).FirstOrDefault().UserName : "";
        }
    }
}
