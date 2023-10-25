using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SwordsAndSandals.InfoStructs;
using SwordsAndSandals.Objects.Classes;
using SwordsAndSandals.Objects.Items.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace SwordsAndSandals
{
    internal class PlayerAdapter : ITarget
    {
        public Player ProcessPlayer(PlayerInfo playerInfo, ContentManager content, Vector2 position, SpriteEffects flip, bool setButtons)
        {

            return Player.GetNewPlayer(GetPlayerFactory(playerInfo.ClassName), content, position, flip, setButtons);
        }

        public PlayerFactory GetPlayerFactory(string className)
        {
            switch (className)
            {
                case "Kunoichi":
                    return new KunoichiFactory();
                case "Samurai":
                    return new SamuraiFactory();
                default:
                    return new SkeletonFactory();
            }
        }
    }
}
