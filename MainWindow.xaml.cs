using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
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
using System.Data;


using wpf_test.db;
using Microsoft.Data.Sqlite;
using System.Data.SQLite;
using System.IO;
using wpf_test.models;

namespace wpf_test
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        m_ucs mUCS;
        public MainWindow()
        {
            InitializeComponent();

            mUCS = new m_ucs(this, new m_datadir());
            mUCS.Show_UC_Main();
        }


    }

}
