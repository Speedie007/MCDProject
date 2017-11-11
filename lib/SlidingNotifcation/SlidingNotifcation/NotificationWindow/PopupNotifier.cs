/*
 *	Created/modified in 2011 by Simon Baer
 *	
 *  Based on the Code Project article by Nicolas Wälti:
 *  http://www.codeproject.com/KB/cpp/PopupNotifier.aspx
 * 
 *  Licensed under the Code Project Open License (CPOL).
 */

using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;

namespace NotificationWindow
{
    /// <summary>
    /// Non-visual component to show a notification window in the right lower
    /// corner of the screen.
    /// </summary>
    [ToolboxBitmapAttribute(typeof(PopupNotifier), "Icon.ico")]
    [DefaultEvent("Click")]
    public class PopupNotifier : Component
    {
        /// <summary>
        /// Event that is raised when the text in the notification window is clicked.
        /// </summary>
        public event EventHandler Click;

        /// <summary>
        /// Event that is raised when the notification window is manually closed.
        /// </summary>
        //  public event EventHandler Close;

        private bool disposed = false;
        private PopupNotifierForm frmPopup;
        private Timer tmrAnimation;
        private Timer tmrWait;
        private Timer timer1;

        private bool isAppearing = true;
        private bool mouseIsOn = false;
        private int maxPosition;
        private double maxOpacity;
        private int posStart;
        private int posStop;
        private double opacityStart;
        private double opacityStop;
        private System.Diagnostics.Stopwatch sw;
        public List<long> timelist = new List<long>();

        public List<long> timelist_wait = new List<long>();

        public List<string> message_type = new List<string>();
        public List<string> message_cont = new List<string>();
        public List<Image> message_image = new List<Image>();

        public List<string> message_type_wait = new List<string>();
        public List<string> message_cont_wait = new List<string>();
        public List<Image> message_image_wait = new List<Image>();


        const int wid = 220;
        const int h_t = 50;
        private int posCurrent = 0;
        bool animation_working = false;

        #region Properties

        [Category("Header"), DefaultValue(typeof(Color), "ControlDark")]
        [Description("Color of the window header.")]
        public Color HeaderColor { get; set; }

        [Category("Appearance"), DefaultValue(typeof(Color), "Control")]
        [Description("Color of the window background.")]
        public Color BodyColor { get; set; }

        [Category("Title"), DefaultValue(typeof(Color), "Gray")]
        [Description("Color of the title text.")]
        public Color TitleColor { get; set; }

        [Category("Content"), DefaultValue(typeof(Color), "ControlText")]
        [Description("Color of the content text.")]
        public Color ContentColor { get; set; }

        [Category("Appearance"), DefaultValue(typeof(Color), "WindowFrame")]
        [Description("Color of the window border.")]
        public Color BorderColor { get; set; }

        [Category("Buttons"), DefaultValue(typeof(Color), "WindowFrame")]
        [Description("Border color of the close and options buttons when the mouse is over them.")]
        public Color ButtonBorderColor { get; set; }

        [Category("Buttons"), DefaultValue(typeof(Color), "Highlight")]
        [Description("Background color of the close and options buttons when the mouse is over them.")]
        public Color ButtonHoverColor { get; set; }

        [Category("Content"), DefaultValue(typeof(Color), "HotTrack")]
        [Description("Color of the content text when the mouse is hovering over it.")]
        public Color ContentHoverColor { get; set; }

        [Category("Appearance"), DefaultValue(50)]
        [Description("Gradient of window background color.")]
        public int GradientPower { get; set; }

        [Category("Content")]
        [Description("Font of the content text.")]
        public Font ContentFont { get; set; }

        [Category("Header")]
        [Description("Font of the title.")]
        public Font HeaderFont { get; set; }


        [Category("Title")]
        [Description("Font of the title.")]
        public Font TitleFont { get; set; }

        [Category("Image")]
        [Description("Size of the icon image.")]
        public Size ImageSize
        {
            get
            {
                if (imageSize.Width == 0)
                {
                    if (Image != null)
                    {
                        return Image.Size;
                    }
                    else
                    {
                        return new Size(0, 0);
                    }
                }
                else
                {
                    return imageSize;
                }
            }
            set { imageSize = value; }
        }

