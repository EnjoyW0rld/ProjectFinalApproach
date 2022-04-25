using System;
namespace GXPEngine
{
    public class jungleBackground : Sprite
    {
        public jungleBackground() : base("Green.png")
        {
            y = 300;
            x = 300;
            this.collider.isTrigger = true;
        }
    }
}
