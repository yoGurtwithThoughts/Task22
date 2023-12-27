using System;
using System.Collections.Generic;
using System.Linq;
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
using NAudio.Wave;
using WaveIn = NAudio.Wave.WaveIn;



namespace Task22
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private NAudio.Wave.WaveIn waveIn;
        private WaveFileWriter waveWriter;
        private string outputFilename = "output.wav";
        
        public MainWindow()
        {
            InitializeComponent();
        }



        private void btA_Click(object sender, RoutedEventArgs e)
        {
          waveIn = new WaveIn();
        waveIn.DataAvailable += WaveIn_DataAvailable;
        waveWriter = new WaveFileWriter(outputFilename, waveIn.WaveFormat);
        waveIn.StartRecording();

        }
        private void WaveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            if (waveWriter == null) return;

            waveWriter.Write(e.Buffer, 0, e.BytesRecorded);
            waveWriter.Flush();
        }
     
        
        private void btSA_Click(object sender, RoutedEventArgs e)
        {
            waveIn.StopRecording();
            waveIn.Dispose();
            waveIn = null;
            waveWriter.Dispose();
            waveWriter = null;

        }

        private void btAA_Click(object sender, RoutedEventArgs e)
        {
            var waveOut = new WaveOut();
            var waveFileReader = new WaveFileReader(outputFilename);
            waveOut.Init(waveFileReader);
            waveOut.Play();
        }

       
    }
}
