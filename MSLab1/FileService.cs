using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSLab1
{
    public class FileService : IFileService
    {
        public List<string> ReadFromFile(string path)
        {
            List<string> fileConctent = new List<string>();
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        fileConctent.Add(line);
                    }
                }
            }
            return fileConctent;
        }
    }
}
