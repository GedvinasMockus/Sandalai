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
    internal class MusicPlayerAdapter : IMusic
    {
        ContentManager content;
        MusicLoader musicLoader;

        public MusicPlayerAdapter(ContentManager con)
        {
            content = con;
            musicLoader = new MusicLoader();
        }

        public void playSong(string songPath)
        {
            Song song = musicLoader.getSongFromURI(songPath);
            MediaPlayer.Play(song);
            MediaPlayer.IsRepeating = true;
        }

        public void stopSong()
        {
            MediaPlayer.Stop();
        }
    }
}
