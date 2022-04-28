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
        public Satelite(Planet p) : base("square.png")
        {
            pos = new Vec2(p.pos.x, p.pos.y - p.GetGravityRadius() + height/2);
            x = pos.x;
            y = pos.y;
            parentPlanet = p;
            Vec2 direction = ((p.pos - pos).Normalized());
            direction.RotateDegrees(-90);
            velocity = direction * 3.5f;
            
        }
        void Update()
        {

            //Planet pl = (Planet)parent;
            pos += velocity;
            x = pos.x;
            y = pos.y;
        }
        public void ApplyThrust(Vec2 acc)
        {
            //acceleration = acc;
            velocity += acc;
        }
        public float Mass() => mass;
    }
}
