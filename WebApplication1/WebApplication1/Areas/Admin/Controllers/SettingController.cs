using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Context;
using WebApplication1.Models;
using WebApplication1.VMs.SettingVMs;

namespace WebApplication1.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class SettingController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public SettingController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            Setting setting = _context.Settings.FirstOrDefault();
            if (setting is null)
            {
                return NotFound();
            }
            SettingGetVM settingGetVM = _mapper.Map < SettingGetVM > (setting);
            return View(settingGetVM);
        }
        public IActionResult Update()
        {
            Setting setting = _context.Settings.FirstOrDefault();
            if (setting is null)
            {
                return NotFound();
            }
            SettingUpdateVM settingUpdateVM=new SettingUpdateVM { settingGetVM = _mapper.Map<SettingGetVM>(setting) };
            return View(settingUpdateVM);
        }
        [HttpPost]
        public IActionResult Update(SettingUpdateVM settingUpdateVM)
        {
            Setting setting = _context.Settings.FirstOrDefault();
            if (setting is null)
            {
                return NotFound();
            }
            setting.Adress= settingUpdateVM.settingPostVM.Adress;
            setting.Logo= settingUpdateVM.settingPostVM.Logo;
            setting.Year= settingUpdateVM.settingPostVM.Year;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
