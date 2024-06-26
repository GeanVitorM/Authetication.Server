﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Authetication.Server.Api.Models;

namespace Authetication.Server.Api.DTOs;

public class AdminDto
{
    [Key]
    public int IdAdmin { get; set; }

    [Required(ErrorMessage = "Username is required")]
    public string? NomeAdmin { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "The Email field is not a valid e-mail address.")]
    public string? EmailAdmin { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string? Password { get; set; }
    public TipoUsuario TipoUsuario { get; set; }

    [ForeignKey("IdUser")]
    public UsuarioDto? Usuario { get; set; }
}