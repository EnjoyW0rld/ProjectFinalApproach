using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    internal class Satelite : Sprite
    {
        Planet parentPlanet;
        public Vec2 pos { get; private set; }
        Vec2 velocity;
        float mass = 3f;
        Vec2 acceleration;
        public Satelite(Planet p) : base("square.png")
        {
            pos = new Vec2(p.pos.x, p.pos.y - p.GetGravityRadius() + height/2);
            x = pos.x;
            y = pos.y;
            parentPlanet = p;
            Vec2 direction = ((p.pos - pos).Normalized());
            direction.RotateDegrees(-90);

            //float force = Planet.GravityForce(mass,p.Mass(),p.pos.DistanceTo(pos));
            //velocity = Vec2.GetUnitVectorDeg(0) * force;
            //velocity = direction * (Mathf.Sqrt((6.672f * Mathf.Pow(10, -7) * p.Mass())/ (pos.DistanceTo(p.pos)) ));
            Console.WriteLine(velocity);
            velocity = direction * 3.5f;
            
        }
        void Update()
        {

            //Planet pl = (Planet)parent;
            Gizmos.DrawArrow(x,y,velocity.x * 10,velocity.y * 10);
            pos += velocity;
            x = pos.x;
            y = pos.y;
        }
        public void ApplyThrust(Vec2 acc)
        {
            acceleration = acc;
            velocity += acc;
        }
        public float Mass() => mass;
    }
}
