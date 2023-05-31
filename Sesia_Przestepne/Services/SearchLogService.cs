using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Sesia_Przestepne.Migrations;
using Sesia_Przestepne.Models;
using Sesia_Przestepne.Services.Interfaces;
using System.Configuration;
using System.Drawing.Printing;

namespace Sesia_Przestepne.Services
{
    public class SearchLogService : ISearchLogService
    {
        private readonly ApplicationDbContext _context;
        private readonly int _pageSize = 20;

        public SearchLogService(ApplicationDbContext context) { 
            _context = context;
        }



        public async Task<bool> AddSearch(Search search)
        {
            if (search == null)
                return false;

            await _context.People.AddAsync(search);

            var created = await _context.SaveChangesAsync();

            return created > 0;

        }

        public async Task<List<Search>> GetCurrentPage(int currentPage)
        {
            if (currentPage < 1) return new List<Search>();
            //pobieramy zapisy z obecnej strony
            return await _context.People
                .Skip((currentPage-1) * _pageSize)
                .Take(_pageSize).ToListAsync();
        }

        public async Task<int> GetPageAmount()
        {
            var peopleCount = await _context.People.CountAsync();
            return (int)Math.Ceiling((double)peopleCount/_pageSize);
        }

        public async Task<bool> IsPeopleNotNull()
        {
            return _context.People != null;
        }

        public async Task<bool> RemoveSearch(int id, string userId)
        {
            //jeśli istnieje zapis o takim id
            if (_context.People.Any(x => x.Id == id))
            {

                //pobieramy wyszukanie
                Search person = await _context.People.Where(x => x.Id == id).FirstAsync();

                //drugie sprawdzenie po stronie serwera
                //jeśli id usuwanego elementu należy do użytkownika wykonującego usuwanie
                if (userId == person.UserId)
                {
                    //usuwamy z bazy danych
                    _context.People.Remove(person);
                    _context.SaveChanges();
                    
                    return true;
                }
            }
            return false;
        }
    }
}
