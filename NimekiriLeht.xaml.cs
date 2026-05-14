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
        try
        {
            kõikRetseptid = FailiHaldur.Loe();

            kategooriaPicker.ItemsSource = kõikRetseptid
                .Select(x => x.Kategooria)
                .Distinct()
                .ToList();

            Näita(kõikRetseptid);
        }
        catch
        {
            collectionView.ItemsSource = new List<RetseptiKategooria>();
        }
    }

    void Näita(List<Retsept> list)
    {
        var grouped = list
            .GroupBy(r => r.Kategooria)
            .Select(g => new RetseptiKategooria(g.Key, g))
            .ToList();

        collectionView.ItemsSource = grouped;
    }

    private void Filtreeri_Clicked(object sender, EventArgs e)
    {
        if (kategooriaPicker.SelectedItem == null)
            return;

        string valitud = kategooriaPicker.SelectedItem.ToString();

        var filtered = kõikRetseptid
            .Where(r => r.Kategooria == valitud)
            .ToList();

        Näita(filtered);
    }

    private void NaitaKoik_Clicked(object sender, EventArgs e)
    {
        Näita(kõikRetseptid);
    }

    private async void Kustuta_Clicked(object sender, EventArgs e)
    {
        try
        {
            var btn = sender as Button;
            var retsept = btn?.CommandParameter as Retsept;

            if (retsept == null)
                return;

            bool vastus = await DisplayAlert("Kustuta",
                "Kas oled kindel?",
                "Jah",
                "Ei");

            if (!vastus)
                return;

            kõikRetseptid.Remove(retsept);
            FailiHaldur.SalvestaKõik(kõikRetseptid);

            Lae();
        }
        catch
        {
            await DisplayAlert("Viga", "Kustutamine ebaõnnestus", "OK");
        }
    }
}