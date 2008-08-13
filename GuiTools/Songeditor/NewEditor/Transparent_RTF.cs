using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Reflection;


//#If Config <= Beta Then 'Stage:Beta
namespace WindowsT.FormsT
{
    /// <summary><see cref="RichTextBox"/> with transparent background</summary>
    /// <remarks>This control is 100% transaprent and cannot have any other than transparent <see cref="TransparentTextBox.BackColor"/>. To make it semi-transparent, put it onto semitransparent panel.</remarks>
    //	[Author("Ðonny", "dzonny@dzonny.cz", "http://dzonny.cz")]
    //[Version(1, 0, typeof(TransparentTextBox), LastChange = "05/21/2007")]
    //[FirstVersion("05/19/2007")]
    //[Prefix("trb")]
    //[DefaultProperty("Text"), DefaultEvent("Click")]
    //[DefaultBindingProperty("Text")]
    //[ToolboxBitmap(typeof(TransparentTextBox), "TransparentTextBox.bmp")]
    public class TransparentTextBox : RichTextBox
    {
        
        

        #region "CTors"
        /// <summary>CTor</summary>
        public TransparentTextBox()
        {
            //Configures current control - I've tryed almost eth and this works the best
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            //This reduces some flickering a little
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.ResetBackColor();            
        }
        /// <summary>CTor with text</summary>
        /// <param name="Text">Default text of control</param>
        public TransparentTextBox(string Text)
            : this()
        {
            this.Text = Text;
        }
        /// <summary>CTor with back color</summary>
        /// <param name="BackColor">Default background color</param>
        public TransparentTextBox(Color BackColor)
            : this()
        {
            this.BackColor = BackColor;
        }
        /// <summary>CTor with text and back color</summary>
        /// <param name="BackColor">Default background color</param>
        /// <param name="Text">Default text of control</param>
        public TransparentTextBox(Color BackColor, string Text)
            : this()
        {
            this.Text = Text;
            this.BackColor = BackColor;
        }
        #endregion
        #region "Paint"
        /// <summary>Gets the required creation parameters when the control handle is created.</summary>
        /// <returns>A <see cref="System.Windows.Forms.CreateParams"/> that contains the required creation parameters when the handle to the control is created.</returns>
        protected override System.Windows.Forms.CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle = cp.ExStyle | 32;
                //Transparent
                return cp;
            }
        }
        ///' <summary>Raises the <see cref="BackColorChanged"/> event.</summary>
        ///' <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
        //Protected Overrides Sub OnBackColorChanged(ByVal e As System.EventArgs)
        //    RecreateHandle()
        //    MyBase.OnBackColorChanged(e)
        //End Sub
        /// <summary>Determines if <see cref="InvalidateParent"/> is currently on call stack to avoid recursive calls</summary>
        private bool InInvalidateParent = false;
        /// <summary>Invalidates given parent rectrangle</summary>
        /// <param name="rect">Rectangle to be invalidated</param>
        private void InvalidateParent(Rectangle rect)
        {
            if (this.Parent == null || InInvalidateParent) return;
            Rectangle pr = default(Rectangle);
            try
            {
                InInvalidateParent = true;
                if (rect.Width == 0 || rect.Height == 0) return;

                pr = new Rectangle(this.Left + rect.Left, this.Top + rect.Top, rect.Width, rect.Height);
                Parent.Invalidate(pr, true);
            }
            finally
            {
                InInvalidateParent = false;
            }
        }
        /// <summary>Processes Windows messages.</summary>
        /// <param name="m">The Windows System.Windows.Forms.Message to process</param>
        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            const Int32 WM_PAINT = 15;
            const Int32 WM_PRINT_CLIENT = 792;
            const Int32 WM_KEYDOWN = 256;
            const Int32 WM_KEYUP = 257;
            const Int32 WM_PASTE = 770;
            const Int32 WM_CUT = 768;

            switch (m.Msg)
            {
                case WM_PRINT_CLIENT:
                case WM_PAINT:
                    if (InInvalidateParent) return;
                    this.DefWndProc(ref m);
                    //This draws the RichTextBox
                    break;
            }
            bool KbdProcessOnStack = KbdProcess;
            try
            {
                //Detect keyboard interaction
                KbdProcess = m.Msg == WM_KEYDOWN || m.Msg == WM_KEYUP || m.Msg == WM_PASTE || m.Msg == WM_CUT;
                base.WndProc(ref m);
            }
            finally
            {
                if (!KbdProcessOnStack) KbdProcess = false;
            }
        }
        /// <summary>True when <see cref="WndProc"/> with keyboard message is on stack. Used in <see cref="OnTextChanged"/> to decide wheather to invalidate whole control (False) or only starting at current line (True)</summary>
        private bool KbdProcess = false;


        public void Update()
        {
            
            //Point pos = this.GetPositionFromCharIndex(this.SelectionStart);
            //InvalidateParent(new Rectangle(this.ClientRectangle.Left, this.ClientRectangle.Top + pos.Y, this.ClientRectangle.Width, this.ClientRectangle.Height - (this.ClientRectangle.Top + pos.Y)));
            InvalidateParent(this.ClientRectangle);
        }


        
        protected override void OnClick(EventArgs e)
        {
            //simple dirty fix for update bug
            if (SelectionEnd == CurrentPosition)
            {
                Update();
            }
            base.OnClick(e);
        }

        
      public int CurrentPosition
			{
			get { return this.SelectionStart; }
			}

		public int SelectionEnd
			{
			get { return SelectionStart + SelectionLength; }
			}
	

        protected override void OnKeyUp(KeyEventArgs e)
        {
            //Text changed by user
            //Point pos = this.GetPositionFromCharIndex(this.SelectionStart);
            //InvalidateParent(new Rectangle(this.ClientRectangle.Left, this.ClientRectangle.Top + pos.Y, this.ClientRectangle.Width, this.ClientRectangle.Height - (this.ClientRectangle.Top + pos.Y)));

            base.OnKeyUp(e);
        }

        

        /// <summary>Raises the <see cref="TextChanged"/> event.</summary>
        /// <param name="e">An <see cref="System.EventArgs"/> that contains the event data</param>
        protected override void OnTextChanged(System.EventArgs e)
        {
            if (KbdProcess)
            {
                //Text changed by user
                Point pos = this.GetPositionFromCharIndex(this.SelectionStart);
                InvalidateParent(new Rectangle(this.ClientRectangle.Left, this.ClientRectangle.Top + pos.Y, this.ClientRectangle.Width, this.ClientRectangle.Height - (this.ClientRectangle.Top + pos.Y)));
            }
            else
            {
                //Text changed programatically
                InvalidateParent(this.ClientRectangle);
            }
            base.OnTextChanged(e);
        }
        /// <summary>Raises the <see cref="HScroll"/> event.</summary>
        /// <param name="e">An <see cref="EventArgs"/> that contains the event data</param>
        protected override void OnHScroll(System.EventArgs e)
        {
            InvalidateParent(this.ClientRectangle);
            base.OnHScroll(e);
        }
        /// <summary>Raises the <see cref="VScroll"/> event.</summary>
        /// <param name="e">An <see cref="EventArgs"/> that contains the event data</param>
        protected override void OnVScroll(System.EventArgs e)
        {
            InvalidateParent(this.ClientRectangle);
            base.OnVScroll(e);
        }
        /// <summary>Raises the <see cref="SelectionChanged"/> event.</summary>
        /// <param name="e">An <see cref="EventArgs"/> that contains the event data</param>
        protected override void OnSelectionChanged(System.EventArgs e)
        {
            InvalidateParent(this.ClientRectangle);
            base.OnSelectionChanged(e);
        }
        /// <summary>Raises the System.Windows.Forms.Control.Move event.</summary>
        /// <param name="e">An System.EventArgs that contains the event data.</param>
        protected override void OnMove(System.EventArgs e)
        {
            RecreateHandle();
            base.OnMove(e);
        }
        #endregion

        #region "Properties"
        /// <summary>Gets or sets the background color for the control.</summary>
        /// <returns>A <see cref="System.Drawing.Color"/> that represents the background color of the control.</returns>
        [DefaultValue(typeof(Color), "Transparent"), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public override System.Drawing.Color BackColor
        {
            get { return base.BackColor; }
            set { base.BackColor = value; }
        }
        /// <summary>Sets <see cref="BackColor"/> to <see cref="Color.Transparent"/></summary>
        public override void ResetBackColor()
        {
            this.BackColor = Color.Transparent;
        }
        #endregion
    }
}
