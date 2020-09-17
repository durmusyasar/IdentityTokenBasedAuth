using IdentityTokenBasedAuth.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityTokenBasedAuth.ResourceViewModel
{
    public class UserViewModelResource
    {
        [Required(ErrorMessage = "Kullanıcı Adı gereklidir !")]
        public string UserName { get; set; }
        [RegularExpression(@"^(0(\d{3}) (\d{3}) (\d{2}) (\d{2}))$", ErrorMessage = "Telefon Numarası Uygun Formatta Değil!")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Email Adresi gereklidir !")]
        [EmailAddress(ErrorMessage = "Email Adresiniz Uygun Formatta Değil!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Şifre gereklidir !")]
        public string Password { get; set; }
        public DateTime? BirthDay { get; set; }
        public string Picture { get; set; }
        public string City { get; set; }
        public Gender Gender { get; set; }
    }
}
