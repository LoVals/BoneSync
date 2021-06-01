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

namespace BoneSync_02
{
    class GenerateChild
    {
        public static void Execute()
        {
            //What needs to be done:

            //>>    Detect what has changed
            //>>    Regenerate the XML File

            //------------------------------------------------------------------------------
            // LOAD PARENT FILE INTO MEMORY
            var XParent = XDocument.Load(@"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\V2.0\TestFiles\XML\Parent.Xml");
            // LOAD ALL CHILDREN NAMES
            string SoundBonesDir = @"C:\Users\lvalsassina\Documents\GitHub\BoneSync\BoneSync\V2.0\TestFiles\FDP\Children";
            var FDPFILE = Directory.GetFiles(SoundBonesDir, "*", SearchOption.TopDirectoryOnly);
            foreach (string file in FDPFILE)
            {
                string BoneName = Path.GetFileNameWithoutExtension(file);
                //EVENTGROUP
                //search the eventgroup containing the element with that value
                //I am assuming the user will not change the Root Folder's name
                XElement EventGroupNode = XParent.Root.Elements("eventgroup").Where(e => e.Element("name").Value == BoneName).FirstOrDefault();
                //currently not working - unsure why
                Console.WriteLine("The Element found is:");
                Console.WriteLine(EventGroupNode);
                Console.ReadLine();
            }

        }
    }
}
