using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static WpfApp1.MainWindow;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            int currentVolumeValue = GetSystemVolumeNormalized(VolumeUnit.Scalar);
            MainVolumeText.Text = currentVolumeValue.ToString();
            MainSlider.Value = currentVolumeValue;
        }

        public enum VolumeUnit
        {
            Decibel,
            Scalar
        }

        public static int GetSystemVolumeNormalized(VolumeUnit volumeUnit)
        {
            float volumeDecibel = GetSystemVolume(VolumeUnit.Scalar);
            return Convert.ToInt32(volumeDecibel * 100);
        }

        [DllImport("VolumeControlPlugin")]
        public static extern float GetSystemVolume(VolumeUnit volumeUnit);

        [DllImport("VolumeControlPlugin")]
        public static extern void SetSystemVolume(double newVolume, VolumeUnit volumeUnit);

        private void MainSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            double sliderValue = Convert.ToDouble(MainSlider.Value / 100);
            SetSystemVolume(sliderValue, VolumeUnit.Scalar);
            MainVolumeText.Text = MainSlider.Value.ToString();
        }
    }
}
