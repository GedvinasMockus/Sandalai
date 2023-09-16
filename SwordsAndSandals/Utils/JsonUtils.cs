using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;

namespace SwordsAndSandals.Utils
{
    public static class JsonUtils
    {
        public static T ReadJson<T>(string filename)
        {
            using (Stream s = File.OpenRead(filename))
            {
                return JsonSerializer.Deserialize<T>(s);
            }
        }
    }
}
