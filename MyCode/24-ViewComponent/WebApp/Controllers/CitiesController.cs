﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using WebApp.Models;

namespace WebApp.Controllers;

[ViewComponent(Name = "CitiesControllerHybrid")]
public class CitiesController : Controller {
    private CitiesData _data;

    public CitiesController(CitiesData cdata) {
        _data = cdata;
    }

    public IActionResult Index() {
        return View(_data.Cities);
    }

    public IViewComponentResult Invoke() {
        return new ViewViewComponentResult() {
            ViewData = new ViewDataDictionary<CityViewModel>(
                ViewData,
                new CityViewModel {
                    Cities = _data.Cities.Count(),
                    Population = _data.Cities.Sum(c => c.Population)
                })
        };
    }
}
