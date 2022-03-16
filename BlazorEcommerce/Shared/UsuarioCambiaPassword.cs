using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorEcommerce.Shared
{
    public class UsuarioCambiaPassword
    {
        [Required, StringLength(100, MinimumLength = 6)]
        public string Password { get; set; } = string.Empty;
        [Compare("Password", ErrorMessage = "Os passwords non coinciden")]
        public string ConfirmaPassword { get; set; } = string.Empty;
    }
}
