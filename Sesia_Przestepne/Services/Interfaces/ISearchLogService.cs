using Sesia_Przestepne.Models;

namespace Sesia_Przestepne.Services.Interfaces
{
    /// <summary>
    /// Oryginalnie miał się nazywać ILeapYearInterface, ale zmieniłem nazwę by bardziej pasowała
    /// </summary>
    public interface ISearchLogService
    {
        /// <summary>
        /// Sprawdza, czy People w bazie danych mają jakiekolwiek rekordy
        /// </summary>
        /// <returns></returns>
        public Task<bool> IsPeopleNotNull();

        /// <summary>
        /// Usuwa wyszukiwanie o id jeśli urzytkownk który je wykonał i podany są tym samym urzytkownikiem 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Task<bool> RemoveSearch(int id, string userId);

        /// <summary>
        /// Zwraca ilość stron
        /// </summary>
        /// <returns></returns>
        public Task<int> GetPageAmount();

        /// <summary>
        /// Zwraca zapisy wyszukań na obecnej stronie
        /// </summary>
        /// <param name="currentPage"></param>
        /// <returns></returns>
        public Task<List<Search>> GetCurrentPage(int currentPage);

        /// <summary>
        /// Dodajemy kolejne wyszukanie do DB
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public Task<bool> AddSearch(Search search);

    }
}
