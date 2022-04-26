using System;									// System contains a lot of default C# libraries 
using GXPEngine;                                // GXPEngine contains the engine
using System.Drawing;							// System.Drawing contains drawing tools such as Color definitions

public class MyGame : Game
{

	Player player;
	public MyGame() : base(800, 600, false,false)		// Create a window that's 800x600 and NOT fullscreen
	{
		targetFps = 60;
		AddChild(player = new Player());
		AddChild(new Planet(new Vec2(400,500),300));
	}

	// For every game object, Update is called every frame, by the engine:
	void Update()
	{
		// Empty
	}

	static void Main()							// Main() is the first method that's called when the program is run
	{
		new MyGame().Start();					// Create a "MyGame" and start it
	}
	public Player GetPlayer()
    {
		return player;
    }
}