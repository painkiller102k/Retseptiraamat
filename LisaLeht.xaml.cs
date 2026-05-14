using Microsoft.Maui.Media;

namespace Retseptiraamat;

public partial class LisaLeht : ContentPage
{
    string pildiTee;

    public LisaLeht()
    {
        InitializeComponent();
    }

    private async void ValiPilt_Clicked(object sender, EventArgs e)
    {
        try
        {
            var result = await MediaPicker.PickPhotoAsync();

            if (result != null)
            {
                pildiTee = result.FullPath;
                previewImage.Source = ImageSource.FromFile(pildiTee);
            }
        }
        catch
        {
            await DisplayAlert("Viga", "Pildi valimine ebaõnnestus", "OK");
        }
    }

    private async void Salvesta_Clicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(nimiEntry.Text) ||
            string.IsNullOrWhiteSpace(kategooriaEntry.Text))
        {
            await DisplayAlert("Viga", "Täida kõik väljad", "OK");
            return;
        }

        var list = FailiHaldur.Loe();

        list.Add(new Retsept
        {
            Nimi = nimiEntry.Text.Trim(),
            Kategooria = kategooriaEntry.Text.Trim(),
            PildiLink = pildiTee ?? ""
        });

        FailiHaldur.SalvestaKõik(list);

        nimiEntry.Text = "";
        kategooriaEntry.Text = "";
        pildiTee = null;
        previewImage.Source = null;

        await DisplayAlert("OK", "Retsept lisatud", "OK");
    }
}