using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    internal class Planet : Sprite
    {
        public Vec2 pos { get; private set; }
        float gravityRadius;
        public float circumference { get { return 2 * Mathf.PI * gravityRadius; } }
        //float gravityPower;
        float mass = 10000f * 1000f;
        public Planet(Vec2 ppos, float rad) : base("triangle.png")
        {
            SetOrigin(width/2, height/2);
            //gravityPower = 10f;
            pos = ppos;
            SetXY(pos.x, pos.y);
            gravityRadius = rad;
            CreateOreol();
        }
        void Update()
        {
            
            //MyGame myGame = (MyGame)game;
            Scene par = (Scene)parent;
            Player player = par.GetPlayer();
            float distanceToPlanet = player.pos.DistanceTo(pos);
            if(distanceToPlanet <= gravityRadius)
            {
                DragShip(player,distanceToPlanet);
            }


            foreach(var satelite in par.GetSatelites())
            {
                float distanceTo = satelite.pos.DistanceTo(pos);
                if(distanceTo <= gravityRadius)
                {
                    float force = GravityForce(satelite.Mass(), mass, distanceTo);
                    Console.WriteLine(force + " speed towards the planet");
                    satelite.ApplyThrust((pos - satelite.pos) * force);
                }
            }
        }

        void DragShip(Player player, float distance)
        {
            /*
            Vec2 direction = (pos - player.pos).Normalized();
            float realGravityPower = gravityPower - ((distance  / gravityRadius) * gravityPower);
            Console.WriteLine(realGravityPower);
            player.AddVelocity(direction * realGravityPower);
            */

            //float force = 6.672f * Mathf.Pow(10,-7) * ((player.Mass() * mass) / Mathf.Pow(distance, 2));
            Vec2 direction = (pos - player.pos).Normalized();
            float force = GravityForce(player.Mass(),mass,distance);
            Console.WriteLine(force);
            player.AddVelocity(direction * force);

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

        public static float GravityForce(float m1, float m2, float distance)
        {
            return 6.672f * Mathf.Pow(10,-7) * (m1 * m2) / Mathf.Pow(distance, 2);
        }
        public float Mass() => mass;
        public float GetGravityRadius() => gravityRadius;
    }
}
