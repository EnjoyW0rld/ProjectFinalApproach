using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    internal class MainMenu : Scene
    {
        public int changeToLevel { get; private set; }
        public MainMenu() : base(11)
        {
            AddChild(new Sprite("Assets/Menu/Main_menu.png"));
            AddChild(new Button(1, new Vec2(1060, 508)));
            AddChild(new Button(2, new Vec2(1060, 658)));
            AddChild(new Button(3, new Vec2(1060, 808)));
        }
        void Update()
        {
            if (Input.GetMouseButton(0)) Console.WriteLine(Input.mouseX + "  " + Input.mouseY);
            Console.WriteLine(changeToLevel);
        }
        public override void UpdateNextSceneNumber(int i)
        {
            changeToLevel = i;
        }
    }
    class Button : AnimationSprite
    {
        int row;
        public Button(int r, Vec2 p) : base("Assets/Menu/mainMenuButtonsSmall.png",3,3)
        {
            x = p.x;
            y = p.y;
            row = r - 1;
        }
        void Update()
        {
            
            SetFrame(row * 3);

            if (HitTestPoint(Input.mouseX, Input.mouseY))
            {
                SetFrame(row * 3 + 1);
                if (Input.GetMouseButton(0))
                {
                    switch (row)
                    {
                        case 0:
                            SetFrame(row * 3 + 2);
                            EventsHandler.LevelChange?.Invoke(DeathScreen.changeToLevel);
                            break;
                        case 1:
                            SetFrame(row * 3 + 2);
                            break;
                        case 2:
                            SetFrame(row * 3 + 2);
                            Environment.Exit(0);
                            break;
                    }
                }
            }
        }
        
    }
}
