using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Conection
{
    public delegate void TcpJsonReceivedEventHandler(object source, EventArgJson e);
    public class TcpServer
    {
        public event TcpJsonReceivedEventHandler TcpJsonReceived;

        private TcpListener serverSocket;
        private TcpClient clientSocket = default(TcpClient);
        private Thread _thread;
        private int port;
        private string json = null;

        public string _json
        {
            get { return json; }
        }


        public TcpServer(int port)
        {
            this.port = port;
            _thread = new Thread(conect);
            _thread.IsBackground = true;
            _thread.Start();
        }
        ~TcpServer()
        {

        }

        private void conect()
        {
            try
            {
                serverSocket = new TcpListener(port);

                serverSocket.Start();


                while (true)
                {
                    json = null;
                    Thread.Sleep(10);
                    clientSocket = serverSocket.AcceptTcpClient();
                    NetworkStream networkStream = clientSocket.GetStream();
                    byte[] bytesFrom = new byte[65536];
                    networkStream.Read(bytesFrom, 0, (int)clientSocket.ReceiveBufferSize);
                    json = System.Text.Encoding.ASCII.GetString(bytesFrom).Replace("\0", String.Empty).Trim();
                    if (!String.IsNullOrWhiteSpace(json))
                        OnTcpJsonReceived(new EventArgJson(json));

                }
            }
            catch (Exception e)
            {
                throw new Exception("nao possivel conectar",e);

            }
            finally
            {
                clientSocket.Close();
                serverSocket.Stop();
                _thread.Abort();
            }
        }


        public virtual void OnTcpJsonReceived(EventArgJson e)
        {
            if (TcpJsonReceived != null)
                TcpJsonReceived(this, e);
        }

    }
}
