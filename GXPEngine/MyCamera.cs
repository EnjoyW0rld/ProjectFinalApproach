﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    internal class MyCamera : Camera
    {
        GameObject toFollow;
        float desiredX;
        public MyCamera(GameObject obj) : base(0,0,1920,1080)
        {
            y = 540;
            toFollow = obj;
        }
        void Update()
        {
            desiredX = toFollow.x;
            x = desiredX * 0.15f + x *0.85f;
            x = Mathf.Clamp(x, 860,x);
            if (Input.GetMouseButtonDown(0)) Console.WriteLine($"X:{Input.mouseX} Y:{Input.mouseY}");
        }
    }
}
