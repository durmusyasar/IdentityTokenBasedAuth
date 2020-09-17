using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityTokenBasedAuth.ResourceViewModel
{
    public class SignInViewModelResource
    {
        [Required(ErrorMessage = "Email Alanı Gereklidir!")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Şifre Alanı Gereklidir!")]
        [MinLength(4, ErrorMessage = "Şifre En Az 4 Karakterli Olmalıdır!")]
        public string Password { get; set; }
    }
}
