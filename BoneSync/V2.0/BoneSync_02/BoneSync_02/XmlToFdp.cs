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
    class XmlToFdp
    {
        public static void Execute()
        //Copies over the GENRATED CHILDREN projects as FDP
        {
            var SoundSoulsFDP = Directory.GetFiles(@"G:\BoneSync\BoneSync\BoneSync\V2.0\TestFiles\FDP", "*.fdp", SearchOption.TopDirectoryOnly);
            File.Move(@"G:\BoneSync\BoneSync\BoneSync\V2.0\TestFiles\XML\Generated\Child1.xml", @"G:\BoneSync\BoneSync\BoneSync\V2.0\TestFiles\FDP\Child1.xml");
            File.Move(@"G:\BoneSync\BoneSync\BoneSync\V2.0\TestFiles\FDP\Child1.xml", @"G:\BoneSync\BoneSync\BoneSync\V2.0\TestFiles\XML\Child1.fdp");
        }
    }
}
