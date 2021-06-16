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
            string CurrentDir = Directory.GetCurrentDirectory();
            Console.ForegroundColor = ConsoleColor.White;
            // will likely change to the SOUNDSOUL root
            Console.WriteLine(" _________________________");
            Console.WriteLine("¦                         ¦");
            Console.WriteLine("¦       Bonesync 0.3      ¦");
            Console.WriteLine("¦_________________________¦");
            Console.WriteLine("");
            Console.WriteLine("Preparing to syncronize...");
            FdpToXml.Execute(CurrentDir);
            //COPIES THE PARENT FILE AS XML FILE
            Console.WriteLine("TEST - REGENERATION OF FDLC_C3471");
            GenerateChild.Execute("fdlc_c3471", CurrentDir);
            //EXECUTES SOUNDBONES REGENERATION
            //Returns an exception
            Console.ReadLine();
        }
    }
}