        public void ResetImageSize()
        {
            imageSize = Size.Empty;
        }

        private bool ShouldSerializeImageSize()
        {
            return (!imageSize.Equals(Size.Empty));
        }

        private Size imageSize = new Size(0, 0);

        [Category("Image")]
        [Description("Icon image to display.")]
        public Image Image { get; set; }

        [Category("Header"), DefaultValue(true)]
        [Description("Whether to show a 'grip' image within the window header.")]
        public bool ShowGripText { get; set; }

        [Category("Header")]
        [Description("Title text to display.")]
        public string HeaderText { get; set; }


        [Category("Header")]
        [Description("Padding of title text.")]
        public Padding HeaderPadding { get; set; }

        [Category("Behavior"), DefaultValue(true)]
        [Description("Whether to scroll the window or only fade it.")]
        public bool Scroll { get; set; }

        [Category("Content")]
        [Description("Content text to display.")]
        public string ContentText { get; set; }

        [Category("Title")]
        [Description("Title text to display.")]
        public string TitleText { get; set; }



        [Category("Title")]
        [Description("Padding of title text.")]
        public Padding TitlePadding { get; set; }

        private void ResetTitlePadding()
        {
            TitlePadding = Padding.Empty;
        }

        private bool ShouldSerializeTitlePadding()
        {
            return (!TitlePadding.Equals(Padding.Empty));
        }

        [Category("Content")]
        [Description("Padding of content text.")]
        public Padding ContentPadding { get; set; }

        private void ResetContentPadding()
        {
            ContentPadding = Padding.Empty;
        }

        private bool ShouldSerializeContentPadding()
        {
            return (!ContentPadding.Equals(Padding.Empty));
        }

        [Category("Image")]
        [Description("Padding of icon image.")]
        public Padding ImagePadding { get; set; }

        private void ResetImagePadding()
        {
            ImagePadding = Padding.Empty;
        }

        private bool ShouldSerializeImagePadding()
        {
            return (!ImagePadding.Equals(Padding.Empty));
        }

        [Category("Header"), DefaultValue(9)]
        [Description("Height of window header.")]
        public int HeaderHeight { get; set; }

        [Category("Buttons"), DefaultValue(true)]
        [Description("Whether to show the close button.")]
        public bool ShowCloseButton { get; set; }

        [Category("Buttons"), DefaultValue(false)]
        [Description("Whether to show the options button.")]
        public bool ShowOptionsButton { get; set; }

        [Category("Behavior")]
        [Description("Context menu to open when clicking on the options button.")]
        public ContextMenuStrip OptionsMenu { get; set; }

        [Category("Behavior"), DefaultValue(3000)]
        [Description("Time in milliseconds the window is displayed.")]
        public int Delay { get; set; }

        [Category("Behavior"), DefaultValue(1000)]
        [Description("Time in milliseconds needed to make the window appear or disappear.")]
        public int AnimationDuration { get; set; }

        [Category("Behavior"), DefaultValue(10)]
        [Description("Interval in milliseconds used to draw the animation.")]
        public int AnimationInterval { get; set; }

        [Category("Appearance")]
        [Description("Size of the window.")]
        public Size Size { get; set; }

        #endregion

