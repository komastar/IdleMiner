using System;

namespace Komastar.IdleMiner.Data
{
    public class DOPathAttribute : Attribute
    {
        public string Path { get; set; }

        public DOPathAttribute(string path)
        {
            Path = path;
        }
    }
}
