using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    internal class SpaceBody : GameObject
    {
        public Vec2 pos { get; private set; }
        protected float gravityRadius;
        protected float mass;
        public BallCollider ballCollider { get; private set; }
        
        Sprite body;

        public SpaceBody(Vec2 ppos, float gRad,int planetR, float m, string path)
        {
            pos = ppos;
            gravityRadius = gRad;
            mass = m * 1000;

            SetXY(pos.x, pos.y);
            body = new Sprite(path, false);
            body.SetOrigin(body.width/2, body.height/2);
            body.height = (planetR * 2);
            body.width = (planetR * 2);
            AddChild(body);
            
        }
        internal virtual void CreateOreol(string path)
        {
            Sprite oreol = new Sprite(path, false);
            oreol.SetOrigin(oreol.width / 2, oreol.height / 2);
            oreol.width = (int)gravityRadius * 2;
            oreol.height = (int)gravityRadius * 2;
            oreol.alpha = 0.2f;
            AddChild(oreol);
        }
        public virtual float GetGravityRadius() => gravityRadius;
        public virtual BallCollider GetCollider() => ballCollider;

        internal virtual void DragShip(Player player, float distance)
        {
            Vec2 direction = (pos - player.pos).Normalized();
            float force = GravityForce(player.Mass(), mass, distance);
            player.AddVelocity(direction * force);
        }
        public static float GravityForce(float m1, float m2, float distance)
        {
            return 6.672f * Mathf.Pow(10, -7) * (m1 * m2) / Mathf.Pow(distance, 2);
        }

    }
}