        /// <summary>
        /// Create a new instance of the popup component.
        /// </summary>
        public PopupNotifier()
        {
            // set default values

            HeaderColor = SystemColors.ControlDark;
            BodyColor = SystemColors.Control;
            TitleColor = System.Drawing.Color.Gray;
            ContentColor = SystemColors.ControlText;
            BorderColor = SystemColors.WindowFrame;
            ButtonBorderColor = SystemColors.WindowFrame;
            ButtonHoverColor = SystemColors.Highlight;
            ContentHoverColor = SystemColors.HotTrack;
            GradientPower = 50;
            ContentFont = SystemFonts.DialogFont;
            TitleFont = SystemFonts.CaptionFont;
            ShowGripText = true;
            Scroll = true;
            TitlePadding = new Padding(0);
            ContentPadding = new Padding(0);
            ImagePadding = new Padding(0);
            HeaderHeight = 9;
            ShowCloseButton = true;
            ShowOptionsButton = false;
            Delay = 3000;
            AnimationInterval = 20;
            AnimationDuration = 1000;
            Size = new Size(400, 100);

            frmPopup = new PopupNotifierForm(this);
            frmPopup.TopMost = true;
            frmPopup.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            frmPopup.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            frmPopup.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            frmPopup.MouseEnter += new EventHandler(frmPopup_MouseEnter);
            frmPopup.MouseLeave += new EventHandler(frmPopup_MouseLeave);
            frmPopup.MouseClick += new MouseEventHandler(frmPopup_MouseClick);

            //    frmPopup.CloseClick += new EventHandler(frmPopup_CloseClick);
            //    frmPopup.LinkClick += new EventHandler(frmPopup_LinkClick);
            //    frmPopup.ContextMenuOpened += new EventHandler(frmPopup_ContextMenuOpened);
            //   frmPopup.ContextMenuClosed += new EventHandler(frmPopup_ContextMenuClosed);

            tmrAnimation = new Timer();
            tmrAnimation.Tick += new EventHandler(tmAnimation_Tick);

            tmrWait = new Timer();
            tmrWait.Tick += new EventHandler(tmWait_Tick);

            timer1 = new Timer();
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Enabled = false;

        }

        private void frmPopup_MouseClick(Object sender, MouseEventArgs e)
        {


            if (e.X > frmPopup.Width - 5 - 16 & e.X < frmPopup.Width - 5 & e.Y > 2 & e.Y < 18)
            {
                Hide();
            }
        }


        private long getcurrenttime()
        {
            return (DateTime.Now.ToFileTime() / 10000000);
        }

        /// <summary>
        /// Show the notification window if it is not already visible.
        /// If the window is currently disappearing, it is shown again.
        /// </summary>
        int fresh_count = 0;
        int pop_show_time = 5;   //sec
        bool do_once = true;
        private Object thisLock = new Object();
        List<Image> image_type = new List<Image>();

        public void popup(string strTitle, string strContent, int nTimeToShow, int nTimeToStay, int nTimeToHide, int notify, Image imageshow)
        {
          

            System.Diagnostics.Debug.WriteLine("New message to show");

            isAppearing = true;
            if (do_once == true)
            {
                timer1.Enabled = false;
                frmPopup.Opacity = 1;
                frmPopup.Show();
                do_once = false;
                animation_working = false;

            }

            //       tmrAnimation.Stop();



            if (timelist.Count > 4)
            {

                timelist_wait.Add(getcurrenttime());
                message_cont_wait.Add(strTitle);
                message_type_wait.Add(strContent);
                message_image_wait.Add(imageshow);


            }
            else
            {



                timelist.Add(pop_show_time + getcurrenttime());
                message_type.Add(strTitle);
                message_cont.Add(strContent);
                message_image.Add(imageshow);

            }



            frmPopup.cont_post = timelist.Count;
            frmPopup.timelist_data = timelist;
            frmPopup.message_cont_show = message_cont;
            frmPopup.message_type_show = message_type;
            frmPopup.message_image_show = message_image;




            if (timer1.Enabled != true)
            {
                timer1.Enabled = true;
                timer1.Interval = pop_show_time * (1000);  //milisecond

            }

          

            if (timelist_wait.Count == 0)
            {
                refreshform(true);
            }

          


        }

