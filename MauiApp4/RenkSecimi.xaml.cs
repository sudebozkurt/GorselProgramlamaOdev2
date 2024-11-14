using Microsoft.Maui.Controls;

namespace MauiApp4
{
    public partial class RenkSecimi : ContentPage
    {
        public RenkSecimi()
        {
            InitializeComponent();
            UpdateColor();
        }

        private void OnSliderValueChanged(object sender, ValueChangedEventArgs e)
        {
            UpdateColor();
        }

        private void UpdateColor()
        {
            int red = (int)RedSlider.Value;
            int green = (int)GreenSlider.Value;
            int blue = (int)BlueSlider.Value;

            RedValueLabel.Text = $"Red: {red}";
            GreenValueLabel.Text = $"Green: {green}";
            BlueValueLabel.Text = $"Blue: {blue}";

            string hexColor = $"#{red:X2}{green:X2}{blue:X2}";
            HexCodeEntry.Text = hexColor;
            ColorPreview.BackgroundColor = Color.FromRgb(red, green, blue);
        }

        private void OnRandomColorButtonClicked(object sender, EventArgs e)
        {
            Random random = new Random();
            RedSlider.Value = random.Next(0, 256);
            GreenSlider.Value = random.Next(0, 256);
            BlueSlider.Value = random.Next(0, 256);
        }
    }
}
