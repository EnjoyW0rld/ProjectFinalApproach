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
        List<SpaceBody> planets = new List<SpaceBody>();
        BallCollider[] colliders;
        MyCamera cam;

        public Level1(string name = "level1.tmx") : base(name)
        {
            planets.Add(new Planet(new Vec2(456,441), 300, 100, 5000, "Assets/planet_purple_cel.png"));
            planets.Add(new Planet(new Vec2(958,740), 300, 100, 5000, "Assets/planet_green.png"));
            planets.Add(new Planet(new Vec2(1115,337), 300, 100, 5000, "Assets/planet_green.png"));
            planets.Add(new Planet(new Vec2(1413,241), 300, 100, 5000, "Assets/planet_green.png"));
            //planets.Add(new Planet(new Vec2(400, 450), 300,100,10000, "Assets/planet_green.png"));
            //planets.Add(new Planet(new Vec2(1200, 450), 500,300,10000, "Assets/planet_purple.png"));
            //planets.Add(new Portal(new Vec2(1600, 450), 400,1,90,200,1000,"circle.png"));
            //planets.Add(new Portal(new Vec2(2400, 450), 300,1,-90,200,1000,"triangle.png"));

            //AddChild(player = new Player());
            cam = new MyCamera(player);
            AddChild(cam);

            foreach (var planet in planets) AddChild(planet);
            //satelites.Add(new Satelite(planets[0]));
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
        public override BallCollider[] GetBallColliders()
        {
            if(colliders == null)
            {
                colliders = new BallCollider[planets.Count];
                for (int i = 0; i < planets.Count; i++)
                {
                    colliders[i] = planets[i].GetCollider();
                }
            }
            return colliders;
        }
        public override SpaceBody[] GetSpaceBodies() => planets.ToArray();

    }
}
