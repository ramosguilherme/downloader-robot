using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Downloader.Robot
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!File.Exists(@"Credenciais.txt"))
            {
                throw new Exception("Deu ruim");
            }

            var credenciais = File.ReadAllLines(@"Credenciais.txt").ToArray();

            if (credenciais.Length == 2)
            {
                var user = credenciais[0];
                var password = credenciais[1];
            }
            else
            {
                throw new Exception("Arquivo com conteúdo não válido");
            }
        }
    }
}
