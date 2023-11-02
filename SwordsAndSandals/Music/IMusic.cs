using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using SwordsAndSandals.Classes;
using SwordsAndSandals.InfoStructs;
using SwordsAndSandals.Stats;
using System;

namespace SwordsAndSandals.Music
{
    public interface IMusic
    {
        void playSong(string songPath);

        void stopSong();
    }
}
