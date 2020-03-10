using System.Collections.Generic;
using System.Threading.Tasks;
using SuperHeroesApp.ComponentClassLib.Models;

namespace SuperHeroesApp.ComponentClassLib.Services
{
    public interface ISuperHeroesService
    {
        Task<List<SuperHeroModel>> GetSuperHeroesAsync();

        Task AddSuperHeroAsync(SuperHeroModel model);
    }
}