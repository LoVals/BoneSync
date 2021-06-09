using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.IO;
using System.Drawing;
using System.Data;
using System.Xml;
using System.Xml.Linq;

namespace BoneSync_02
{
    class BonesyncMain
    {
        static void Main(string[] args)
        {
            string rootPath = @"D:\Programs\Static\Dark_Souls_Mods\SoundSouls\SoundBones";                                      //Replace this with Current Directory + \Soundbones as the script resides in the same folder as pre build sync
            string CurrentDir = Directory.GetCurrentDirectory();
            Console.ForegroundColor = ConsoleColor.Green;
            // will likely change to the SOUNDSOUL root
            Console.WriteLine(" _________________________");
            Console.WriteLine("¦                         ¦");
            Console.WriteLine("¦       Bonesync 0.2      ¦");
            Console.WriteLine("¦_________________________¦");
            Console.WriteLine("");
            Console.WriteLine("Preparing to syncronize...");
            //FdpToXml.Execute();                                                                                                 //COPIES THE PARENT FILE AS XML FILE
            GenerateChild.Execute();
            Console.ReadLine();
        }
    }
}
