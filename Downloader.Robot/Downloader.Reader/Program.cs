using System;

using System.IO;

using System.Data;

using System.Data.OleDb;

using System.Collections.Generic;

using System.Text;

namespace Downloader.Reader
{
    class Program
    {
        static void Main(string[] args)

        {
            // https://social.msdn.microsoft.com/Forums/pt-BR/04be0aee-70d3-4c27-83b2-8417b98f3aa5/converter-xls-em-csv-no-c?forum=504
            string sourceFile, worksheetName, targetFile;

            sourceFile = @"Data\Lista de estoque com novas encomendas em destaque1.xls"; worksheetName = "sheet1"; targetFile = @"Data\saida.csv";

            convertExcelToCSV(sourceFile, worksheetName, targetFile);

        }


        static void convertExcelToCSV(string sourceFile, string worksheetName, string targetFile)

        {

            string strConn = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\" + sourceFile + "; Extended Properties=\" Excel.0;HDR=Yes;IMEX=1\"";

            OleDbConnection conn = null;

            StreamWriter wrtr = null;

            OleDbCommand cmd = null;

            OleDbDataAdapter da = null;

            try

            {

                conn = new OleDbConnection(strConn);

                conn.Open();



                cmd = new OleDbCommand("SELECT * FROM [" + worksheetName + "$]", conn);

                cmd.CommandType = CommandType.Text;

                wrtr = new StreamWriter(targetFile);



                da = new OleDbDataAdapter(cmd);

                DataTable dt = new DataTable();

                da.Fill(dt);



                for (int x = 0; x < dt.Rows.Count; x++)

                {

                    string rowString = "";

                    for (int y = 0; y < dt.Columns.Count; y++)

                    {

                        rowString += "\"" + dt.Rows[x][y].ToString() + "\",";

                    }

                    wrtr.WriteLine(rowString);

                }

                Console.WriteLine();

                Console.WriteLine("Done! Your " + sourceFile + " has been converted into " + targetFile + ".");

                Console.WriteLine();

            }

            catch (Exception exc)

            {

                Console.WriteLine(exc.ToString());

                Console.ReadLine();

            }

            finally

            {

                if (conn.State == ConnectionState.Open)

                    conn.Close();

                conn.Dispose();

                cmd.Dispose();

                da.Dispose();

                wrtr.Close();

                wrtr.Dispose();

            }
        }
    }
}
