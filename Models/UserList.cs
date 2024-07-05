using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracticeMVCProject.Models;

public partial class UserList
{
    public int UserId { get; set; }

    [NotMapped]
    public string EncrptedId { get; set; } = null!;
    public string UserName { get; set; } = null!;

    public string UserPassword { get; set; } = null!;

    public string Email { get; set; } = null!;
}
