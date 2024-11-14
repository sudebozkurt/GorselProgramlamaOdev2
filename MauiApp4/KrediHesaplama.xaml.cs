namespace MauiApp4
{
    public partial class KrediHesaplama : ContentPage
    {
        public KrediHesaplama()
        {
            InitializeComponent();
        }

        private double kkdf;
        private double bsmv;

        // Kredi türü
        private void OnPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            switch (OptionsPicker.SelectedIndex)
            {
                case 0:
                    kkdf = 15;
                    bsmv = 10;
                    break;
                case 1:
                    kkdf = 0;
                    bsmv = 0;
                    break;
                case 2:
                    kkdf = 15;
                    bsmv = 5;
                    break;
                case 3:
                    kkdf = 0;
                    bsmv = 5;
                    break;
            }
            CalculateLoan();
        }

        private void OnVadeSliderValueChanged(object sender, ValueChangedEventArgs e)
        {
            VadeEntry.Text = e.NewValue.ToString("0");
            CalculateLoan();
        }

        private void OnVadeEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            if (double.TryParse(VadeEntry.Text, out double vade))
            {
                if (vade >= VadeSlider.Minimum && vade <= VadeSlider.Maximum)
                {
                    VadeSlider.Value = vade;
                    CalculateLoan();
                }
            }
        }

        private void CalculateLoan()
        {
            if (double.TryParse(KrediTutari.Text, out double kredi) &&
                double.TryParse(FaizOrani.Text, out double faiz) &&
                double.TryParse(VadeEntry.Text, out double vade) && vade > 0)
            {
                double brutFaiz = (faiz + (faiz * bsmv / 100) + (faiz * kkdf / 100)) / 100;
                double taksit = (Math.Pow(1 + brutFaiz, vade) * brutFaiz) / (Math.Pow(1 + brutFaiz, vade) - 1) * kredi;
                double toplamOdeme = taksit * vade;
                double toplamFaiz = toplamOdeme - kredi;

                TaksitLabel.Text = $"Aylık Taksit: {taksit:F2} TL";
                ToplamOdemeLabel.Text = $"Toplam Ödeme: {toplamOdeme:F2} TL";
                ToplamFaizLabel.Text = $"Toplam Faiz: {toplamFaiz:F2} TL";
            }
        }

        private void OnHesaplaButtonClicked(object sender, EventArgs e)
        {
            CalculateLoan();
        }
    }
}
