using wpf_test.models;
using System;
using System.Collections.Generic;
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

namespace wpf_test.UCs
{
    /// <summary>
    /// Interaction logic for ucMain.xaml
    /// </summary>
    public partial class ucMain : UserControl
    {
        m_ucs? mUCs;
        public ucMain(m_ucs mUCs)
        {
            this.mUCs = mUCs;
            InitializeComponent();
        }

        private void OnGoSqliteBrowser_Click(object sender, RoutedEventArgs e)
        {
            mUCs?.Show_UC_SqliteBrowser();
        }

        private void OnGoFileConcatenate_Click(object sender, RoutedEventArgs e)
        {
            mUCs?.Show_UC_FileConcatenate();
        }
    }
}
