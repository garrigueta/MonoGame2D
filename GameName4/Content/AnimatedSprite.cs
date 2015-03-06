using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameName4.Content
{
    class AnimatedSprite
    {
        public Texture2D Texture { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        private int currentFrame;
        private int totalFrames;
        private int steps;

        int movement = 2;
        int direction = 0;

        public Vector2 position;
        public Vector2 velocity;

        public bool hasJumped;

        public Rectangle destinationRectangle;
        public Rectangle sourceRectangle;

        public AnimatedSprite(Texture2D texture, int rows, int columns, Vector2 newPosition)
        {
            Texture = texture;
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
                    if(turbo)
                        velocity.Y = -4f;
                    else
                        velocity.Y = -3f;
                    hasJumped = true;
                }

                
                if ((position.Y + 32) >= 455)
                    hasJumped = false;

                if (hasJumped == false)
                    velocity.Y = 0f;

               float i = 1; velocity.Y += 0.15f * i; 
                    
            }
            
        }
        
        public void Draw(SpriteBatch spriteBatch)
        {
            int width = Texture.Width / Columns;
            int height = Texture.Height / Rows;
            int column = currentFrame % Columns;
            if (column>=3)
                column = 0;
            
            sourceRectangle = new Rectangle(width * column, height * direction, width, height);
            destinationRectangle = new Rectangle((int)position.X, (int)position.Y, width, height);

            spriteBatch.Begin();
            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
            spriteBatch.End();
        }
    }

}
