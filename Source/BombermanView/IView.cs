using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using BombermanCommon;

namespace BombermanView
{
    interface IView : IInitilizableObject
    {
        Texture2D Texture2D { get; }
        Vector2 Vector2 { get; }

        void LoadContent(ContentManager Content);
        void UnloadContent();

        void Initialize(Game game);
    }
}