        private void refreshform(bool anim)
        {



            //   tmrAnimation.Stop();

            Size = new Size(wid, 20 + (h_t * timelist.Count));

            System.Diagnostics.Debug.WriteLine(timelist.Count);
            if (anim == false)
            {
                //while going down content need to be refreshed first then and then displayed on screen 
                frmPopup.Refresh();
                frmPopup.Size = Size;
            }
            else
            {
                //while animation is on form size refresh not require 
                frmPopup.Size = Size;
            }

            frmPopup.painting_require = false;

            if (Scroll == true & anim == true)
            {
                if (animation_working == true)
                {
                    posStart = posCurrent;
                    System.Diagnostics.Debug.WriteLine("Postion start ");
                    System.Diagnostics.Debug.WriteLine(posStart);

                    posStop = Screen.PrimaryScreen.WorkingArea.Bottom - (frmPopup.Height);

                }
                else
                {
                    posStart = Screen.PrimaryScreen.WorkingArea.Bottom - (h_t * (timelist.Count - 1) + 20);
                    posStop = Screen.PrimaryScreen.WorkingArea.Bottom - (frmPopup.Height);
                }
            }
            else
            {
                posStart = Screen.PrimaryScreen.WorkingArea.Bottom - (frmPopup.Height);
                posStop = Screen.PrimaryScreen.WorkingArea.Bottom - (frmPopup.Height);
            }

            opacityStart = 1;
            opacityStop = 1;

            frmPopup.Location = new Point(Screen.PrimaryScreen.WorkingArea.Right - frmPopup.Size.Width - 1, posStart);
            System.Diagnostics.Debug.WriteLine("value at refresh");
            System.Diagnostics.Debug.WriteLine(frmPopup.Location.Y.ToString());



            if (anim == true)
            {
                animation_working = true;
                frmPopup.Opacity = 1;
                tmrAnimation.Interval = AnimationInterval;
                tmrAnimation.Start();
                sw = System.Diagnostics.Stopwatch.StartNew();
                System.Diagnostics.Debug.WriteLine("Animation started.");
            }
            else
            {
                frmPopup.Opacity = 1;
            }

            frmPopup.painting_require = true;
            // referesh require to fill netw

          





        }



        private Object timerLock = new Object();

        private void timer1_Tick(object sender, EventArgs e)
        {

            timer1.Enabled = false;


            if (timelist.Count > 0)
            {

                for (int i = 0; i < timelist.Count; i++)
                {
                    if (timelist[i] <= getcurrenttime())
                    {

                        timelist.RemoveAt(i);
                        message_cont.RemoveAt(i);
                        message_type.RemoveAt(i);
                        message_image.RemoveAt(i);


                        i--;
                    }
                }
                System.Diagnostics.Debug.WriteLine("Current timeout removed list count" + timelist.Count);
                int temp_count = 5 - timelist.Count;
                for (int j = 0; j < temp_count; j++)
                {
                    if (timelist_wait.Count > 0)
                    {

                        timelist.Add(getcurrenttime() + pop_show_time);
                        message_type.Add(message_cont_wait[0]);
                        message_cont.Add(message_type_wait[0]);
                        message_image.Add(message_image_wait[0]);

                        System.Diagnostics.Debug.WriteLine("waiting list count" + timelist_wait.Count);
                        System.Diagnostics.Debug.WriteLine(message_cont_wait[0]);
                        timelist_wait.RemoveAt(0);
                        message_cont_wait.RemoveAt(0);
                        message_type_wait.RemoveAt(0);
                        message_image_wait.RemoveAt(0);

                    }
                    else
                    {
                        break;
                    }

                }

                if (timelist.Count > 0)
                {

                    System.Diagnostics.Debug.WriteLine("showing list count" + timelist.Count);
                    int temp = (int)(timelist[0] - getcurrenttime()); ;

                    if (temp > 0)
                    {
                        System.Diagnostics.Debug.WriteLine("Next timeout of list update system" + temp);
                        timer1.Interval = (temp * 1000);
                        timer1.Enabled = true;
                    }

                    else
                    {

                        timer1.Interval = 1;
                        timer1.Enabled = true;
                    }


                }

                else
                {

                    //none

                }



            }

            else
            {
                //none
            }






            frmPopup.cont_post = timelist.Count;
            frmPopup.timelist_data = timelist;
            frmPopup.message_cont_show = message_cont;
            frmPopup.message_type_show = message_type;
            frmPopup.message_image_show = message_image;

            System.Diagnostics.Debug.WriteLine("refresh from function called" + timelist.Count);
          
            refreshform(false);   
            // while coming down downpart is cut and then bought down cause flicks
         
            // Clearing form
            //  frmPopup.Show();
            if (timelist.Count == 0)
            {

                animation_working = false;
                sw.Reset();
                tmrAnimation.Stop();
                do_once = true;
                frmPopup.Hide();
                System.Diagnostics.Debug.WriteLine("from hide call");
                isAppearing = false;
            }



            //  listBox1.DataSource = null;

            //   listBox1.DataSource = timelist;

        }


