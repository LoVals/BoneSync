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
    class FdpToXml
        //CLASS RESPONSIBLE FOR CONVERTING THE FDP FILES TO XML
    {
        public static void Execute()
        //Copies over the PARENT project as XML
        {
            var SoundSoulsFDP = Directory.GetFiles(@"G:\BoneSync\BoneSync\BoneSync\V2.0\TestFiles\FDP", "Parent.fdp", SearchOption.TopDirectoryOnly);
            File.Delete(@"G:\BoneSync\BoneSync\BoneSync\V2.0\TestFiles\XML\Parent.xml");
            File.Copy(@"G:\BoneSync\BoneSync\BoneSync\V2.0\TestFiles\FDP\Parent.fdp", @"G:\BoneSync\BoneSync\BoneSync\V2.0\TestFiles\XML\Parent.fdp");
            File.Move(@"G:\BoneSync\BoneSync\BoneSync\V2.0\TestFiles\XML\Parent.fdp", @"G:\BoneSync\BoneSync\BoneSync\V2.0\TestFiles\XML\Parent.xml");
        }
    }
}
