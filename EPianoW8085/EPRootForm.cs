using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;

namespace EPianoW8085
{
    public partial class EPRootForm : Form
    {
        static SerialPort _sPort;

        public EPRootForm()
        {
            InitializeComponent();

            _sPort = new SerialPort();
            _sPort.PortName = "COM5";
            _sPort.BaudRate = 9600;
            _sPort.Open();
            //_sPort.Bytes
        }

        //public void configPort()
        //{
        //    _serialPort = new SerialPort();
        //    _serialPort.PortName = "COM5";
        //    _serialPort.BaudRate = 9600;
        //    //_serialPort.Parity = //Parity;
        //    //_serialPort.DataBits = INT;
        //    //_serialPort.StopBits = //StopBits;
        //    //_serialPort.Handshake = //Handshake;

        //    string message;
        //    //StringComparer stringComparer = StringComparer.OrdinalIgnoreCase;
        //    //Thread readThread = new Thread(Read);
        //    //// Set the read/write timeouts
        //    //_serialPort.ReadTimeout = 500;
        //    //_serialPort.WriteTimeout = 500;
        //    _serialPort.Open();
        //    //readThread.Start();
        //    while (true)
        //    {
        //        message = "Demo Demoday";
        //        _serialPort.WriteLine(String.Format("<data>: {0}", message));
        //        string messageR = _serialPort.ReadLine();
        //        MessageBox.Show(messageR);
        //    }
        //    //readThread.Join();
        //    //Read();
            
        //    //_serialPort.Close();
        //}

        //public static void Read()
        //{
        //    while (true)
        //    {
        //        try
        //        {
        //            string message = _serialPort.ReadLine();
        //            MessageBox.Show(message);
        //        }
        //        catch (TimeoutException) { }
        //    }
        //}

        List<Label> listLbls = new List<Label>();
        List<Label> listLblBNs = new List<Label>();

        // Danh sach dinh nghia ky tu tuong ung voi index cua phim dan
        Dictionary<Keys, int> keyToId = new Dictionary<Keys, int>()
        {
            {Keys.Z, 0},
            {Keys.C, 1},
            {Keys.B, 2},
            {Keys.N, 3},
            {Keys.A, 4},
            {Keys.D, 5},
            {Keys.G, 6},
            {Keys.H, 7},
            {Keys.K, 8},
            {Keys.Q, 9},
            {Keys.W, 10},
            {Keys.R, 11},
            {Keys.Y, 12},
            {Keys.I, 13},
            {Keys.O, 14},
            {Keys.D1, 15},
            {Keys.D3, 16},
            {Keys.D4, 17},
            {Keys.D6, 18},
            {Keys.D8, 19},
            {Keys.D0, 20}
        };
        Dictionary<Keys, int> keyToIdBN = new Dictionary<Keys, int>()
        {
            {Keys.X, 0},
            {Keys.V, 1},
            {Keys.M, 2},
            {Keys.S, 3},
            {Keys.F, 4},
            {Keys.J, 5},
            {Keys.L, 6},
            {Keys.E, 7},
            {Keys.T, 8},
            {Keys.U, 9},
            {Keys.P, 10},
            {Keys.D2, 11},
            {Keys.D5, 12},
            {Keys.D7, 13},
            {Keys.D9, 14}
        };
        
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            //List<Panel> listPns = new List<Panel>();
            //List<Panel> listPnBNs = new List<Panel>();
            int n = 21; // Kich thuoc Piano tinh theo so phim dan trang
            int Ww = 60, Hw = 160;  // Kich thuoc phim dan trang
            int Wb = Ww / 3 * 2, Hb = Hw / 3 * 2;   // Kich thuoc phim dan den
            int Yp = 0;    // Diem dat phim dan
            // Yp chua co tac dung ==========================================

