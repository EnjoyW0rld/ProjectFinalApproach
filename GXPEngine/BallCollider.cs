using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    public class BallCollider : GameObject
    {
        public Vec2 pos;
        public readonly float radius;

        Sprite debugC;
        public BallCollider(Vec2 p, float r)
        {
            debugC = new Sprite("circle.png");
            debugC.SetOrigin(debugC.width/2,debugC.height/2);
            debugC.height = (int)(r * 2);
            debugC.width = (int)(r * 2);
            debugC.alpha = 0.5f;
            //AddChild(debugC);
            pos = p;
            radius = r;

        }
        void Update()
        {
            GameObject obj = (GameObject)parent;
            pos.x = obj.x;
            pos.y = obj.y;
        }
        public GameObject GetParent()
        {
            try
            {
                return this.parent as GameObject;
            }
            catch (Exception e)
            {

                Console.WriteLine(e);
            }
            return null;
        }

    }
}
