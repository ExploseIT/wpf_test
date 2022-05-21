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
using wpf_test.db;
using wpf_test.models;
namespace wpf_test.UCs
{
    /// <summary>
    /// Interaction logic for ucSqlite.xaml
    /// </summary>
    public partial class ucSqlite : UserControl
    {
        int m_page = 1;
        m_safehash mSafeHash;
        int pageCount;
        string m_hashsearch = "";
        m_ucs? mUCs;
        public ucSqlite(m_ucs mUCs)
        {
            InitializeComponent();
            this.mUCs = mUCs;

            mSafeHash = new m_safehash(new dbContext(this.mUCs.getDataDir()));
            doUpdate();
        }

        public void doUpdate()
        {
            var shPage = mSafeHash.doReadPage(m_page, m_hashsearch);
            pageCount = mSafeHash.doGetPageCount(m_page, m_hashsearch);
            dg_datagrid.ItemsSource = shPage;
            int pageRows = shPage.Count;
            labTotalCount.Content = $"Total rows {pageRows}, Total pages {pageCount - 1}";
            tbPageNumber.Text = $"{m_page}";
        }

        void OnPageDown(object sender, RoutedEventArgs e)
        {
            if (m_page > 1)
            {
                m_page = m_page - 1;
            }
            doUpdate();
        }

        void OnPageUp(object sender, RoutedEventArgs e)
        {
            if (m_page < pageCount)
            {
                m_page = m_page + 1;
            }
            else if (m_page > pageCount)
            {
                m_page = pageCount;
            }
            doUpdate();
        }

        private void tbPageNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                string m_page_new = tbPageNumber.Text;
                int iPageNew;
                if (int.TryParse(m_page_new, out iPageNew))
                {
                    if (iPageNew >= 0 && iPageNew <= pageCount)
                    {
                        m_page = iPageNew;
                        doUpdate();
                    }
                }
            }
        }

        private void OnHashSearch(object sender, RoutedEventArgs e)
        {
            m_hashsearch = tbHashSearch.Text;
            doUpdate();
        }

        private void OnExit_Click(object sender, RoutedEventArgs e)
        {
            mUCs?.Show_UC_Main();
        }
    }
}