            for (int i = 0; i < n; i++)
            {
                Label p = new Label();

                //Button b = new Button();
                //b.Text = "Demo Click";
                //b.AutoSize = true;
                //p.Controls.Add(b);

                if (((i % 7) == 0 || (i % 7) == 1 || (i % 7) == 3 || (i % 7) == 4 || (i % 7) == 5) && (i != n - 1))
                {
                    Label pt = new Label();
                    pt.BorderStyle = BorderStyle.Fixed3D;
                    pt.BackColor = Color.Black;
                    pt.Width = Wb;
                    pt.Height = Hb;
                    if(i == 0)
                        pt.Location = new Point(panel1.Location.X + 3 + Ww - Wb/2, panel1.Location.Y + Yp + 1);
                    else
                        pt.Location = new Point(listLbls[i - 1].Location.X + 2*Ww + 3 - Wb / 2, listLbls[i - 1].Location.Y + 1);
                    panel1.Controls.Add(pt);
                    listLblBNs.Add(pt);
                }

                listLbls.Add(p);
                listLbls[i].BorderStyle = BorderStyle.Fixed3D;
                listLbls[i].BackColor = Color.White;
                listLbls[i].Width = Ww;
                listLbls[i].Height = Hw;
                listLbls[i].Text = keyToId.Keys.ElementAt(i).ToString();
                listLbls[i].Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                listLbls[i].TextAlign = ContentAlignment.BottomCenter;
                listLbls[i].ForeColor = Color.DarkBlue;
                //if (i == 4)
                //    listLbls[i].Text = ",";
                //if (i == 5)
                //    listLbls[i].Text = "/";
                //if (i == 11)
                //    listLbls[i].Text = ";";
                //if (i == 19)
                //    listLbls[i].Text = "]";
                //if (i == 20)
                //    listLbls[i].Text = "`~";
                //if (i == 27)
                //    listLbls[i].Text = "=";
                //listLbls[i].  CHINH FONT CHU;
                if (i == 0)
                    listLbls[i].Location = new Point(panel1.Location.X + 2, panel1.Location.Y + Yp);
                else
                    listLbls[i].Location = new Point(listLbls[i - 1].Location.X + Ww + 1, listLbls[i - 1].Location.Y);

                panel1.Controls.Add(listLbls[i]);
            }
            for (int i = 0; i < n/7*5; i++)
            {
                listLblBNs[i].Text = keyToIdBN.Keys.ElementAt(i).ToString();
                listLblBNs[i].Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                //if (i == 3)
                //    listLblBNs[i].Text = ".";
                //if (i == 8)
                //    listLblBNs[i].Text = "''";
                //if (i == 13)
                //    listLblBNs[i].Text = "[";
                //if (i == 14)
                //    listLblBNs[i].Text = "\\";
                //if (i == 19)
                //    listLblBNs[i].Text = "-";
                listLblBNs[i].TextAlign = ContentAlignment.BottomCenter;
                listLblBNs[i].ForeColor = Color.Yellow;
            }

            //Them cac ComboBox setup cac thong so cho cong COM
            //ComboBox cbCOM = new ComboBox();
            //ComboBox cbBR = new ComboBox();
            //cbCOM.ForeColor = Color.DarkGreen;
            //cbCOM.BackColor = Color.LightPink;
            //cbBR.ForeColor = Color.DarkGreen;
            //cbBR.BackColor = Color.LightPink;
            //string[] itemsCOM =
            //{
            //    "COM1",
            //    "COM2",
            //    "COM3",
            //    "COM4",
            //    "COM5",
            //    "COM6",
            //    "COM7",
            //    "COM8",
            //    "COM9"
            //};
            //string[] itemsBR =
            //{
            //    "1200",
            //    "2400",
            //    "4800",
            //    "9600",
            //    "14400"
            //};
            //cbCOM.Items.AddRange(itemsCOM);
            //cbBR.Items.AddRange(itemsBR);
            //cbCOM.Location = new Point(210, 3);
            //panel1.Controls.Add(cbCOM);
            //panel1.Controls.Add(cbBR);
            //TAM THAY BANG LABEL

