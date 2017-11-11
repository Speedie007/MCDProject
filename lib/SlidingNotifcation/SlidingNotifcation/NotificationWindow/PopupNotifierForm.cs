/*
 *	Created/modified in 2011 by Simon Baer
 *	
 *  Based on the Code Project article by Nicolas Wälti:
 *  http://www.codeproject.com/KB/cpp/PopupNotifier.aspx
 * 
 *  Licensed under the Code Project Open License (CPOL).
 */

using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections.Generic;

namespace NotificationWindow
{
    /// <summary>
    /// This is the form of the actual notification window.
    /// </summary>
    public class PopupNotifierForm : System.Windows.Forms.Form
    {
        /// <summary>
        /// Event that is raised when the text is clicked.
        /// </summary>
        public event EventHandler LinkClick;

        /// <summary>
        /// Event that is raised when the notification window is manually closed.
        /// </summary>
        public event EventHandler CloseClick;

        internal event EventHandler ContextMenuOpened;
        internal event EventHandler ContextMenuClosed;

        private bool mouseOnClose = false;
        private bool mouseOnLink = false;
        private bool mouseOnOptions = false;
        private int heightOfTitle;
        private bool mouseinside = false;
        public bool painting_require = false;

        #region GDI objects

        private bool gdiInitialized = false;
        private Rectangle rcBody;
        private Rectangle rcHeader;
        private Rectangle rcForm;
        private Rectangle RectClose1;
        private LinearGradientBrush brushBody;
        private LinearGradientBrush brushHeader;
        private Brush brushButtonHover;
        private Pen penButtonBorder;
        private Pen penContent;
        private Pen penBorder;
        private Brush brushForeColor;
        private Brush brushLinkHover;
        private Brush brushContent;
        private Brush brushTitle;
        public Boolean paintreq = false;

        Dictionary<string,string> _item = new Dictionary<string,string>();
      //  public List<string> _item = new List<string>();
        public int cont_post;
        public int frm_hight = 70;
        public int frm_wide = 220;
        int spacing = 50;
        public List<long> timelist_data = new List<long>();
        public List<string> message_type_show = new List<string>();
        public List<string> message_cont_show = new List<string>();
        public List<Image> message_image_show = new List<Image>();
        
        #endregion

        /// <summary>
        /// Create a new instance.
        /// </summary>
        /// <param name="parent">PopupNotifier</param>
        public PopupNotifierForm(PopupNotifier parent)
        {
            Parent = parent;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.ShowInTaskbar = false;

            this.VisibleChanged += new EventHandler(PopupNotifierForm_VisibleChanged);
            this.MouseMove += new MouseEventHandler(PopupNotifierForm_MouseMove);
            this.MouseUp += new MouseEventHandler(PopupNotifierForm_MouseUp);
           this.Paint += new PaintEventHandler(PopupNotifierForm_Paint);
           this.MouseEnter += new EventHandler(PopupNotifierForm_MouseEnter);
            this.MouseLeave +=new EventHandler(PopupNotifierForm_MouseLeave);
         
           
            //this.Paint -= new PaintEventHandler(PopupNotifierForm_Paint);
        }

      



        private void PopupNotifierForm_MouseEnter(object sender, EventArgs e)
        {
            mouseinside = true;
            Invalidate();
        }




        private void PopupNotifierForm_MouseLeave(object sender, EventArgs e)
        {

            mouseinside = false;
            Invalidate();
        }

