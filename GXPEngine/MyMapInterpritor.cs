using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledMapParser;

namespace GXPEngine
{
    internal class MyMapInterpritor
    {
        static string root = "Assets/Levels/";
        public static ObjectGroup[] GetLevel(string fileName)
        {
            Map map = MapParser.ReadMap(root + fileName);
            ObjectGroup[] objects = map.ObjectGroups;
            return objects;
        }

        public static GameObject[] GetGameObjects(string fileName)
        {

            List<GameObject> objects = new List<GameObject>();
            foreach (ObjectGroup objGroup in GetLevel(fileName))
            {
                foreach(TiledObject obj in objGroup.Objects)
                {
                    objects.Add(InitializeObject(obj));
                }
            }
            return objects.ToArray();
        }

        static GameObject InitializeObject(TiledObject obj)
        {
            switch (obj.Type)
            {
                case "Planet":
                    return (new Planet(new Vec2(obj.X + obj.Width/2,obj.Y + obj.Height/2)
                        ,obj.GetIntProperty("GravityRadius"),
                        (int)(obj.Width/2), obj.GetIntProperty("mass") * 1000,
                        "Assets/" + obj.Name));
                    break;
            }
            return null;
        }
    }
}
