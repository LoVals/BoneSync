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
    class XmlToFdp
    {
        public static void Execute(string SoundBonesDir, string XMLRegenDir)
        //Copies over the GENRATED CHILDREN projects as FDP
        {
            var SoundSoulsFDP = Directory.GetFiles(SoundBonesDir, "*.fdp", SearchOption.TopDirectoryOnly);
            foreach (var file in SoundSoulsFDP)
            {
                string filename = Path.GetFileName(file);
                File.Delete(SoundBonesDir + @"\Backup\" + filename);
                File.Move(file, SoundBonesDir + @"\Backup\"+ filename);
            }
            var XMLtoConvert = Directory.GetFiles(XMLRegenDir, "*.xml", SearchOption.TopDirectoryOnly);
            Console.WriteLine("Converting XML files to FDP");
            foreach (var file in XMLtoConvert)
            {
                string filename = Path.GetFileNameWithoutExtension(file);
                Console.WriteLine("Moving File: " + filename);
                File.Move(file, SoundBonesDir + @"\" + filename + ".fdp");
            }
        }
    }
}
