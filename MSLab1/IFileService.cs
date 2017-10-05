using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSLab1
{
    public interface IFileService
    {
        List<string> ReadFromFile(string path);
    }
}
