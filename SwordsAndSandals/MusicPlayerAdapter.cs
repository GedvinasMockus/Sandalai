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
    internal class MusicPlayerAdapter : Music
    {
        ContentManager content;
        MusicLoader musicLoader;

        public MusicPlayerAdapter(ContentManager con)
        {
            this.content = con;
            musicLoader = new MusicLoader();
        }

        public void playSong(String songPath)
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
