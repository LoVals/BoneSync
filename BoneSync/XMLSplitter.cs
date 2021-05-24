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

            //----------------------------------------------------------------------------------------------------------------------------------------
            //EVENTCATEGORYSPLIT
            //----------------------------------------------------------------------------------------------------------------------------------------
            
            var EventCategoryNugget = SkeletonFile.Descendants("eventcategory").Select(d => new XDocument(new XElement("eventcategory", d)));
            int EvCounter = 0;
            EventNuggetStart:
            foreach (var TargetNugget in EventCategoryNugget)
            {
                EvCounter = EvCounter + 1;
                TargetNugget.Save(@"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\PatchData\Nuggets\EventCategoryNugget_0" + EvCounter + ".xml");
                break;
            }
            XElement DeletingCache = XElement.Load(@"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\PatchData\SoundSouls.xml");
            XElement DeleteME = DeletingCache.Element("eventcategory");
            DeleteME.Remove();
            DeletingCache.Save(@"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\PatchData\SoundSouls.xml");
            //Overwriting the file is the key to get only what you need
            SkeletonFile = XDocument.Load(BackupFileLocation);
            EventCategoryNugget = SkeletonFile.Descendants("eventcategory").Select(d => new XDocument(new XElement("eventcategory", d)));
            if (EvCounter != 5)
            {
                goto EventNuggetStart;
            }

            //----------------------------------------------------------------------------------------------------------------------------------------
            //SOUNDDEFFOLDER
            //----------------------------------------------------------------------------------------------------------------------------------------
            
            var SoundDefFolderNugget = SkeletonFile.Descendants("sounddeffolder").Select(d => new XDocument(new XElement("sounddeffolder", d)));
            int SDFCounter = 0;
            foreach (var TargetNugget in SoundDefFolderNugget)
            {
                SDFCounter = SDFCounter + 1;
                TargetNugget.Save(@"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\PatchData\Nuggets\SoundDefFolderNugget_0" + SDFCounter + ".xml");
                break;
            }
            DeletingCache = XElement.Load(@"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\PatchData\SoundSouls.xml");
            DeleteME = DeletingCache.Element("sounddeffolder");
            DeleteME.Remove();
            DeletingCache.Save(@"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\PatchData\SoundSouls.xml");
            //Overwriting the file is the key to get only what you need
            SkeletonFile = XDocument.Load(BackupFileLocation);
            EventCategoryNugget = SkeletonFile.Descendants("sounddeffolder").Select(d => new XDocument(new XElement("sounddeffolder", d)));
            if (EvCounter != 5)
            {
                goto EventNuggetStart;
            }

            //----------------------------------------------------------------------------------------------------------------------------------------
            //EVENTGROUP
            //----------------------------------------------------------------------------------------------------------------------------------------

            var EventGroupNugget = SkeletonFile.Descendants("eventgroup").Select(d => new XDocument(new XElement("eventgroup", d)));
            int EGCounter = 0;
            foreach (var TargetNugget in EventGroupNugget)
            {
                EGCounter = EGCounter + 1;
                TargetNugget.Save(@"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\PatchData\Nuggets\EventGroupNugget_0" + EGCounter + ".xml");
                break;
            }
            DeletingCache = XElement.Load(@"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\PatchData\SoundSouls.xml");
            DeleteME = DeletingCache.Element("eventgroup");
            DeleteME.Remove();
            DeletingCache.Save(@"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\PatchData\SoundSouls.xml");

            //----------------------------------------------------------------------------------------------------------------------------------------
            //SOUNDBANK
            //----------------------------------------------------------------------------------------------------------------------------------------

            var SoundbankNugget = SkeletonFile.Descendants("soundbank").Select(d => new XDocument(new XElement("soundbank", d)));
            int SBCounter = 0;
            foreach (var TargetNugget in SoundbankNugget)
            {
                SBCounter = SBCounter + 1;
                TargetNugget.Save(@"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\PatchData\Nuggets\SoundbankNugget_0" + SBCounter + ".xml");
                break;
            }
            DeletingCache = XElement.Load(@"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\PatchData\SoundSouls.xml");
            DeleteME = DeletingCache.Element("soundbank");
            DeleteME.Remove();
            DeletingCache.Save(@"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\PatchData\SoundSouls.xml");
        }

        //----------------------------------------------------------------------------------------------------------------------------------------
        //This is likely very inefficient - I am sure there are better ways to do this shit
        //----------------------------------------------------------------------------------------------------------------------------------------

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
