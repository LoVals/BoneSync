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
    class FdpToXml
        //CLASS RESPONSIBLE FOR CONVERTING THE FDP FILES TO XML
    {
        public static void Execute(string SoundSoulsRoot)
        //Copies over the PARENT project as XML
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Converting Soundsouls FMOD project into the XML format...");
            var SoundBones = Directory.GetFiles(SoundSoulsRoot+ @"\SoundBones", "*.fdp", SearchOption.TopDirectoryOnly);
            var BoneCache = Directory.GetFiles(SoundSoulsRoot + @"\XML\Bones", "*.xml", SearchOption.TopDirectoryOnly);
            var BoneXMLDir = SoundSoulsRoot + @"\XML\Bones";
            File.Delete(SoundSoulsRoot+ @"\XML\SoundSouls.xml");
            File.Copy(SoundSoulsRoot + @"\SoundSouls.fdp", SoundSoulsRoot + @"\XML\SoundSouls.fdp");
            File.Move(SoundSoulsRoot + @"\XML\SoundSouls.fdp", SoundSoulsRoot + @"\XML\SoundSouls.xml");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Soundsouls project successfully converted");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            
            Console.WriteLine("Deleting Old Data...");
            foreach(var file in BoneCache)
            {
                File.Delete(file);
            }
            Console.WriteLine("Fetching SoundBones...");
            foreach (var file in SoundBones)
            {
                Console.ForegroundColor = ConsoleColor.White;                
                string filename = Path.GetFileNameWithoutExtension(file);
                Console.WriteLine("SoundBone detected: " + filename);
                File.Copy(file, BoneXMLDir + @"\"+ filename+".xml");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(filename + " Converted to XML successfully");
                Console.WriteLine();
            }
        }
    }
}
