using System;
using System.Collections.Generic;

namespace WpfApp1.Models;

public partial class Book
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Author { get; set; } = null!;

    public int? Price { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
