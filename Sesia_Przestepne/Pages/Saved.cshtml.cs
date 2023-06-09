using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Sesia_Przestepne.Models;
using System.Text.Json.Serialization;

namespace Sesia_Przestepne.Pages
{
    public class SavedModel : PageModel
    {
        public List<Search> People = new List<Search>();
        public void OnGet()
        {
            var Data = HttpContext.Session.GetString("People");
            if (Data != null)
                People = JsonConvert.DeserializeObject<List<Search>>(Data);
        }
    }
}
