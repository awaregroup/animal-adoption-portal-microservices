using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AnimalAdoption.Web.Identity.Pages
{
    public class AuthenticateModel : PageModel
    {
        public string RedirectUrl { get; set; }

        public void OnGet(string redirectUrl)
        {
            RedirectUrl = redirectUrl;
        }

        public IActionResult OnGetSelectTeam(string id, string redirectUrl)
        {
            //TODO: check the valid urls
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(redirectUrl))
            {
                return Redirect("/error");
            }

            //switch name to convert from object to name
            string name = "";
            switch (id)
            {
                case "pencil":
                    name = "Charlie";
                    break;
                case "flower":
                    name = "Jackie";
                    break;
                case "icecream":
                    name = "Alex";
                    break;
                case "basketball":
                    name = "Logan";
                    break;
                case "orange":
                    name = "Murphy";
                    break;
                case "placeholder":
                    name = "Placeholder";
                    break;
            }

            if (string.IsNullOrEmpty(name))
            {
                return Redirect("/error");
            }

            //redirect to redirectUrl if base url matches
            return Redirect($"{redirectUrl}?z_name={name}");
        }
    }
}