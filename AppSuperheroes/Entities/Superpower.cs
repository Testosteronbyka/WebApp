using AppSuperheroes.Entities;

namespace AppSuperheroes.Entities
{
    public class Superpower
    {
        public int Id { get; set; }
        public string PowerName { get; set; }
        public virtual ICollection<HeroPower> HeroPowers { get; set; } = new List<HeroPower>();
    }
}