using ServiceStack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Downloader.Reader
{
    class Program
    {
        static void Main(string[] args)
        {
            //var caminho = @"Data\\NBA_player_of_the_week.csv";
            var caminho = @"Data\\Lista de estoque com novas encomendas em destaque1.xls";
            //var arquivo = new FileStream(caminho, FileMode.Open, FileAccess.Read);
            //string text;
            //using (var streamReader = new StreamReader(arquivo, Encoding.UTF8))
            //{
            //    text = streamReader.ReadToEnd();
            //}

            string[] filePaths = Directory.GetFiles(@"C:\\Users\\Guilherme\\Desktop");


            HttpWebRequest request = WebRequest.Create("http://google.com") as HttpWebRequest;

            //request.Accept = "application/xrds+xml";  
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            WebHeaderCollection header = response.Headers;

            var encoding = ASCIIEncoding.ASCII;
            using (var reader = new System.IO.StreamReader(response.GetResponseStream(), encoding))
            {
                string responseText = reader.ReadToEnd();
            }

            //string sourceFile = @"C:\Users\Public\public\test.txt";
            //string destinationFile = @"C:\Users\Public\private\test.txt";

            //// To move a file or folder to a new location:
            //System.IO.File.Move(sourceFile, destinationFile);

            //// To move an entire directory. To programmatically modify or combine
            //// path strings, use the System.IO.Path class.
            //System.IO.Directory.Move(@"C:\Users\Public\public\test\", @"C:\Users\Public\private");


            //public object Any()
            //{
            //string fileFullPath = "...";
            string mimeType = "application/ms-office";
            FileInfo fi = new FileInfo(caminho);

            byte[] reportBytes = File.ReadAllBytes(fi.FullName);
            var result = new HttpResult(reportBytes, mimeType);
            var a = fi.OpenRead();
            var b = a.ConvertTo<CsvOnly>().ToString();
            //result.ResponseText.ReadAllText();
            result.Headers.Add("Content-Disposition", "attachment;filename=YOUR_NAME_HERE.pdf;");

            //return result;
            //}
        }
    }
}
