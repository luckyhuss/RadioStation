using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace RadioStation
{
    public partial class FormMain : Form
    {
        private string title { get; set; }
        public FormMain()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // set title
            title = "Radio station";
            this.Text = title;

            using (StreamReader sReader = new StreamReader("radiolist.ini"))
            {
                //create a string object that would hold the
                //value of each line in our file
                string line = String.Empty;
                string[] text = null;

                // set the data member to display and hide
                listBoxStations.DisplayMember = "Key";
                listBoxStations.ValueMember = "Value";

                //iterate for each line item in our file
                while ((line = sReader.ReadLine()) != null)
                {
                    text = line.Split(';'); // get radio name and url
                    if (text != null && text.Length == 2)
                    {
                        // read data
                        listBoxStations.Items.Add(new KeyValuePair<string, string>(text[0], text[1]));
                    }
                }
            }
        }

        private void listBoxStations_DoubleClick(object sender, EventArgs e)
        {
            if (listBoxStations.SelectedItem != null)
            {
                // get value pair for the selected item
                KeyValuePair<string, string> kvp = (KeyValuePair<string, string>)listBoxStations.SelectedItem;
                
                // get the url to play
                string url = kvp.Value;
                this.Text = string.Format("{0} - {1}", title, kvp.Key);

                // play url
                windowsMediaPlayer.URL = url;
            }
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            // close everything before exit
            windowsMediaPlayer.close();            
            this.Close();
        }

        private void buttonRecord_Click(object sender, EventArgs e)
        {
            // open goldwave
            Process.Start(@"C:\Program Files\GoldWave\GoldWave.exe");
        }
    }
}
