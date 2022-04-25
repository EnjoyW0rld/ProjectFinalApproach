using System;
namespace GXPEngine
{
    public class Enemy : Sprite
    {
        const float speed = 1.5f;
        AnimationSprite bat;

        public Enemy(float x, float y) : base("colors.png")
        {
            SetOrigin(width / 2, height / 2);
            SetXY(x, y);
            this.alpha = 0f;
            bat = new AnimationSprite("Bat.png", 7, 1);
            bat.SetOrigin(bat.width / 2, 5);
            bat.SetXY(0, -15);
            bat.SetScaleXY(1.8f, 1.8f);
            bat.collider.isTrigger = true;
            this.collider.isTrigger = true;
            AddChild(bat);
        }

        void Update()
        {
            Move(speed, 0);
            bat.SetCycle(1, 7);
            bat.Animate(0.25f);
        }

        void OnCollision(GameObject other)
        {
            if (other is Waypoint)
            {
                if (this.DistanceTo(other) <= speed)
                {
                    rotation = other.rotation;
                    bat.Mirror(true, false);
                    
                    if(rotation == 180)
                    {
                        bat.Mirror(true, true);
                    }
                    if (rotation == 90)
                    {
                        bat.rotation = 270;
                    }
                    if (rotation == 270)
                    {
                        bat.rotation = 90;
                    }
                }
            }
        }
    }
}
