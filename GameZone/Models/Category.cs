namespace GameZone.Models
{
    public class Category:TempEntity
    {
        public ICollection<Game> Games { get; set; } = new List<Game>();
    }
}
