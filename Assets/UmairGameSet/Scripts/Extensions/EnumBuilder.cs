using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class EnumBuilder : MonoBehaviour
{
    public static void CreateEnumFile (string Name, string nameSpace, string copyPath, List<string> itemTypes = null)
    {
        if (File.Exists (copyPath))
            File.Delete (copyPath);
        using (StreamWriter outfile = new StreamWriter (copyPath))
        {
            outfile.WriteLine ("namespace " + nameSpace + " {");
            outfile.WriteLine ("     public enum " + Name + " {");
            if (itemTypes != null)
                for (int i = 0; i < itemTypes.Count; i++)
                {
                    outfile.WriteLine ("       " + itemTypes[i] + "=" + i + ( i == itemTypes.Count - 1 ? "" : "," ));
                }
            outfile.WriteLine ("     }");
            outfile.WriteLine ("}");
        }

    }
}
