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
            XDocument XCache = XDocument.Load(@"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\TEST_B.xml");
            Console.Clear();
            Console.WriteLine("Parsing Files...");
            XMLParse.DiffMain(XProject, XCache);
        } //MAIN SEQUENCE

        public static void DiffMain(XDocument CachedProject, XDocument ModifiedProject)
        {

            //Testing the Google DiffPatch API with this script
            //need to figure out if I can diff a whole file and output the result into a patch to apply to a specific bone
            //out of all the things I tried this is the most promising - DO NOT CAN THIS BUT EXPAND
            //https://github.com/google/diff-match-patch for more info

            //XML comparison Setup for Main Projects: VARIABLES
            //----------------------------------------------------------------------------
            string ModString = ModifiedProject.Document.ToString(SaveOptions.DisableFormatting);
            string CacheString = CachedProject.Document.ToString(SaveOptions.DisableFormatting);
            int DIFFID = 0;
            List<string> PatchList = new List<string>();
            string DiffDataFile = (@"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\PatchData\MainDiffData.txt"); //Will need curent direcroties & Shit
            FileStream ostrm;
            StreamWriter writer;
            TextWriter oldOut = Console.Out;
            //----------------------------------------------------------------------------
            Console.WriteLine("Diff protocol starting up");
            diff_match_patch XmlDocs = new diff_match_patch();
            XmlDocs.Diff_Timeout = 0;
            List<Patch> Patch = XmlDocs.patch_make(ModString, CacheString);
            ostrm = new FileStream(DiffDataFile, FileMode.OpenOrCreate, FileAccess.Write);
            writer = new StreamWriter(ostrm);
            Console.SetOut(writer);
            for (int i = 0; i < Patch.Count; i++)
            {
                DIFFID = DIFFID + 1;
                    Console.WriteLine(Patch[i]);
                //One Blob per patch
                //THIS WILL RETURN:
                //@@ -37,17 +37,17 @@      --- Change's Coordinates in A,B - C,D format: STILL TO BE DECIPHERED

                //d % 3e % 7b6660b         --- d>{6660b is the chunk of text (8 character) that preceeds the changed data - Special Characters encoded in Hex
                //   - 1                   --- Removed 1 from the old file
                //   + 2                   --- Added 2 to the old file
                //71-01ae-                 --- 71-01ae- is the chunk of text (8 character) that follows the changed data
            }
            Console.SetOut(oldOut);
            writer.Close();
            ostrm.Close();
            var files = File.ReadAllLines(DiffDataFile);
            files.ToList().ForEach(s => Console.WriteLine(s));
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Diff Ended Successfully.");
            //Issue with main project: MAXIMUM SIZE EXCEEDED - NEED TO MOD HKCU\Software\Microsoft\VisualStudio\16.0_06bd41dc_Config\XmlEditor\MaxFileSizeSupportedByLanguageService in REGEDIT
            //

        } //this function will diff two target files. For testing purposes those are set as static variables.
    }
}
