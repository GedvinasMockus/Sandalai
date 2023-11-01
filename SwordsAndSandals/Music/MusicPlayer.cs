using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwordsAndSandals.Music
{
    internal class MusicPlayer : IMusic
    {
        ContentManager content;

        public MusicPlayer(ContentManager con)
        {
            content = con;
        }

        public void playSong(string songPath)
        {
            MediaPlayer.Play(content.Load<Song>(songPath));
            MediaPlayer.IsRepeating = true;
        }

        public void stopSong()
        {
            MediaPlayer.Stop();
        }
    }
}
