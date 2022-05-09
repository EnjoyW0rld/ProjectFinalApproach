using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    internal class Planet : SpaceBody
    {
        public enum PlanetState {Start,Regular,End }
        public new Vec2 pos { get; private set; }
        public new BallCollider ballCollider { get; private set; }
        public readonly PlanetState st;

        public Planet(Vec2 ppos, float gRad, int planetR,
            float m, string path, string oreolPath = "Assets/blueSphere.png", PlanetState plst = PlanetState.Regular) : base(ppos, gRad, planetR, m, path)
        {
            pos = ppos;
            st = plst;
            ballCollider = new BallCollider(ppos, planetR);
            AddChild(ballCollider);
            CreateOreol(oreolPath);
        }
        void Update()
        {
            
            //MyGame myGame = (MyGame)game;
            Scene par = (Scene)parent;
            Player player = par.GetPlayer();
            float distanceToPlanet = player.pos.DistanceTo(pos);

            //Check if ship is in gravity affected area
            if(distanceToPlanet <= gravityRadius && distanceToPlanet > ballCollider.radius + player.ballCollider.radius)
            {
                Console.WriteLine();
                DragShip(player,distanceToPlanet);
                
            }
            

            //Check if sattelites are in the gravity area
            Satelite[] satelites = par.GetSatelites();
            if(satelites != null && satelites.Length > 0)
            {

            foreach(var satelite in satelites)
            {
                float distanceTo = satelite.pos.DistanceTo(pos);
                if(distanceTo <= gravityRadius)
                {
                    float force = GravityForce(satelite.Mass(), mass, distanceTo);
                    satelite.ApplyThrust((pos - satelite.pos) * force);
                }
            }
            }
        }


        

        void CreateOreol()
        {
            Sprite oreol = new Sprite("circle.png", false);
            oreol.SetOrigin(oreol.width / 2, oreol.height / 2);
            oreol.width = (int)gravityRadius * 2;
            oreol.height = (int)gravityRadius * 2;
            oreol.alpha = 0.2f;
            AddChild(oreol);
        }

        
        public float Mass() => mass;
        //public float GetGravityRadius() => gravityRadius;
        public override BallCollider GetCollider()
        {
            return ballCollider;
        }
    }
}
