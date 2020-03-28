using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SuperHeroesApp.ComponentClassLib.Models;

namespace SuperHeroesApp.ComponentClassLib.Services
{
    public class SuperHeroesService: ISuperHeroesService
    {
        private readonly List<SuperHeroModel> _superHeroesList;

        public SuperHeroesService()
        {
            _superHeroesList = new List<SuperHeroModel>
            {
                new SuperHeroModel
                {
                    Id = 1,
                    Name = "Natasha Romanoff",
                    Character = "BLACK WIDOW",
                    Universe = UniverseType.Marvel,
                    Description = "Trusted by some and feared by most, the Black Widow strives to make up for the bad she had done in the past by helping the world, even if that means getting her hands dirty in the process."
                },
                new SuperHeroModel
                {
                    Id = 2,
                    Name = "Carol Danvers",
                    Character = "CAPTAIN MARVEL",
                    Universe = UniverseType.Marvel,
                    Description = "Near death, Carol Danvers was transformed into a powerful warrior for the Kree. Now, returning to Earth years later, she must remember her past in order to to prevent a full invasion by shapeshifting aliens, the Skrulls."
                },
                new SuperHeroModel
                {
                    Id = 3,
                    Name = "Peter Parker",
                    Character = "SPIDER-MAN",
                    Universe = UniverseType.Marvel,
                    Description = "With amazing spider-like abilities, teenage science whiz Peter Parker fights crime and dreams of becoming an Avenger as Spider-Man."
                },
                new SuperHeroModel
                {
                    Id = 4,
                    Name = "Tony Stark",
                    Character = "IRON MAN",
                    Universe = UniverseType.Marvel,
                    Description = "Inventor Tony Stark applies his genius for high-tech solutions to problems as Iron Man, the armored Avenger."
                }
            };
        }
        
        public Task<List<SuperHeroModel>> GetSuperHeroesAsync()
        {
            return Task.FromResult(_superHeroesList);
        }

        public Task AddSuperHeroAsync(SuperHeroModel model)
        {
            _superHeroesList.Add(model);
            
            return Task.CompletedTask;
        }

        public Task<SuperHeroModel> GetSuperHero(int id)
        {
            var superHero = _superHeroesList.FirstOrDefault(h => h.Id == id);

            return Task.FromResult(superHero);
        }
    }
}

// All materials were taken from https://www.marvel.com/