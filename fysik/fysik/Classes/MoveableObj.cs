using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Fysik_YakupY
{
    class MoveableObj : PhysicalObj
    {
        // Objektets nuvarande hastighet
        public Vector2 hastighet;
        // Objektets hastighetsförändring över tid
        public Vector2 acceleration; 

        public MoveableObj(Vector2 pos, Vector2 hastighet, 
            Vector2 acceleration)
            : base(pos)
        {
            this.acceleration = acceleration;
            this.hastighet = hastighet;
        }


        public override void Update(GameTime gT)
        {
            /* Förändrar positionsvektorn med hastigheten multiplicerat med 
            *  förfluten tid sen appen startades */
            pos += hastighet * (float)gT.ElapsedGameTime.TotalSeconds +
                ((acceleration * (float)Math.Pow(gT.ElapsedGameTime.TotalSeconds, 2)) / 2);

            /* Förändrar hastighetsvektorn med acceleration-vektorn multiplicerat
             * med förfluten tid sen appen startades */
            hastighet += acceleration * (float)gT.ElapsedGameTime.TotalSeconds;
        }
    }
}
