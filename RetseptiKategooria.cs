using System.Collections.ObjectModel;

namespace Retseptiraamat;

public class RetseptiKategooria : ObservableCollection<Retsept>
{
    public string Nimetus { get; set; }

    public RetseptiKategooria(string nimetus, IEnumerable<Retsept> items) : base(items)
    {
        Nimetus = nimetus;
    }
}