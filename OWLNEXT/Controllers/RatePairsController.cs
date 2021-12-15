using Microsoft.AspNetCore.Mvc;
using OWLNEXT.Business.Contract;
using OWLNEXT.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OWLNEXT.Controllers
{
    public class RatePairsController : Controller
    {
        private readonly IMoneyService _moneyService;
        private readonly IRatePairsService _ratePairsService;
        public RatePairsController(IMoneyService moneyService ,IRatePairsService ratePairsService)
        {
            _moneyService = moneyService;
            _ratePairsService = ratePairsService;
        }
        public IActionResult Index()
        {
            var model = new RatePairsModels(_moneyService, _ratePairsService);
            return View(model);
        }
    }
}
