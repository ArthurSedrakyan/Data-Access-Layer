using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment11
{
    public interface IMyDatabase<T>
    {
        IEnumerable<T> GetData(string code, KeyValuePair<string, object>[] parameters);
    }
}
