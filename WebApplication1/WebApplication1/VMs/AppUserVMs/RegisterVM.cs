using System.ComponentModel.DataAnnotations;

namespace WebApplication1.VMs.AppUserVMs
{
    public class RegisterVM
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        [Compare(nameof(Password))]
        public string ConfirmedPassword { get; set; }

    }
}
