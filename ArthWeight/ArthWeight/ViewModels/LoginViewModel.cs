﻿using System.ComponentModel.DataAnnotations;

namespace ArthWeight.ViewModels
{
  public sealed class LoginViewModel
  {
    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
    public bool RememberMe { get; set; }
  }
}
