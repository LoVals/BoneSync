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
    class BoneDiffer
    {
        public static void SkeletonToBone(string Skeleton, string Bone, String BoneName)
        {
            XDocument XSkeleton = XDocument.Load(Skeleton);
            XDocument XBone = XDocument.Load(Bone);
            Console.WriteLine("Comparing Skeleton Project to targer SoundBone: " + Bone);
            BoneDiffer.DiffBone(XSkeleton, XBone, BoneName);
            //------------------------------------------
            string BoneFolder = Path.GetFileNameWithoutExtension(BoneName);
            BoneDiffer.PatchCompare(@"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\PatchData\" + BoneFolder, @"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\PatchData\MainDiffData.txt");
            Console.WriteLine("FoundAllMatches, proceeding...");
            Console.ReadKey();
        }

        public static void DiffBone(XDocument SoundSkeleton, XDocument SoundBone, String BoneID)
        {
            // TO CONTINUE
            string SkeletonString = SoundSkeleton.Document.ToString(SaveOptions.DisableFormatting);
            string BoneString = SoundBone.Document.ToString(SaveOptions.DisableFormatting);
            List<string> PatchList = new List<string>();
            //----------------------------------------------------------------------------
            //Diffs bones with the Skeleton
            Console.WriteLine("Diff Skeleton with " + BoneID);
            diff_match_patch BonePatch = new diff_match_patch();
            BonePatch.Diff_Timeout = 0;
            List<Patch> Patch = BonePatch.patch_make(SkeletonString, BoneString);


            for (int i = 0; i < Patch.Count; i++)
            {
                BoneDiffer.PatchPrinter(Patch[i], BoneID, i);
            }
        }

        public static void PatchPrinter(DiffMatchPatch.Patch DataInput, string BoneID, int Part)
        {
            string DiffDataFile = (@"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\PatchData\" + BoneID + @"\Patch_" + BoneID + "_" + Part + ".txt");
            FileStream ostrm;
            StreamWriter writer;
            TextWriter oldOut = Console.Out;
            ostrm = new FileStream(DiffDataFile, FileMode.OpenOrCreate, FileAccess.Write);
            writer = new StreamWriter(ostrm);
            Console.SetOut(writer);
            Console.WriteLine(DataInput);
            Console.SetOut(oldOut);
            writer.Close();
            ostrm.Close();
            var files = File.ReadAllLines(DiffDataFile);
            files.ToList().ForEach(s => Console.WriteLine(s));
            Console.WriteLine("Diff Ended Successfully.");
        }
        public static void PatchCompare(string BonePatchFilePath, string SkeletonPatchFilePath) //INPUT Soundbones Patch parts Directory + Skeleton Patch file
        {

            string MainDiffData = File.ReadAllText(SkeletonPatchFilePath);
            //---------------------------------------------------------------------------
            //Searches for matching patterns between the main changelist and the bone diff results
            var files = Directory.GetFiles(BonePatchFilePath, "*.txt", SearchOption.TopDirectoryOnly); //will get all txt files in patch directory
            Console.WriteLine("Comparing Diff results with Parent Patch");
            foreach (string file in files)
            {
                Console.WriteLine("Parsing PatchPart -  "+file);
                Console.WriteLine("Matches found will be printed on screen");
                string BonePatchData = File.ReadAllText(file);
                if (BoneDiffer.FindMatch(BonePatchData, MainDiffData) == true)
                {
                    ConsoleColor newForeColor = ConsoleColor.Green;
                    Console.WriteLine("Match found for " + file);
                    Console.WriteLine(BonePatchData);
                    Console.ReadLine();
                }
                else;
                {
                    Console.WriteLine("No Match found for" + BonePatchData);
                }

            }

        }
        public static bool FindMatch(string SkeletonPatchFilePathIN, string BonePatchDataIN)
        {
            diff_match_patch MatchTest = new diff_match_patch();
            MatchTest.Match_Threshold = 0.01f;
            Console.WriteLine(MatchTest.Match_Threshold);
            MatchTest.Match_Distance = 1;
            var FoundMatch = MatchTest.match_main(SkeletonPatchFilePathIN, BonePatchDataIN, -1); //the matching algorythm isn't really working right now - WHY?
            
            if  (FoundMatch != -1) //match found
            {
                Console.WriteLine("Match found, Patch part is valid");
                Console.WriteLine(FoundMatch);
                return true;
            }
            else
            {
                return false;
            }       
        }
    }
}
