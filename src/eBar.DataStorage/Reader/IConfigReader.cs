using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBar.DataStorage.Reader
{
    public interface IConfigReader
    {
        public string GetConnectionString();
    }
}
