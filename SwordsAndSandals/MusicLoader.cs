using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwordsAndSandals
{
    public class MusicLoader
    {
        public Song getSongFromURI(string songPath)
        {
            String fullPath = "Content/Music/" + songPath + ".ogg";
            Uri uri = new Uri(fullPath, UriKind.Relative);
            Song song = Song.FromUri("song", uri);

            return song;
        }
    }
}
