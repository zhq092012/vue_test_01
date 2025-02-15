﻿using System;
using System.ComponentModel.DataAnnotations;

namespace dataprovider.Web.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "用户名")]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }
    }
}
