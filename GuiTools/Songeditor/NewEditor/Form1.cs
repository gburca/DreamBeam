using System;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Media;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Resources;
using System.Reflection;
using Microsoft.Win32;
using System.IO;
using System.IO.IsolatedStorage;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace DreamBeam
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        #region Tables, Lists and Constants
        #region Fonts

        const string proportionalFontName = "Verdana";
        const string monoFontName =         "Courier New";

        const string proportionalFontDefinition =   @"{\f0\fnil\fcharset0 " + proportionalFontName + @";}";
        const string monoFontDefinition =           @"{\f1\fnil\fcharset0 " + monoFontName + @";}";

        const int proportionalFontSize =    8;
        const int monoFontSize =            9;

        #endregion

        #region Color Table

        int[] rgbText =         {   0,   0,   0 };
        int[] rgbCharacter =    { 128, 128, 128 };
        int[] rgbShade =        { 238, 238, 238 };
        int[] rgbLiteral =      { 144,   0,  16 };
        int[] rgbComment =      {  32, 128,   0 };
        int[] rgbKeyword =      {  13,   0, 255 };
        int[] rgbClass =        {  43, 145, 175 };

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

        string colorDefinitions = "";

        #endregion

        #region ASCII Values

        // Some ASCII constants
        const char LF =     (char)0x0A;     // Line Feed
        const char CR =     (char)0x0D;     // Carriage Return
        const char Space =  (char)0x20;     // Space char

        #endregion

        #region Main Form Parameters

        bool rtfvisible = false;

        bool isFirstRun = true;

        int maxFormHeight = 712;    // Expanded
        int minFormHeight = 568;    // Not expanded

        const int columns = 94;     // Shading width

        const string upArrow =      "r";
        const string downArrow =    "s";

        bool expanded = false;

        #endregion

        #region isoFiles

        IsolatedStorageFile isoStore =
          IsolatedStorageFile.GetStore(IsolatedStorageScope.User
          | IsolatedStorageScope.Assembly, null, null);

        const string isoF = "isolatedFirst.iso";
        const string isoS = "isolatedSource.iso";       // Source
        const string isoC = "isolatedClassList.iso";    // Class names

        #endregion

        #region Initialized Lists

        string keywordList = "";

        string unformattedClassList = "";
        string formattedClassList = "";

        #endregion
        #endregion

        // ----------------------------------------------------------------------------------------
        // ON LOAD  ON LOAD  ON LOAD  ON LOAD  ON LOAD  ON LOAD  ON LOAD  ON LOAD  ON LOAD  ON LOAD  
        // ----------------------------------------------------------------------------------------
        private void Form1_Load(object sender, EventArgs e)
        {
            // Startup screen
            this.Height = minFormHeight;
            btnArrow.Text = downArrow;

            // Set Font properties for both Rich Text Boxes
            rt1.Font = new Font(proportionalFontName, proportionalFontSize, FontStyle.Regular);
            rt2.Font = new Font(monoFontName, monoFontSize, FontStyle.Regular);

            rt1.Text = "";

            t1.Visible = false;     // t1 shows the background RTF encoding

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

            // On first run, we will create in isolated storage
            // a Class List (isoC) and a source file (isoS). The
            // default, first run, source file content is a resource.

            //keywordList = Resources1.Keywords;
            //InitializeDataFiles();
        }

        private void InitializeDataFiles()
        {
           string[] fileNames = isoStore.GetFileNames(isoF);

           int i = 0;
           foreach ( string s in fileNames ) { i++; }

           if (i == 0)  // FIRST RUN
           {
               isFirstRun = true;

               // CREATE isoF CREATE isoF CREATE isoF CREATE isoF CREATE isoF CREATE isoF CREATE isoF 
               IsolatedStorageFileStream fs = new IsolatedStorageFileStream(
                                                              isoF,
                                                              FileMode.Create,
                                                              isoStore);
               fs.Close();


               // CREATE isoS CREATE isoS CREATE isoS CREATE isoS CREATE isoS CREATE isoS CREATE isoS 
               fs = new IsolatedStorageFileStream(
                                            isoS,
                                            FileMode.Create,
                                            isoStore);

               // Write default source to it
               using (StreamWriter sw = new StreamWriter(fs))
               {
                   //sw.Write(Resources1.DefaultSource);
                   sw.Close();
               }

               fs.Close();

               // Read the file into rt1
               fs = new IsolatedStorageFileStream(
                                             isoS,
                                             FileMode.Open,
                                             isoStore);

               using (StreamReader sr = new StreamReader(fs))
               {
                   rt1.Text = sr.ReadToEnd();
                   sr.Close();
               }

               fs.Close();


               // CREATE isoC CREATE isoC CREATE isoC CREATE isoC CREATE isoC CREATE isoC CREATE isoC 
               fs = new IsolatedStorageFileStream(
                                               isoC,
                                               FileMode.Create,
                                               isoStore);

               using (StreamWriter sw = new StreamWriter(fs))
               {
                   sw.Write("Form");
                   sw.Close();
               }

               fs.Close();

               // Load the Class List
               fs = new IsolatedStorageFileStream(
                            isoC,
                            FileMode.Open,
                            isoStore);

               using (StreamReader sr = new StreamReader(fs))
               {
                   unformattedClassList = sr.ReadToEnd();
                   formattedClassList = Format(unformattedClassList);
                   
                   rt3.Text = unformattedClassList;
                   sr.Close();
               }

               fs.Close();
           }
           else         // (NOT FIRST RUN)
           {
               isFirstRun = false;

               // Open isoS ... Open isoS ... Open isoS ... Open isoS ... Open isoS ... Open isoS ... 
               IsolatedStorageFileStream fs = new IsolatedStorageFileStream(
                                               isoS,
                                               FileMode.Open,
                                               isoStore);

               // ... and copy it into rt1
               using (StreamReader sr = new StreamReader(fs))
               {
                   rt1.Text = sr.ReadToEnd();
                   sr.Close();
               }

               fs.Close();

               // Open isoC ... Open isoC ... Open isoC ... Open isoC ... Open isoC ... Open isoC ...
               fs = new IsolatedStorageFileStream(
                                           isoC,
                                           FileMode.Open,
                                           isoStore);

               // ... and load the Class List
               using (StreamReader sr = new StreamReader(fs))
               {
                   unformattedClassList = sr.ReadToEnd();

                   // Remove leading spaces
                   string re = @"^\s*";
                   Regex r = new Regex(re);
                   unformattedClassList = r.Replace(
                       unformattedClassList,
                       "");

                   formattedClassList = Format(unformattedClassList);

                   rt3.Text = unformattedClassList;
                   sr.Close();
               }

               fs.Close();

               btnLoadSource.Enabled = true;
           }
        }

        // -----------------------------------------------------------------------------------------------
        // SORT CLASS LIST ... SORT CLASS LIST ... SORT CLASS LIST ... SORT CLASS LIST ... SORT CLASS LIST
        private void btnSort_Click(object sender, EventArgs e)
        {
            string[] classNames = rt3.Text.Split(' ');
            Array.Sort(classNames);

            rt3.Clear();
            for (int i = 0; i < classNames.Length; i++)
            {
                rt3.Text += classNames[i] + " ";
            }

            // Delete any leading spaces
            string re = @"^\s*";
            Regex r = new Regex(re);
            rt3.Text = r.Replace(
                rt3.Text,
                "");

            SaveClassList();
        }

        // -----------------------------------------------------------------------------------------------
        // SAVE CLASS LIST ... SAVE CLASS LIST ... SAVE CLASS LIST ... SAVE CLASS LIST ... SAVE CLASS LIST 
        private void btnSaveClassList_Click(object sender, EventArgs e)
        {
            SaveClassList();
        }

        private void SaveClassList()
        {
            formattedClassList = Format(rt3.Text);
            IsolatedStorageFileStream fs = new IsolatedStorageFileStream(
                                                   isoC,
                                                   FileMode.Truncate,
                                                   isoStore);

            using (StreamWriter sw = new StreamWriter(fs))
            {
                int i = -1;
                while (i < (rt3.Lines.Length - 2))
                {
                    i++;
                    sw.WriteLine(rt3.Lines[i]);
                }

                // The last line - we don't need a LF
                i++;
                sw.Write(rt3.Lines[i]);

                sw.Close();
            }

            fs.Close();
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

        private void btnUndo_Click(object sender, EventArgs e)
        {
            rt1.SelectionColor = Color.Black;
            rt1.SelectionBackColor = rt1.BackColor;
            rt1.SelectionFont = new Font(proportionalFontName, proportionalFontSize, FontStyle.Regular);

            rt1.DeselectAll();
            rt1.Focus();
        }

        private void btnShowRTF_Click(object sender, EventArgs e)
        {
            if (!rtfvisible)
            {
                t1.Text = rt1.Rtf;
                t1.Visible = true;
                rtfvisible = true;
            }
            else
            {
                t1.Visible = false;
                rtfvisible = false;
            }
        }

        private void btnHideRTF_Click(object sender, EventArgs e)
        {
            t1.Visible = false;
        }

        private string Format(string s) // Format the unformatted class list
        {
            // Delete any leading spaces and substitute \b
            string re = @"^\s*";
            Regex r = new Regex(re);
            s = r.Replace(
                s,
                @"\b");

            // Delete any trailing spaces and substitute \b
            re = @"(\w+)\s*$";
            r = new Regex(re);
            s = r.Replace(
                s,
                "$1" + @"\b");

            // Replace any embedded spaces with \b|\b
            re = @"\s+";
            r = new Regex(re);
            return s = r.Replace(
                             s,
                             @"\b|\b");
        }

        private void btnSaveSource_Click(object sender, EventArgs e)
        {
                // Save the rt1 content to isoS
                IsolatedStorageFileStream fs =
                      new IsolatedStorageFileStream(isoS,
                      //FileMode.OpenOrCreate, isoStore);
                      FileMode.Truncate, isoStore);

                using (StreamWriter sw = new StreamWriter(fs))
                {
                    // Terminate lines with LF - up to but not including the last line
                    int i = -1;
                    while (i < (rt1.Lines.Length - 2))
                    {
                        i++;
                        sw.WriteLine(rt1.Lines[i]);
                    }

                    // The last line - we don't need a LF
                    i++;
                    sw.Write(rt1.Lines[i]);

                    sw.Close();
                }

                fs.Close();

                btnLoadSource.Enabled = true;
        }
    
        private void RemoveTrailingSpaces() // OK
        {
            string lineBuffer = "";
            string trimmed = "";

            string re = @"\s+$";
            Regex r = new Regex(re);

            for (int i = 0; i < (rt1.Lines.Length - 1); i++)
            {
                lineBuffer = rt1.Lines[i];

                lineBuffer = r.Replace(
                                 lineBuffer,
                                 "") + LF;

                rt1.Lines[i] = lineBuffer;
                trimmed += lineBuffer;
            }
            rt1.Text = trimmed;
        }



        private void btnArrow_Click(object sender, EventArgs e)
        {
            if (expanded)
            {
                btnArrow.Text = downArrow;
                this.Height = minFormHeight;
                expanded = false;
            }

            else

            {
                btnArrow.Text = upArrow;
                this.Height = maxFormHeight;
                expanded = true;
            }
        }

        // BUTTON T1 BUTTON T1 BUTTON T1 BUTTON T1 BUTTON T1 BUTTON T1 BUTTON T1 BUTTON T1 BUTTON T1
        private void button1_Click(object sender, EventArgs e)      // Test Button
        {
            // List the files currently in Isolated Storage.
            string[] fileNames = isoStore.GetFileNames("*");
            int n = fileNames.Length;

            rt1.Clear();
            if (fileNames.Length > 0)
            {
                rt1.Text = "" + CR + LF + "The following .ISO files exist:" + CR + LF + LF;
                for (int i = 0; i < fileNames.Length; ++i)
                {
                    rt1.Text += fileNames[i] + CR + LF;
                }
            }
            else
                rt1.Text = "" + CR + LF + "There are no .ISO files" + CR + LF;
        }

        // BUTTON T2 BUTTON T2 BUTTON T2 BUTTON T2 BUTTON T2 BUTTON T2 BUTTON T2 BUTTON T2 BUTTON T2
        private void btnTest2_Click(object sender, EventArgs e)
        {
            string[] fileNames = isoStore.GetFileNames("*");

            rt1.Clear();
            if (fileNames.Length > 0)
            {
                for (int i = 0; i < fileNames.Length; ++i)
                {
                    isoStore.DeleteFile(fileNames[i]);
                }
                rt1.Text = "" + CR + LF + "All .ISO files have been deleted:" + CR + LF + LF;
            }
        }

        private void PasteResult(string workstring)
        {
            rt1.SelectedRtf = workstring;
        }

        private static string ApplyShading(string workstring)
        {
            Regex r = new Regex(@"\\f0");
            return workstring = r.Replace(
                                      workstring,
                                      @"\f0" + @"\highlight3");
        }

        private string ConstructWorkstring()
        {
            if (rt1.SelectedText != "")
            {
                rt2.Text = rt1.SelectedText;

                // Pad out the rt2 text with spaces
                string bufferString = "";
                string[] bufferArray = rt2.Text.Split(LF);

                char padder = Space;

                int i = -1;
                while (i < ((bufferArray.Length - 2) - 1))  // All but the last line
                {
                    i++;
                    bufferString += bufferArray[i].PadRight(columns, padder) + LF;
                }

                // We don't want a LF tacked onto the last line of the selection
                i++;
                bufferString += bufferArray[i].PadRight(columns, padder);
                rt2.Text = bufferString;

                string workstring = rt2.Rtf;
                return workstring;
            }
            else
            {
                    MessageBox.Show("You must select some text ...", " Error ...", MessageBoxButtons.OK);
                    return "";
            }
        }

        // SHADE only ... SHADE only ... SHADE only ... SHADE only ... SHADE only ... SHADE only ... 
        private void btnShade_Click(object sender, EventArgs e)
        {
            string workstring = ConstructWorkstring();
            if (workstring != "")
            {
                // Add a color table
                workstring = CreateColorTable(workstring);

                // Apply shading
                workstring = ApplyShading(workstring);

                // Copy the results back to rt1
                PasteResult(workstring);

                rt1.Focus();
            }
    }

        // SYNTAX only ... SYNTAX only ... SYNTAX only ... SYNTAX only ... SYNTAX only ... SYNTAX on
        private void btnSyntax_Click(object sender, EventArgs e)
        {
            string workstring = ConstructWorkstring();
            if (workstring != "")
            {
                // Add a color table
                workstring = CreateColorTable(workstring);

                // Apply highlighting
                workstring = SyntaxHighlight(workstring);

                // Copy the results back to rt1
                PasteResult(workstring);

                rt1.Focus();
            }
        }

        // SHADING and HIGHLIGHTING ... SHADING and HIGHLIGHTING ... SHADING and HIGHLIGHTING ... SH
        private void btnShadeSyntax_Click(object sender, EventArgs e)
        {
            string workstring = ConstructWorkstring();
            if (workstring != "")
            {
                // Add a color table
                workstring = CreateColorTable(workstring);

                // Apply shading
                workstring = ApplyShading(workstring);

                // Apply highlighting
                workstring = SyntaxHighlight(workstring);

                // Copy the results back to rt1
                PasteResult(workstring);

                rt1.Focus();
            }
        }

        private void btnLoadSource_Click(object sender, EventArgs e)
        {
            if (isFirstRun)
            {
                //rt1.Text = Resources1.DefaultSource;
                RemoveTrailingSpaces();
            }
            else
            {
                rt1.Clear();

                IsolatedStorageFileStream fs = new IsolatedStorageFileStream(
                                                                    isoS,
                                                                    FileMode.Open,
                                                                    isoStore);

                using (StreamReader sr = new StreamReader(fs))
                {
                    rt1.Text = sr.ReadToEnd();
                    RemoveTrailingSpaces();

                    sr.Close();
                }

                fs.Close();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        private string SyntaxHighlight(string workstring)
        {
            // Keyword
            Regex r = new Regex(keywordList);
            workstring = r.Replace(
                         workstring,
                         new MatchEvaluator(KeywordHandler));
            
             //Class name
            r = new Regex(formattedClassList);
            workstring = r.Replace(
                         workstring,
                         new MatchEvaluator(KeyclassHandler));

            // Character
            r = new Regex(@"'.?'");
            //(@"('[^ ]*?(?<!\)')|"); 
            workstring = r.Replace(
                         workstring,
                         new MatchEvaluator(CharacterHandler));

            // Literal
            r = new Regex(@"@?""[^""]*""");
            workstring = r.Replace(
                         workstring,
                         new MatchEvaluator(LiteralHandler));

            // Comment (Type 1): // ... ... 
            r = new Regex(@"//.*\\par");
            //(@"(/(?!//)/[^ ]*)|"); 
            workstring = r.Replace(
                         workstring,
                         new MatchEvaluator(CommentHandler));

            // Comment (Type 2): /* ... ... */
            r = new Regex(@"/\*.*?\*/", RegexOptions.Singleline);
            //(@"(/*.*?*/)|");
            workstring = r.Replace(
                         workstring,
                         new MatchEvaluator(CommentHandler));

            // Any comments embedded in literals will have been
            // highlighted and we need to clean up such instances
            r = new Regex(@""".*/\*.*?\*/.*\\par");
            workstring = r.Replace(
                         workstring,
                         new MatchEvaluator(CleanupHandler));
            
            return workstring;
        }

        #region Delegates
        // -----------------------------------------------------------------------------------------------------
        // Class Name Handler ... Class Name Handler ... Class Name Handler ... Class Name Handler ... Class Nam
        // -----------------------------------------------------------------------------------------------------
        static string KeyclassHandler(Match m)
        {
            string keyclass = m.ToString();
            return keyclass = CL + keyclass + TX;
        }

        // -----------------------------------------------------------------------------------------------------
        // Keyword Handler ... Keyword Handler ... Keyword Handler ... Keyword Handler ... Keyword Handler ... K
        // -----------------------------------------------------------------------------------------------------
        static string KeywordHandler(Match m)
        {
            string keyword = m.ToString();
            return keyword = KY + keyword + TX;
        }

        // -----------------------------------------------------------------------------------------------------
        // Character Handler ... Character Handler ... Character Handler ... Character Handler ... Character Han
        // -----------------------------------------------------------------------------------------------------
        static string CharacterHandler(Match m)
        {
            string character = m.ToString();
            return character = CH + character + TX;
        }

        // -----------------------------------------------------------------------------------------------------
        // Literal Handler ... Literal Handler ... Literal Handler ... Literal Handler ... Literal Handler ... L 
        // -----------------------------------------------------------------------------------------------------
        static string LiteralHandler(Match m)
        {
            string literal = m.ToString();

            // Remove any highlighting of Class Names, Keywords,
            // Characters, Comments in the literal
            literal = Regex.Replace(
                        literal,
                        escCL + "|" + escKY + "|" + escCH + "|" + escCO + "|" + escTX,
                        "");

            return literal = LI + literal + TX;
        }

        // -----------------------------------------------------------------------------------------------------
        // Comment Handler ... Comment Handler ... Comment Handler ... Comment Handler ... Comment Handler ... C
        // -----------------------------------------------------------------------------------------------------
        static string CommentHandler(Match m)
        {
            string comment = m.ToString();

            // Remove any highlighting of Class Names, Keywords,
            // Characters, Literals embedded in the comment
            comment = Regex.Replace(
                        comment,
                        escCL + "|" + escKY + "|" + escCH + "|" + escLI + "|" + escTX,
                        "");

            return comment = CO + comment + TX;
        }

        // -----------------------------------------------------------------------------------------------------
        // Cleanup Handler ... Cleanup Handler ... Cleanup Handler ... Cleanup Handler ... Cleanup Handler ... C
        // -----------------------------------------------------------------------------------------------------
        static string CleanupHandler(Match m)
        {
            string literal = m.ToString();

            // Remove any highlighting of Comments
            // embedded in the Literal
            literal = Regex.Replace(
                        literal,
                        escCO + "|" + escTX,
                        "");

            return literal = LI + literal + TX;
        }

        // -----------------------------------------------------------------------------------------------------
        // Remove Handler ... Remove Handler ... Remove Handler ... Remove Handler ... Remove Handler ... Remove
        // -----------------------------------------------------------------------------------------------------
        static string RemoveHandler(Match m)
        {
            string s = m.ToString();
            return s = " ";
        }

        #endregion

        // ------------------------------------------------------------------
        private void rt1_MouseDoubleClick_1(object sender, MouseEventArgs e)
        {
            // Update the UNFORMATTED text box ...
            unformattedClassList = rt3.Text;

            string re = @"(\w+)\s*";
            Regex r = new Regex(re);
            unformattedClassList = r.Replace(
                unformattedClassList,
                "$1" + " ");

            unformattedClassList += rt1.SelectedText + " ";

            rt3.Text = unformattedClassList;

            formattedClassList = Format(rt3.Text);

            rt1.SelectionColor = Color.Teal;
            rt1.DeselectAll();

            SaveClassList();
        }

        private void rt3_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string s = rt3.SelectedText;

            // A selected word will always include a trailing space
            // character, which we need to remove
            string re = @"(\w+)\s*$";
            Regex r = new Regex(re);
            s = r.Replace(
                      s,
                      "$1");

            re = s;
            r = new Regex(re);
            rt3.Text = r.Replace(
                             rt3.Text,
                             new MatchEvaluator(RemoveHandler));

            rt3.DeselectAll();

            formattedClassList = Format(s);
            SaveClassList();
        }
    }
}
