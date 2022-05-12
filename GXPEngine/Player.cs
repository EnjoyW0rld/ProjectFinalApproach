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
        float SPEED;
        int mass;
        float maxSpeed;
        int health;
        int damageCooldown;


        Sprite body;
        Sprite shield;
        public readonly BallCollider ballCollider;

        public int fuelAmount { get; private set; }
        bool ReachedEnd;

        public Player()
        {
            shield = new Sprite("Assets/shield.png");
            shield.scale = 0.3f;
            shield.y = -60;
            mass = PlayerInfo.mass;
            SPEED = PlayerInfo.Acceleraion;
            maxSpeed = PlayerInfo.MaxSpeed;
            fuelAmount = PlayerInfo.fuelCount;
            health = PlayerInfo.MaxHealth;
            //pos = new Vec2(200,200);
            body = new Sprite("Assets/newShip.png");
            body.SetOrigin(body.width/2,body.height/2);
            body.scale = 0.2f;
            AddChild(body);
            //thruster = new Thruster();
            //AddChild(thruster);
            ballCollider = new BallCollider(pos, body.width / 2 - 25);
            AddChild(ballCollider);
            //EventsHandler.EnteredEndPlanet += EaseToPlanet;
        } 
        public void SetStartPos(Vec2 posit)
        {
            pos = posit;
            UpdatePosition();
        }
        void Update()
        {

            oldPos = pos;
            pos += velocity;
            //velocity *= 0.9f;
            if(fuelAmount > 0)
            {
                    HandleControls();
            
            
            }
            
            UpdatePosition();

            CollisionInfo colInfo = FindEarliestCollsion();

            if (Input.GetKeyDown(Key.U)) EventsHandler.LevelChange(10);

            if(colInfo != null)
            {
                ResolveCollision(colInfo);
            }

            if(velocity.Length() > maxSpeed)
            {
                velocity = velocity.Normalized() * maxSpeed;
            }

            if(((pos.x > 1960 || pos.x < -40) || (pos.y > 1120 || pos.y < -40)) && !((Scene)parent).isFinished)
            {
                EventsHandler.LevelChange?.Invoke(9);                
            }
            if(health <= 0) EventsHandler.LevelChange?.Invoke(9);
            PlayerInfo.currentFuelCount = fuelAmount;
            PlayerInfo.currentHealth = health; 

            if(damageCooldown < Time.time && HasChild(shield))
            {
                RemoveChild(shield);
            }
        }
        void UpdatePosition()
        {
            x = pos.x;
            y = pos.y;
        }

        void HandleControls()
        {
            //Debug options, remove later
            //if(Input.GetKey(Key.R)) pos = new Vec2(100,100);
            if(Input.GetKey(Key.T)) velocity = new Vec2();

            acceleration = new Vec2();
            if (Input.GetKey(Key.A)) rotation -= 2;
            if(Input.GetKey(Key.D)) rotation += 2;
            //if(Input.GetKey(Key.S)) acceleration = Vec2.GetUnitVectorDeg(rotation);
            if(Input.GetKey(Key.W)) acceleration = Vec2.GetUnitVectorDeg(rotation);

            if (acceleration.Length() > 0)
            {
                fuelAmount--;
            }

            velocity += acceleration.Normalized() * SPEED;
             
        }
        void ShipShoot()
        {
            if (Input.GetKey(Key.A)) rotation -= 2;
            if (Input.GetKey(Key.D)) rotation += 2;
            
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
                if (ColParent is Planet)
                {
                    Planet planet = (Planet)ColParent;
                    if(planet.st == Planet.PlanetState.End)
                    {
                        Scene sc = parent as Scene;
                        EventsHandler.LevelChange?.Invoke(sc.sceneNumber);
                        EventsHandler.ChangeEmotion?.Invoke(0);

                        return;
                    }
                    EventsHandler.ShakeScreen?.Invoke();
                    EventsHandler.ChangeEmotion?.Invoke(2);
                    if(damageCooldown < Time.time)
                    {
                        health--;
                        damageCooldown = Time.time + 2000;
                        AddChild(shield);
                        SoundManager.Instance().PlaySound(4);
                    }
                }
                if(ColParent is Satelite)
                {
                    Satelite collSatelite = (Satelite)ColParent;
                    collSatelite.Collided();
                    collSatelite.ApplyThrust(velocity);
                    EventsHandler.ShakeScreen?.Invoke();
                    EventsHandler.ChangeEmotion?.Invoke(2);
                    if (damageCooldown < Time.time)
                    {
                        health--;
                        SoundManager.Instance().PlaySound(4);
                        damageCooldown = Time.time + 2000;
                        AddChild(shield);
                    }
                }
            }
            //pos = oldPos + velocity * colInfo.TOI;
            velocity.Reflect(colInfo.normal.Normalized(),0.8f);
        }
        void TeleportUsingPortal(Portal port)
        {
            Scene sc = (Scene)parent;
            SpaceBody[] bodies = sc.GetSpaceBodies();
            foreach(SpaceBody b in bodies)
            {
                if(b is Portal && b != port)
                {
                    Portal othP = (Portal)b;
                    if(othP.gateNumber == port.gateNumber)
                    {
                        Vec2 propDirection = Vec2.GetUnitVectorDeg(othP.facingDirection);
                        pos = othP.pos + propDirection * (othP.ballCollider.radius + ballCollider.radius);
                        velocity = propDirection * velocity.Length();
                        SoundManager.Instance().PlaySound(1);
                    }
                }
            }
        }

        //Use only in Tween!!
        public void ChangePos(float px = -1,float py = -1)
        {
            if (px != -1) pos = new Vec2(px, pos.y);
            if (py != -1)
            {
                pos = new Vec2(pos.x, py);

            }
            UpdatePosition();
        }

        void EaseToPlanet(BallCollider ballC)
        {
            Vec2 offset = ((pos - ballC.pos).Normalized() * (ballC.radius +ballCollider.radius + 50));
            Vec2 direction = ballC.pos + offset;
            ReachedEnd = true;
            AddChild(new Tween(Tween.Parameter.x, 1, direction.x,Tween.Function.easeInQuad));
            AddChild(new Tween(Tween.Parameter.y, 1, direction.y,Tween.Function.easeInQuad));
            EventsHandler.EnteredEndPlanet -= EaseToPlanet;
            
        }
    }
}