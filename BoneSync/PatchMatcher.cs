using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiffMatchPatch;
using System.Xml.Linq;
using System.Xml;

namespace BoneSync
{
    class PatchMatcher
    {
        public static void MatchAndPatch(XDocument ParentPatch, XDocument PatchChild)
        {
            string PARENT = ParentPatch.Document.ToString(SaveOptions.DisableFormatting);
            string CHILD = PatchChild.Document.ToString(SaveOptions.DisableFormatting);

            diff_match_patch MatchPatchDocs = new diff_match_patch();
            MatchPatchDocs.Diff_Timeout = 0;
            //----------------------------------------------------------------------
            //List<Patch> MATCH = match_main(text, pattern, loc)
            //for (int i = 0; i < Patch.Count; i++)
            //{
            //    XMLParse.WritePatch(MATCH[i]);
            //}
        }
    }
}
