using System;
namespace Server
{  
    class Converter
    {
        internal static string Pack(Message message)
        {
            string result =
                message.Author + "<author>" + message.CreationTicks
                + "<ticks>" + message.Text+"<text>"+message.Picture;

            return result;
        }

        internal static Message Unpack(string line)
        {            
            string[] separators =
                new string[3] { "<author>", "<ticks>","<text>" };
            var temp = line.Split
                (separators,System.StringSplitOptions.None);
            var result = new Message();
            result.Author = 
                (temp[0]==String.Empty)?"undefined":temp[0];
            result.CreationTicks = 
                (temp[1]==String.Empty)?DateTime.Now.Ticks:Convert.ToInt64(temp[1]);
            result.Text = temp[2];
            result.Picture = temp[3];

            return result;
        }
    }
}
