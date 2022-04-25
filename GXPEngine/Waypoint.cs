using System;
namespace GXPEngine
{
    public class Waypoint : Sprite
    {
        public Waypoint(float x, float y, float rotation) : base("square.png")
        {
            this.alpha = 0f;
            SetOrigin(width * 0.5f, height * 0.5f);
            SetXY(x, y);
            Turn(rotation);
        }
    }
}
