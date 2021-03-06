﻿using ArthWeight.Data;
using ArthWeight.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DutchTreat.Controllers
{
  public class AppController : Controller
  {
        private readonly IArthwindsRepository _arthwindsRepository;

        public AppController(IArthwindsRepository arthwindsRepository)
        {
            _arthwindsRepository = arthwindsRepository;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(WeightViewModel weightViewModel)
        {
            if (ModelState.IsValid)
            {
                ViewBag.UserMessage = "Weight added";
                ModelState.Clear();
            }
            return View();
        }

        public IActionResult History()
        {
            var weightEntries = _arthwindsRepository.GetWeightEntries();
            return View(weightEntries);
        }
    }
}
