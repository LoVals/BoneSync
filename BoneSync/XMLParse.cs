using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;

namespace BoneSync
{
    class XMLParse
    {

        public static void ParseXML()
        {
            //bool FileMatch = true;
            XDocument xmlA = XDocument.Load(@"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\TEST_A.xml");
            IEnumerable<XElement> xElements = xmlA.Descendants("guid");
            XDocument xmlB = XDocument.Load(@"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\TEST_B.xml");
            IEnumerable<XElement> xElementsB = xmlB.Descendants("guid");
            Console.Clear();
            //XMLParse.CheckIfSame(@"D:\Programs\Static\Dark_Souls_Mods\SoundSouls\XMLs\SoundSoulsXML\TEST_A.xml", @"D:\Programs\Static\Dark_Souls_Mods\SoundSouls\XMLs\SoundSoulsXML\TEST_B.xml");
            Console.WriteLine("Parsing Files...");
            //search for missing GUID - this is to add any event to that is muissing on the specific child
            //might need to get the name of the child somehow: Possibly referencing it in FNC?

            //Need to isolate searches to the following folders: sounddeffolder; eventcategory

            foreach (XElement element in xElements)
            {
                Console.WriteLine("GuID in File A: " + element.Value);
            }

            foreach (XElement element in xElementsB)
            {
                Console.WriteLine("GuID in File B: " + element.Value);
            }

            Console.ReadLine();
        }

        public bool CheckIfSame(string file1, string file2)
        {
            int file1byte;
            int file2byte;
            FileStream fs1;
            FileStream fs2;
            if (file1 == file2)
            {
                return true;
            }
            fs1 = new FileStream(file1, FileMode.Open);
            fs2 = new FileStream(file2, FileMode.Open);
            if (fs1.Length != fs2.Length)
            {
                fs1.Close();
                fs2.Close();
                return false;
            }
            do
            {
                // Read one byte from each file.
                file1byte = fs1.ReadByte();
                file2byte = fs2.ReadByte();
            }
            while ((file1byte == file2byte) && (file1byte != -1));

            // Close the files.
            fs1.Close();
            fs2.Close();

            return ((file1byte - file2byte) == 0);
        }
    }
}
