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
    class Patcher
    {
        public static void Apply(XDocument PatchData, XDocument XBone)                                  //Input = Patchdata = result of ChildDiff + ParentDiff filter // XBone = SoundBone XML File
        {
            string PatchToText = PatchData.Document.ToString(SaveOptions.DisableFormatting);
            diff_match_patch PatchList = new diff_match_patch();
            List<Patch> Yolo = PatchList.patch_fromText(PatchToText);                                   //converts text log to patch data
            string BoneToPatch = XBone.Document.ToString(SaveOptions.DisableFormatting);
            diff_match_patch ApplyPatch = new diff_match_patch();
            ApplyPatch.patch_apply(Yolo, BoneToPatch);
            Console.WriteLine(ApplyPatch);
            Console.ReadLine();

            //CODE FOR APPLYING PATCH TO THE SOUNDBONES
        }
    }
}
