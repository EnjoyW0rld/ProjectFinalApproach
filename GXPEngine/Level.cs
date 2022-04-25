using System;
using System.Collections.Generic;
using TiledMapParser;
using GXPEngine;

public class Level : GameObject
{
    
    Player player;
    public int currentLevel = 1;
    public int score;
    public static bool restart = false;
    const int SIZE = 64;
    const int WIDTH = 15;
    const int HEIGHT = 15;

    Enemy[] enemies = new Enemy[5];
    int[,] level = new int[HEIGHT, WIDTH]
    {
            { 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4},
            { 4, 1, 1, 4, 1, 1, 1, 1, 1, 1, 1, 4, 1, 1, 4},
            { 4, 1, 1, 4, 1, 1, 1, 1, 1, 1, 1, 4, 1, 1, 4},
            { 4, 1, 1, 4, 1, 1, 1, 5, 1, 1, 1, 4, 1, 1, 4},
            { 4, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 4},
            { 4, 5, 1, 5, 1, 1, 1, 1, 1, 1, 1, 5, 1, 5, 4},
            { 4, 1, 1, 1, 1, 5, 1, 1, 1, 5, 1, 1, 1, 1, 4},
            { 4, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 4},
            { 4, 1, 1, 1, 1, 1, 1, 5, 1, 1, 1, 1, 1, 1, 4},
            { 4, 1, 1, 1, 1, 5, 1, 1, 1, 5, 1, 1, 1, 1, 4},
            { 4, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 4},
            { 4, 1, 1, 5, 1, 1, 1, 1, 1, 1, 1, 5, 1, 1, 4},
            { 4, 1, 1, 4, 1, 1, 1, 1, 1, 1, 1, 4, 1, 1, 4},
            { 4, 1, 1, 4, 1, 1, 1, 5, 1, 1, 1, 4, 1, 1, 4},
            { 4, 5, 3, 5, 3, 5, 3, 3, 3, 5, 3, 5, 3, 5, 4}
    };
    


    public Level()
    {
        
        setupLevel(currentLevel);
        if (player == null)
        {
            Player player = new Player(450, 400);
            
            AddChild(player);
            HUD hud;
            hud = new HUD(player);
            hud.collider.isTrigger = true;
            AddChild(hud);
        }
    }

    void setupLevel(int currentLevel)
    {
        for (int row = 0; row < HEIGHT; row++)
        {
            for (int col = 0; col < WIDTH; col++)
            {
                int tile = level[row, col];

                switch (tile)
                {
                    case 1:
                        Background background = new Background();
                        AddChild(background);
                        background.x = col * SIZE;
                        background.y = row * SIZE;
                        break;
                    case 3:
                        Lava lava = new Lava();
                        AddChild(lava);
                        lava.x = col * SIZE;
                        lava.y = row * SIZE;
                        break;
                    case 4:
                        Wall wall = new Wall();
                        AddChild(wall);
                        wall.x = col * SIZE;
                        wall.y = row * SIZE;
                        break;
                    case 5:
                        Ground ground = new Ground();
                        AddChild(ground);
                        ground.x = col * SIZE;
                        ground.y = row * SIZE;
                        break;
                }
            }
        }

        
        //================================
        enemies[0]= new Enemy(100, 300);
        AddChild(enemies[0]);

        Waypoint waypoint1 = new Waypoint(100, 300, 0);
        waypoint1.collider.isTrigger = true;
        AddChild(waypoint1);

        Waypoint waypoint2 = new Waypoint(400, 300, 180);
        waypoint2.collider.isTrigger = true;
        AddChild(waypoint2);
        //================================
        enemies[1] = new Enemy(500, 300);
        AddChild(enemies[1]);

        Waypoint waypoint3 = new Waypoint(500, 300, 0);
        waypoint3.collider.isTrigger = true;
        AddChild(waypoint3);

        Waypoint waypoint4 = new Waypoint(750, 300, 180);
        waypoint4.collider.isTrigger = true;
        AddChild(waypoint4);
        //================================
        enemies[2] = new Enemy(150, 100);
        AddChild(enemies[2]);

        Waypoint waypoint5 = new Waypoint(150, 100, 90);
        waypoint5.collider.isTrigger = true;
        AddChild(waypoint5);

        Waypoint waypoint6 = new Waypoint(150, 800, 270);
        waypoint6.collider.isTrigger = true;
        AddChild(waypoint6);
        //================================
        enemies[3] = new Enemy(650, 100);
        AddChild(enemies[3]);

        Waypoint waypoint7 = new Waypoint(650, 100, 90);
        waypoint7.collider.isTrigger = true;
        AddChild(waypoint7);

        Waypoint waypoint8 = new Waypoint(650, 800, 270);
        waypoint8.collider.isTrigger = true;
        AddChild(waypoint8);
        //================================
        enemies[4] = new Enemy(50, 700);
        AddChild(enemies[4]);

        Waypoint waypoint9 = new Waypoint(50, 700, 0);
        waypoint9.collider.isTrigger = true;
        AddChild(waypoint9);

        Waypoint waypoint10 = new Waypoint(700, 700, 180);
        waypoint10.collider.isTrigger = true;
        AddChild(waypoint10);
        //================================
        Collectible crystal1 = new Collectible();
        crystal1.spawn();
        AddChild(crystal1);

    }

    void Update()
    {
            if(player != null)
            {
                x = game.width / (2 * game.scaleX) - player.x;
                y = game.height / (2 * game.scaleY) - player.y;
                score = player.GetScore();
            }
            if(restart)
            {
                for(int i = 0; i<5; i++)
                {
                    enemies[i].visible = true;
                }
                restart = false;
            }
        
    }
}