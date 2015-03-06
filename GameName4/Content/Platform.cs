using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameName4.Content
{
    class Platform
    {
        Texture2D texture;
        Vector2 position;
        public Rectangle rectangle;


        public Platform(Texture2D _texture, Vector2 _position)
        {
            texture = _texture;
            position = _position;

            rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

        public void Draw(SpriteBatch spirteBatch)
        {
            spirteBatch.Draw(texture, rectangle, Color.White);
        }

    }
}
