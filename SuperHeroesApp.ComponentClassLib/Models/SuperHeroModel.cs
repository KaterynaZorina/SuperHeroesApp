using System.ComponentModel.DataAnnotations;

namespace SuperHeroesApp.ComponentClassLib.Models
{
    public class SuperHeroModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Character { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(300)]
        public string Description { get; set; }

        [Required]
        public UniverseType Universe { get; set; }
    }
}