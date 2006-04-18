
/*/----------------------------------------------------------------------------
                                    LISTEX v1.0
                                   -------------
Coded By : Nnamdi Onyeyiri
Email    : theeclypse@hotmail.com
Website  : http://www.onyeyiri.co.uk
                                   -------------
ListEx is my version of the Sonork Chat Window's listbox.  It can have those
cool colours, the line under each item, and an image / bullet next to the items.
If you have any problems using it, just let me know.

                                                         © 2002 Nnamdi Onyeyiri
----------------------------------------------------------------------------/*/

using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;
using System.Reflection;

namespace Lister
{
	/////
	// A struct to hold information on each item in the listbox.
	// This information is called up in the OnDrawItem method.
	public struct ItemInfo
	{
		public ItemInfo(string txt, int img, Color clr1, Color clr2)
		{
			this.clr = clr1;
			this.colorFocus = clr2;
			this.text = txt;
			this.img = img;
		}
		private Color clr;
		public Color Color
		{
			get { return this.clr; }
			set { this.clr = value; }
		}
		private int img;

		public int Img
		{
			get { return this.img; }
			set { this.img = value; }
		}
		private Color colorFocus;
		public Color ColorFocus
		{
			get { return this.colorFocus; }
			set { this.colorFocus = value; }
		}
		private string text;
		public string Text
		{
			get { return this.text; }
			set { this.text = value; }
		}
		
		public static bool operator==(ItemInfo i1, ItemInfo i2)
		{
			if ((i1.Color == i2.Color) &&
				(i1.Img == i2.Img) &&
				(i1.ColorFocus == i2.ColorFocus) &&
				(i1.Text == i2.Text))
				return true;
			
			return false;
		}
		
		public static bool operator!=(ItemInfo i1, ItemInfo i2)
		{
			if ((i1.Color != i2.Color) &&
				(i1.Img != i2.Img) &&
				(i1.ColorFocus != i2.ColorFocus) &&
				(i1.Text != i2.Text))
				return true;
			
			return false;
		}

		public override bool Equals(object obj)
		{
			ItemInfo Ii = (ItemInfo)obj;
			if ((Ii.Color == this.Color) &&
				(Ii.Img == this.Img) &&
				(Ii.ColorFocus == this.ColorFocus) &&
				(Ii.Text == this.Text))
				return true;
			
			return false;
		}
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
	
	/////
	// The ListEx class.Where ll the work is done. *Duh*
	public class ListEx : ListBox
	{
		/////
		// A bunch of variables.
		ArrayList itemInfo;		// specifies the array of item info, for
									// colours information etc.
		MenuItem[] menuItems;		// The list of items for the Context menu.
		int Selected;				// The currently selected item. [used for txtbox]
		TextBox tb;					// The TextBox object.
		bool HasTextBox = false;	// Whether of not the text box is displayed.
		private ImageList imgs;		// The image list of bullets
		public Color lineColor;		// The colour of the line.
		public bool running = false; 	// manual, if program is running

		/////
		// Properties to access private variables.
		public ImageList Imgs
		{
			get { return this.imgs; }
			set { this.imgs = value; }
		}
		
		public Color LineColor
		{
			get { return this.lineColor; }
			set { this.lineColor = value; }
		}
		
		public static Color ClrNormal
		{
			get 
			{
				return Color.FromArgb(80, SystemColors.Control);
			}
		}
		
		public static Color ClrFocus
		{
			get 
			{
				return SystemColors.ControlDark;
			}
		}
		
		/////
		// This is the constructor
		public ListEx()
		{
			/////
			// Initialise the ItemInfo information to empty.
			this.itemInfo = new ArrayList();
			
			/////
			// Extra settings to set drawing mode.
			DrawMode = DrawMode.OwnerDrawVariable;	
			SetStyle(ControlStyles.DoubleBuffer, true);
			SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			
			/////
			// Set the line color
			this.lineColor = Color.FromArgb(199,199,199);

		}	
		
