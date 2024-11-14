namespace MauiApp4;

public partial class VucutKitleIndeksi : ContentPage
{
	public VucutKitleIndeksi()
	{
		InitializeComponent();
	}

    private void vki()
    {
        if (double.TryParse(KgEntry.Text, out double kilo) && double.TryParse(BoyEntry.Text, out double boy))
        {
            boy = boy / 100;
            double vki = kilo / (boy * boy);
            string sonuc;

            if (vki < 16)
                sonuc = "Ýleri düzeyde zayýf";
            else if (vki >= 16 && vki <= 16.99)
                sonuc = "Orta düzeyde zayýf";
            else if (vki >= 17 && vki <= 18.49)
                sonuc = "Hafif düzeyde zayýf";
            else if (vki >= 18.50 && vki <= 24.9)
                sonuc = "Normal kilo";
            else if (vki >= 25 && vki <= 29.99)
                sonuc = "Fazla Kilolu";
            else if (vki >= 30 && vki <= 34.99)
                sonuc = "1. derece obez";
            else if (vki >= 35 && vki <= 39.99)
                sonuc = "2. derece obez";
            else
                sonuc = "3. derece obez";

            SonucLabel.Text = $"VKÝ Sonucu: {vki:F2}\n{sonuc}";
        }
        else
        {
            SonucLabel.Text = "Lütfen geçerli bir kilo ve boy girin!";
        }
    }


    private void OnTextChanged(object sender, TextChangedEventArgs e)
    {
        vki();
    }

    private void OnSliderValueChanged(object sender, ValueChangedEventArgs e)
    {
        if (sender == KgSlider)
        {
            KgEntry.Text=e.NewValue.ToString("F0");
        }
        else if (sender == boySlider)
        {
            BoyEntry.Text = e.NewValue.ToString("F0");
        }
        vki();

    }
}