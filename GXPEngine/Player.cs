using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    public class Player : GameObject
    {
        public Vec2 pos { get; private set; }
        Vec2 oldPos;
        //public Vec2 velocity { get; private set; }
        Vec2 velocity;
        Vec2 acceleration;
        const float SPEED = 0.5f;
        const float mass = 1000f; 
        Sprite body;
        Thruster thruster;
        public readonly BallCollider ballCollider;


        public Player()
        {
            pos = new Vec2(200,200);
            body = new Sprite("circle.png");
            body.SetOrigin(body.width/2,body.height/2);
            AddChild(body);
            thruster = new Thruster();
            AddChild(thruster);
            ballCollider = new BallCollider(pos, body.width / 2);
            AddChild(ballCollider);
        }
        void Update()
        {
            oldPos = pos;
            HandleControls();
            pos += velocity;
            //velocity *= 0.9f;
            UpdatePosition();
            CollisionInfo colInfo = FindEarliestCollsion();
            if(colInfo != null)
            {
                ResolveCollision(colInfo);
            }
            if(velocity.Length() > 5)
            {
                velocity = velocity.Normalized() * 5;
            }
        }
        void UpdatePosition()
        {
            x = pos.x;
            y = pos.y;
        }
        void HandleControls()
        {
            if(Input.GetKey(Key.R)) pos = new Vec2(100,100);
            if(Input.GetKey(Key.T)) velocity = new Vec2();
            acceleration = new Vec2();
            //if (Input.GetKey(Key.A)) acceleration.x = -1;
            //if (Input.GetKey(Key.D)) acceleration.x = 1;
            //if (Input.GetKey(Key.W)) acceleration.y = -1;
            //if(Input.GetKey(Key.S)) acceleration.y = 1;
            if(Input.GetKey(Key.S)) acceleration = Vec2.GetUnitVectorDeg(thruster.rotation + 90);
            if(Input.GetKey(Key.W)) acceleration = Vec2.GetUnitVectorDeg(thruster.rotation - 90);

            velocity += acceleration.Normalized() * SPEED;
             
            if (Input.GetKey(Key.G)) rotation++;
        }
        public void AddVelocity(Vec2 vel)
        {
            velocity += vel;
        }
        public float Mass() => mass;

        CollisionInfo FindEarliestCollsion()
        {
            Scene sc = (Scene)parent;
            BallCollider[] colliders = sc.GetBallColliders();
            CollisionInfo coll = null;
            float earliestTOI = 10;

            foreach(BallCollider otherCollider in colliders)
            {
                    float TOI = CollisionTOI(otherCollider);
                if(pos.DistanceTo(otherCollider.pos) <= ballCollider.radius + otherCollider.radius)
                {
                    if (earliestTOI > TOI)
                    {
                        //coll = new CollisionInfo(otherCollider.pos - ballCollider.pos, TOI,otherCollider.radius);
                        coll = new CollisionInfo(ballCollider.pos - otherCollider.pos, TOI, otherCollider);
                        earliestTOI = TOI;
                    }
                }
            }

            return coll;
        }
        float CollisionTOI(BallCollider othColl)
        {
            Vec2 u = oldPos - othColl.pos;
            float a = Mathf.Pow(velocity.Length(), 2);
            float b = u.Dot(velocity) * 2;
            float c = Mathf.Pow(u.Length(), 2) - Mathf.Pow(othColl.radius + ballCollider.radius, 2);
            float D = Mathf.Pow(b, 2) - (4 * a * c);
            if(c < 0)
            {
                if (b < 0) return 0;
                else return 10;
            }
            if (a < 0.01f) return 10;
            if(D > 0)
            {
                float TOI = (-b - Mathf.Sqrt(D)) / (2 * a);
                
                if(TOI < 1 && TOI >= 0) return TOI;
            }
            return 10;
        }
        void ResolveCollision(CollisionInfo colInfo)
        {
            if(colInfo.collisionObject != null)
            {
                var ColParent = colInfo.collisionObject.GetParent();
                if (ColParent is Portal)
                {
                    Portal portal = (Portal)ColParent;
                    TeleportUsingPortal(portal);
                    return;
                }
            }
            //pos = oldPos + velocity * colInfo.TOI;
            velocity.Reflect(colInfo.normal.Normalized(),0.8f);
        }
        void TeleportUsingPortal(Portal port)
        {
            Vec2 propDirection = Vec2.GetUnitVectorDeg(port.facingDirection);
            velocity = propDirection * velocity.Length();
            Scene sc = (Scene)parent;
            SpaceBody[] bodies = sc.GetSpaceBodies();
            foreach(SpaceBody b in bodies)
            {
                if(b is Portal && b != port)
                {
                    Portal othP = (Portal)b;
                    if(othP.gateNumber == port.gateNumber)
                    {
                        pos = othP.pos + propDirection * (othP.ballCollider.radius + ballCollider.radius);
                    }
                }
            }
        }
    }
}