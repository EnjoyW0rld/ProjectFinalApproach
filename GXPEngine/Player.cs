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
        Sprite body;
        public Player()
        {
            AddChild(body = new Sprite("circle.png"));
        }
        void Update()
        {
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
            acceleration = new Vec2();
            if (Input.GetKey(Key.A)) acceleration.x = -1;
            if (Input.GetKey(Key.D)) acceleration.x = 1;
            if (Input.GetKey(Key.W)) acceleration.y = -1;
            if(Input.GetKey(Key.S)) acceleration.y = 1;
            velocity += acceleration.Normalized() * SPEED;

            if (Input.GetKey(Key.G)) rotation++;
        }
        public void AddVelocity(Vec2 vel)
        {
            velocity += vel;
        }
    }
}
