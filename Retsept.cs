using System.Collections.Generic;

namespace Retseptiraamat
{
    public class Retsept
    {
        public string Nimi { get; set; }
        public string Kategooria { get; set; }
        public string PildiLink { get; set; }
    }

    public class RetseptiGrupp : List<Retsept>
    {
        public string Nimetus { get; set; }

        public RetseptiGrupp(string nimetus, IEnumerable<Retsept> items)
            : base(items)
        {
            Nimetus = nimetus;
        }
    }
}