using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SwordsAndSandals.Classes;
using SwordsAndSandals.InfoStructs;
using SwordsAndSandals.Stats;

namespace SwordsAndSandals
{
    internal interface ITarget
    {
        Player ProcessPlayer(PlayerInfo playerInfo, ContentManager content, Vector2 position, SpriteEffects flip, Attributes attributes, bool setButtons);
    }
}
