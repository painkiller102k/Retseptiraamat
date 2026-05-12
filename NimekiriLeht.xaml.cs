namespace Retseptiraamat;

public partial class NimekiriLeht : ContentPage
{
    List<Retsept> kõikRetseptid = new();

    public NimekiriLeht()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        Lae();
    }

    void Lae()
    {
        kõikRetseptid = FailiHaldur.Loe();

        var grupeeritud = kõikRetseptid
            .GroupBy(x => x.Kategooria)
            .Select(g => new RetseptiGrupp(g.Key, g))
            .ToList();

        listView.ItemsSource = grupeeritud;
    }

    private void Kustuta_Clicked(object sender, EventArgs e)
    {
        var menu = sender as MenuItem;
        var retsept = menu.BindingContext as Retsept;

        kõikRetseptid.Remove(retsept);

        FailiHaldur.KirjutaKogu(kõikRetseptid);

        Lae();
    }
}