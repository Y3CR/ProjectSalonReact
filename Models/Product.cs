using System;
using System.Collections.Generic;

namespace ProjectSalonReact.Models;

public partial class Product
{
    public int IdProduct { get; set; }

    public string? NameProduct { get; set; }

    public string? Description { get; set; }

    public int? Stocks { get; set; }

    public string? ImagePath { get; set; }

    public DateTime? DateAdmission { get; set; }
}