            panel1.AutoSize = true;

            // Them cac label de de chu thich cho ban phim
            //for (int i = 0; i < 28; i++)
            //{ 
            //    Label lbl = new Label();
            //    lbl.Text = keyToId.Keys.ElementAt(i).ToString();
            //    lbl.BorderStyle = BorderStyle.None;
            //    lbl.TextAlign = ContentAlignment.MiddleCenter;
            //    lbl.ForeColor = Color.DarkBlue;
            //    lbl.Location = new Point(50, 150);
            //    //if (i == 0)
            //    //    lbl.Location = new Point(panel1.Location.X + 4, panel1.Location.Y + Yp + Hw*3/4);
            //    //else
            //    //    lbl.Location = new Point(listPns[i - 1].Location.X + Ww + 3, listPns[i - 1].Location.Y + Hw*3/4);
            //    listPns[i].Controls.Add(lbl);
            //}
            //for (int i = 0; i < 20; i++)
            //{
            //    Label lbl = new Label();
            //    lbl.Text = keyToIdBN.Keys.ElementAt(i).ToString();
            //    lbl.BorderStyle = BorderStyle.None;
            //    lbl.TextAlign = ContentAlignment.MiddleCenter;
            //    lbl.ForeColor = Color.Yellow;
            //    listPnBNs[i].Controls.Add(lbl);
            //}
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            string sData;
            for(int i = 0; i < 21; i++)
            {
                if (e.KeyCode == keyToId.Keys.ElementAt(i))
                {
                    listLbls[i].BackColor = Color.LightSalmon;
                    if (i == 15)
                        sData = "1";
                    else if(i == 16)
                        sData = "3";
                    else if (i == 17)
                        sData = "4";
                    else if (i == 18)
                        sData = "6";
                    else if (i == 19)
                        sData = "8";
                    else if (i == 20)
                        sData = "0";
                    else
                        sData = keyToId.Keys.ElementAt(i).ToString();
                    _sPort.Write(sData);
                }
            }
            //MessageBox.Show(Text = e.KeyCode.ToString());
            for (int i = 0; i < 15; i++)
            {
                if (e.KeyCode == keyToIdBN.Keys.ElementAt(i))
                {
                    listLblBNs[i].BackColor = Color.LightSlateGray;
                    if(i == 11)
                        sData = "2";
                    else if (i == 12)
                        sData = "5";/////////CAN CHINH SUA
                    else if (i == 13)
                        sData = "7";
                    else if (i == 14)
                        sData = "9";
                    else
                        sData = keyToIdBN.Keys.ElementAt(i).ToString();
                    char[] charData = sData.ToCharArray();      ///GUI BYTE CHAR[]
                    _sPort.Write(charData, 0, 1);
                    //char c0 = charData[0];
                    //char c1 = charData[1];
                    //string rData = _sPort.ReadLine();
                    //MessageBox.Show("DATA:   " + rData);
                }
            }

            //configPort();
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            for (int i = 0; i < 21; i++)
            {
                if (e.KeyCode == keyToId.Keys.ElementAt(i))
                {
                    listLbls[i].BackColor = Color.White;
                }
            }
            for (int i = 0; i < 15; i++)
            {
                if (e.KeyCode == keyToIdBN.Keys.ElementAt(i))
                {
                    listLblBNs[i].BackColor = Color.Black;
                }
            }
        }

        //private void sCOM_SelectedValueChanged(object sender, EventArgs e)
        //{
        //    //ComboBox cb = sender as ComboBox;
        //    //_sPort.PortName = cb.SelectedValue.ToString();
        //    //MessageBox.Show(_sPort.PortName);
        //}

        //private void sBR_SelectedValueChanged(object sender, EventArgs e)
        //{
        //    //ComboBox cb = sender as ComboBox;
        //    //_sPort.BaudRate = Convert.ToInt32(cb.SelectedValue);
        //    //MessageBox.Show(_sPort.BaudRate.ToString());
        //}
    }
}