		/////
		// This is the constructor
		public ListEx(ImageList imgs)
		{
			/////
			// Initialise the ItemInfo information to empty.
			this.itemInfo = new ArrayList();
			
			/////
			// Extra settings to set drawing mode.
			DrawMode = DrawMode.OwnerDrawVariable;	
			SetStyle(ControlStyles.DoubleBuffer, true);
			SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			
			/////
			// Setup the image list
			this.imgs = imgs;
			
			/////
			// Set the line color
			this.lineColor = Color.FromArgb(199,199,199);
		}
		
		/////
		// Override to the ondraw function.
		protected override void OnDrawItem(DrawItemEventArgs e)
		{
			/////
			// Make sure we actually have an item to draw
			// before drawing, as we dont want an exception thrown
			// about our ItemInfo array do we?
			if(e.Index > -1 && e.Index < this.itemInfo.Count)
			{
				/////
				// Draw the usual stuff.
				e.DrawBackground();
				e.DrawFocusRectangle();

				/////
				// Check what state we are in before choosing the color.
				Rectangle bounds = e.Bounds;
				Color clr;
				if ((e.State & DrawItemState.Focus) == 0)
					clr = ((ItemInfo)this.itemInfo[e.Index]).Color;
				else
					clr = ((ItemInfo)this.itemInfo[e.Index]).ColorFocus;

				/////
				// Fill the items area with the needed colour.
				e.Graphics.FillRectangle(new SolidBrush(clr), bounds);

				/////
				// Draw the divinding line.
				Point p1 = new Point(bounds.X,bounds.Bottom-1);
				Point p2 = new Point(bounds.X+this.Size.Width, bounds.Y+bounds.Height-1);
				e.Graphics.DrawLine(new Pen(lineColor),p1, p2);

				/////
				// Format and draw the text.
				StringFormat strfmt = new StringFormat();
				strfmt.Alignment = StringAlignment.Near;
				Rectangle TxtPos = new Rectangle();
				TxtPos.X = bounds.Left+5;
				if (this.imgs != null)
					TxtPos.X = TxtPos.X+this.imgs.ImageSize.Width;
				else
					TxtPos.X = TxtPos.X+10;
				TxtPos.Y = (bounds.Top+5);
				TxtPos.Width = this.Size.Width-16;
				e.Graphics.DrawString(this.Items[e.Index].ToString(), Font, new SolidBrush(e.ForeColor), TxtPos, strfmt);
				/*
				if (((ItemInfo)this.itemInfo[e.Index]).Img != -1)
				{
					if (imgs != null)
					{
						/////
						// Draw the image as we have an array.
						Rectangle rect = new Rectangle();
						rect.Y = e.Bounds.Y+3;
						rect.X = e.Bounds.X+2;
						this.imgs.Draw(e.Graphics, rect.Left,rect.Top,((ItemInfo)this.itemInfo[e.Index]).Img);
					}
					else
					{
						/////
						// Draw the bullet point
						Rectangle rect = new Rectangle();
						rect.Y = e.Bounds.Y+7;
						rect.X = e.Bounds.X+5;
						rect.Width = 5;
						rect.Height = 5;
						e.Graphics.FillEllipse(new SolidBrush(Color.Black), rect);
					}
				}
				else
				{
					/////
					// Draw the bullet point
					Rectangle rect = new Rectangle();
					rect.Y = e.Bounds.Y+7;
					rect.X = e.Bounds.X+5;
					rect.Width = 5;
					rect.Height = 5;
					e.Graphics.FillEllipse(new SolidBrush(Color.Black), rect);
				} 				*/
			}

			base.OnDrawItem(e);

		}

		/////
		// Override for the OnMeasureItem, to make sure the item
		// is as big as needed
		protected override void OnMeasureItem(MeasureItemEventArgs e)
		{

			/////
			// Set the height - based on the height of the text,
			// its length, and the width available.
			e.ItemHeight = 21;
			SizeF sf;
			if (Items.Count > e.Index) 
			{
				string s = Items[e.Index].ToString();
				sf = e.Graphics.MeasureString(s,Font,Width);
			} 
			else 
			{
				sf = new SizeF(1, 1);
			}
			int htex = 10;
			e.ItemWidth = Width;	
			e.ItemHeight = (int)sf.Height + htex;	
			
			/////
			// Check we have an image list before checking if the item
			// is tall enough to fit the images.
			if (this.imgs != null)
			{
				if (e.ItemHeight < this.imgs.ImageSize.Height)
					e.ItemHeight = this.imgs.ImageSize.Height+10;
			}
		  	
			base.OnMeasureItem(e);
		}
	
