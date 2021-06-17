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
            string OldXMLDir = CurrentDir + @"\XML\RegeneratedFiles";
            Console.ForegroundColor = ConsoleColor.White;
            // will likely change to the SOUNDSOUL root
            Console.WriteLine(" _________________________");
            Console.WriteLine("¦                         ¦");
            Console.WriteLine("¦       Bonesync 0.3      ¦");
            Console.WriteLine("¦_________________________¦");
            Console.WriteLine("");
            Console.WriteLine("Preparing to syncronize...");
            Console.WriteLine("Sweeping Bonedust off...");
            var OLDXML = Directory.GetFiles(OldXMLDir , "*.xml", SearchOption.TopDirectoryOnly);
            foreach (var file in OLDXML)
            {
                File.Delete(file);
            }
            FdpToXml.Execute(CurrentDir);
            //COPIES THE PARENT FILE AS XML FILE
            Console.WriteLine("SOUNDBONES REGENERATION IS STARTING");
            var SoundBones = Directory.GetFiles(CurrentDir + @"\SoundBones", "*.fdp", SearchOption.TopDirectoryOnly);
            int fCount = Directory.GetFiles(CurrentDir + @"\SoundBones", "*.fdp", SearchOption.TopDirectoryOnly).Length;
            int CurrentCount = 0;
            var SoundSoulsDOC = XDocument.Load(CurrentDir + @"\XML\SoundSouls.xml");
            foreach (var file in SoundBones)
            {
                //EXECUTES SOUNDBONES REGENERATION
                CurrentCount = CurrentCount + 1;
                string filename = Path.GetFileNameWithoutExtension(file);
                Console.WriteLine("Regenerating: " + filename+" - ["+CurrentCount+"/"+ fCount+"]");
                GenerateChild.Execute(filename, CurrentDir, SoundSoulsDOC);
            }

            XmlToFdp.Execute(CurrentDir + @"\SoundBones", CurrentDir + @"\XML\RegeneratedFiles");
            //Returns an exception
            Console.ReadLine();
        }
    }
}
