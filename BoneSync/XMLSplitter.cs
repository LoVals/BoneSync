using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.IO;
using System.Drawing;
using System.Data;
using System.Xml;
using System.Xml.Linq;

namespace BoneSync
{
    class XMLSplitter
    {
        //This shit needs testing
        public static void SkeletonSplit(string SoundSoulsXML)
        {
            XMLSplitter.WipeOldNuggets();
            XDocument SkeletonFile = XDocument.Load(SoundSoulsXML);
            var EventCategoryNugget = SkeletonFile.Descendants("eventcategory").Select(d => new XDocument(new XElement("eventcategory", d)));
            int EvCounter = 0;
            foreach (var TargetNugget in EventCategoryNugget)
            {
                EvCounter = EvCounter + 1;
                TargetNugget.Save(@"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\PatchData\Nuggets\EventCategoryNugget_0" + EvCounter + ".xml");
            }
            var SoundDefFolderNugget = SkeletonFile.Descendants("sounddeffolder").Select(d => new XDocument(new XElement("sounddeffolder", d)));
            int SDFCounter = 0;
            foreach (var TargetNugget in SoundDefFolderNugget)
            {
                SDFCounter = SDFCounter + 1;
                TargetNugget.Save(@"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\PatchData\Nuggets\SoundDefFolderNugget_0" + SDFCounter + ".xml");
            }
            var EventGroupNugget = SkeletonFile.Descendants("eventgroup").Select(d => new XDocument(new XElement("eventgroup", d)));
            int EGCounter = 0;
            foreach (var TargetNugget in EventGroupNugget)
            {
                EGCounter = EGCounter + 1;
                TargetNugget.Save(@"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\PatchData\Nuggets\EventGroupNugget_0" + EGCounter + ".xml");
            }
            var SoundbankNugget = SkeletonFile.Descendants("soundbank").Select(d => new XDocument(new XElement("soundbank", d)));
            int SBCounter = 0;
            foreach (var TargetNugget in SoundbankNugget)
            {
                SBCounter = SBCounter + 1;
                TargetNugget.Save(@"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\PatchData\Nuggets\SoundbankNugget_0" + SBCounter + ".xml");
            }
        }
        public static void WipeOldNuggets()
        {
            string[] filePaths = Directory.GetFiles(@"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\PatchData\Nuggets\");
            foreach (string filePath in filePaths)
            {
                File.Delete(filePath);
            }
        }
    }
}
