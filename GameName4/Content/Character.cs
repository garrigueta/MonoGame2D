using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameName4.Content
{
    class Character
    {
        Texture2D texture;
        public int Rows { get; set; }
        public int Columns { get; set; }
        private int currentFrame;
        private int totalFrames;
        private int steps;

        int movement = 2;
        int direction = 0;

        Vector2 position;
        public Vector2 velocity;

        public bool hasJumped;

        public Rectangle rectangle;

        public Character(Texture2D newTexture, Vector2 newPosition)
        {
            texture = newTexture;
            position = newPosition;
            hasJumped = true;
        }

        public Character(Texture2D _texture, int rows, int columns, Vector2 newPosition)
        {
            texture = _texture;
            Rows = rows;
            Columns = columns;
            currentFrame = 0;
            totalFrames = Rows * Columns;
            steps = 0;

            position = newPosition;
            hasJumped = true;
        }

        public void Update(GameTime gameTime)
        {
            steps++;
            if (steps == 10)
            {
                currentFrame++;
                if (currentFrame == totalFrames)
                    currentFrame = 0;
                steps = 0;
            }
            position += velocity;
            bool turbo = false;


            rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);

            GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);
            if (gamePadState.IsConnected)
            {
                if (gamePadState.Buttons.X == ButtonState.Pressed)
                {
                    turbo = true;
                }
                if (turbo)
                    movement = 2;
                else
                    movement = 1;
                /*if (gamePadState.DPad.Down == ButtonState.Pressed)
                {                    
                    position.Y += movement;
                    direction = 0;
                }*/
                if (gamePadState.DPad.Left == ButtonState.Pressed)
                {
                    position.X -= movement;
                    direction = 1;
                }
                if (gamePadState.DPad.Right == ButtonState.Pressed)
                {
                    position.X += movement;
                    direction = 2;
                }
                if (gamePadState.DPad.Right == ButtonState.Pressed) velocity.X = 1f * (float)movement;
                else if (gamePadState.DPad.Left == ButtonState.Pressed) velocity.X = -1f * (float)movement;
                else velocity.X = 0f;

                if ((gamePadState.Buttons.A == ButtonState.Pressed) && hasJumped == false)
                {
                    position.Y -= 10f;
                    if (turbo)
                        velocity.Y = -4f;
                    else
                        velocity.Y = -3f;
                    hasJumped = true;
                }

                
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Right)) velocity.X = 3f;
            else if (Keyboard.GetState().IsKeyDown(Keys.Left)) velocity.X = -3f; else velocity.X = 0f;

            if (Keyboard.GetState().IsKeyDown(Keys.Space) && hasJumped == false)
            {
                position.Y -= 10f;
                velocity.Y = -5f;
                hasJumped = true;
            }

                        
            if (hasJumped == false)
                velocity.Y = 0f;

            float i = 1;
            velocity.Y += 0.15f * i;

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);
        }
    }
}
