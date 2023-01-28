using AutoMapper;
using WebApplication1.Models;
using WebApplication1.VMs.PortfolioVMs;
using WebApplication1.VMs.AppUserVMs;
using WebApplication1.VMs.SettingVMs;

namespace WebApplication1.Profiles
{
    public class Mapper:Profile
    {
        public Mapper()
        {
            CreateMap<Portfoilo, PortfolioGetVM>();
            CreateMap<PortfolioPostVM, Portfoilo>();
            CreateMap<RegisterVM, AppUser>();
            CreateMap<LoginVM, AppUser>();
            CreateMap<Setting, SettingGetVM>();
            CreateMap<SettingPostVM, Setting>();
        }
    }
}
