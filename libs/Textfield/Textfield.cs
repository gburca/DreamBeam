using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VerseEditor
{
    public delegate void ControlChangeHandler(object sender, EventArgs e);
    //public delegate void EventHandler(object sender, EventArgs e);

    public partial class Textfield : UserControl
    {
        public Textfield()
        {
            InitializeComponent();

             textBox.TextChanged += new EventHandler(ControlChanged);
        }
        
        //public event DreamBeam.ControlChangeHandler ControlChangedEvent;

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            this.Height = 10 + textBox.Lines.Count() * 13;
            NotifyControlChangeListeners(sender, e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(this.textBox.Lines.Count().ToString());
           // MessageBox.Show(this.textBox.get);
        }

        // Declare the event, which is associated with our
        // delegate SubmitClickedHandler(). Add some attributes
        // for the Visual C# control property.
        [Category("Action")]
        [Description("Fires when a text format option setting is changed.")]
        public event ControlChangeHandler ControlChangedEvent;

        private void ControlChanged(Object sender, EventArgs e)
        {
            // If we're in the process of changing all the controls, wait until
            // they're all changed before firing the event.
           
            {
                NotifyControlChangeListeners(sender, e);
            }
        }

        protected void NotifyControlChangeListeners(Object sender, EventArgs e)
        {
            MessageBox.Show("ASDF");
            //if (ControlChangedEvent != null)
            {
                //ControlChangedEvent = new ControlChangeHandler(sender, e);
                //ControlChangedEvent(this, e);
            }
        }
    }
}
