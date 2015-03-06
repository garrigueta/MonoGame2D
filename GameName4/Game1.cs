#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using GameName4.Content;
#endregion

namespace GameName4
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private Texture2D background;
        
        private SpriteFont font;
        
        private FrameCounter _frameCounter = new FrameCounter();

        private Character player;

        Vector2 location = new Vector2(400, 240);

        List<Platform> platforms = new List<Platform>();



        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Tile.TileSetTexture = Content.Load<Texture2D>(@"Textures\TileSets\part1_tileset");
            
            background = Content.Load<Texture2D>("stage1"); // change these names to the names of your images
            font = Content.Load<SpriteFont>("Score"); // Use the name of your sprite font file here instead of 'Score'.
            
            Texture2D texture = Content.Load<Texture2D>("Frank");

            player = new Character(texture, 8, 3, location);

            platforms.Add(new Platform(Content.Load<Texture2D>("platform3"), new Vector2(100, 450)));
            platforms.Add(new Platform(Content.Load<Texture2D>("platform3"), new Vector2(250, 420)));
            platforms.Add(new Platform(Content.Load<Texture2D>("platform3"), new Vector2(400, 400)));
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            player.Update(gameTime);

            foreach (Platform platform in platforms)
                if (player.rectangle.isOnTopOf(platform.rectangle))
                {
                    player.velocity.Y = 0f;
                    player.hasJumped = false;         
                }                                             
                    

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            _frameCounter.Update(deltaTime);

            var fps = string.Format("FPS: {0}", _frameCounter.AverageFramesPerSecond);

            GraphicsDevice.Clear(Color.CornflowerBlue);
           
            spriteBatch.Begin();

            spriteBatch.Draw(background, new Rectangle(0, 0, 800, 482), Color.White);

            foreach (Platform platform in platforms)
                platform.Draw(spriteBatch);
            

            spriteBatch.DrawString(font, fps, new Vector2(1, 1), Color.Aquamarine);

            player.Draw(spriteBatch);
            
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

static class RectangleHelper
{
    const int penetrationMargin = 5;
    public static bool isOnTopOf(this Rectangle r1, Rectangle r2)
    {
        return (r1.Bottom >= r2.Top - penetrationMargin &&
            r1.Bottom <= r2.Top + 1 &&
            r1.Right >= r2.Left + 5 &&
            r1.Left <= r2.Right - 5);
    }
}