        /// <summary>
        /// The form is shown/hidden.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PopupNotifierForm_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                mouseOnClose = false;
                mouseOnLink = false;
                mouseOnOptions = false;
            }
        }

        /// <summary>
        /// Used in design mode.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ClientSize = new System.Drawing.Size(392, 66);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PopupNotifierForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.TopMost = true;
            this.ResumeLayout(false);
           

        }

        /// <summary>
        /// Gets or sets the parent control.
        /// </summary>
        public new PopupNotifier Parent { get; set; }

        /// <summary>
        /// Add two values but do not return a value greater than 255.
        /// </summary>
        /// <param name="input">first value</param>
        /// <param name="add">value to add</param>
        /// <returns>sum of both values</returns>
        private int AddValueMax255(int input, int add)
        {
            return (input + add < 256) ? input + add : 255;
        }

        /// <summary>
        /// Subtract two values but do not returns a value below 0.
        /// </summary>
        /// <param name="input">first value</param>
        /// <param name="ded">value to subtract</param>
        /// <returns>first value minus second value</returns>
        private int DedValueMin0(int input, int ded)
        {
            return (input - ded > 0) ? input - ded : 0;
        }

        /// <summary>
        /// Returns a color which is darker than the given color.
        /// </summary>
        /// <param name="color">Color</param>
        /// <returns>darker color</returns>
        private Color GetDarkerColor(Color color)
        {
            return System.Drawing.Color.FromArgb(255, DedValueMin0((int)color.R, Parent.GradientPower), DedValueMin0((int)color.G, Parent.GradientPower), DedValueMin0((int)color.B, Parent.GradientPower));
        }

        /// <summary>
        /// Returns a color which is lighter than the given color.
        /// </summary>
        /// <param name="color">Color</param>
        /// <returns>lighter color</returns>
        private Color GetLighterColor(Color color)
        {
            return System.Drawing.Color.FromArgb(255, AddValueMax255((int)color.R, Parent.GradientPower), AddValueMax255((int)color.G, Parent.GradientPower), AddValueMax255((int)color.B, Parent.GradientPower));
        }

        /// <summary>
        /// Gets the rectangle of the content text.
        /// </summary>
        private RectangleF RectContentText
        {
            get
            {
                if (Parent.Image != null)
                {
                    return new RectangleF(
                        Parent.ImagePadding.Left + Parent.ImageSize.Width + Parent.ImagePadding.Right + Parent.ContentPadding.Left,
                        Parent.HeaderHeight + Parent.TitlePadding.Top + heightOfTitle + Parent.TitlePadding.Bottom + Parent.ContentPadding.Top,
                        this.Width - Parent.ImagePadding.Left - Parent.ImageSize.Width - Parent.ImagePadding.Right - Parent.ContentPadding.Left - Parent.ContentPadding.Right - 16 - 5,
                        this.Height - Parent.HeaderHeight - Parent.TitlePadding.Top - heightOfTitle - Parent.TitlePadding.Bottom - Parent.ContentPadding.Top - Parent.ContentPadding.Bottom - 1);
                }
                else
                {
                    return new RectangleF(
                        Parent.ContentPadding.Left,
                        Parent.HeaderHeight + Parent.TitlePadding.Top + heightOfTitle + Parent.TitlePadding.Bottom + Parent.ContentPadding.Top,
                        this.Width - Parent.ContentPadding.Left - Parent.ContentPadding.Right - 16 - 5,
                        this.Height - Parent.HeaderHeight - Parent.TitlePadding.Top - heightOfTitle - Parent.TitlePadding.Bottom - Parent.ContentPadding.Top - Parent.ContentPadding.Bottom - 1);
                }
            }
        }

        /// <summary>
        /// gets the rectangle of the close button.
        /// </summary>
        private Rectangle RectClose
        {
            get { return new Rectangle(this.Width - 5 - 16, Parent.HeaderHeight + 3, 16, 16); }
        }

        /// <summary>
        /// Gets the rectangle of the options button.
        /// </summary>
        private Rectangle RectOptions
        {
            get { return new Rectangle(this.Width - 5 - 16, Parent.HeaderHeight + 3 + 16 + 5, 16, 16); }
        }

        /// <summary>
        /// Update form to display hover styles when the mouse moves over the notification form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PopupNotifierForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (Parent.ShowCloseButton)
            {
                mouseOnClose = RectClose1.Contains(e.X, e.Y);
            }
            if (Parent.ShowOptionsButton)
            {
                mouseOnOptions = RectOptions.Contains(e.X, e.Y);
            }
            mouseOnLink = RectContentText.Contains(e.X, e.Y);
            Invalidate();
        }

        /// <summary>
        /// A mouse button has been released, check if something has been clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PopupNotifierForm_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (RectClose.Contains(e.X, e.Y) && (CloseClick != null))
                {
                    CloseClick(this, EventArgs.Empty);
                }
                if (RectContentText.Contains(e.X, e.Y) && (LinkClick != null))
                {
                    LinkClick(this, EventArgs.Empty);
                }
                if (RectOptions.Contains(e.X, e.Y) && (Parent.OptionsMenu != null))
                {
                    if (ContextMenuOpened != null)
                    {
                        ContextMenuOpened(this, EventArgs.Empty);
                    }
                    Parent.OptionsMenu.Show(this, new Point(RectOptions.Right - Parent.OptionsMenu.Width, RectOptions.Bottom));
                    Parent.OptionsMenu.Closed += new ToolStripDropDownClosedEventHandler(OptionsMenu_Closed);
                }
            }
        }

        /// <summary>
        /// The options popup menu has been closed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OptionsMenu_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            Parent.OptionsMenu.Closed -= new ToolStripDropDownClosedEventHandler(OptionsMenu_Closed);

            if (ContextMenuClosed != null)
            {
                ContextMenuClosed(this, EventArgs.Empty);
            }
        }

  
        /// <summary>
        /// Free all GDI objects.
        /// </summary>
        private void DisposeGDIObjects()
        {
            if (gdiInitialized)
            {
                brushBody.Dispose();
                brushHeader.Dispose();
                brushButtonHover.Dispose();
                penButtonBorder.Dispose();
                penContent.Dispose();
                penBorder.Dispose();
                brushForeColor.Dispose();
                brushLinkHover.Dispose();
                brushContent.Dispose();
                brushTitle.Dispose();
            }
        }


        /// <summary>
        /// Draw the notification form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 

        
        
       
        private void PopupNotifierForm_Paint(object sender, PaintEventArgs e)
        {
            if (true == painting_require)
            {

                // draw window
                if (cont_post == 1)
                {
                    rcBody = new Rectangle(0, 0, this.Width, frm_hight);
                    rcHeader = new Rectangle(0, 0, this.Width, Parent.HeaderHeight);
                    rcForm = new Rectangle(0, 0, this.Width - 1, (frm_hight - 1));
                    //     RectClose1 = new Rectangle(this.Width - 5 - 16, Parent.HeaderHeight + 3 , 16, 16);
                    RectClose1 = new Rectangle(this.Width - 5 - 16, 2, 16, 16);
                    //frmPopup_CloseClick

                }

                else
                {
                    rcBody = new Rectangle(0, 0, this.Width, ((cont_post) * 75));
                    rcHeader = new Rectangle(0, 0, this.Width, Parent.HeaderHeight);
                    //  RectClose1 = new Rectangle(this.Width - 5 - 16, Parent.HeaderHeight + 3, 16, 16);
                    RectClose1 = new Rectangle(this.Width - 5 - 16, 2, 16, 16);

                }

                Brush brushBody = new SolidBrush(Parent.BodyColor);
                Brush brushHeader = new SolidBrush(Parent.HeaderColor);
                e.Graphics.FillRectangle(brushBody, rcBody);
                e.Graphics.FillRectangle(brushHeader, rcHeader);

             //   System.Diagnostics.Debug.WriteLine("paint called");
                for (int i = 0; i < cont_post; i++)
                {
                    showcontent(e, _item, i);


                }
            }
              
        
          

         
        }

        public void increasehight(int current_heightvalue)
        {
            this.Height = Screen.PrimaryScreen.WorkingArea.Bottom - current_heightvalue ;
        }

      

        bool paint_once = true;

        private void showcontent(PaintEventArgs e, Dictionary<string, string> _item, int cont_position)
        {

           

           
            brushButtonHover = new SolidBrush(Parent.ButtonHoverColor);
            penButtonBorder = new Pen(Parent.ButtonBorderColor);
            penContent = new Pen(Parent.ContentColor, 2);
            penBorder = new Pen(Parent.BorderColor);
            brushForeColor = new SolidBrush(ForeColor);
            brushLinkHover = new SolidBrush(Parent.ContentHoverColor);
            brushContent = new SolidBrush(Parent.ContentColor);
            brushTitle = new SolidBrush(Parent.TitleColor);
            Pen pen_divde = new Pen( Color.White);
            gdiInitialized = true;

          
            //Dispatcher Test
            if (Parent.ShowGripText)
            {

                e.Graphics.DrawString(Parent.HeaderText, Parent.HeaderFont, brushTitle, (int)((rcHeader.X)), (int)(rcHeader.Y) + 2);


            }


            //paint of close button
            if (Parent.ShowCloseButton)
            {
                
                if (mouseOnClose)
                {
                    e.Graphics.FillRectangle(brushButtonHover, RectClose1);
                    e.Graphics.DrawRectangle(penButtonBorder, RectClose1);
                }

                if (mouseinside == true)
                {
                    e.Graphics.DrawLine(penContent, RectClose1.Left + 4, RectClose1.Top + 4, RectClose1.Right - 4, RectClose1.Bottom - 4);
                    e.Graphics.DrawLine(penContent, RectClose1.Left + 4, RectClose1.Bottom - 4, RectClose1.Right - 4, RectClose1.Top + 4);

                }
            }
               


            // draw icon
            if (Parent.Image != null)
            {
                e.Graphics.DrawImage(message_image_show[cont_position], Parent.ImagePadding.Left, Parent.HeaderHeight + Parent.ImagePadding.Top + (spacing * cont_position), Parent.ImageSize.Width, Parent.ImageSize.Height);
             
            }

         

            // calculate height of title
            heightOfTitle = (int)e.Graphics.MeasureString("A", Parent.TitleFont).Height;
            int titleX = Parent.TitlePadding.Left;
            if (Parent.Image != null)
            {
                titleX += Parent.ImagePadding.Left + Parent.ImageSize.Width + Parent.ImagePadding.Right;
            }

            // draw message type
           
            e.Graphics.DrawString(message_type_show[cont_position], Parent.TitleFont, brushTitle, titleX + 5, Parent.HeaderHeight + Parent.TitlePadding.Top + (spacing * cont_position));
            //draw message detail;
            int temp_string_length = message_cont_show[cont_position].Length;
            if (temp_string_length < 25)
            {
                e.Graphics.DrawString(message_cont_show[cont_position].Substring(0,25), Parent.ContentFont, brushContent, titleX + 5, Parent.HeaderHeight + 18 + Parent.TitlePadding.Top + (spacing * cont_position));
            }
            else
            {
                e.Graphics.DrawString(message_cont_show[cont_position].Substring(0, 25).Trim(), Parent.ContentFont, brushContent, titleX + 5, Parent.HeaderHeight + 18 + Parent.TitlePadding.Top + (spacing * cont_position));
         
                e.Graphics.DrawString(message_cont_show[cont_position].Substring(26).Trim(), Parent.ContentFont, brushContent, titleX + 5, Parent.HeaderHeight + 28 + Parent.TitlePadding.Top + (spacing * cont_position));
          

            }
            //   e.Graphics.DrawString(Parent.ContentText, Parent.ContentFont, brushContent, RectContentText);
            if (cont_position > 0)
            {
                Point start_point = new Point(Parent.ImagePadding.Left, 19+ (50 * cont_position));
                Point end_point = new Point(Parent.ImagePadding.Left + 200,19 + (50 * cont_position));
                e.Graphics.DrawLine(pen_divde, start_point, end_point);
            }
        }

       
        /// <summary>
        /// Dispose GDI objects.
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
           
            base.Dispose(disposing);
        }
    }
}