		/////
		// Override to the OnDoubleClick, so that the Edit text box
		// can be drawn.
		protected override void OnDoubleClick(EventArgs e)
		{
			if (HasItems())
			{
			 /*	/////
				// Get the rect of the current item.
				Rectangle irect = GetItemRectangle(this.SelectedIndex);
				
				/////
				// Check if there is a vertical scrollbar.
				int iSWidth = SystemInformation.VerticalScrollBarWidth;
				int h = (this.PreferredHeight < this.Size.Height) ? 0 : iSWidth;
				
				/////
				// Initiate the textbox.
				tb = new TextBox();
				tb.Size = new Size(this.Size.Width-28-h, irect.Height-10);
				tb.Text = this.Items[this.SelectedIndex].ToString();
				tb.BorderStyle = BorderStyle.None;
				
				/////
				// Check if we want a vertical scrollbar.
				string s = Items[this.SelectedIndex].ToString();
				Graphics grfx = CreateGraphics();
				SizeF sf = grfx.MeasureString("hello",Font,Width);
				int IH = (int)sf.Height + 10;			
				if (!(GetItemHeight(this.SelectedIndex) ==  (Font.Height+10)))
					tb.ScrollBars = ScrollBars.Vertical;
				
				/////
				// More setup for the textbox.
				tb.SelectAll();
				tb.Multiline = true;
				Rectangle rect = GetItemRectangle(this.SelectedIndex);
				rect.X = rect.X+5;
				if (this.imgs != null)
					rect.X = rect.X+this.imgs.ImageSize.Width;
				else
					rect.X = rect.X+10;
				rect.Y = rect.Y+4;
				tb.Location = new Point(rect.X, rect.Y);
				this.Controls.Add(tb);
				tb.Show();
				tb.Focus();
				this.HasTextBox = true;  */
				this.Selected = this.SelectedIndex;
			}
			base.OnDoubleClick(e);
		}
		
		/////
		// Override to the OnClick method
		protected override void OnClick(EventArgs e)
		{
			/////
			// Check if we have a textbox shown.
			// If we do, get rid of it.
			if (HasItems())
			{
				if (this.HasTextBox)
				{
					this.Items[this.Selected] = tb.Text;
					tb.Hide();
					this.Parent.Controls.Remove(tb);
					this.HasTextBox = false;
				}
			}
		}
		
		/////
		// Override to the OnMouseUp evnt
		protected override void OnMouseUp(MouseEventArgs e)
		{
			if (HasItems())
			{
				/////
				// select the item that was right cliked on
				if (e.Button == MouseButtons.Right)
				{
					Rectangle mrect = new Rectangle();
					Point p = PointToClient(Control.MousePosition);
					mrect.X = p.X;
					mrect.Y = p.Y;
					mrect.Width = 1;
					mrect.Height = 1;
					
					for (int i=0; i < this.Items.Count; i++)
					{
						Rectangle rect = new Rectangle();
						rect = GetItemRectangle(i);
						if (rect.Contains(mrect))
							this.SelectedIndex = i;
					}
				}
				
				//////
				// If it was a right click and there is no textbox
				// Show the context menu - if it has one.
				if ((e.Button == MouseButtons.Right) && (!this.HasTextBox))
				{
					if (this.menuItems != null)
					{
						ContextMenu cm = new ContextMenu(this.menuItems);
						cm.Show(this, PointToClient(Control.MousePosition));
					}
				}
				
				/////
				// If it was a left mouse click, and over the area of the bullet/image
				// invoke the PressIcon event.
				if (e.Button == MouseButtons.Left)
				{
					Rectangle mrect = new Rectangle();
					Point p  = PointToClient(Control.MousePosition);
					mrect.X = p.X;
					mrect.Y = p.Y;
					mrect.Width = 1;
					mrect.Height = 1;
					Rectangle irect = GetItemRectangle(this.SelectedIndex);
					if (this.imgs != null)
					{
						irect.Width = this.imgs.ImageSize.Width;
						irect.Height = this.imgs.ImageSize.Height;
					}
					else
					{
						irect.Width = 16;
						irect.Height = 16;
					}
					if(irect.Contains(mrect))
							OnPressIcon(this.SelectedIndex);
				}
			}
			
			base.OnMouseUp(e);
		}
		
