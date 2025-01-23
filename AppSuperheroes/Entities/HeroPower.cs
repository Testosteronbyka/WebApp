using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppSuperheroes.Entities;

public partial class HeroPower
{
    [Key]
    [Column(Order = 0)]
    public int HeroId { get; set; }

    [Key]
    [Column(Order = 1)]
    public int PowerId { get; set; }

    [ForeignKey("HeroId")]
    public virtual Superhero Hero { get; set; }

    [ForeignKey("PowerId")]
    public virtual Superpower Power { get; set; }
}