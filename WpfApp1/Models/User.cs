using System;
using System.Collections.Generic;

namespace WpfApp1.Models;

public partial class User
{
    public int Id { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Role { get; set; } = null!;

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