		/////
		// used to set the context menu for the control.
		public void SetMenu(MenuItem[] items)
		{
			this.menuItems = new MenuItem[items.Length];
			for(int i=0; i < items.Length; i++)
			{
				this.menuItems[i] = items[i];
			}
		}
		
		////
		// events
		public delegate void EventHandler(int Index);
		public event EventHandler PressIcon;
		protected virtual void OnPressIcon(int Index)
		{
			if (PressIcon != null)
				PressIcon(Index);
		}
		
		//////
		// Function to add an item
		public void Add(string text, Color clr, Color clr2, int img)
		{
			this.Items.Add(text);
			ItemInfo Ii = new ItemInfo();
			Ii = new ItemInfo(text, img, clr, clr2);
			this.itemInfo.Add(Ii);
		}
		
		//////
		// Function to add an item
		public void Add(string text, Color clr, Color clr2)
		{
			this.Items.Add(text);
			ItemInfo Ii = new ItemInfo();
			Ii = new ItemInfo(text, -1, clr, clr2);
			this.itemInfo.Add(Ii);
		}
		
		//////
		// Function to add an item
		public void Add(string text)
		{
			this.Items.Add(text);
			ItemInfo Ii = new ItemInfo();
			Ii = new ItemInfo(text, -1, ListEx.ClrNormal, ListEx.ClrFocus);
			this.itemInfo.Add(Ii);
		}
		
		//////
		// Function to add an item
		public void Add(string text, int img)
		{
			this.Items.Add(text);
			ItemInfo Ii = new ItemInfo();
			Ii = new ItemInfo(text, img, ListEx.ClrNormal, ListEx.ClrFocus);
			this.itemInfo.Add(Ii);
		}
		
		//////
		// Function to remove an item
		public void Remove(int id)
		{
			if ((HasItems()) && (this.SelectedIndex != -1))
			{
				this.Items.RemoveAt(id);
				this.itemInfo.RemoveAt(id);
			}
		}
		
		/////
		// Change an items text
		public void ChangeItem(int id, string text)
		{
			if (HasItems())
			{
				ItemInfo Ii = ((ItemInfo)this.itemInfo[id]);
				Ii.Text = text;
				this.itemInfo[id] = Ii;
				this.Items[id] = (object)text;
			}
		}
		
		/////
		// Change an items text
		public void ChangeItemInfo(int id, Color clrNormal, Color clrFocus)
		{
			if (HasItems())
			{
				ItemInfo Ii = ((ItemInfo)this.itemInfo[id]);
				Ii.Color = clrNormal;
				Ii.ColorFocus = clrFocus;
				this.itemInfo[id] = Ii;
			}
		}
		
		/////
		// Override to the inherited WndProc
		// I needed to use it to capture the scroll event
		//[CustomPermissionAttribute(SecurityAction.LinkDemand)]
		protected override void WndProc(ref Message m)
		{
			/////
			// if we have a scroll event, and a textbox is shown
			// relocate the textbox.
			if ((m.Msg == 0x115) && (this.HasTextBox))
			{
				tb.Hide();
				Rectangle rect = GetItemRectangle(this.Selected);
				rect.X = rect.X+this.imgs.ImageSize.Width+5;
				rect.Y = rect.Y+4;
				tb.Location = new Point(rect.X, rect.Y);
				tb.Show();
			}
			base.WndProc(ref m);
		}
		
		/////
		// Helper function, to make sure there are items in the list
		public bool HasItems()
		{
			if (this.Items.Count > 0)
				return true;
			return false;
		}
	}
}

