using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication1.Context;
using WebApplication1.Models;
using WebApplication1.VMs.PortfolioVMs;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public HomeController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            List<Portfoilo> portfoilos=_context.Portfoilos.ToList();
            List<PortfolioGetVM> portfolioGets = _mapper.Map<List<PortfolioGetVM>>(portfoilos);
            return View(portfolioGets);
        }


    }
}