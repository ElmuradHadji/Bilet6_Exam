using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Data;
using System.IO;
using System.IO.Pipes;
using WebApplication1.Context;
using WebApplication1.Extentions;
using WebApplication1.Models;
using WebApplication1.VMs.PortfolioVMs;

namespace WebApplication1.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class PortfoiloController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public PortfoiloController(AppDbContext context, IMapper mapper, IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _env = env;
        }

        public IActionResult Index()
        {
            List<Portfoilo> portfoilos = _context.Portfoilos.ToList();
            List<PortfolioGetVM> portfolioGetVMs = _mapper.Map<List<PortfolioGetVM>>(portfoilos);
            return View(portfolioGetVMs);
        }
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Create(PortfolioPostVM portfolioPostVM)
        {
            if (!ModelState.IsValid)
            {
                return View(portfolioPostVM);
            }
            Portfoilo portfoilo = _mapper.Map<Portfoilo>(portfolioPostVM);
            if (!portfolioPostVM.formFile.IsFormatOkay("Image"))
            {
                ModelState.AddModelError("fileForm", "Invalid File Format!");
                return View(portfolioPostVM);
            }
            if (!portfolioPostVM.formFile.IsSizeOkay(2))
            {
                ModelState.AddModelError("fileForm", "Invalid File size !");
                return View(portfolioPostVM);
            }
            portfoilo.Image = portfolioPostVM.formFile.CreateFile(_env.WebRootPath, "assets/img");
            _context.Add(portfoilo);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }



        public async Task<IActionResult> Update(int id)
        {
            Portfoilo portfoilo = await _context.Portfoilos.FindAsync(id);
            if (portfoilo is null)
            {
                return NotFound();
            }
            else
            {
                PortfolioUpdateVM portfolioUpdateVM= new PortfolioUpdateVM { portfolioGetVM = _mapper.Map<PortfolioGetVM>(portfoilo) };
              
                return View(portfolioUpdateVM);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Update(PortfolioUpdateVM portfolioUpdateVM)
        {
            Portfoilo portfoilo = await _context.Portfoilos.FindAsync(portfolioUpdateVM.portfolioGetVM.Id);
            if (portfoilo is null)
            {
                return NotFound();
            }
            else
            {
                portfoilo.AlternativeText= portfolioUpdateVM.portfolioPostVM.AlternativeText;
                portfoilo.Id = portfolioUpdateVM.portfolioGetVM.Id;
                if (portfolioUpdateVM.portfolioPostVM.formFile is not null)
                {
                    if (!portfolioUpdateVM.portfolioPostVM.formFile.IsFormatOkay("Image"))
                    {
                        ModelState.AddModelError("fileForm", "Invalid File Format!");
                        return View(portfolioUpdateVM.portfolioPostVM);
                    }
                    if (!portfolioUpdateVM.portfolioPostVM.formFile.IsSizeOkay(2))
                    {
                        ModelState.AddModelError("fileForm", "Invalid File size !");
                        return View(portfolioUpdateVM.portfolioPostVM);
                    }
                    portfoilo.Image = portfolioUpdateVM.portfolioPostVM.formFile.CreateFile(_env.WebRootPath, "assets/img");

                }
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));

            }
        }
        public async Task<IActionResult> Delete(int id)
        {
            Portfoilo portfoilo = await _context.Portfoilos.FindAsync(id);
            if (portfoilo is null)
            {
                return NotFound();
            }
            else
            {
                _context.Remove(portfoilo);
                _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
        }
    }
}
