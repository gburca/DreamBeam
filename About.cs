/*

DreamBeam - a Church Song Presentation Program
Copyright (C) 2004 Stefan Kaufmann

This program is free software; you can redistribute it and/or
modify it under the terms of the GNU General Public License
as published by the Free Software Foundation; either version 2
of the License, or (at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program; if not, write to the Free Software
Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.

*/

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace DreamBeam
{


	/// <summary>
	/// Zusammenfassende Beschreibung für WinForm
	/// </summary>
	public class About : System.Windows.Forms.Form
	{
		/// <summary>
		/// Erforderliche Designervariable
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label Label;
		private System.Windows.Forms.Label LabelVersion;
		private System.Windows.Forms.Button button1;
		public string version = "";
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.LinkLabel linkLabel1;
		private System.Windows.Forms.LinkLabel linkLabel2;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.RichTextBox richTextBox1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.LinkLabel linkLabel3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.LinkLabel linkLabel4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.LinkLabel linkLabel5;


		public About()
		{
			//
			// Erforderlich für die Unterstützung des Windows Forms-Designer
			//

			InitializeComponent();

			//
			// TODO: Fügen Sie nach dem Aufruf von InitializeComponent() Konstruktorcode hinzu.
			//
		}

		/// <summary>
		/// Ressourcen nach der Verwendung bereinigen
		/// </summary>
		protected override void Dispose (bool disposing)
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		#region Vom Windows Form-Designer erzeugter Code
		/// <summary>
		/// Erforderliche Methode zur Unterstützung des Designers -
		/// ändern Sie die Methode nicht mit dem Quelltext-Editor.
		/// </summary>
		private void InitializeComponent() {
			this.panel1 = new System.Windows.Forms.Panel();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.linkLabel5 = new System.Windows.Forms.LinkLabel();
			this.label5 = new System.Windows.Forms.Label();
			this.linkLabel4 = new System.Windows.Forms.LinkLabel();
			this.label4 = new System.Windows.Forms.Label();
			this.linkLabel3 = new System.Windows.Forms.LinkLabel();
			this.label3 = new System.Windows.Forms.Label();
			this.linkLabel2 = new System.Windows.Forms.LinkLabel();
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.LabelVersion = new System.Windows.Forms.Label();
			this.Label = new System.Windows.Forms.Label();
			this.panel1.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.White;
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.tabControl1);
			this.panel1.Controls.Add(this.linkLabel2);
			this.panel1.Controls.Add(this.linkLabel1);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.button1);
			this.panel1.Controls.Add(this.LabelVersion);
			this.panel1.Controls.Add(this.Label);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(450, 370);
			this.panel1.TabIndex = 0;
			this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Location = new System.Drawing.Point(3, 115);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(444, 221);
			this.tabControl1.TabIndex = 10;
			// 
			// tabPage1
			// 
			this.tabPage1.BackColor = System.Drawing.Color.White;
			this.tabPage1.Controls.Add(this.richTextBox1);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Size = new System.Drawing.Size(436, 195);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "License";
			// 
			// richTextBox1
			// 
			this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.richTextBox1.Location = new System.Drawing.Point(0, 0);
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.Size = new System.Drawing.Size(436, 195);
			this.richTextBox1.TabIndex = 12;
			this.richTextBox1.Text = "                              GNU GENERAL PUBLIC LICENSE\n\t\t       " +  
				"Version 2, June 1991\n\n Copyright (C) 1989, 1991 Free Software Fou" +  
				"ndation, Inc.\n                       59 Temple Place, Suite 330, " +  
				"Boston, MA  02111-1307  USA\n Everyone is permitted to copy and di" +  
				"stribute verbatim copies\n of this license document, but changing " +  
				"it is not allowed.\n\n\t\t\t    Preamble\n\n  The licenses for most soft" +  
				"ware are designed to take away your\nfreedom to share and change i" +  
				"t.  By contrast, the GNU General Public\nLicense is intended to gu" +  
				"arantee your freedom to share and change free\nsoftware--to make s" +  
				"ure the software is free for all its users.  This\nGeneral Public " +  
				"License applies to most of the Free Software\nFoundation\'s softwar" +  
				"e and to any other program whose authors commit to\nusing it.  (So" +  
				"me other Free Software Foundation software is covered by\nthe GNU " +  
				"Library General Public License instead.)  You can apply it to\nyou" +  
				"r programs, too.\n\n  When we speak of free software, we are referr" +  
				"ing to freedom, not\nprice.  Our General Public Licenses are desig" +  
				"ned to make sure that you\nhave the freedom to distribute copies o" +  
				"f free software (and charge for\nthis service if you wish), that y" +  
				"ou receive source code or can get it\nif you want it, that you can" +  
				" change the software or use pieces of it\nin new free programs; an" +  
				"d that you know you can do these things.\n\n  To protect your right" +  
				"s, we need to make restrictions that forbid\nanyone to deny you th" +  
				"ese rights or to ask you to surrender the rights.\nThese restricti" +  
				"ons translate to certain responsibilities for you if you\ndistribu" +  
				"te copies of the software, or if you modify it.\n\n  For example, i" +  
				"f you distribute copies of such a program, whether\ngratis or for " +  
				"a fee, you must give the recipients all the rights that\nyou have." +  
				"  You must make sure that they, too, receive or can get the\nsourc" +  
				"e code.  And you must show them these terms so they know their\nri" +  
				"ghts.\n\n  We protect your rights with two steps: (1) copyright the" +  
				" software, and\n(2) offer you this license which gives you legal p" +  
				"ermission to copy,\ndistribute and/or modify the software.\n\n  Also" +  
				", for each author\'s protection and ours, we want to make certain\n" +  
				"that everyone understands that there is no warranty for this free" +  
				"\nsoftware.  If the software is modified by someone else and passe" +  
				"d on, we\nwant its recipients to know that what they have is not t" +  
				"he original, so\nthat any problems introduced by others will not r" +  
				"eflect on the original\nauthors\' reputations.\n\n  Finally, any free" +  
				" program is threatened constantly by software\npatents.  We wish t" +  
				"o avoid the danger that redistributors of a free\nprogram will ind" +  
				"ividually obtain patent licenses, in effect making the\nprogram pr" +  
				"oprietary.  To prevent this, we have made it clear that any\npaten" +  
				"t must be licensed for everyone\'s free use or not licensed at all" +  
				".\n\n  The precise terms and conditions for copying, distribution a" +  
				"nd\nmodification follow.\n\n\t\t    GNU GENERAL PUBLIC LICENSE\n   TER" +  
				"MS AND CONDITIONS FOR COPYING, DISTRIBUTION AND MODIFICATION\n\n  0" +  
				". This License applies to any program or other work which contain" +  
				"s\na notice placed by the copyright holder saying it may be distri" +  
				"buted\nunder the terms of this General Public License.  The \"Progr" +  
				"am\", below,\nrefers to any such program or work, and a \"work based" +  
				" on the Program\"\nmeans either the Program or any derivative work " +  
				"under copyright law:\nthat is to say, a work containing the Progra" +  
				"m or a portion of it,\neither verbatim or with modifications and/o" +  
				"r translated into another\nlanguage.  (Hereinafter, translation is" +  
				" included without limitation in\nthe term \"modification\".)  Each l" +  
				"icensee is addressed as \"you\".\n\nActivities other than copying, di" +  
				"stribution and modification are not\ncovered by this License; they" +  
				" are outside its scope.  The act of\nrunning the Program is not re" +  
				"stricted, and the output from the Program\nis covered only if its " +  
				"contents constitute a work based on the\nProgram (independent of h" +  
				"aving been made by running the Program).\nWhether that is true dep" +  
				"ends on what the Program does.\n\n  1. You may copy and distribute " +  
				"verbatim copies of the Program\'s\nsource code as you receive it, i" +  
				"n any medium, provided that you\nconspicuously and appropriately p" +  
				"ublish on each copy an appropriate\ncopyright notice and disclaime" +  
				"r of warranty; keep intact all the\nnotices that refer to this Lic" +  
				"ense and to the absence of any warranty;\nand give any other recip" +  
				"ients of the Program a copy of this License\nalong with the Progra" +  
				"m.\n\nYou may charge a fee for the physical act of transferring a c" +  
				"opy, and\nyou may at your option offer warranty protection in exch" +  
				"ange for a fee.\n\n  2. You may modify your copy or copies of the P" +  
				"rogram or any portion\nof it, thus forming a work based on the Pro" +  
				"gram, and copy and\ndistribute such modifications or work under th" +  
				"e terms of Section 1\nabove, provided that you also meet all of th" +  
				"ese conditions:\n\n    a) You must cause the modified files to carr" +  
				"y prominent notices\n    stating that you changed the files and th" +  
				"e date of any change.\n\n    b) You must cause any work that you di" +  
				"stribute or publish, that in\n    whole or in part contains or is " +  
				"derived from the Program or any\n    part thereof, to be licensed " +  
				"as a whole at no charge to all third\n    parties under the terms " +  
				"of this License.\n\n    c) If the modified program normally reads c" +  
				"ommands interactively\n    when run, you must cause it, when start" +  
				"ed running for such\n    interactive use in the most ordinary way," +  
				" to print or display an\n    announcement including an appropriate" +  
				" copyright notice and a\n    notice that there is no warranty (or " +  
				"else, saying that you provide\n    a warranty) and that users may " +  
				"redistribute the program under\n    these conditions, and telling " +  
				"the user how to view a copy of this\n    License.  (Exception: if " +  
				"the Program itself is interactive but\n    does not normally print" +  
				" such an announcement, your work based on\n    the Program is not " +  
				"required to print an announcement.)\n\nThese requirements apply to" +  
				" the modified work as a whole.  If\nidentifiable sections of that " +  
				"work are not derived from the Program,\nand can be reasonably cons" +  
				"idered independent and separate works in\nthemselves, then this Li" +  
				"cense, and its terms, do not apply to those\nsections when you dis" +  
				"tribute them as separate works.  But when you\ndistribute the same" +  
				" sections as part of a whole which is a work based\non the Program" +  
				", the distribution of the whole must be on the terms of\nthis Lice" +  
				"nse, whose permissions for other licensees extend to the\nentire w" +  
				"hole, and thus to each and every part regardless of who wrote it." +  
				"\n\nThus, it is not the intent of this section to claim rights or c" +  
				"ontest\nyour rights to work written entirely by you; rather, the i" +  
				"ntent is to\nexercise the right to control the distribution of der" +  
				"ivative or\ncollective works based on the Program.\n\nIn addition, m" +  
				"ere aggregation of another work not based on the Program\nwith the" +  
				" Program (or with a work based on the Program) on a volume of\na s" +  
				"torage or distribution medium does not bring the other work under" +  
				"\nthe scope of this License.\n\n  3. You may copy and distribute the" +  
				" Program (or a work based on it,\nunder Section 2) in object code " +  
				"or executable form under the terms of\nSections 1 and 2 above prov" +  
				"ided that you also do one of the following:\n\n    a) Accompany it " +  
				"with the complete corresponding machine-readable\n    source code," +  
				" which must be distributed under the terms of Sections\n    1 and " +  
				"2 above on a medium customarily used for software interchange; or" +  
				",\n\n    b) Accompany it with a written offer, valid for at least t" +  
				"hree\n    years, to give any third party, for a charge no more tha" +  
				"n your\n    cost of physically performing source distribution, a c" +  
				"omplete\n    machine-readable copy of the corresponding source cod" +  
				"e, to be\n    distributed under the terms of Sections 1 and 2 abov" +  
				"e on a medium\n    customarily used for software interchange; or,\n" +  
				"\n    c) Accompany it with the information you received as to the " +  
				"offer\n    to distribute corresponding source code.  (This alterna" +  
				"tive is\n    allowed only for noncommercial distribution and only " +  
				"if you\n    received the program in object code or executable form" +  
				" with such\n    an offer, in accord with Subsection b above.)\n\nThe" +  
				" source code for a work means the preferred form of the work for\n" +  
				"making modifications to it.  For an executable work, complete sou" +  
				"rce\ncode means all the source code for all modules it contains, p" +  
				"lus any\nassociated interface definition files, plus the scripts u" +  
				"sed to\ncontrol compilation and installation of the executable.  H" +  
				"owever, as a\nspecial exception, the source code distributed need " +  
				"not include\nanything that is normally distributed (in either sour" +  
				"ce or binary\nform) with the major components (compiler, kernel, a" +  
				"nd so on) of the\noperating system on which the executable runs, u" +  
				"nless that component\nitself accompanies the executable.\n\nIf distr" +  
				"ibution of executable or object code is made by offering\naccess t" +  
				"o copy from a designated place, then offering equivalent\naccess t" +  
				"o copy the source code from the same place counts as\ndistribution" +  
				" of the source code, even though third parties are not\ncompelled " +  
				"to copy the source along with the object code.\n\n  4. You may not" +  
				" copy, modify, sublicense, or distribute the Program\nexcept as ex" +  
				"pressly provided under this License.  Any attempt\notherwise to co" +  
				"py, modify, sublicense or distribute the Program is\nvoid, and wil" +  
				"l automatically terminate your rights under this License.\nHowever" +  
				", parties who have received copies, or rights, from you under\nthi" +  
				"s License will not have their licenses terminated so long as such" +  
				"\nparties remain in full compliance.\n\n  5. You are not required to" +  
				" accept this License, since you have not\nsigned it.  However, not" +  
				"hing else grants you permission to modify or\ndistribute the Progr" +  
				"am or its derivative works.  These actions are\nprohibited by law " +  
				"if you do not accept this License.  Therefore, by\nmodifying or di" +  
				"stributing the Program (or any work based on the\nProgram), you in" +  
				"dicate your acceptance of this License to do so, and\nall its term" +  
				"s and conditions for copying, distributing or modifying\nthe Progr" +  
				"am or works based on it.\n\n  6. Each time you redistribute the Pro" +  
				"gram (or any work based on the\nProgram), the recipient automatica" +  
				"lly receives a license from the\noriginal licensor to copy, distri" +  
				"bute or modify the Program subject to\nthese terms and conditions." +  
				"  You may not impose any further\nrestrictions on the recipients\' " +  
				"exercise of the rights granted herein.\nYou are not responsible fo" +  
				"r enforcing compliance by third parties to\nthis License.\n\n  7. If" +  
				", as a consequence of a court judgment or allegation of patent\nin" +  
				"fringement or for any other reason (not limited to patent issues)" +  
				",\nconditions are imposed on you (whether by court order, agreemen" +  
				"t or\notherwise) that contradict the conditions of this License, t" +  
				"hey do not\nexcuse you from the conditions of this License.  If yo" +  
				"u cannot\ndistribute so as to satisfy simultaneously your obligati" +  
				"ons under this\nLicense and any other pertinent obligations, then " +  
				"as a consequence you\nmay not distribute the Program at all.  For " +  
				"example, if a patent\nlicense would not permit royalty-free redist" +  
				"ribution of the Program by\nall those who receive copies directly " +  
				"or indirectly through you, then\nthe only way you could satisfy bo" +  
				"th it and this License would be to\nrefrain entirely from distribu" +  
				"tion of the Program.\n\nIf any portion of this section is held inva" +  
				"lid or unenforceable under\nany particular circumstance, the balan" +  
				"ce of the section is intended to\napply and the section as a whole" +  
				" is intended to apply in other\ncircumstances.\n\nIt is not the purp" +  
				"ose of this section to induce you to infringe any\npatents or othe" +  
				"r property right claims or to contest validity of any\nsuch claims" +  
				"; this section has the sole purpose of protecting the\nintegrity o" +  
				"f the free software distribution system, which is\nimplemented by " +  
				"public license practices.  Many people have made\ngenerous contrib" +  
				"utions to the wide range of software distributed\nthrough that sys" +  
				"tem in reliance on consistent application of that\nsystem; it is u" +  
				"p to the author/donor to decide if he or she is willing\nto distri" +  
				"bute software through any other system and a licensee cannot\nimpo" +  
				"se that choice.\n\nThis section is intended to make thoroughly clea" +  
				"r what is believed to\nbe a consequence of the rest of this Licens" +  
				"e.\n\n  8. If the distribution and/or use of the Program is restri" +  
				"cted in\ncertain countries either by patents or by copyrighted int" +  
				"erfaces, the\noriginal copyright holder who places the Program und" +  
				"er this License\nmay add an explicit geographical distribution lim" +  
				"itation excluding\nthose countries, so that distribution is permit" +  
				"ted only in or among\ncountries not thus excluded.  In such case, " +  
				"this License incorporates\nthe limitation as if written in the bod" +  
				"y of this License.\n\n  9. The Free Software Foundation may publish" +  
				" revised and/or new versions\nof the General Public License from t" +  
				"ime to time.  Such new versions will\nbe similar in spirit to the " +  
				"present version, but may differ in detail to\naddress new problems" +  
				" or concerns.\n\nEach version is given a distinguishing version num" +  
				"ber.  If the Program\nspecifies a version number of this License w" +  
				"hich applies to it and \"any\nlater version\", you have the option o" +  
				"f following the terms and conditions\neither of that version or of" +  
				" any later version published by the Free\nSoftware Foundation.  If" +  
				" the Program does not specify a version number of\nthis License, y" +  
				"ou may choose any version ever published by the Free Software\nFou" +  
				"ndation.\n\n  10. If you wish to incorporate parts of the Program i" +  
				"nto other free\nprograms whose distribution conditions are differe" +  
				"nt, write to the author\nto ask for permission.  For software whic" +  
				"h is copyrighted by the Free\nSoftware Foundation, write to the Fr" +  
				"ee Software Foundation; we sometimes\nmake exceptions for this.  O" +  
				"ur decision will be guided by the two goals\nof preserving the fre" +  
				"e status of all derivatives of our free software and\nof promoting" +  
				" the sharing and reuse of software generally.\n\n\t\t\t    NO WARRANTY" +  
				"\n\n  11. BECAUSE THE PROGRAM IS LICENSED FREE OF CHARGE, THERE IS " +  
				"NO WARRANTY\nFOR THE PROGRAM, TO THE EXTENT PERMITTED BY APPLICABL" +  
				"E LAW.  EXCEPT WHEN\nOTHERWISE STATED IN WRITING THE COPYRIGHT HOL" +  
				"DERS AND/OR OTHER PARTIES\nPROVIDE THE PROGRAM \"AS IS\" WITHOUT WAR" +  
				"RANTY OF ANY KIND, EITHER EXPRESSED\nOR IMPLIED, INCLUDING, BUT NO" +  
				"T LIMITED TO, THE IMPLIED WARRANTIES OF\nMERCHANTABILITY AND FITNE" +  
				"SS FOR A PARTICULAR PURPOSE.  THE ENTIRE RISK AS\nTO THE QUALITY A" +  
				"ND PERFORMANCE OF THE PROGRAM IS WITH YOU.  SHOULD THE\nPROGRAM PR" +  
				"OVE DEFECTIVE, YOU ASSUME THE COST OF ALL NECESSARY SERVICING,\nRE" +  
				"PAIR OR CORRECTION.\n\n  12. IN NO EVENT UNLESS REQUIRED BY APPLICA" +  
				"BLE LAW OR AGREED TO IN WRITING\nWILL ANY COPYRIGHT HOLDER, OR ANY" +  
				" OTHER PARTY WHO MAY MODIFY AND/OR\nREDISTRIBUTE THE PROGRAM AS PE" +  
				"RMITTED ABOVE, BE LIABLE TO YOU FOR DAMAGES,\nINCLUDING ANY GENERA" +  
				"L, SPECIAL, INCIDENTAL OR CONSEQUENTIAL DAMAGES ARISING\nOUT OF TH" +  
				"E USE OR INABILITY TO USE THE PROGRAM (INCLUDING BUT NOT LIMITED\n" +  
				"TO LOSS OF DATA OR DATA BEING RENDERED INACCURATE OR LOSSES SUSTA" +  
				"INED BY\nYOU OR THIRD PARTIES OR A FAILURE OF THE PROGRAM TO OPERA" +  
				"TE WITH ANY OTHER\nPROGRAMS), EVEN IF SUCH HOLDER OR OTHER PARTY H" +  
				"AS BEEN ADVISED OF THE\nPOSSIBILITY OF SUCH DAMAGES.\n\n\t\t     END O" +  
				"F TERMS AND CONDITIONS";
			// 
			// tabPage2
			// 
			this.tabPage2.BackColor = System.Drawing.Color.White;
			this.tabPage2.Controls.Add(this.linkLabel5);
			this.tabPage2.Controls.Add(this.label5);
			this.tabPage2.Controls.Add(this.linkLabel4);
			this.tabPage2.Controls.Add(this.label4);
			this.tabPage2.Controls.Add(this.linkLabel3);
			this.tabPage2.Controls.Add(this.label3);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Size = new System.Drawing.Size(436, 195);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Thanks for 3rd Party Controls and Scripts";
			// 
			// linkLabel5
			// 
			this.linkLabel5.LinkColor = System.Drawing.Color.Black;
			this.linkLabel5.Location = new System.Drawing.Point(256, 152);
			this.linkLabel5.Name = "linkLabel5";
			this.linkLabel5.Size = new System.Drawing.Size(120, 16);
			this.linkLabel5.TabIndex = 14;
			this.linkLabel5.TabStop = true;
			this.linkLabel5.Text = "www.codeproject.com";
			this.linkLabel5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.linkLabel5.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel5_LinkClicked);
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(16, 128);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(368, 16);
			this.label5.TabIndex = 13;
			this.label5.Text = "And thanks to the Code Project for all their Tutorials and Code-Sn" +  
				"ippets.";
			// 
			// linkLabel4
			// 
			this.linkLabel4.LinkColor = System.Drawing.Color.Black;
			this.linkLabel4.Location = new System.Drawing.Point(256, 96);
			this.linkLabel4.Name = "linkLabel4";
			this.linkLabel4.Size = new System.Drawing.Size(136, 16);
			this.linkLabel4.TabIndex = 12;
			this.linkLabel4.TabStop = true;
			this.linkLabel4.Text = "www.crosswire.org/sword";
			this.linkLabel4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.linkLabel4.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel4_LinkClicked);
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(16, 72);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(392, 16);
			this.label4.TabIndex = 11;
			this.label4.Text = "Thanks to the Sword Project, for their Bible Tools.";
			// 
			// linkLabel3
			// 
			this.linkLabel3.LinkColor = System.Drawing.Color.Black;
			this.linkLabel3.Location = new System.Drawing.Point(256, 48);
			this.linkLabel3.Name = "linkLabel3";
			this.linkLabel3.Size = new System.Drawing.Size(87, 16);
			this.linkLabel3.TabIndex = 10;
			this.linkLabel3.TabStop = true;
			this.linkLabel3.Text = "www.divil.co.uk";
			this.linkLabel3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.linkLabel3.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel3_LinkClicked);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(16, 16);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(392, 32);
			this.label3.TabIndex = 0;
			this.label3.Text = "Most of those eye candy things (menus, Toolbars, Docking Panels, D" +  
				"ocument Manager) are from Tim Dawson. Great!";
			// 
			// linkLabel2
			// 
			this.linkLabel2.LinkColor = System.Drawing.Color.Black;
			this.linkLabel2.Location = new System.Drawing.Point(182, 97);
			this.linkLabel2.Name = "linkLabel2";
			this.linkLabel2.Size = new System.Drawing.Size(87, 16);
			this.linkLabel2.TabIndex = 9;
			this.linkLabel2.TabStop = true;
			this.linkLabel2.Text = "www.staeff.de";
			this.linkLabel2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
			// 
			// linkLabel1
			// 
			this.linkLabel1.LinkColor = System.Drawing.Color.Black;
			this.linkLabel1.Location = new System.Drawing.Point(182, 80);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new System.Drawing.Size(87, 16);
			this.linkLabel1.TabIndex = 6;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "staeff@staeff.de";
			this.linkLabel1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(150, 64);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(151, 15);
			this.label2.TabIndex = 5;
			this.label2.Text = "(c)2004 by Stefan Kaufmann";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(187, 46);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(77, 16);
			this.label1.TabIndex = 4;
			this.label1.Text = "Free Software";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// button1
			// 
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button1.Location = new System.Drawing.Point(185, 341);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(80, 23);
			this.button1.TabIndex = 3;
			this.button1.Text = "OK";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// LabelVersion
			// 
			this.LabelVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.LabelVersion.Location = new System.Drawing.Point(174, 31);
			this.LabelVersion.Name = "LabelVersion";
			this.LabelVersion.Size = new System.Drawing.Size(106, 16);
			this.LabelVersion.TabIndex = 2;
			// 
			// Label
			// 
			this.Label.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Label.Location = new System.Drawing.Point(140, 0);
			this.Label.Name = "Label";
			this.Label.Size = new System.Drawing.Size(171, 30);
			this.Label.TabIndex = 1;
			this.Label.Text = "DreamBeam";
			// 
			// About
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(450, 370);
			this.ControlBox = false;
			this.Controls.Add(this.panel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "About";
			this.Opacity = 0.89999997615814209;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "About DreamBeam";
			this.TopMost = true;
			this.Load += new System.EventHandler(this.WinForm_Load);
			this.panel1.ResumeLayout(false);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.ResumeLayout(false);
		}
		#endregion

		private void WinForm_Load(object sender, System.EventArgs e)
		{
			this.LabelVersion.Text = "Version: " + Application.ProductVersion.Substring(0, Application.ProductVersion.Length - Tools.Reverse(Application.ProductVersion).IndexOf(".")-1);
		}
		
		private void button1_Click(object sender, System.EventArgs e)
		{
			this.Hide();
		}
		
		private void linkLabel1_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start("mailto:"+linkLabel1.Text);
		}

		private void panel1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{

		}

		private void linkLabel2_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start("http://"+linkLabel2.Text);
		}

		private void linkLabel3_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e) {
			System.Diagnostics.Process.Start("http://"+linkLabel3.Text);
		}

		private void linkLabel4_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e) {
			System.Diagnostics.Process.Start("http://"+linkLabel4.Text);
		}

		private void linkLabel5_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e) {
			System.Diagnostics.Process.Start("http://"+linkLabel5.Text);
		}



	}
}
