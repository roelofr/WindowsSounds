using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SystemStuff.Properties;

namespace SystemStuff
{
    public partial class Display : Form
    {
        private EventListener _handler;
        private List<String> _logList;

        public Display()
        {
            var menu = new ContextMenu(new MenuItem[]
            {
                new MenuItem("Weergeven", TrayClick), 
                new MenuItem("Afsluiten", CloseApp)
            });

            InitializeComponent();

            Load += Loaded;
            FormClosing += Closing;

            _trayIcon.DoubleClick += TrayClick;

            _trayIcon.ContextMenu = menu;
            _trayIcon.Icon = Properties.Resources.note;
        }

        private void TrayClick(object s, EventArgs e)
        {
            HideTray();
        }

        private void CloseApp(object sender, EventArgs e)
        {
            var btns = MessageBoxButtons.YesNo;
            var resp = MessageBox.Show(this, Resources.CloseText, Resources.CloseTitle, btns);
            if (resp == DialogResult.Yes)
                Application.Exit();
        }

        private new void Closing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                ShowTray();
                return;
            }

            HideTray();
        }

        private void ShowTray()
        {
            _trayIcon.Visible = true;
            Visible = false;
            ShowInTaskbar = false;
        }

        private void HideTray()
        {
            _trayIcon.Visible = false;
            Visible = true;
            ShowInTaskbar = true;
        }

        private void Loaded(object sender, EventArgs e)
        {
            InitializeEventHandler();
            ShowTray();
        }

        private void InitializeEventHandler()
        {
            _logList = new List<String>();
            _handler = new EventListener();
            _handler.EventHappened += handler_EventHappened;

            _handler.Start();
        }

        private void RepaintLog()
        {
            LogList.BeginUpdate();
            LogList.Items.Clear();
            foreach (var data in _logList)
            {
                LogList.Items.Add(data);
            }
            LogList.EndUpdate();
            LogList.SelectedIndex = LogList.Items.Count - 1;
        }

        private void handler_EventHappened(string data)
        {
            _logList.Add(data);
            RepaintLog();
        }
    }
}