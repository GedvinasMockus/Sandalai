using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SwordsAndSandals.InfoStructs;
using SwordsAndSandals.Objects.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwordsAndSandals
{
    internal interface ITarget
    {
        Player ProcessPlayer(PlayerInfo playerInfo, ContentManager content, Vector2 position, SpriteEffects flip, bool setButtons);
    }
}
