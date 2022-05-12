using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GXPEngine
{
    internal class PlayerInfo
    {
        static string[] defaultText = { "fuel:100", "MaxSpeed:5", "AcceleraionSpeed:0.5","Mass:1000","PlayerHealth:3"};
        public static int fuelCount;
        public static float MaxSpeed;
        public static float Acceleraion;
        public static int mass;

        public static int MaxHealth;
        public static int currentFuelCount;
        public static int currentHealth;

        public static void LoadPlayerInfo()
        {
            if (!File.Exists("Assets/PlayerInfo.txt"))
            {
                File.WriteAllLines("Assets/PlayerInfo.txt", defaultText);
            }
            string[] allLines = File.ReadAllLines("Assets/PlayerInfo.txt");
            foreach (string str in allLines)
            {
                string[] dividedStr = str.Split(':');
                switch (dividedStr[0])
                {
                    case "fuel":
                        int.TryParse(dividedStr[1], out fuelCount);
                        break;
                    case "MaxSpeed":
                        float.TryParse(dividedStr[1], out MaxSpeed);
                        break;
                    case "AcceleraionSpeed":
                        float.TryParse(dividedStr[1], out Acceleraion);
                        break;
                    case "Mass":
                        int.TryParse(dividedStr[1],out mass);
                        break;
                    case "PlayerHealth":
                        int.TryParse(dividedStr[1], out MaxHealth);
                        break;
                }
            }
            currentFuelCount = fuelCount;
            currentHealth = MaxHealth;

        }
    }
}
