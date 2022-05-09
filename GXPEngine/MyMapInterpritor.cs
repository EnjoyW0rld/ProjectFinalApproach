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
        static Dictionary<string, Map> maps = new Dictionary<string, Map>();
        static string root = "";
        public static ObjectGroup[] GetLevel(string fileName)
        {
            Map map = getMap(fileName);

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
                        "Assets/" + obj.Name,"Assets/blueSphere.png",GetPlanetState(obj.GetStringProperty("PlanetRole"))));
                    break;
            }
            return null;
        }
        static Planet.PlanetState GetPlanetState(string str)
        {
            switch (str)
            {
                case "Start":
                    return Planet.PlanetState.Start;
                case "End":
                    return Planet.PlanetState.End;
                default:
                    return Planet.PlanetState.Regular;
            }
        }
        static Map getMap(string path)
        {
            if(maps.Count > 0 && maps.ContainsKey(path))
            {
                return maps[path];
            }
            else
            {
                Map map =  MapParser.ReadMap(root + path);
                maps.Add(path, map);
                return map;
            }
        }
        public static int GetLevelNumber(string path)
        {
            Map map = getMap(path);
            return map.GetIntProperty("LevelNumber");
        }

    }
}
