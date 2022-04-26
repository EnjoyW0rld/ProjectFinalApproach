using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    internal class Level1 : Scene
    {
        Player player;
        List<Satelite> satelites = new List<Satelite>();
        List<Planet> planets = new List<Planet>();
        public Level1()
        {
            planets.Add(new Planet(new Vec2(400, 450), 200));
            planets.Add(new Planet(new Vec2(1200, 450), 500));
            AddChild(player = new Player());
            foreach (var planet in planets) AddChild(planet);
            satelites.Add(new Satelite(planets[0]));
            //satelites.Add(new Satelite(planets[1]));
            foreach(var sat in satelites)AddChild(sat);
        }
        public override Player GetPlayer()
        {
            return player;
        }
        public override Satelite[] GetSatelites()
        {
            return satelites.ToArray();
        }

    }
}
