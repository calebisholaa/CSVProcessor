using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSVProcessor
{
    internal class Reader
    {

        string fileName = "";
        string filePath = "";
        string fileContent = "";



        List<Guid> productID = new List<Guid>();
        List<string> productName = new List<string>();
        List<string> productType = new List<string>();
        List<string> productSubtype = new List<string>();

        


        public void OpenAndReadFile()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    this.filePath = openFileDialog.FileName;

                    //Read the contents of the file into a stream
                    var fileStream = openFileDialog.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        fileContent = reader.ReadToEnd();
                    }
                }
            }
            // Console.WriteLine(fileContent + "From Open");
        }

        public void OpenAndParseFile()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    this.filePath = openFileDialog.FileName;

                    //Read the contents of the file into a stream
                    var fileStream = openFileDialog.OpenFile();



                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        var lineCounter = 0;
                        while (!reader.EndOfStream)
                        {
                            var line = reader.ReadLine();
                            var values = line.Split(',');

                            if (values.Length == 1)
                            {
                                productType[lineCounter - 1] += values[0];
                            }
                            else
                            {
                                productName.Add(values[0]);
                                productType.Add(values[1]);
                                productSubtype.Add(values[2]);

                                lineCounter++;
                            }

                        }


                        reader.Close();

                        // PrintResult();
                    }
                }

            }



        }

        public void WriteCSV()
        {


            Stream stream;
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog.FilterIndex = 2;
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {

                using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName))
                {
                    Console.WriteLine("Writing New File...\n");
                    writer.Write("ProductId" + "," + "ProductName" + "," + "ProductType" + "," + "ProductSubtype" + ","
                        + "ProductDescription" + "," + "ProductImage" + "," + "FileName" + "," + "OS" + "\n");

                    for (int i = 1; i < productName.Count; i++)
                    {
                        
                        writer.Write(productID[i] + "," + productName[i] + "," + productType[i] +  ","+ productSubtype[i] + "\n");
                       
                    }

                    writer.Close();
                }

                
            }
          
            Console.WriteLine("New File written\nYou can exist now.\nPress Enter...");



        }

      
        public void Run()
        {
            OpenAndParseFile();
            GenerateGuid();
            WriteCSV();

        }

        public void GenerateGuid()
        {
           
            for (int i = 0; i < productName.Count; i++)
            {
                Guid productGuid = Guid.NewGuid();
                productID.Add(productGuid);
            }

        }

      
    }
}
