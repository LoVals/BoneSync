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
            string BackupFileLocation = @"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\PatchData\SoundSouls.xml";
            File.Copy(SoundSoulsXML, BackupFileLocation);
            XDocument SkeletonFile = XDocument.Load(BackupFileLocation);
            
            var EventCategoryNugget = SkeletonFile.Descendants("eventcategory").Select(d => new XDocument(new XElement("eventcategory", d)));
            int EvCounter = 0;
            do
            {
                try
                {
                    foreach (var TargetNugget in EventCategoryNugget)
                    {

                        EvCounter = EvCounter + 1;
                        Console.WriteLine("Nugget for Event Category 1");
                        TargetNugget.Save(@"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\PatchData\Nuggets\EventCategoryNugget_0" + EvCounter + ".xml");
                        TargetNugget.Remove(); //When parent is deleted, shit happens
                        EventCategoryNugget = SkeletonFile.Descendants("eventcategory").Select(d => new XDocument(new XElement("eventcategory", d)));
                    }
                }
                catch (System.InvalidOperationException); //ALL THIS NEEDS REWORK
                                                          // IDEA - SXPORT THE 1300 files but cull the ones too small. They belong to children which can be considered useless data
            }

            while ();

                //----------------------------------------------------------------------------------------------------------------------------------------------------------------
                var SoundDefFolderNugget = SkeletonFile.Descendants("sounddeffolder").Select(d => new XDocument(new XElement("sounddeffolder", d)));
                int SDFCounter = 0;
            foreach (var TargetNugget in SoundDefFolderNugget)
            {
                SDFCounter = SDFCounter + 1;
                TargetNugget.Save(@"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\PatchData\Nuggets\SoundDefFolderNugget_0" + SDFCounter + ".xml");
                TargetNugget.Remove();
            }
            var EventGroupNugget = SkeletonFile.Descendants("eventgroup").Select(d => new XDocument(new XElement("eventgroup", d)));
            int EGCounter = 0;
            foreach (var TargetNugget in EventGroupNugget)
            {
                EGCounter = EGCounter + 1;
                TargetNugget.Save(@"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\PatchData\Nuggets\EventGroupNugget_0" + EGCounter + ".xml");
                TargetNugget.Remove(); TargetNugget.Remove();
            }
            var SoundbankNugget = SkeletonFile.Descendants("soundbank").Select(d => new XDocument(new XElement("soundbank", d)));
            int SBCounter = 0;
            foreach (var TargetNugget in SoundbankNugget)
            {
                SBCounter = SBCounter + 1;
                TargetNugget.Save(@"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\PatchData\Nuggets\SoundbankNugget_0" + SBCounter + ".xml");
                TargetNugget.Remove();
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

        public static bool ElementExists(XDocument DocumentName, string ValueName)
        {
            var TestNugget = DocumentName.Elements(ValueName);
            if (TestNugget == null)
            {
                return false;
            }
            //var EventName = DocumentName.SelectSingleNode//Descendants("eventcategory").Select(d => new XDocument(new XElement("eventcategory", d)));
            else
            {
                return true;
            }
        }
    }
}
