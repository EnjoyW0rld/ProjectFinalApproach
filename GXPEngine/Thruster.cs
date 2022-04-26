using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    internal class Thruster : Sprite
    {
        float rotationSpeed = 2;
        public Thruster() : base("colors.png")
        {
            SetOrigin(width / 2, 0);
            width = width / 2;

        }
        void Update()
        {
            if (Input.GetKey(Key.A)) rotation += rotationSpeed;
            if(Input.GetKey(Key.D)) rotation -= rotationSpeed;
        }
    }
}
