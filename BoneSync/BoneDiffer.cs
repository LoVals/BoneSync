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
            Console.WriteLine("Comparing Skeleton Project to target SoundBone: " + Bone);
            //need to add file cleanup  
            BoneDiffer.WipeOldFiles(BoneName);
            BoneDiffer.DiffBone(XSkeleton, XBone, BoneName);
            //------------------------------------------
            string BoneFolder = Path.GetFileNameWithoutExtension(BoneName);
            BoneDiffer.PatchCompare(@"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\PatchData\" + BoneFolder, @"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\PatchData\MainDiffData.txt");
            Console.WriteLine("Match protocol finished, proceeding to next step...");
            Console.ReadKey();
        }

        public static void DiffBone(XDocument SoundSkeleton, XDocument SoundBone, String BoneID)
        {
            string SkeletonString = SoundSkeleton.Document.ToString(SaveOptions.DisableFormatting);
            string BoneString = SoundBone.Document.ToString(SaveOptions.DisableFormatting);
            List<string> PatchList = new List<string>();
            //----------------------------------------------------------------------------
            //Diffs bones with the Skeleton
            Console.WriteLine("Diff Skeleton with " + BoneID);
            diff_match_patch BonePatch = new diff_match_patch();
            BonePatch.Diff_Timeout = 0;                                                        //Timing out fucks up large files comparison
                                                                                               //I would need to figure out a way to speed this shit up = is the issue file-size?
                                                                                               // The issue is complexity - what if I were to split the fucking file?
            List<Patch> Patch = BonePatch.patch_make(BoneString, SkeletonString);
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
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Parsing PatchPart -  "+file);
                Console.WriteLine();
                Console.WriteLine("Matches found will be printed on screen");
                FileInfo fi = new FileInfo(file);
                long FilterBySize = fi.Length;
                int Filtervalue = BoneDiffer.FilterResult(FilterBySize);
                switch (Filtervalue)
                {
                    case 0:     //Change detected by size is less than 1kb - it's likely a legit change
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            string BonePatchData = File.ReadAllText(file);
                            if (BoneDiffer.FindMatch(MainDiffData, BonePatchData) == true)
                            {
                                Console.WriteLine();
                                Console.WriteLine("Match found for " + file);
                                //Console.WriteLine(BonePatchData);
                                Console.ReadLine();
                            }

                        }
                        break;
                    case 1:     //Change detected by size is between 1kb and 4kb - it could be a legit change - needs manual confirmation
                        {
                            string BonePatchData = File.ReadAllText(file);
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            bool ConfirmValid = false;
                            int Confirm;
                            Console.WriteLine("!!! WARNING - PATCH SIZE IS OVER 3KB !!!");
                            Console.WriteLine("The data is likely a large change, or it could contain a chunk of data that shouldn't be there");
                            Console.WriteLine("the data included in the patch is:");
                            Console.WriteLine("");
                            Console.WriteLine(BonePatchData);
                            Console.WriteLine("");
                            do
                            {
                                Console.WriteLine("Would you still like to commit this change? - 1 = yes; 0 = no");
                                string input = Console.ReadLine();
                                Int32.TryParse(input, out Confirm);
                                if (Confirm == 0 || Confirm == 1)
                                {
                                    ConfirmValid = true;
                                }
                                else 
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("ERROR - INPUT INVALID - Try Again");
                                    Console.WriteLine("");
                                }

                            }
                            while (ConfirmValid == false);

                                switch (Confirm)
                                {
                                case 0:
                                    break;
                                case 1:
                                    {
                                        if (BoneDiffer.FindMatch(MainDiffData, BonePatchData) == true)
                                        {
                                            Console.WriteLine();
                                            Console.WriteLine("Match found for " + file);
                                            //Console.WriteLine(BonePatchData);
                                            Console.ReadLine();
                                        }
                                    }
                                    break;
                                }                              
                        }
                        break;
                    case 2:     // change is larger than 4kb - this is likely data related to something else...
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("SEVERE WARNING - PATCH DATA EXCEEDS EXPECTED NUGGET SIZE");
                            Console.WriteLine("Data is invalid: Automatically discarded");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        break;

                //Finish this fucking Filter
                }
            }
        }
        public static bool FindMatch(string SkeletonPatchFilePathIN, string BonePatchDataIN)
        {
            diff_match_patch MatchTest = new diff_match_patch();
            Console.WriteLine(BonePatchDataIN);
            Console.WriteLine();
            MatchTest.Match_Threshold = 0.69f;
            MatchTest.Match_Distance = 30;
            var FoundMatch = MatchTest.match_main(SkeletonPatchFilePathIN, BonePatchDataIN, -1); //the matching algorythm isn't really working right now - WHY?
            
            if  (FoundMatch != -1) //match found
            {
                Console.WriteLine("Match found, Patch part is valid");
                return true;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Match Not found for this part: Patch part is invalid");
                return false;
            }       
        }

        public static void WipeOldFiles(string TargetFolder)
        {
            string[] filePaths = Directory.GetFiles(@"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\PatchData\" + TargetFolder);
            //File.Delete(filePaths + @"\PreviousVersion\SoundSoulsCache.xml");
            foreach (string filePath in filePaths)
            {
                File.Delete(filePath);
            }
        }

        public static int FilterResult(long DataInput)
        {
            if(DataInput<400)
            {
                return 0;
            }
            if(DataInput>400)
            {
                if (DataInput < 3072)
                {
                    return 1;
                }
                else if (DataInput > 3072)
                {
                    return 2;
                }
            }
            return 1;
        }

    }
}
