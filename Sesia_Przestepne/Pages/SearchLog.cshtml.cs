using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Sesia_Przestepne.Models;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authorization;

namespace Sesia_Przestepne.Pages
{
    [Authorize]
    public class SearchLogModel : PageModel
    {
        public List<Person> People = new List<Person>();
        public void OnGet()
        {
            var Data = HttpContext.Session.GetString("People");
            if (Data != null)
                People = JsonConvert.DeserializeObject<List<Person>>(Data);
        }
    }
}