        /// <summary>
        /// Hide the notification window.
        /// </summary>
        public void Hide()
        {
            frmPopup.painting_require = false;
      //      System.Diagnostics.Debug.WriteLine("Animation stopped.");
       //     System.Diagnostics.Debug.WriteLine("Wait timer stopped.");

            tmrWait.Stop();

            animation_working = false;
            sw.Reset();
            tmrAnimation.Stop();
            do_once = true;
            frmPopup.Hide();
        //    System.Diagnostics.Debug.WriteLine("from hide call");
            isAppearing = false;
            frmPopup.cont_post = 0;
            frmPopup.timelist_data.Clear();
            frmPopup.message_cont_show.Clear();
            frmPopup.message_type_show.Clear();
            timelist.Clear(); ;
            timelist_wait.Clear();

            message_type.Clear();
            message_cont.Clear();
            message_image.Clear();

            message_type_wait.Clear();
            message_cont_wait.Clear();
            message_image_wait.Clear();
        }




        /// <summary>
        /// Update form position and opacity to show/hide the window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void tmAnimation_Tick(object sender, EventArgs e)
        {
            frmPopup.painting_require = false;
           
                long elapsed = sw.ElapsedMilliseconds;

                posCurrent = (int)(posStart + ((posStop - posStart) * elapsed / AnimationDuration));
                bool neg = (posStop - posStart) < 0;
                if ((neg && posCurrent < posStop) ||
                    (!neg && posCurrent > posStop))
                {
                    posCurrent = posStop;
                }



                frmPopup.increasehight(posCurrent);
                frmPopup.Top = posCurrent;
                frmPopup.painting_require = true;

        //        System.Diagnostics.Debug.WriteLine(frmPopup.Top);


                if (elapsed > AnimationDuration)
                {

                    animation_working = false;
                    sw.Reset();
                    tmrAnimation.Stop();
               //     System.Diagnostics.Debug.WriteLine("Animation stopped.");

                }
            

        }

        /// <summary>
        /// The wait timer has elapsed, start the animation to hide the window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmWait_Tick(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Wait timer elapsed.");
            tmrWait.Stop();
            tmrAnimation.Interval = AnimationInterval;
            tmrAnimation.Start();
            sw.Stop();
            sw.Start();
            System.Diagnostics.Debug.WriteLine("Animation started.");
        }

        /// <summary>
        /// Start wait timer if the mouse leaves the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void frmPopup_MouseLeave(object sender, EventArgs e)
        {

            if (frmPopup.Visible)
            {
                timer1.Start();
            }


            /*  System.Diagnostics.Debug.WriteLine("MouseLeave");
            if (frmPopup.Visible && (OptionsMenu == null || !OptionsMenu.Visible))
            {
                tmrWait.Interval = Delay;
                tmrWait.Start();
                System.Diagnostics.Debug.WriteLine("Wait timer started.");
            }
            mouseIsOn = false;
           */
        }


        /// <summary>
        /// Stop wait timer if the mouse enters the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmPopup_MouseEnter(object sender, EventArgs e)
        {

            if (isAppearing)
            {
                timer1.Stop();
            }

            /*
            System.Diagnostics.Debug.WriteLine("MouseEnter");
            if (!isAppearing)
            {
                frmPopup.Top = maxPosition;
                frmPopup.Opacity = maxOpacity;
                tmrAnimation.Stop();
                System.Diagnostics.Debug.WriteLine("Animation stopped.");
            }

            tmrWait.Stop();
            System.Diagnostics.Debug.WriteLine("Wait timer stopped.");

            mouseIsOn = true;
             */
        }

        /// <summary>
        /// Dispose the notification form.
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing && frmPopup != null)
                {
                    frmPopup.Dispose();
                }
                disposed = true;
            }
            base.Dispose(disposing);
        }
    }
}
