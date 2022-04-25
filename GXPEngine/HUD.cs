using System;
using System.Drawing;

namespace GXPEngine
{
    public class HUD : Canvas
    {
        private Player _player;
        //private Menu menu;
        Font scoreFont = new Font(SystemFonts.MenuFont.FontFamily, 170, FontStyle.Bold);
        Font bulletFont = new Font(SystemFonts.MenuFont.FontFamily, 30, FontStyle.Bold);
        Font timerFont = new Font(SystemFonts.MenuFont.FontFamily, 20, FontStyle.Bold);

        public int time = 60;
        public int seconds;
        public static int secondsLeft = 60;
        public static int currentTime = 60;
        public HUD(Player player) : base(1000,800)
        {
            _player = player;
        }

        void Update()
        {
            graphics.Clear(Color.Empty);
            graphics.DrawString(""+_player.GetScore(), scoreFont, Brushes.Orange, 420, 230);
            graphics.DrawString("Bullets: " + _player.GetBullets(), bulletFont, Brushes.DarkOrange, 400, 460);
            graphics.DrawString("Time left: " + currentTime, timerFont, Brushes.DarkOrange, 410, 590);

            LateAddChild(_player);
            time += Time.deltaTime * 10;
            seconds = time / 10000;
            currentTime = secondsLeft - seconds;
        }
    }
}
