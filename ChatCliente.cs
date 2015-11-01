using System;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.IO;

namespace Chat
{
	public class Cliente
	{
		private TcpClient tc;
		private StreamWriter sw;
		private StreamReader sr;

		public Cliente ()
		{
			tc =  new TcpClient("localhost", 9999);
			Stream s = tc.GetStream ();
			sw = new StreamWriter (s);
			sr = new StreamReader (s);
		}

		public void Write(string l) {
			sw.WriteLine (l);
			sw.Flush ();
		}

		public void Run() {
			string l;
			while ((l = this.sr.ReadLine()) != null) {
				Console.WriteLine ("Recebido: " + l);
			}
		}

	}


	class MainClass
	{
		public static void Main (string[] args)
		{
			Cliente c = new Cliente ();
			Thread t = new Thread (c.Run);
			t.Start ();

			string l;
			while ((l = Console.ReadLine()) != null) {
				c.Write (l);
			}
		}
	}

}

