using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    internal class Planet : GameObject
    {
        public Vec2 pos { get; private set; }
        float gravityRadius;
        public float circumference { get { return 2 * Mathf.PI * gravityRadius; } }
        //float gravityPower;
        float mass = 10000f * 1000f;
        public BallCollider ballCollider { get; private set; }
        Sprite body;

        public Planet(Vec2 ppos, float rad)
        {
            pos = ppos;
            gravityRadius = rad;
            
            SetXY(pos.x, pos.y);
            body = new Sprite("circle.png", false);
            body.SetOrigin(body.width/2,body.height/2);
            body.height = 200;
            body.width = 200;

            AddChild(body);
            CreateOreol();
            ballCollider = new BallCollider(ppos, 100);
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
                DragShip(player,distanceToPlanet);
                
            }
            

            //Check if sattelites are in the gravity area
            foreach(var satelite in par.GetSatelites())
            {
                float distanceTo = satelite.pos.DistanceTo(pos);
                if(distanceTo <= gravityRadius)
                {
                    float force = GravityForce(satelite.Mass(), mass, distanceTo);
                    satelite.ApplyThrust((pos - satelite.pos) * force);
                }
            }
        }


        /// <summary>
        /// Method to apply gravity to the player ship
        /// </summary>
        /// <param name="player"></param>
        /// <param name="distance"></param>
        void DragShip(Player player, float distance)
        {
            
            Vec2 direction = (pos - player.pos).Normalized();
            float force = GravityForce(player.Mass(),mass,distance);
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
