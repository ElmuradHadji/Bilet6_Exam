using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Context;
using WebApplication1.Models;
using WebApplication1.VMs.AppUserVMs;

namespace WebApplication1.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(AppDbContext context, IMapper mapper, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<AppUser> signInManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Index()
        {
            //await _roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
            //await _roleManager.CreateAsync(new IdentityRole { Name = "User" });

            return Json("OK");
        }
        public async Task<IActionResult> CreateRole()
        {
            return RedirectToAction(nameof(Register));
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid)
            {
                return View(registerVM);
            }
            if (await _userManager.FindByNameAsync(registerVM.UserName.ToLower()) is not null)
            {
                ModelState.AddModelError("", "User already exsists");
                return View(registerVM);
            }
            AppUser user = _mapper.Map<AppUser>(registerVM);
            user.UserName = user.UserName.ToLower();

            var Result = await _userManager.CreateAsync(user, registerVM.Password);
            if (!Result.Succeeded)
            {
                foreach (var error in Result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(registerVM);
            }
            var Result1 = await _userManager.AddToRoleAsync(user, "Admin");
            if (!Result1.Succeeded)
            {
                foreach (var error in Result1.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(registerVM);
            }
            return RedirectToAction(nameof(Login));
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid)
            {
                return View(loginVM);
            }
            AppUser user = await _userManager.FindByNameAsync(loginVM.UserName.ToLower());
            if (user is null)
            {
                ModelState.AddModelError("", "UserName Or Password is invalid");
                return View(loginVM);
            }

            user.UserName = loginVM.UserName;

            var Result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, true, true);

            if (Result.IsLockedOut)
            {
                ModelState.AddModelError("", "You re blocked");
                return View(loginVM);
            }
            if (!Result.Succeeded)
            {
                ModelState.AddModelError("", "UserName Or Password is invalid");
                return View(loginVM);
            }
            return RedirectToAction("Index", "Home");


        }
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));

        }
    }
}
