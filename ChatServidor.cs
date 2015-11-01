using System;
using System.Net.Sockets;
using System.Net;
using System.Collections;
using System.Threading;
using System.IO;

namespace Chat
{
	class Recebedor {
		private StreamReader sr;
		private IList array;

		public Recebedor(StreamReader sr, IList array) {
			this.sr = sr;
			this.array = array;
		}

		public void Run() {
			string l;
			while ((l = sr.ReadLine ()) != null) {
				foreach (var i in this.array) {
					StreamWriter sw = i as StreamWriter;
					Console.Write ("Recebido: " + l);
					sw.WriteLine (l);
					sw.Flush ();
					Console.WriteLine (l);
				}
			}			
		}
	}

	class Servidor {
		private int porta = 9999;
		private IPAddress ipAddress;
		private TcpListener listener;
		private ArrayList clients = new ArrayList();

		public Servidor() {
			Console.WriteLine ("Iniciando servidor");
			ipAddress = Dns.GetHostEntry("localhost").AddressList[0];
			listener = new TcpListener (ipAddress, porta);
			listener.Start ();
		}

		public void Run() {
			while (true) {
				TcpClient client = listener.AcceptTcpClient ();
				Stream s = client.GetStream ();
				clients.Add (new StreamWriter(s));

				Recebedor r = new Recebedor (new StreamReader(s), clients);
				Thread t = new Thread (r.Run);
				t.Start ();
			}	
		}
	}

	class MainClass
	{
		public static void Main (string[] args)
		{
			Servidor s = new Servidor ();
			Thread t = new Thread (s.Run);
			t.Start ();
		}
	}
}

