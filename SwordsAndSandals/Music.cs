using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using SwordsAndSandals.Classes;
using SwordsAndSandals.InfoStructs;
using SwordsAndSandals.Stats;
using System;

namespace SwordsAndSandals
{
    internal interface Music
    {
        void playSong(String songPath);

        void stopSong();
    }
}
