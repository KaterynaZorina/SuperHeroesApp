using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using SuperHeroesApp.ComponentClassLib.Models;

namespace SuperHeroesApp.ComponentClassLib.Components.SuperHeroesList
{
    public partial class SuperHeroesList
    {
        [Parameter]
        public List<SuperHeroModel> Items { get; set; } = new List<SuperHeroModel>();
        
        [Parameter]
        public RenderFragment AddHeroItem { get; set; }
    }
}

// All materials were taken from https://www.marvel.com/ 