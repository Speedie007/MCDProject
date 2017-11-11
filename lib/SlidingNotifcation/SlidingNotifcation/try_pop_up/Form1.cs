using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NotificationWindow;
using System.Threading;

namespace try_pop_up
{
    public partial class Form1 : Form
    {
        PopupNotifier popupNotifier1;
        public Form1()
        {
            InitializeComponent();
            popupNotifier1 = new NotificationWindow.PopupNotifier();

            popupNotifier1.AnimationDuration = 250;
            popupNotifier1.AnimationInterval = 1;
            popupNotifier1.BodyColor = System.Drawing.SystemColors.GradientActiveCaption;
            popupNotifier1.BorderColor = System.Drawing.Color.Aqua;
            popupNotifier1.ButtonBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            popupNotifier1.ContentFont = new System.Drawing.Font("Tahoma", 8F);
            popupNotifier1.ContentPadding = new System.Windows.Forms.Padding(0, 17, 0, 0);
            popupNotifier1.ContentText = null;
            popupNotifier1.GradientPower = 300;
            popupNotifier1.HeaderColor = System.Drawing.Color.SteelBlue;
            popupNotifier1.HeaderFont = new System.Drawing.Font("Bookman Old Style", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            popupNotifier1.HeaderHeight = 20;
            popupNotifier1.HeaderPadding = new System.Windows.Forms.Padding(0);
            popupNotifier1.HeaderText = "Header Text";
          //  popupNotifier1.Image = global::try_pop_up.Properties.Resources.DispatcherIcon;
            popupNotifier1.ImagePadding = new System.Windows.Forms.Padding(10, 13, 0, 0);
            popupNotifier1.ImageSize = new System.Drawing.Size(30, 30);
            popupNotifier1.OptionsMenu = null;
            popupNotifier1.Scroll = false;
            popupNotifier1.ShowCloseButton = false;
            popupNotifier1.Size = new System.Drawing.Size(220, 75);
            popupNotifier1.TitleColor = System.Drawing.Color.Black;
            popupNotifier1.TitleFont = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            popupNotifier1.TitlePadding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            popupNotifier1.TitleText = "Hello";


            popupNotifier1.TitleText = "Hello";
            popupNotifier1.ContentText = "content text";
            popupNotifier1.ShowCloseButton = true;
            popupNotifier1.ShowOptionsButton = false;
            popupNotifier1.ShowGripText = true;
            popupNotifier1.Delay = 5000;
            popupNotifier1.AnimationInterval = 1;
            popupNotifier1.AnimationDuration = 400;
            popupNotifier1.Scroll = true;
            popupNotifier1.ShowCloseButton = true;



            popupNotifier1.Image = Properties.Resources._2;


        }
        int count = 0;

        private void Show_Click(object sender, EventArgs e)
        {

            switch (count%5)
            {
                case 1:
                    popupNotifier1.popup("Message Type" + count, "Message Detail Message Detail Message Detail " + count, 10, 10, 10, 10, Properties.Resources._1);
                    break;
                case 2:
                    popupNotifier1.popup("Message Type" + count, "Message Detail Message Detail Message Detail " + count, 10, 10, 10, 10, Properties.Resources._2);
                    break;
                case 3:
                    popupNotifier1.popup("Message Type" + count, "Message Detail Message Detail Message Detail " + count, 10, 10, 10, 10, Properties.Resources._3);
                    break;
                case 4:
                    popupNotifier1.popup("Message Type" + count, "Message Detail Message Detail Message Detail " + count, 10, 10, 10, 10, Properties.Resources._4);
                    break;
                default:
                    popupNotifier1.popup("Message Type" + count, "Message Detail Message Detail Message Detail " + count, 10, 10, 10, 10, Properties.Resources.StartRecording);
                    break;
            }
          


         //   popupNotifier1.popup("User Busy" + count, "User Not Avaiable User Not Avaiable User Not Avaiable " + count, 10, 10, 10, 10, Properties.Resources._2);


            count++;


            System.Diagnostics.Debug.WriteLine("pop up called" + count);

        }




    }
}
