using System.ComponentModel.DataAnnotations;

namespace GameZone.Models
{
    public class Game:TempEntity
    {
        [MaxLength(2500)]
        public string Description { get; set; } = string.Empty;

        [MaxLength(500)]
        public string Cover { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public Category Category { get; set; } = default!;
        public ICollection<GameDevice> Games { get; set; } = new List<GameDevice>();

    }
}
