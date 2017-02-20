using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication
{
    class Program
    {
        private string teststr;
        static void Main(string[] args)
        {

            Program p = new Program();
            Console.WriteLine(p.GetStr1("test1"));
            Console.WriteLine(p.GetStr2("test2"));
            Console.ReadKey();
        }

        public string GetStr1(string str)
        {
            this.teststr = str;
            return this.teststr;
        }
        public string GetStr2(string str)
        {
            teststr = str;
            return teststr;
        }
    }
}
