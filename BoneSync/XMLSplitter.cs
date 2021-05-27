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
            string BackupFileLocation = @"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\PatchData\SoundSoulsSplitCache.xml";
            File.Copy(SoundSoulsXML, BackupFileLocation);
            XDocument SkeletonFile = XDocument.Load(BackupFileLocation);
            string ParentFile;

            //----------------------------------------------------------------------------------------------------------------------------------------
            //EVENTCATEGORYSPLIT -- MULTIPLE_ MERGED
            //----------------------------------------------------------------------------------------------------------------------------------------

            var EventCategoryNugget = SkeletonFile.Descendants("eventcategory").Select(d => new XDocument(new XElement("eventcategory", d)));
            int EvCounter = 0;
            ParentFile = @"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\PatchData\Nuggets\EventCategoryNuggets.xml";
        EventNuggetStart:
            foreach (var TargetNugget in EventCategoryNugget)
            {
                EvCounter = EvCounter + 1;
                if (EvCounter == 1)
                {
                    TargetNugget.Save(@"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\PatchData\Nuggets\EventCategoryNuggets.xml");
                }
                else
                {
                    TargetNugget.Save(@"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\PatchData\Nuggets\EventCategoryNugget_0" + EvCounter + ".xml");
                    string NuggetToMerge = @"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\PatchData\Nuggets\EventCategoryNugget_0" + EvCounter + ".xml";
                    XMLSplitter.MergeNuggets(ParentFile, NuggetToMerge);
                }
                
                break;
            }
            XElement DeletingCache = XElement.Load(@"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\PatchData\SoundSoulsSplitCache.xml");
            XElement DeleteME = DeletingCache.Element("eventcategory");
            DeleteME.Remove();
            DeletingCache.Save(@"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\PatchData\SoundSoulsSplitCache.xml");
            //Overwriting the file is the key to get only what you need
            SkeletonFile = XDocument.Load(BackupFileLocation);
            EventCategoryNugget = SkeletonFile.Descendants("eventcategory").Select(d => new XDocument(new XElement("eventcategory", d)));
            if (EvCounter != 5)
            {
                goto EventNuggetStart;
            }

            //----------------------------------------------------------------------------------------------------------------------------------------
            //SOUNDDEFFOLDER -- THERE'S ALWAYS GONNA BE ONE
            //----------------------------------------------------------------------------------------------------------------------------------------

            var SoundDefFolderNugget = SkeletonFile.Descendants("sounddeffolder").Select(d => new XDocument(new XElement("sounddeffolder", d)));
            int SDFCounter = 0;
            foreach (var TargetNugget in SoundDefFolderNugget)
            {
                SDFCounter = SDFCounter + 1;
                TargetNugget.Save(@"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\PatchData\Nuggets\SoundDefFolderNugget.xml");
                break;
            }
            DeletingCache = XElement.Load(@"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\PatchData\SoundSoulsSplitCache.xml");
            DeleteME = DeletingCache.Element("sounddeffolder");
            DeleteME.Remove();
            DeletingCache.Save(@"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\PatchData\SoundSoulsSplitCache.xml");
            //Overwriting the file is the key to get only what you need
            SkeletonFile = XDocument.Load(BackupFileLocation);
            EventCategoryNugget = SkeletonFile.Descendants("sounddeffolder").Select(d => new XDocument(new XElement("sounddeffolder", d)));

            //----------------------------------------------------------------------------------------------------------------------------------------
            //EVENTGROUP -- THERE'S ALWAYS GONNA BE ONE
            //----------------------------------------------------------------------------------------------------------------------------------------

            var EventGroupNugget = SkeletonFile.Descendants("eventgroup").Select(d => new XDocument(new XElement("eventgroup", d)));
            int EGCounter = 0;
            foreach (var TargetNugget in EventGroupNugget)
            {
                EGCounter = EGCounter + 1;
                TargetNugget.Save(@"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\PatchData\Nuggets\EventGroupNugget.xml");
                break;
            }
            DeletingCache = XElement.Load(@"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\PatchData\SoundSoulsSplitCache.xml");
            DeleteME = DeletingCache.Element("eventgroup");
            DeleteME.Remove();
            DeletingCache.Save(@"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\PatchData\SoundSoulsSplitCache.xml");

            //----------------------------------------------------------------------------------------------------------------------------------------
            //SOUNDBANKS -- MULTIPLE _ MERGED
            //----------------------------------------------------------------------------------------------------------------------------------------

            var SoundbankNugget = SkeletonFile.Descendants("soundbank").Select(d => new XDocument(new XElement("soundbank", d)));
            int SBCounter = 0;
            ParentFile = @"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\PatchData\Nuggets\SoundbanksNugget.xml";
            foreach (var TargetNugget in SoundbankNugget)
            {
                SBCounter = SBCounter + 1;
                if (SBCounter == 1)
                {
                    TargetNugget.Save(@"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\PatchData\Nuggets\SoundbanksNugget.xml");  
                }
                else
                {
                    TargetNugget.Save(@"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\PatchData\Nuggets\SoundbankNugget_0" + SBCounter + ".xml");
                    string NuggetToMerge = @"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\PatchData\Nuggets\SoundbankNugget_0" + SBCounter + ".xml";
                    XMLSplitter.MergeNuggets(ParentFile, NuggetToMerge);
                }                
                DeletingCache = XElement.Load(@"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\PatchData\SoundSoulsSplitCache.xml");
                DeleteME = DeletingCache.Element("soundbank");
                DeleteME.Remove();
                DeletingCache.Save(@"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\PatchData\SoundSoulsSplitCache.xml");
            }
            // HACKY AF BUT IT WORKS
        }

        public static void WipeOldNuggets()
        {
            string[] filePaths = Directory.GetFiles(@"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\PatchData\Nuggets\");
            foreach (string filePath in filePaths)
            {
                File.Delete(filePath);
            }
        }

        public static void MergeNuggets(string ParentName, string NuggetName)
        {
            XElement xFileRoot = XElement.Load(ParentName);
            XElement xFileChild = XElement.Load(NuggetName);
            xFileRoot.Add(xFileChild);
            xFileRoot.Save(ParentName);
            File.Delete(NuggetName);
        }

        public static void ChildSplit(string ChildDir, string ChildName)
        {
            string ParentFile;
            string BackupFileLocation = @"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\PatchData\ChildData"+ChildName+"_Bkp.xml";
            File.Copy(ChildDir, BackupFileLocation);
            XDocument ChildFile = XDocument.Load(BackupFileLocation);
            //----------------------------------------------------------------------------------------------------------------------------------------
            //EVENTCATEGORYSPLIT for Child elements
            //----------------------------------------------------------------------------------------------------------------------------------------

            var EventCategoryNugget = ChildFile.Descendants("eventcategory").Select(d => new XDocument(new XElement("eventcategory", d)));
            int EvCounter = 0;
        EventNuggetStart:
            foreach (var TargetNugget in EventCategoryNugget)
            {
                EvCounter = EvCounter + 1;
                if (EvCounter ==1)
                {
                    TargetNugget.Save(@"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\PatchData\Nuggets\ChildData\"+ChildName+"_EventCategoryNuggets.xml");
                }
                else 
                {
                    TargetNugget.Save(@"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\PatchData\Nuggets\ChildData\" + ChildName + "_EventCategoryNugget_0" + EvCounter + ".xml");
                    string NuggetToMerge = @"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\PatchData\Nuggets\ChildData\" + ChildName + "_EventCategoryNugget_0" + EvCounter + ".xml";
                    ParentFile = @"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\PatchData\Nuggets\ChildData\"+ChildName+"_EventCategoryNuggets.xml";
                    XMLSplitter.MergeNuggets(ParentFile, NuggetToMerge);
                }
                break;
            }
            XElement DeletingCache = XElement.Load(@"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\PatchData\ChildData" + ChildName + "_Bkp.xml");
            XElement DeleteME = DeletingCache.Element("eventcategory");
            DeleteME.Remove();
            DeletingCache.Save(@"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\PatchData\ChildData" + ChildName + "_Bkp.xml");
            //Overwriting the file is the key to get only what you need
            ChildFile = XDocument.Load(BackupFileLocation);
            EventCategoryNugget = ChildFile.Descendants("eventcategory").Select(d => new XDocument(new XElement("eventcategory", d)));
            if (EvCounter != 5)
            {
                goto EventNuggetStart;
            }

            //----------------------------------------------------------------------------------------------------------------------------------------
            //SOUNDDEFFOLDER
            //----------------------------------------------------------------------------------------------------------------------------------------

            var SoundDefFolderNugget = ChildFile.Descendants("sounddeffolder").Select(d => new XDocument(new XElement("sounddeffolder", d)));
            foreach (var TargetNugget in SoundDefFolderNugget)
            {
                TargetNugget.Save(@"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\PatchData\Nuggets\ChildData\"+ ChildName + "_SoundDefFolderNugget.xml");
                break;
            }
            DeletingCache = XElement.Load(@"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\PatchData\ChildData" + ChildName + "_Bkp.xml");
            DeleteME = DeletingCache.Element("sounddeffolder");
            DeleteME.Remove();
            DeletingCache.Save(@"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\PatchData\ChildData" + ChildName + "_Bkp.xml");
            //Overwriting the file is the key to get only what you need
            ChildFile = XDocument.Load(BackupFileLocation);
            EventCategoryNugget = ChildFile.Descendants("sounddeffolder").Select(d => new XDocument(new XElement("sounddeffolder", d)));
            //----------------------------------------------------------------------------------------------------------------------------------------
            //EVENTGROUP
            //----------------------------------------------------------------------------------------------------------------------------------------

            var EventGroupNugget = ChildFile.Descendants("eventgroup").Select(d => new XDocument(new XElement("eventgroup", d)));
            foreach (var TargetNugget in EventGroupNugget)
            {
                TargetNugget.Save(@"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\PatchData\Nuggets\ChildData\" + ChildName + "_EventGroupNugget.xml");
                break;
            }
            DeletingCache = XElement.Load(@"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\PatchData\ChildData" + ChildName + "_Bkp.xml");
            DeleteME = DeletingCache.Element("eventgroup");
            DeleteME.Remove();
            DeletingCache.Save(@"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\PatchData\ChildData" + ChildName + "_Bkp.xml");

            //----------------------------------------------------------------------------------------------------------------------------------------
            //SOUNDBANKS
            //----------------------------------------------------------------------------------------------------------------------------------------

            var SoundbankNugget = ChildFile.Descendants("soundbank").Select(d => new XDocument(new XElement("soundbank", d)));
            int SBCounter = 0;
            ParentFile = @"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\PatchData\Nuggets\ChildData\" + ChildName + "SoundbanksNugget.xml";
            foreach (var TargetNugget in SoundbankNugget)
            {
                SBCounter = SBCounter + 1;
                if (SBCounter == 1)
                {
                    TargetNugget.Save(@"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\PatchData\Nuggets\ChildData\" + ChildName + "_SoundbanksNugget.xml");
                }
                else
                {
                    TargetNugget.Save(@"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\PatchData\Nuggets\"+ ChildName+"_SoundbankNugget_0" + SBCounter + ".xml");
                    string NuggetToMerge = @"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\PatchData\Nuggets\"+ ChildName+"_SoundbankNugget_0" + SBCounter + ".xml";
                    //XMLSplitter.MergeNuggets(NuggetToMerge);
                }
                DeletingCache = XElement.Load(@"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\PatchData\ChildData" + ChildName + "_Bkp.xml");
                DeleteME = DeletingCache.Element("soundbank");
                DeleteME.Remove();
                DeletingCache.Save(@"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\PatchData\ChildData" + ChildName + "_Bkp.xml");
            }
        } //This whole function might be useless - I could just diff the nuggets to the child
    }
}
