using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangXinTest
{
    public class Class1
    {
        public static string str { get; set; }
        public Class1(string str)
        {
            Class1.str = str;
        }

        public static void GetDBContext()
        {
            Console.WriteLine(str);
        }
    }
}
