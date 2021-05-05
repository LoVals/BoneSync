using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;
using DiffMatchPatch;

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
            XMLParse.DiffMatch();
            Console.ReadLine();
        }

        public static void DiffMatch()
        {

            //Testing the Google DiffPatch API with this scrit
            //need to figure out if I can diff a whole file and output the result into a patch to apply to a specific bone
            //out of all the things I tried this is the most promising - DO NOT CAN THIS BUT EXPAND

            diff_match_patch dmp = new diff_match_patch();
            List<Diff> diff = dmp.diff_main("Hello World.", "Hello World.");
            // Result: [(-1, "Hell"), (1, "G"), (0, "o"), (1, "odbye"), (0, " World.")]
            dmp.diff_cleanupSemantic(diff);
            // Result: [(-1, "Hello"), (1, "Goodbye"), (0, " World.")]
            for (int i = 0; i < diff.Count; i++)
            {
                Console.WriteLine(diff[i]);
            }

        }
    }
}
