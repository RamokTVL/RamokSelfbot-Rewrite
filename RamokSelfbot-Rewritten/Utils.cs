using System;
using System.Drawing;


namespace RamokSelfbot
{
    public class Utils
    {
        public static void Print(string text)
        {
            Console.Write("[");
            Colorful.Console.Write(DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second, Color.IndianRed);
            Console.Write("] ");
            Colorful.Console.WriteLine(text, Color.IndianRed);
        }
    }
}
