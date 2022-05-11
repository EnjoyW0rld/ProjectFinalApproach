using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    internal class Satelite : Sprite
    {
        SpaceBody parentPlanet;
        public Vec2 pos;
        Vec2 oldPos;
        Vec2 velocity;
        float mass = 3f;
        bool HasCollided;
        float degree;
        BallCollider _coll;

        public Satelite(SpaceBody p, float speed) : base("Assets/metheor_2.png")
        {
            degree = speed;
            SetOrigin(width/2, height/2);
            scale = 0.2f;
            pos = p.GetCollider().pos;
            pos += new Vec2(p.GetGravityRadius(),0);
            x = pos.x;
            y = pos.y;
            parentPlanet = p;
            _coll = new BallCollider(pos, width / 2);
            AddChild(_coll);
            /*
            pos = new Vec2(p.pos.x, p.pos.y - p.GetGravityRadius() + height/2);
            //parentPlanet = p;
            Vec2 direction = ((p.pos - pos).Normalized());
            direction.RotateDegrees(-90);
            velocity = direction * 3.5f;
            */    
        }
        
        void Update()
        {
            if (!HasCollided)
            {
                //pos.RotateDegrees(degree);
                pos.RotateAroundDegrees(degree, parentPlanet.pos);
            }
            else
            {
                CheckPlanetCollisions();
            pos += velocity;
            }
            if(Input.GetKeyDown(Key.G)) HasCollided = true;

            x = pos.x;
            y = pos.y;
        }

        public void CheckPlanetCollisions()
        {
            Scene sc = (Scene)parent;
            BallCollider[] colliders = sc.GetBallColliders();
            CollisionInfo coll = null;
            float earliestTOI = 10;

            foreach (BallCollider otherCollider in colliders)
            {
                if(pos.DistanceTo(otherCollider.pos) < otherCollider.radius + _coll.radius)
                {
                if (otherCollider.GetParent() is Planet)
                    LateDestroy();
                if(otherCollider.GetParent() is Satelite &&
                    otherCollider.GetParent() != this)
                    {
                        Vec2 normal = (_coll.pos - otherCollider.pos).Normalized();
                        velocity.Reflect(normal, 0.8f);
                    }

                }

                /*float TOI = CollisionTOI(otherCollider);
                if (pos.DistanceTo(otherCollider.pos) <= _coll.radius + otherCollider.radius)
                {
                    if (earliestTOI > TOI)
                    {
                        //coll = new CollisionInfo(otherCollider.pos - ballCollider.pos, TOI,otherCollider.radius);
                        coll = new CollisionInfo(_coll.pos - otherCollider.pos, TOI, otherCollider);
                        earliestTOI = TOI;
                    }
                }*/
            }

        }
        public void ApplyThrust(Vec2 acc)
        {
            //acceleration = acc;
            if(HasCollided)
            velocity += acc;
        }
        public float Mass() => mass;
        public BallCollider GetBallCollider() => _coll;
        public void Collided()
        {
            HasCollided = true;
        }

       
    }
}
