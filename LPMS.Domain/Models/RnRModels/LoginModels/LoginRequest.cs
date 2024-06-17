﻿using System.ComponentModel.DataAnnotations;

namespace LPMS.Domain.Models.RnRModels.LoginModels
{
    public class LoginRequest
    {
        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;

        [Required, DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}
