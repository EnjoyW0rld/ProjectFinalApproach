using System;									// System contains a lot of default C# libraries 
using GXPEngine;                                // GXPEngine contains the engine
using System.Drawing;                         // System.Drawing contains drawing tools such as Color definitions

public class MyGame : Game
{

	private Level _level;

	public MyGame() : base(800, 600, false)     // Create a window that's 800x600 and NOT fullscreen
	{
		/*EasyDraw canvas = new EasyDraw(800, 600);


		Sprite sprite = new Sprite("tutorial.png");
		sprite.SetXY(width / 2, height / 2);
		canvas.DrawSprite(sprite);
		AddChild(canvas);
		AddChild(sprite);*/
		
		Menu menu = new Menu();
		AddChild(menu);

	}


	void Update()
	{
		if(Input.GetKeyDown(Key.R))
        {
			//ResetLevel();
        }
	}

	static void Main()							// Main() is the first method that's called when the program is run
	{
		new MyGame().Start();                   // Create a "MyGame" and start i
	}
	void ResetLevel()
    {

		_level.Remove();
		_level = null;
        
		//_level = new Level();
		//LateAddChild(_level);
    }
}