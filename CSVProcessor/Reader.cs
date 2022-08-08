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

        //string fileName = "";
        string filePath = "";
        string fileContent = "";



       // List<Guid> productID = new List<Guid>();
        List<string> productID = new List<string>();
        List<string> productName = new List<string>();
        List<string> productType = new List<string>();
        List<string> productSubtype = new List<string>();


        List<Guid> versionID = new List<Guid>();
        List<string>  releaseNote = new List<string>();
        List<string> fileName = new List<string>();
        List<DateTime> releaseDate = new List<DateTime>();
        List<string> versionProductID = new List<string>();


        


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
                                productID.Add(values[0]);
                                productName.Add(values[1]);
                                productType.Add(values[2]);
                                productSubtype.Add(values[3]);

                                lineCounter++;
                            }

                          

                        }


                      

                        reader.Close();

                        versionProductID = productID;

                        // PrintResult();
                    }
                }

            }



        }


        public void parseVersions()
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
                                releaseNote.Add(values[0]);
                                fileName.Add(values[1]);
                               // releaseDate.Add(values[2]);

                                lineCounter++;
                            }



                        }




                        reader.Close();

                        // PrintResult();
                    }
                }

            }
        }

        public void WriteVersions()
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
                    writer.Write("VersionID" + "," + "ReleaseNotes" +"," + "FileName"  + "," + "ReleaseDate" + ","
                        + "ProductID" +  "\n");

                    for (int i = 1; i < productID.Count; i++)
                    {

                        writer.Write(versionID[i] + "," + releaseNote[i] + "," +  fileName[i] + "," + releaseDate[i] + "," + productID[i] + "\n");

                    }

                    writer.Close();
                }


            }
        }

       

        #region
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
                        
                        writer.Write(productID[i] +","+ productName[i] + "," + productType[i] +  "," + productSubtype[i] + "\n");
                       
                    }

                    writer.Close();
                }

                
            }

            foreach (var item in productSubtype)
            {
               // Console.WriteLine(item);
            }
          
            Console.WriteLine("New File written\nYou can exist now.\nPress Enter...");



        }
        #endregion

        public void Run()
        {
            OpenAndParseFile();
            //GenerateGuidProduct();
            GenerateGuidVersion();
            GenerateReleaseDate();
            ReleaseNotes();
            WriteVersions();
           // WriteCSV();

        }

        public void GenerateGuidProduct()
        {       
            for (int i = 0; i < productName.Count; i++)
            {
                Guid productGuid = Guid.NewGuid();
               // productID.Add(productGuid);
            }

        }

        public void GenerateGuidVersion()
        {

            for (int i = 0; i < productName.Count; i++)
            {
                Guid versionGuid = Guid.NewGuid();
                versionID.Add(versionGuid);
            }

        }

        public void GenerateReleaseDate()
        {
            for (int i = 0; i < productName.Count; i++)
            {
                releaseDate.Add(DateTime.UtcNow);
            }
        }

        public void ReleaseNotes()
        {
            for (int i = 0; i < productName.Count; i++)
            {
                releaseNote.Add("Null");
                fileName.Add("Null");
            }
        }



    }
}
