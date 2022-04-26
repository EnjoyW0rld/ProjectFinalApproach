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
        Vec2 velocity;
        Vec2 acceleration;
        const float SPEED = 0.5f;
        const float mass = 1000f; 
        Sprite body;
        Thruster thruster;
        public Player()
        {
            pos = new Vec2(200,200);
            body = new Sprite("circle.png");
            body.SetOrigin(body.width/2,body.height/2);
            AddChild(body);
            thruster = new Thruster();
            AddChild(thruster);
        }
        public Vec2 oldPos;
        void Update()
        {
            oldPos = pos;
            HandleControls();
            pos += velocity;
            //velocity *= 0.9f;
            UpdatePosition();
            
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
    }
}
