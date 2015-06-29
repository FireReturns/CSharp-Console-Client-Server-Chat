using System;
using System.IO;

namespace Chat
{
    class Image
    {

        internal static void getcode(string path)
        {
            var img=File.ReadAllBytes("avatar.bmp");
            File.WriteAllBytes("img.bmp",img);
        }
        
    }
}
