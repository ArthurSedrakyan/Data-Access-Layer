using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment11
{
    class Program
    {
        static void Main(string[] args)
        {
            
            MyDatabase<Entity> en = new MyDatabase<Entity>();
            //KeyValuePair<String, object> valuePair = new KeyValuePair<string, object>();
            string code = @"C:\Users\User\Source\Repos\Data-Access-Layer\My Database\query.txt";
            var tuy= en.GetData(code,null);
           
            Console.ReadLine();
         }
    }
}
