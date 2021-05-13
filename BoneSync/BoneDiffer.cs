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
            Console.WriteLine("Comparing Skeleton Project to targer SoundBone: "+Bone);
            BoneDiffer.DiffBone(XSkeleton, XBone, BoneName);
           //BoneDiffer.PatchCompare("TEMP","TEMP2");
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
                //THIS WILL RETURN:
                //@@ -37,17 +37,17 @@      --- Change's Coordinates in A,B - C,D format: STILL TO BE DECIPHERED

                //d % 3e % 7b6660b         --- d>{6660b is the chunk of text (8 character) that preceeds the changed data - Special Characters encoded in Hex
                //   - 1                   --- Removed 1 from the old file
                //   + 2                   --- Added 2 to the old file
                //71-01ae-                 --- 71-01ae- is the chunk of text (8 character) that follows the changed data
            }
        }

        public static void PatchPrinter(DiffMatchPatch.Patch DataInput, string BoneID, int Part)
        { 
            string DiffDataFile = (@"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\PatchData\"+BoneID+@"\Patch_"+BoneID+"_"+Part+".txt");
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
        public static void PatchCompare(string BonePatchFilePath, string SkeletonPatchFilePath)
        {
            string BonePatchData = File.ReadAllText(BonePatchFilePath);
            string MainDiffData = File.ReadAllText(SkeletonPatchFilePath);
            //---------------------------------------------------------------------------
            //Searches for matching patterns between the main changelist and the bone diff results
            var files = Directory.GetFiles(BonePatchFilePath, "*.txt", SearchOption.TopDirectoryOnly);
            Console.WriteLine("Comparing Diff results with Parent Patch");
            foreach (string file in files)
            {
                Console.WriteLine(file);
                //Add diff algorythm + Document Merge
            }

        }
    }
}
