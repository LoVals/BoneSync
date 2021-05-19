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
            var xDoc = XDocument.Parse(SoundSoulsXML); // loading source xml
            var xmls = xDoc.Root.Elements().ToArray(); // split into elements

            for (int i = 0; i < xmls.Length; i++)
            {
                // write each element into different file
                using (var file = File.CreateText(string.Format("SounSouls_Split_{0}.xml", i + 1)))
                {
                    file.Write(xmls[i].ToString());
                }
            }
        }
    }
}
