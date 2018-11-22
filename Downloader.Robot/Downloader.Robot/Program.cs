using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Downloader.Robot
{
    class Program
    {
        private static string User { get; set; }
        private static string Password { get; set; }

        static void Main(string[] args)
        {
            Log("Etapa 1 - Verificando se arquivo existe");
            if (!File.Exists(@"Credenciais.txt"))
            {
                throw new Exception("Deu ruim");
            }

            Log("Etapa 2 - Abrindo arquivo");
            var credenciais = File.ReadAllLines(@"Credenciais.txt").ToArray();

            Log("Etapa 3 - Validando conteúdo do arquivo");
            if (credenciais.Length == 2)
            {
                User = credenciais[0];
                Password = credenciais[1];
            }
            else
            {
                throw new Exception("Arquivo com conteúdo não válido");
            }

            Log("Etapa 4 - Tentando comunicação com a página");
            var address = @"http://www.facebook.com/";
            var client = new HtmlWeb();
            if (client.StatusCode == HttpStatusCode.OK)
            {
                var htmlDoc = client.Load(address);
                var nodeUser = htmlDoc.DocumentNode.SelectSingleNode(@"//input[@id='email']");
                var nodePassword = htmlDoc.DocumentNode.SelectSingleNode(@"//input[@id='pass']");
                var nodeLogin = htmlDoc.DocumentNode.SelectSingleNode(@"//input[@name='Entrar']");

                nodeUser.Attributes.Add("value", User);
                Log(nodeUser.OuterHtml);

                nodePassword.Attributes.Add("value", Password);
                Log(nodePassword.OuterHtml);
            }

            StringBuilder sb = new StringBuilder();
            Program.AppendParameter(sb, "email", User);
            Program.AppendParameter(sb, "pass", Password);
            byte[] byteArray = Encoding.UTF8.GetBytes(sb.ToString());

            string url = "http://www.facebook.com/";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.AllowAutoRedirect = true;
            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(byteArray, 0, byteArray.Length);
            }

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            var html = new HtmlDocument();
            var res = client.Load(response.ResponseUri.ToString());

            Thread.Sleep(20000);
        }

        protected static void AppendParameter(StringBuilder sb, string name, string value)
        {
            string encodedValue = HttpUtility.UrlEncode(value);
            sb.AppendFormat("{0}={1}&", name, encodedValue);
        }

        private void SendDataToService()
        {
            StringBuilder sb = new StringBuilder();
            AppendParameter(sb, "value", User);
            AppendParameter(sb, "value", Password);

            byte[] byteArray = Encoding.UTF8.GetBytes(sb.ToString());

            string url = "http://www.facebook.com/";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";

            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(byteArray, 0, byteArray.Length);
            }

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        }

        private static void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}
