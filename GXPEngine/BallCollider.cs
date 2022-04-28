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
        public BallCollider(Vec2 p, float r)
        {
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
