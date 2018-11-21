using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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

                nodeUser.Attributes.Append("value");
                nodeUser.SetAttributeValue("value", User);
                Log(nodeUser.OuterHtml);

                nodePassword.Attributes.Append("value");
                nodePassword.SetAttributeValue("value", Password);
                Log(nodePassword.OuterHtml);
            }

            Thread.Sleep(20000);
        }

        private static void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}
