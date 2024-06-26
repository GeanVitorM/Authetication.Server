﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Authetication.Server.Api.Models;

public class Admin
{
    [Key]
    public int IdAdmin { get; set; }

    [MaxLength(100)]
    public string? NomeAdmin { get; set; }

    [MaxLength(100)]
    public string? EmailAdmin { get; set; }

    public TipoUsuario TipoUsuario { get; set; }

    [ForeignKey("IdUser")]
    public Usuario? Usuario { get; set; }
}
