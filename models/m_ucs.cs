using wpf_test.UCs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace wpf_test.models
{
    public class m_ucs
    {
        private MainWindow m_window;
        private StackPanel? m_panel;
        private m_datadir? mDataDir;

        public m_ucs(MainWindow window, m_datadir mDataDir)
        {
            m_window = window;
            m_panel = (StackPanel?)window.FindName("sp_UCHolder");
            this.mDataDir = mDataDir;
        }

        public void Show_UC_SqliteBrowser()
        {
            m_panel?.Children.Clear();
            m_panel?.Children.Add(new ucSqlite(this));

        }

        public void Show_UC_Main()
        {
            m_panel?.Children.Clear();
            m_panel?.Children.Add(new ucMain(this));

        }

        public void Show_UC_FileConcatenate()
        {
            m_panel?.Children.Clear();
            m_panel?.Children.Add(new ucFileConcatenate(this));
        }

        public m_datadir getDataDir()
        {
            return mDataDir!;
        }
    }
}
