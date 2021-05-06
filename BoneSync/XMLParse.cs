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

            XDocument xmlA = XDocument.Load(@"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\TEST_A.xml");
            XDocument xmlB = XDocument.Load(@"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\TEST_B.xml");
            Console.Clear();
            Console.WriteLine("Parsing Files...");
            XMLParse.DiffMatch(xmlA, xmlB);
            Console.ReadLine();
        }

        public static void DiffMatch(XDocument ModifiedProject, XDocument CachedProject)
        {

            //Testing the Google DiffPatch API with this script
            //need to figure out if I can diff a whole file and output the result into a patch to apply to a specific bone
            //out of all the things I tried this is the most promising - DO NOT CAN THIS BUT EXPAND
            //https://github.com/google/diff-match-patch for more info

            //XML comparison Setup for Main Projects: VARIABLES
            //----------------------------------------------------------------------------
            IEnumerable<XElement> SoundDefModifications = ModifiedProject.Descendants("sounddeffolder");
            IEnumerable<XElement> EventGroupModifications = ModifiedProject.Descendants("eventgroup");
            IEnumerable<XElement> SoundDefCache = CachedProject.Descendants("sounddeffolder");
            IEnumerable<XElement> EventGroupCache = CachedProject.Descendants("eventgroup");
            List<string> ModificationsList = new List<string>();
            List<string> CacheList = new List<string>();
            //----------------------------------------------------------------------------

            //Reading values and adding them to a list
            //----------------------------------------------------------------------------
            foreach (XElement element in SoundDefModifications)
            {
                Console.WriteLine("Detected Sound Def: " + element.Value);
                ModificationsList.Add(element.Value);
                Console.WriteLine();
            }

            foreach (XElement element in EventGroupModifications)
            {
                Console.WriteLine("Detected Event: " + element.Value);
                ModificationsList.Add(element.Value);
                Console.WriteLine();
            }

            foreach (XElement element in SoundDefCache)
            {
                Console.WriteLine("Caching Sound Def from Cache: " + element.Value);
                CacheList.Add(element.Value);
                Console.WriteLine();
            }

            foreach (XElement element in EventGroupCache)
            {
                Console.WriteLine("Caching Event from Cache: " + element.Value);
                CacheList.Add(element.Value);
                Console.WriteLine();
            }

            diff_match_patch XmlDocs = new diff_match_patch();
            List<Diff> diff = XmlDocs.diff_main("", "");
            XmlDocs.patch_make("TextA","TextB");
            //diff_match_patch dmp = new diff_match_patch();
            //List<Diff> diff = dmp.diff_main("Hello World.", "Hello World.");
            // Result: [(-1, "Hell"), (1, "G"), (0, "o"), (1, "odbye"), (0, " World.")]
            //dmp.diff_cleanupSemantic(diff);
            // Result: [(-1, "Hello"), (1, "Goodbye"), (0, " World.")]
            //for (int i = 0; i < diff.Count; i++)
           // {
             //   Console.WriteLine(diff[i]);
            //}

        }
    }
}
