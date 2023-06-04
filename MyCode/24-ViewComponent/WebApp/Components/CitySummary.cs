using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Components;

public class CitySummary : ViewComponent
{
    private CitiesData _data;

    public CitySummary(CitiesData cdata)
    {
        _data = cdata;
    }

    public IViewComponentResult Invoke(string themeName = "success")
    {
        ViewBag.Theme = themeName;

        return View(new CityViewModel
        {
            Cities = _data.Cities.Count(),
            Population = _data.Cities.Sum(c => c.Population)
        });
    }
}
