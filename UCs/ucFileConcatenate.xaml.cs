using wpf_test.models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace wpf_test.UCs
{
    /// <summary>
    /// Interaction logic for ucFileConcatenate.xaml
    /// </summary>
    public partial class ucFileConcatenate : UserControl
    {
        m_ucs? mUCs;
        public ucFileConcatenate(m_ucs mUCs)
        {
            this.mUCs = mUCs;
            InitializeComponent();

            doConcatenation();
        }

        public void doConcatenation()
        {
            byte[] buffer;
            string hashValue;

            string filesPath = mUCs!.getDataDir().getDataDirectory();
            string outputFile = System.IO.Path.Combine(filesPath, "OutputFile");
            if (File.Exists(outputFile))
            {
                File.Delete(outputFile);
            }
            string frPath = System.IO.Path.Combine(filesPath, "sliced_files");
            string[] files = Directory.GetFiles(frPath);
            MemoryStream stream = new MemoryStream();

            int fcount = 1;

            using (FileStream fs = File.Create(outputFile))
            {
                string f_part = $"part_{fcount}";
                string? cfPath = files.FirstOrDefault<string>(f => f.IndexOf(f_part) > 1);
                while (cfPath != null)
                {
                    var fread = File.ReadAllBytes(cfPath);
                    stream.Write(fread, 0, fread.Length);
                    fcount++;
                    f_part = $"part_{fcount}";
                    cfPath = files.FirstOrDefault<string>(f => f.IndexOf(f_part) > 1);
                }
                stream.Seek(0, SeekOrigin.Begin);
                stream.CopyTo(fs);
                stream.Seek(0, SeekOrigin.Begin);
                buffer = stream.ToArray();
            }

            using (SHA1 myHash = SHA1.Create())
            {
                hashValue = GetHash(myHash, buffer);
                labHashValue.Content=$"SHA1 hash value: {hashValue}";
            }


            Uri ofUri = new Uri(outputFile);
            imgOutput.Source = BitmapFromUri(ofUri);

        }

        public static ImageSource BitmapFromUri(Uri source)
        {
            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = source;
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.EndInit();
            return bitmap;
        }

        private static string GetHash(HashAlgorithm hashAlgorithm, byte[] input)
        {

            // Convert the input string to a byte array and compute the hash.
            //byte[] data = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(input));
            byte[] data = hashAlgorithm.ComputeHash(input);

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            var sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
        private void OnExit_Click(object sender, RoutedEventArgs e)
        {
            string filesPath = mUCs!.getDataDir().getDataDirectory();
            string outputFile = System.IO.Path.Combine(filesPath, "OutputFile");

            string sMessage;

            mUCs?.Show_UC_Main();
        }
    }
}
