using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreatorUI
{
    public static class Utils
    {
        public static string FixLeftStr(string src, string symb, int count)
        {
            var len = src.Length;
            if(len<count)
            {
                var countsymb = count - len;
                for(int ic=0;ic<countsymb;ic++)
                {
                    src = symb + src;
                }
            }
            return src;
        }
    }
}
