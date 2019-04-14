using SendInput;
using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using System.Windows.Media.Imaging;
using MessagingToolkit.QRCode.Codec;
using System.IO;
using System.Drawing.Imaging;
using Conection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Sockets;
using Model;
using System.Collections;
using System.Collections.Generic;
using ToggleSwitch;
using Timer = System.Timers.Timer;

namespace view
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>


    public partial class MainWindow : Window
    {
        private const int PORT = 6666;
        TcpServer _tcpServer;
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();

            setQrCode();
            try
            {
                _tcpServer = new TcpServer(PORT);
                _tcpServer.TcpJsonReceived += (new TcpJsonReceivedEventHandler(tcpJsonReceived));


            }
            catch (Exception ex)
            {

                MessageBox.Show("erro ao iniciar o servidor" + ex.Message);
            }

        }

        public void setQrCode()
        {
            IPHostEntry host;
            string localIP = null;
            host = Dns.GetHostEntry(Dns.GetHostName());
            localIP = host.AddressList[1].ToString();

            if (localIP == null)
                throw new Exception("local ip not found!");

            JObject json = JObject.FromObject(new
            {
                ip = localIP,
                port = PORT.ToString(),
            });


            QRCodeEncoder encoder = new QRCodeEncoder();

            using (var bitmap = encoder.Encode(json.ToString().Trim()))
            using (var stream = new MemoryStream())
            {
                bitmap.Save(stream, ImageFormat.Png);

                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                stream.Seek(0, SeekOrigin.Begin);
                bi.StreamSource = stream;
                bi.CacheOption = BitmapCacheOption.OnLoad;
                bi.EndInit();
                imQrcode.Source = bi;
            }

        }

        public void keyVenda(List<Product> lista)
        {
            foreach (Product item in lista)
            {
                lvLista.Items.Add(new SendVkey().SendPressKey(item.Codigo));

                lvLista.Items.Add(new SendVkey().SendPressKey(SendInput.keymaps.WindowsVirtualKey.VK_TAB));
                lvLista.Items.Add(new SendVkey().SendPressKey(item.Qtde.ToString()));
                if (!String.IsNullOrEmpty(item.CodigoLote))
                {
                    Thread.Sleep(500);
                    lvLista.Items.Add(new SendVkey().SendPressKey(SendInput.keymaps.WindowsVirtualKey.VK_TAB));
                    lvLista.Items.Add(new SendVkey().SendPressKey(SendInput.keymaps.WindowsVirtualKey.VK_TAB));
                    lvLista.Items.Add(new SendVkey().SendPressKey(SendInput.keymaps.WindowsVirtualKey.VK_TAB));
                    lvLista.Items.Add(new SendVkey().SendPressKey(item.CodigoLote));
                }
                lvLista.Items.Add(new SendVkey().SendPressKey(SendInput.keymaps.WindowsVirtualKey.VK_RETURN));

            }
        }
        public void keyBarcode(List<Product> lista)
        {
            foreach (Product item in lista)
            {
                Thread.Sleep(300);
                lvLista.Items.Add(new SendVkey().SendPressKey(item.Codigo));
                lvLista.Items.Add(new SendVkey().SendPressKey(SendInput.keymaps.WindowsVirtualKey.VK_TAB));
                lvLista.Items.Add(new SendVkey().SendPressKey("1"));
                Thread.Sleep(500);
                lvLista.Items.Add(new SendVkey().SendPressKey(SendInput.keymaps.WindowsVirtualKey.VK_F2));
                Thread.Sleep(700);
            }
        }

        private void tcpJsonReceived(object source, EventArgJson e)
        {

            Dispatcher.BeginInvoke(new Action(delegate
            {
                try
                {
                    Ocupar();
                    // testar(); 
                    List<Product> lista = JsonConvert.DeserializeObject<List<Product>>(e.Json);
                    txtNome.Text = e.Json;
                    if (tglVenda.IsChecked)
                        keyBarcode(lista);
                    else
                        keyVenda(lista);


                }
                catch (Exception ex)
                {
                    MessageBox.Show("erro ao iniciar o servidor" + ex.Message);
                }

            }));

        }

        private void Ocupar()
        {
            imBusy.Visibility = Visibility.Visible;
       
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0,0,1);
            dispatcherTimer.Start();
            
        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            imBusy.Visibility = Visibility.Hidden;
            dispatcherTimer.Stop();
        }
        private void btTestar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void testar()
        {
            Thread.Sleep(10);
            lvLista.Items.Add(new SendVkey().SendPressKey("747752870474"));

            lvLista.Items.Add(new SendVkey().SendPressKey(SendInput.keymaps.WindowsVirtualKey.VK_TAB));
            lvLista.Items.Add(new SendVkey().SendPressKey('2'));
            lvLista.Items.Add(new SendVkey().SendPressKey(SendInput.keymaps.WindowsVirtualKey.VK_RETURN));

            lvLista.Items.Add(new SendVkey().SendPressKey("42526907388"));

            lvLista.Items.Add(new SendVkey().SendPressKey(SendInput.keymaps.WindowsVirtualKey.VK_TAB));
            lvLista.Items.Add(new SendVkey().SendPressKey('5'));
            lvLista.Items.Add(new SendVkey().SendPressKey(SendInput.keymaps.WindowsVirtualKey.VK_RETURN));

            lvLista.Items.Add(new SendVkey().SendPressKey("7891112113732"));

            lvLista.Items.Add(new SendVkey().SendPressKey(SendInput.keymaps.WindowsVirtualKey.VK_TAB));
            lvLista.Items.Add(new SendVkey().SendPressKey('3'));
            lvLista.Items.Add(new SendVkey().SendPressKey(SendInput.keymaps.WindowsVirtualKey.VK_RETURN));

            lvLista.Items.Add(new SendVkey().SendPressKey("7891112112025"));

            lvLista.Items.Add(new SendVkey().SendPressKey(SendInput.keymaps.WindowsVirtualKey.VK_TAB));
            lvLista.Items.Add(new SendVkey().SendPressKey('1'));
            lvLista.Items.Add(new SendVkey().SendPressKey(SendInput.keymaps.WindowsVirtualKey.VK_RETURN));

            txtNome.Text = Marshal.GetLastWin32Error().ToString(); ;
        }


    }


}
