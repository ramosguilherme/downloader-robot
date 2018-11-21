using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Downloader.Robot
{
    class Program
    {
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
                var user = credenciais[0];
                var password = credenciais[1];
            }
            else
            {
                throw new Exception("Arquivo com conteúdo não válido");
            }

            Thread.Sleep(2000);
        }

        private static void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}
