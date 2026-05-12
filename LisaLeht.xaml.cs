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
                await DisplayAlert("Valitud", "Pilt lisatud!", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Viga", ex.Message, "OK");
        }
    }

    private async void Salvesta_Clicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(nimiEntry.Text) ||
            string.IsNullOrWhiteSpace(kategooriaEntry.Text))
        {
            await DisplayAlert("Viga", "Täida kõik väljad!", "OK");
            return;
        }

        if (string.IsNullOrWhiteSpace(pildiTee))
        {
            await DisplayAlert("Viga", "Vali kõigepealt pilt telefonist!", "OK");
            return;
        }

        FailiHaldur.Salvesta(
            nimiEntry.Text.Trim(),
            kategooriaEntry.Text.Trim(),
            pildiTee);

        nimiEntry.Text = "";
        kategooriaEntry.Text = "";
        pildiTee = null;

        await DisplayAlert("OK", "Retsept salvestatud!", "OK");
    }
}