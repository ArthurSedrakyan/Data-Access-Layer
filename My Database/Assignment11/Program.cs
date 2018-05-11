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
            KeyValuePair<String, Entity> valuePair = new KeyValuePair<string, Entity>();
            string code = "select FirstName,LastName,EmailPromotion from Person.Person";
            var tuy= en.GetData(code,valuePair);
            Console.ReadLine();
         }
    }
}
