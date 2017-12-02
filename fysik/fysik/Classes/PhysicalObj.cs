using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Fysik_YakupY
{
    public abstract class PhysicalObj
    {
        // Gör om pixlar till SI-korrekta meter!
        public const int pixelPerMeter = 100; 
        public Vector2 pos;
        // Detta är bollens elasticitet, hur energin omvandlas till potentiell energi
        public float elast;    

        public PhysicalObj(Vector2 pos)
        {
            this.pos = pos;
        }

        public virtual void Update(GameTime gameTime)
        { }

        public virtual void Draw(SpriteBatch spriteBatch)
        { }
    }
}
