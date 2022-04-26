using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    internal class Planet : Sprite
    {
        Vec2 pos;
        float gravityRadius;
        float gravityPower;
        float mass = 10000f * 1000f;
        public Planet(Vec2 ppos, float rad) : base("triangle.png")
        {
            SetOrigin(width/2, height/2);
            gravityPower = 10f;
            pos = ppos;
            SetXY(pos.x, pos.y);
            gravityRadius = rad;
            CreateOreol();
        }
        void Update()
        {
            
            MyGame myGame = (MyGame)game;
            Player player = myGame.GetPlayer();
            float distanceToPlanet = player.pos.DistanceTo(pos);
            if(distanceToPlanet <= gravityRadius)
            {
                DragShip(player,distanceToPlanet);
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
            Vec2 direction = (pos - player.pos).Normalized();
            float force = 6.672f * Mathf.Pow(10,-7) * ((player.Mass() * mass) / Mathf.Pow(distance, 2));
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
    }
}
