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

            XDocument XProject = XDocument.Load(@"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\TEST_A.xml");
            XDocument XCache = XDocument.Load(@"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\TEST_C.xml");
            Console.Clear();
            Console.WriteLine("Parsing Files...");
            XMLParse.DiffMatch(XProject, XCache);
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
            List<string> PatchList = new List<string>();
            //----------------------------------------------------------------------------
            
            diff_match_patch XmlDocs = new diff_match_patch();
            XmlDocs.Diff_Timeout = 0;
            //List<Diff> diff = XmlDocs.diff_main(ModString, CacheString);
            List<Patch> Patch = XmlDocs.patch_make(ModString, CacheString);           
            for (int i = 0; i < Patch.Count; i++)
            {
                    XMLParse.WritePatch(Patch[i]);
                //THIS WILL RETURN:
                //@@ -37,17 +37,17 @@      --- Change's Coordinates in A,B - C,D format: STILL TO BE DECIPHERED

                //d % 3e % 7b6660b         --- d>{6660b is the chunk of text (8 character) that preceeds the changed data - Special Characters encoded in Hex
                //   - 1                   --- Removed 1 from the old file
                //   + 2                   --- Added 2 to the old file
                //71-01ae-                 --- 71-01ae- is the chunk of text (8 character) that follows the changed data
            }

        }

        public static void WritePatch(DiffMatchPatch.Patch DataInput)
        {

            //NOT GOOD AS IS - ONLY WRITES DOWN THE LAST COMMAND
            string DiffDataFile = (@"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\DiffData.txt");
            FileStream ostrm;
            StreamWriter writer;
            TextWriter oldOut = Console.Out;
            try
            {
                ostrm = new FileStream(DiffDataFile, FileMode.OpenOrCreate, FileAccess.Write);
                writer = new StreamWriter(ostrm);
            }
            catch (Exception e)
            {
                Console.WriteLine("Cannot open Redirect.txt for writing");
                Console.WriteLine(e.Message);
                return;
            }
            Console.SetOut(writer);
            Console.WriteLine(DataInput);
            Console.SetOut(oldOut);
            writer.Close();
            ostrm.Close();
            var files = File.ReadAllLines(DiffDataFile);
            files.ToList().ForEach(s => Console.WriteLine(s));
        }

        //RATHER THAN INTERPRETING THE DATA: Diff children and apply the content change only to matching IDs (FIRST HALF)
    }
}
