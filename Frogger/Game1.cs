using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Frogger
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        static int chunkScreen;
        const int MAX_PLAYER_SPEED = 30;
        const int MAX_ENEMY_SPEED = 15;
        static int offSet = 0;

        int lifes = 3;

        Random rand = new Random();

        Rectangle startScreenRec, gamePlayRec, helpScreenRec, lossScreenRec, winScreenRec;
        Texture2D startScreenPic, gamePlayPic, helpScreenPic, lossScreenPic, winScreenPic;

        Rectangle startButtonRec, helpButtonRec, backButtonRec, healthRec, healthBackground;
        Texture2D startButtonPic, helpButtonPic, backButtonPic, healthPic;

        Texture2D enemyPic1,enemyPic2, playerPicRight, playerPicLeft, currPlayer;
        Rectangle playerRec;

        KeyboardState keys;
        KeyboardState prevKeys;
        MouseState mouse;
        MouseState prevMouse;
        GamePadState pad1;
        GamePadState prevPad1;

        Color startButtonColor, helpbuttonColor, backButtonColor = Color.White;

        List<Enemy> enemies1 = new List<Enemy>();
        List<Enemy> enemies2 = new List<Enemy>();
        List<Enemy> enemies3 = new List<Enemy>();
        List<Enemy> enemies4 = new List<Enemy>();
        List<Enemy> enemies5 = new List<Enemy>();
        List<Enemy> enemies6 = new List<Enemy>();

        int randSpeed1, randSpeed2, randSpeed3, randSpeed4, randSpeed5, randSpeed6;



        ScreenState Screen = ScreenState.startScreen;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            IsMouseVisible = true;

            chunkScreen = GraphicsDevice.Viewport.Height / 9;
            offSet = -chunkScreen;

            startScreenRec = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            lossScreenRec = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height); ;
            winScreenRec = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            gamePlayRec = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            helpScreenRec = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

            startButtonRec = new Rectangle((GraphicsDevice.Viewport.Width / 2)-(475/2), GraphicsDevice.Viewport.Height / 2, 475, 123);
            helpButtonRec = new Rectangle((GraphicsDevice.Viewport.Width / 2) - (475 / 2), (GraphicsDevice.Viewport.Height / 2) + 123, 475, 123);
            backButtonRec = new Rectangle((GraphicsDevice.Viewport.Width / 2) - (475 / 2), GraphicsDevice.Viewport.Height / 2, 475, 123);

            playerRec = new Rectangle((GraphicsDevice.Viewport.Width / 2), (chunkScreen * 9) + offSet, 50, chunkScreen);

            healthRec = new Rectangle(GraphicsDevice.Viewport.Width - (chunkScreen * 3), (chunkScreen + offSet) + ((chunkScreen / 2) - (chunkScreen / 2 )/ 2), chunkScreen * 3, chunkScreen/2);
            healthBackground = new Rectangle(GraphicsDevice.Viewport.Width - (chunkScreen * 3), (chunkScreen + offSet) + ((chunkScreen / 2) - (chunkScreen / 2) / 2), chunkScreen * 3, chunkScreen / 2);

            changeSpeed();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            startScreenPic = Content.Load<Texture2D>("images/intro screen");
            gamePlayPic = Content.Load<Texture2D>("images/football field");
            lossScreenPic = Content.Load<Texture2D>("images/loss screen");
            winScreenPic = Content.Load<Texture2D>("images/win screen");
            helpScreenPic = Content.Load<Texture2D>("images/help screen"); 

            enemyPic1 = Content.Load<Texture2D>("images/enemys/enemy1");
            enemyPic2 = Content.Load<Texture2D>("images/enemys/enemy2");

            playerPicRight = Content.Load<Texture2D>("images/player/drum_major_right");
            playerPicLeft = Content.Load<Texture2D>("images/player/drum_major_left");
            currPlayer = playerPicRight;

            healthPic = Content.Load<Texture2D>("images/empty pixel");

            startButtonPic = Content.Load<Texture2D>("images/buttons/start/normal_start");
            backButtonPic = Content.Load<Texture2D>("images/buttons/back/normal_back");
            helpButtonPic = Content.Load<Texture2D>("images/buttons/help/normal_help");

            for (int numEnemeys = 0; numEnemeys < 3; numEnemeys++)
            {
                enemies1.Add(new Enemy(enemyPic1, enemyPic2, new Rectangle(rand.Next(GraphicsDevice.Viewport.Width),
                                                               (chunkScreen * 2) + offSet,
                                                               60, chunkScreen), true));

                enemies2.Add(new Enemy(enemyPic1, enemyPic2, new Rectangle(rand.Next(GraphicsDevice.Viewport.Width),
                                                               (chunkScreen * 3) + offSet,
                                                               60, chunkScreen), true));

                enemies3.Add(new Enemy(enemyPic1, enemyPic2, new Rectangle(rand.Next(GraphicsDevice.Viewport.Width),
                                                              (chunkScreen * 4) + offSet,
                                                              60, chunkScreen), true));

                enemies4.Add(new Enemy(enemyPic1, enemyPic2, new Rectangle(rand.Next(GraphicsDevice.Viewport.Width),
                                                              (chunkScreen * 6) + offSet,
                                                              60, chunkScreen), true));

                enemies5.Add(new Enemy(enemyPic1, enemyPic2, new Rectangle(rand.Next(GraphicsDevice.Viewport.Width),
                                                              (chunkScreen * 7) + offSet,
                                                              60, chunkScreen), true));

                enemies6.Add(new Enemy(enemyPic1, enemyPic2, new Rectangle(rand.Next(GraphicsDevice.Viewport.Width),
                                                              (chunkScreen * 8) + offSet,
                                                              60, chunkScreen), true));

            }

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            keys = Keyboard.GetState();
            mouse = Mouse.GetState();
            pad1 = GamePad.GetState(PlayerIndex.One);

            if (!(keys.IsKeyDown(Keys.Escape)) && (prevKeys.IsKeyDown(Keys.Escape)))
            {
                this.Exit();
            }

            switch (Screen)
            {
                case ScreenState.startScreen:
                    updateStartScreen();
                    break;
                case ScreenState.helpScreen:
                    updateHelpScreen();
                    break;
                case ScreenState.playingScreen:
                    updatePlayingScreen();
                    updatePlayer();
                    checkLoss();
                    checkWin();
                    break;
                case ScreenState.endingScreen:
                    updateEndingScreen();
                    break;
                case ScreenState.winningScreen:
                    updateEndingScreen();
                    break;
            }

            prevKeys = keys;
            prevMouse = mouse;
            prevPad1 = pad1;

            base.Update(gameTime);
        }

        /// <summary>
        /// Checks each time the loop has gone thorugh to see if the condition of winning has been meet
        /// To win the player rectangle must be in the first chunk of the screen
        /// It will then put the player back to home postion then reset the health bar and change the speed and postion of the enemy
        /// </summary>
        private void checkWin()
        {
            if(playerRec.Y == (chunkScreen + offSet))
            {
                playerRec.X = (GraphicsDevice.Viewport.Width / 2);
                playerRec.Y = (chunkScreen * 9) + offSet;
                healthRec.Width = chunkScreen * 3;

                changePos();
                changeSpeed();
                
                Screen = ScreenState.winningScreen;
            }
        }

        /// <summary>
        /// Checks each time the loops has gone thorugh to see if the condition of losing has been meet
        /// To lose, the life varible must reach 0
        /// it will then reset the health bar and change the speed and postion of the enemy
        /// </summary>
        private void checkLoss()
        {
            if(lifes == 0)
            {
                Screen = ScreenState.endingScreen;
                healthRec.Width = chunkScreen * 3;

                changePos();
                changeSpeed();
            }
        }

        /// <summary>
        /// Changes the position of the enemy on the x axis
        /// </summary>
        private void changePos()
        {
            foreach (Enemy e in enemies1)
            {
                e.setPos(rand.Next(GraphicsDevice.Viewport.Width));
            }

            foreach (Enemy e in enemies2)
            {
                e.setPos(rand.Next(GraphicsDevice.Viewport.Width));
            }

            foreach (Enemy e in enemies3)
            {
                e.setPos(rand.Next(GraphicsDevice.Viewport.Width));
            }

            foreach (Enemy e in enemies4)
            {
                e.setPos(rand.Next(GraphicsDevice.Viewport.Width));
            }

            foreach (Enemy e in enemies5)
            {
                e.setPos(rand.Next(GraphicsDevice.Viewport.Width));
            }

            foreach (Enemy e in enemies6)
            {
                e.setPos(rand.Next(GraphicsDevice.Viewport.Width));
            }
        }

        /// <summary>
        /// Changes the speed of the enemy to a random speed from -MAX_ENEMY_SPEED to MAX_ENEMY_SPEED
        /// </summary>
        private void changeSpeed()
        {
            randSpeed1 = rand.Next(-MAX_ENEMY_SPEED, MAX_ENEMY_SPEED);
            randSpeed2 = rand.Next(-MAX_ENEMY_SPEED, MAX_ENEMY_SPEED);
            randSpeed3 = rand.Next(-MAX_ENEMY_SPEED, MAX_ENEMY_SPEED);
            randSpeed4 = rand.Next(-MAX_ENEMY_SPEED, MAX_ENEMY_SPEED);
            randSpeed5 = rand.Next(-MAX_ENEMY_SPEED, MAX_ENEMY_SPEED);
            randSpeed6 = rand.Next(-MAX_ENEMY_SPEED, MAX_ENEMY_SPEED);

            while((randSpeed1 == 0 || randSpeed2 == 0 || randSpeed3 == 0 || randSpeed4 == 0 || randSpeed5 == 0 || randSpeed6 == 0))
            {
                randSpeed1 = rand.Next(-MAX_ENEMY_SPEED, MAX_ENEMY_SPEED);
                randSpeed2 = rand.Next(-MAX_ENEMY_SPEED, MAX_ENEMY_SPEED);
                randSpeed3 = rand.Next(-MAX_ENEMY_SPEED, MAX_ENEMY_SPEED);
                randSpeed4 = rand.Next(-MAX_ENEMY_SPEED, MAX_ENEMY_SPEED);
                randSpeed5 = rand.Next(-MAX_ENEMY_SPEED, MAX_ENEMY_SPEED);
                randSpeed6 = rand.Next(-MAX_ENEMY_SPEED, MAX_ENEMY_SPEED);
            }
        }

        /// <summary>
        /// Runs each time the loop is on the ending screen
        /// checks if player has clicked the back button and resets the life count back to 3 and goes to the starting screen
        /// will highlight the button if the player is hovering over it
        /// </summary>
        private void updateEndingScreen()
        {
            if (backButtonRec.Contains(new Point(mouse.X, mouse.Y)) && (!(mouse.LeftButton == ButtonState.Pressed) && prevMouse.LeftButton == ButtonState.Pressed))
            {
                backButtonColor = Color.Red;
                lifes = 3;
                Screen = ScreenState.startScreen;
            }
            else if (backButtonRec.Contains(new Point(mouse.X, mouse.Y)) && mouse.LeftButton == ButtonState.Pressed)
            {
                backButtonColor = Color.Red;
            }
            else if (backButtonRec.Contains(new Point(mouse.X, mouse.Y)))
            {
                backButtonColor = Color.Yellow;
            }
            else
            {
                backButtonColor = Color.White;
            }
        }

        /// <summary>
        /// Runs each time the loop is in the update screen
        /// checks if the player has pressed the back button and will move the rectangle to a new position when clicked
        /// will highlight the button if the player is hovering over it
        /// </summary>
        private void updateHelpScreen()
        {
            if (backButtonRec.Contains(new Point(mouse.X, mouse.Y)) && (!(mouse.LeftButton == ButtonState.Pressed) && prevMouse.LeftButton == ButtonState.Pressed))
            {
                backButtonColor = Color.Red;
                backButtonRec.X = (GraphicsDevice.Viewport.Width / 2) - (475 / 2);
                backButtonRec.Y = GraphicsDevice.Viewport.Height / 2;
                backButtonRec.Width = 475;
                backButtonRec.Height = 123;
                Screen = ScreenState.startScreen;
            }
            else if (backButtonRec.Contains(new Point(mouse.X, mouse.Y)) && mouse.LeftButton == ButtonState.Pressed)
            {
                backButtonColor = Color.Red;

            }
            else if (backButtonRec.Contains(new Point(mouse.X, mouse.Y)))
            {
                backButtonColor = Color.Yellow;
            }
            else
            {
                backButtonColor = Color.White;
            }
        }

        /// <summary>
        /// Updates each enemey in all 6 list to move in its respected direction
        /// it also checks it it has hit the border of the screen and if it collides with the player 
        /// </summary>
        private void updatePlayingScreen()
        {
            foreach (Enemy e in enemies1)
            {
                e.reset(GraphicsDevice.Viewport.Width);
                e.move(randSpeed1);
                if (e.collide(playerRec))
                {
                    playerRec.X = (GraphicsDevice.Viewport.Width / 2);
                    playerRec.Y = (chunkScreen * 9) + offSet;
                    healthRec.Width = healthRec.Width - chunkScreen;
                    lifes--;
                }
            }

            foreach (Enemy e in enemies2)
            {
                e.reset(GraphicsDevice.Viewport.Width);
                e.move(randSpeed2);
                if (e.collide(playerRec))
                {
                    playerRec.X = (GraphicsDevice.Viewport.Width / 2);
                    playerRec.Y = (chunkScreen * 9) + offSet;
                    healthRec.Width = healthRec.Width - chunkScreen;
                    lifes--;
                }
            }

            foreach (Enemy e in enemies3)
            {
                e.reset(GraphicsDevice.Viewport.Width);
                e.move(randSpeed3);
                if (e.collide(playerRec))
                {
                    playerRec.X = (GraphicsDevice.Viewport.Width / 2);
                    playerRec.Y = (chunkScreen * 9) + offSet;
                    healthRec.Width = healthRec.Width - chunkScreen;
                    lifes--;
                }
            }

            foreach (Enemy e in enemies4)
            {
                e.reset(GraphicsDevice.Viewport.Width);
                e.move(randSpeed4);
                if (e.collide(playerRec))
                {
                    playerRec.X = (GraphicsDevice.Viewport.Width / 2);
                    playerRec.Y = (chunkScreen * 9) + offSet;
                    healthRec.Width = healthRec.Width - chunkScreen;
                    lifes--;
                }
            }

            foreach (Enemy e in enemies5)
            {
                e.reset(GraphicsDevice.Viewport.Width);
                e.move(randSpeed5);
                if (e.collide(playerRec))
                {
                    playerRec.X = (GraphicsDevice.Viewport.Width / 2);
                    playerRec.Y = (chunkScreen * 9) + offSet;
                    healthRec.Width = healthRec.Width - chunkScreen;
                    lifes--;
                }
            }

            foreach (Enemy e in enemies6)
            {
                e.reset(GraphicsDevice.Viewport.Width);
                e.move(randSpeed6);
                if (e.collide(playerRec))
                {
                    playerRec.X = (GraphicsDevice.Viewport.Width / 2);
                    playerRec.Y = (chunkScreen * 9) + offSet;
                    healthRec.Width = healthRec.Width - chunkScreen;
                    lifes--;
                }
            }

        }

        /// <summary>
        /// updates the player sprite when the player clicks the d-pad or arrow keys
        /// also checks for if the player has hit the border and will not let them go pass
        /// </summary>
        private void updatePlayer()
        {
            if ((keys.IsKeyDown(Keys.Right) && !prevKeys.IsKeyDown(Keys.Right)) || ((pad1.DPad.Right == ButtonState.Pressed) && !(prevPad1.DPad.Right == ButtonState.Pressed)))
            {
                playerRec.X = playerRec.X + chunkScreen;
                currPlayer = playerPicRight;
            }

            else if (keys.IsKeyDown(Keys.Left) && !prevKeys.IsKeyDown(Keys.Left) || ((pad1.DPad.Left == ButtonState.Pressed) && !(prevPad1.DPad.Left == ButtonState.Pressed)))
            {
                playerRec.X = playerRec.X - chunkScreen;
                currPlayer = playerPicLeft;
            }

            else if (keys.IsKeyDown(Keys.Up) && !prevKeys.IsKeyDown(Keys.Up) || ((pad1.DPad.Up == ButtonState.Pressed) && !(prevPad1.DPad.Up == ButtonState.Pressed)))
            {
                playerRec.Y = playerRec.Y - chunkScreen;
            }

            else if (keys.IsKeyDown(Keys.Down) && !prevKeys.IsKeyDown(Keys.Down) || ((pad1.DPad.Down == ButtonState.Pressed) && !(prevPad1.DPad.Down == ButtonState.Pressed)))
            {
                playerRec.Y = playerRec.Y + chunkScreen;
            }

            if(playerRec.X < 0)
            {
                playerRec.X = playerRec.X + chunkScreen;
            }

            if (playerRec.X > GraphicsDevice.Viewport.Width - playerRec.Width)
            {
                playerRec.X = GraphicsDevice.Viewport.Width - (chunkScreen - playerRec.Width);
            }

            if(playerRec.Y > GraphicsDevice.Viewport.Height - playerRec.Height)
            {
                playerRec.Y = GraphicsDevice.Viewport.Height - playerRec.Height;
            }
        }

        /// <summary>
        /// updtaes the Start screen each time the loop is ran through and checks to see if they pushed a button
        /// </summary>
        private void updateStartScreen()
        {
            if (startButtonRec.Contains(new Point(mouse.X, mouse.Y)) && (!(mouse.LeftButton == ButtonState.Pressed) && prevMouse.LeftButton == ButtonState.Pressed))
            {
                startButtonColor = Color.Red;
                Screen = ScreenState.playingScreen;
            }
            else if (startButtonRec.Contains(new Point(mouse.X, mouse.Y)) && mouse.LeftButton == ButtonState.Pressed)
            {
                startButtonColor = Color.Red;
            }
            else if (startButtonRec.Contains(new Point(mouse.X, mouse.Y)))
            {
                startButtonColor = Color.Yellow;
            }
            else
            {
                startButtonColor = Color.White;
            }

            if (helpButtonRec.Contains(new Point(mouse.X, mouse.Y)) && (!(mouse.LeftButton == ButtonState.Pressed) && prevMouse.LeftButton == ButtonState.Pressed))
            {
                backButtonColor = Color.Red;
                Screen = ScreenState.helpScreen;
            }
            else if (helpButtonRec.Contains(new Point(mouse.X, mouse.Y)) && mouse.LeftButton == ButtonState.Pressed)
            {
                helpbuttonColor = Color.Red;
                backButtonRec.X = 50;
                backButtonRec.Y = 50;
                backButtonRec.Width = 475 / 3;
                backButtonRec.Height = 123 / 3;
            }
            else if (helpButtonRec.Contains(new Point(mouse.X, mouse.Y)))
            {
                helpbuttonColor = Color.Yellow;
            }
            else
            {
                helpbuttonColor = Color.White;
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            switch (Screen)
            {
                case ScreenState.startScreen:
                    spriteBatch.Draw(startScreenPic, startScreenRec, Color.White);
                    spriteBatch.Draw(startButtonPic, startButtonRec, startButtonColor);
                    spriteBatch.Draw(helpButtonPic, helpButtonRec, helpbuttonColor);
                    break;
                case ScreenState.playingScreen:
                    spriteBatch.Draw(gamePlayPic, gamePlayRec, Color.White);
                    spriteBatch.Draw(healthPic, healthBackground, Color.Gray);
                    spriteBatch.Draw(healthPic, healthRec, Color.Red);
                    foreach(Enemy e in enemies1)
                    {
                        e.Draw(spriteBatch);
                    }

                    foreach (Enemy e in enemies2)
                    {
                        e.Draw(spriteBatch);
                    }

                    foreach (Enemy e in enemies3)
                    {
                        e.Draw(spriteBatch);
                    }

                    foreach (Enemy e in enemies4)
                    {
                        e.Draw(spriteBatch);
                    }

                    foreach (Enemy e in enemies5)
                    {
                        e.Draw(spriteBatch);
                    }

                    foreach (Enemy e in enemies6)
                    {
                        e.Draw(spriteBatch);
                    }
                    spriteBatch.Draw(currPlayer, playerRec, Color.White);
                    break;
                case ScreenState.helpScreen:
                    spriteBatch.Draw(helpScreenPic, helpScreenRec, Color.White); 
                    spriteBatch.Draw(backButtonPic, backButtonRec, backButtonColor);
                    break;
                case ScreenState.endingScreen:
                    spriteBatch.Draw(lossScreenPic, lossScreenRec, Color.White);
                    spriteBatch.Draw(backButtonPic, backButtonRec, backButtonColor);
                    break;
                case ScreenState.winningScreen:
                    spriteBatch.Draw(winScreenPic, winScreenRec, Color.White);
                    spriteBatch.Draw(backButtonPic, backButtonRec, backButtonColor);
                    break;
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }

    enum ScreenState
    {
        startScreen,
        helpScreen,
        playingScreen,
        winningScreen,
        endingScreen
    }

    class Enemy
    {
        Random randE = new Random();

        private Texture2D texture;
        private Texture2D texture2;
        private Texture2D curr;
        private Boolean isVisible;
        private Rectangle rect;

        public Enemy(Texture2D t, Texture2D t2, Rectangle r, Boolean v)
        {
            rect = r;
            texture = t;
            texture2 = t2;
            isVisible = v;

            curr = texture;

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (isVisible)
            {
                spriteBatch.Draw(curr, rect, Color.White);
            }
        }

        public void move(int speed)
        {
            rect.X = rect.X + speed;
            if(speed > 0)
            {
                curr = texture2;
            }
            else
            {
                curr = texture;
            }
        }

        public void reset(int maxWidth)
        {
            if(rect.X < 0)
            {
                rect.X = maxWidth;
            }

            if (rect.X > maxWidth)
            {
                rect.X = 0;
            }
        }

        public void setPos(int RanPos)
        {
            rect.X = RanPos;
        }

        public Boolean collide(Rectangle playerRec)
        {
            if (rect.Intersects(playerRec))
            {
                return true;
            }

            return false;
        }

        public Boolean getVisibility() { return isVisible; }
        public void setVisibility(Boolean b) { isVisible = b; }
        public Rectangle getRect() { return rect; }

    }


}
