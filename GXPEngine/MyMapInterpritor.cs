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

        public static GameObject[] GetGameObjects(ObjectGroup[] objGroups)
        {
            foreach (ObjectGroup objGroup in objGroups)
            {
                foreach(TiledObject obj in objGroup.Objects)
                {
                    
                }
            }
            return null;
        }

        static void InitializeObject(TiledObject obj)
        {
            //switch()
        }
    }
}
