using System;
using System.Collections.Generic;

namespace AppSuperheroes.Entities;

public partial class Gender
{
    public int Id { get; set; }

    public string? Gender1 { get; set; }

    public virtual ICollection<Superhero> Superheroes { get; set; } = new List<Superhero>();
}