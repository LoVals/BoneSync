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
        }

        public static void DiffBone(XDocument SoundSkeleton, XDocument SoundBone, String BoneID)
        {
            // TO CONTINUE
            string ModString = ModifiedProject.Document.ToString(SaveOptions.DisableFormatting);
            string CacheString = CachedProject.Document.ToString(SaveOptions.DisableFormatting);
            List<string> PatchList = new List<string>();
            string DiffDataFile = (@"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\DiffData.txt");
            FileStream ostrm;
            StreamWriter writer;
            TextWriter oldOut = Console.Out;
        }
    }
}
