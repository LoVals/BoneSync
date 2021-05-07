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
            string ModString = ModifiedProject.Document.ToString(SaveOptions.DisableFormatting);
            string CacheString = CachedProject.Document.ToString(SaveOptions.DisableFormatting);
            //STRINGS MIGHT BE USELESS
            List<string> ModificationsList = new List<string>();
            List<string> CacheList = new List<string>();
            //----------------------------------------------------------------------------

            diff_match_patch XmlDocs = new diff_match_patch();
            List<Diff> diff = XmlDocs.diff_main(ModString, CacheString);
            XmlDocs.patch_make(ModString, CacheString);     //Meed to find a way to convert the patch into a txt file tha I can split and feed to the various children
            //diff_match_patch dmp = new diff_match_patch();
            //List<Diff> diff = dmp.diff_main("Hello World.", "Hello World.");
            // Result: [(-1, "Hell"), (1, "G"), (0, "o"), (1, "odbye"), (0, " World.")]
            //XmlDocs.diff_cleanupSemantic(diff);
            // Result: [(-1, "Hello"), (1, "Goodbye"), (0, " World.")]
            for (int i = 0; i < diff.Count; i++)
            {
                    Console.WriteLine(diff[i]);
            }

        }
    }
}
