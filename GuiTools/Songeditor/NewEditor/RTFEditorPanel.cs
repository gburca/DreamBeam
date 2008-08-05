using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace DreamBeam
{
    public partial class RTFEditorPanel : UserControl
    {

        const int columns = 94;     // Shading width
        string seperator = "\n\n\n";
        private bool changingText = false;
        private bool textChanged = false;

        public RTFEditorPanel()
        {
      

            InitializeComponent();



            richTextBox.Font = new Font(proportionalFontName, proportionalFontSize, FontStyle.Regular);
           


            string r = @"\red";
            string g = @"\green";
            string b = @"\blue";

            string tx = r + rgbText[0].ToString() + g + rgbText[1].ToString() + b + rgbText[2].ToString() + ";";
            string ch = r + rgbCharacter[0].ToString() + g + rgbCharacter[1].ToString() + b + rgbCharacter[2].ToString() + ";";
            string sh = r + rgbShade[0].ToString() + g + rgbShade[1].ToString() + b + rgbShade[2].ToString() + ";";
            string li = r + rgbLiteral[0].ToString() + g + rgbLiteral[1].ToString() + b + rgbLiteral[2].ToString() + ";";
            string co = r + rgbComment[0].ToString() + g + rgbComment[1].ToString() + b + rgbComment[2].ToString() + ";";
            string ky = r + rgbKeyword[0].ToString() + g + rgbKeyword[1].ToString() + b + rgbKeyword[2].ToString() + ";";
            string cl = r + rgbClass[0].ToString() + g + rgbClass[1].ToString() + b + rgbClass[2].ToString() + ";";

            // These colors will make up a new color table
            colorDefinitions = tx + ch + sh + li + co + ky + cl; 

        }

        private static string ApplyShading(string workstring)
        {
            Regex r = new Regex(@"\\f0");
            return workstring = r.Replace(
                                      workstring,
                                      @"\f0" + @"\highlight3");
        }

        private void button1_Click(object sender, EventArgs e)
        {
         
        }


        private void UpdateText()
        {
            changingText = true;
            StringBuilder sb = new StringBuilder();
            int i = 1;
            foreach (string line in richTextBox.Lines)
            {
                i++;
                sb.Append(line.Trim());
                if (i <= richTextBox.Lines.Length) sb.Append("\n");
            }

            string[] strarray = DreamTools.SplitString(sb.ToString(), seperator);
            
            string result = RTFHeader + RTFFontTable + RTFColorTable;

            i = 0;
            Console.WriteLine(strarray.Length.ToString());
            foreach (string str in strarray)
            {

                i++;
                string s = ConstructWorkstring(str);               
                result += RTFVerseStyle + s;
                Console.WriteLine(i.ToString());
                if (i<strarray.Length)  result += RTFCleanStyle + @"\par\par\par ";

            }
            //MessageBox.Show(result);
            result += "}";
            richTextBox.Rtf = result;
            changingText = false;
        }


        private string ConstructWorkstring()
        {
            if (richTextBox.SelectedText != "")
            {
                rt2.Text = richTextBox.Text;

                // Pad out the rt2 text with spaces
                //string bufferString = "";
                StringBuilder bufferString = new StringBuilder();
                string[] bufferArray = rt2.Text.Split(LF);

                char padder = Space;

                int i = -1;
                while (i < ((bufferArray.Length - 2) - 1))  // All but the last line
                {
                    i++;                    
                    bufferString.Append(bufferArray[i].PadRight(columns, padder) + LF);
                }

                // We don't want a LF tacked onto the last line of the selection
                i++;
                //bufferString += bufferArray[i].PadRight(columns, padder);
                bufferString.Append(bufferArray[i].PadRight(columns, padder));
                rt2.Text = bufferString.ToString();
                string workstring = rt2.Rtf;
                return workstring;
            }
            else
            {
                MessageBox.Show("You must select some text ...", " Error ...", MessageBoxButtons.OK);
                return "";
            }
        }

        private string ConstructWorkstring(string Line)
        {
                //rt2.Text = richTextBox.Text;

                // Pad out the rt2 text with spaces
                //string bufferString = "";
            StringBuilder bufferString = new StringBuilder();
                string[] bufferArray = Line.Split(LF);

                char padder = Space;

                int i = -1;
                while (i < ((bufferArray.Length - 2)))  // All but the last line
                {
                    i++;
                    bufferString.Append(bufferArray[i].PadRight(columns, padder) + @"\par ");
                    //bufferString += bufferArray[i].PadRight(columns, padder) + @"\par ";
                }

                // We don't want a LF tacked onto the last line of the selection
                i++;
                //bufferString += bufferArray[i].PadRight(columns, padder) + @"\par ";
                bufferString.Append(bufferArray[i].PadRight(columns, padder) );
                // rt2.Text = bufferString.ToString();
                
                return bufferString.ToString();

        }



        private string CreateColorTable(string s)
        {
            // Remove any existing Color Table ...
            string re = @"{\\colortbl .*;}";
            Regex r = new Regex(re);
            s = r.Replace
                      (s,
                      "");

            // ...  and insert a new one
            re = ";}}";
            r = new Regex(re);
            return s = r.Replace
                             (s,
                             re + @"{\colortbl ;" + colorDefinitions + @"}");
        }



        #region Tables, Lists and Constants
        #region Fonts

        const string proportionalFontName = "Verdana";
        const string monoFontName = "Courier New";

        const string proportionalFontDefinition = @"{\f0\fnil\fcharset0 " + proportionalFontName + @";}";
        const string monoFontDefinition = @"{\f1\fnil\fcharset0 " + monoFontName + @";}";

        const int proportionalFontSize = 8;
        const int monoFontSize = 9;

        #endregion

        #region Color Table

        int[] rgbText = { 0, 0, 0 };
        int[] rgbCharacter = { 128, 128, 128 };
        int[] rgbShade = { 238, 238, 238 };
        int[] rgbLiteral = { 144, 0, 16 };
        int[] rgbComment = { 32, 128, 0 };
        int[] rgbKeyword = { 13, 0, 255 };
        int[] rgbClass = { 43, 145, 175 };

        // It is useful to set up some aliases ...
        public const string TX = @"\cf1 ";
        public const string CH = @"\cf2 ";
        public const string SH = @"\cf3 ";
        public const string LI = @"\cf4 ";
        public const string CO = @"\cf5 ";
        public const string KY = @"\cf6 ";
        public const string CL = @"\cf7 ";

        // ... and their escaped versions
        public const string escTX = @"\\cf1 ";
        public const string escCH = @"\\cf2 ";
        public const string escSH = @"\\cf3 ";
        public const string escLI = @"\\cf4 ";
        public const string escCO = @"\\cf5 ";
        public const string escKY = @"\\cf6 ";
        public const string escCL = @"\\cf7 ";

        public const string RTFHeader = @"{\rtf1\ansi\ansicpg1252\deff0\deflang1031";
        public const string RTFFontTable = "\n" + @"{\fonttbl{\f0\fnil\fcharset0 Verdana;}{\f1\fnil\fcharset0 Courier New;}}";
        public const string RTFColorTable = "\n"+ @"{\colortbl ;\red238\green238\blue238;\red0\green128\blue128;}";
        public const string RTFCleanStyle = "\n"+@"\highlight0\f0\fs17 ";
        public const string RTFVerseStyle = "\n" + @"\highlight2\f1\fs18 ";

        string colorDefinitions = "";

        #endregion

        #region ASCII Values

        // Some ASCII constants
        const char LF = (char)0x0A;     // Line Feed
        const char CR = (char)0x0D;     // Carriage Return
        const char Space = (char)0x20;

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            f.Show();
        }



        #endregion

        private void richTextBox_ContentsResized(object sender, ContentsResizedEventArgs e)
        {
            if (e.NewRectangle.Height > this.Size.Height)
            this.Height = e.NewRectangle.Height;
        }
        #endregion

        private void richTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!changingText)
            {
                int pos = richTextBox.SelectionStart;
                UpdateText();
                richTextBox.Focus();
                richTextBox.SelectionStart = pos;
                
            }

        }

        private void richTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Delete:
                    if (richTextBox.Lines[richTextBox.CurrentLine - 1].Trim().Length == 0)
                    {
                        changingText = true;
                        textChanged = true;
                        richTextBox.Lines = DreamTools.deleteStringArrayLineAt(richTextBox.Lines, richTextBox.CurrentLine - 1);                        
                    }
                    break;
            }
        }

    

        private void richTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (!changingText && textChanged)
            {
                changingText = true;
                textChanged = false;
                this.UpdateText();

            }
            
        }


    }
}
