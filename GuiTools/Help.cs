using System;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Diagnostics;
using System.Collections;
using System.Windows.Forms;
using System.Data;
using Rilling.Common.UI.Forms;




namespace DreamBeam
{

	public class Help: GuiTemplate
	{
		private About About = new About();
		private MyBalloonWindow TheHelpBalloon = new MyBalloonWindow();
		private Language Lang = new Language();
		
		public Help(MainForm impForm, ShowBeam impShowBeam) :  base(impForm,impShowBeam)
		{
		}


        	#region Help

			#region Intro & Main Components
						///<summary> Displays Help Balloons - with Intro and Main Components </summary>
						public void HelpIntro(){
							int intHelpNum = 1;
							int intButtonClicked;
							Point pos;
							_MainForm.Size = new Size(790,570);
							do{
								switch(intHelpNum){
									case 1:
										// Start With Introduction

										TheHelpBalloon.AnchorQuadrant = AnchorQuadrant.Auto;

										TheHelpBalloon.button3.Hide();
										pos = new Point(_MainForm.Location.X+_MainForm.ToolBars_MenuBar_Song.ButtonBounds.X + _MainForm.ToolBars_MenuBar_Edit.ButtonBounds.X +_MainForm.ToolBars_MenuBar_View.ButtonBounds.X + 20  ,_MainForm.Location.Y+30);
										intButtonClicked = TheHelpBalloon.ShowHelp(pos,Lang.say("Help.Intro.1.Title"),Lang.say("Help.Intro.1"));
										_MainForm.Focus();
										TheHelpBalloon.Dispose();
										switch (intButtonClicked){
											case 0:
												intHelpNum = 0;
											break;
											case 1:
												intHelpNum = 0;
											break;
											case 2:
												intHelpNum++;
											break;
										}
									break;
								case 2:

                // Main Toolbar
                TheHelpBalloon= new MyBalloonWindow();
				pos = new Point(_MainForm.Location.X+20,_MainForm.Location.Y+_MainForm.ToolBars_topSandBarDock.Bounds.Height+10);
                intButtonClicked = TheHelpBalloon.ShowHelp(pos,Lang.say("Help.Intro.2.Title"),Lang.say("Help.Intro.2"));
                _MainForm.Focus();
                TheHelpBalloon.Dispose();
                switch (intButtonClicked){
                case 0:
					intHelpNum = 0;
                    break;
                case 1:
                    intHelpNum--;
                    break;
                case 2:
                    intHelpNum++;
                    break;
                }
                break;
            case 3:

                // Component ToolBar
                TheHelpBalloon= new MyBalloonWindow();
				TheHelpBalloon.AnchorQuadrant = AnchorQuadrant.Left;
                TheHelpBalloon.AnchorOffset = -1;
				pos = new Point(_MainForm.Location.X+40,_MainForm.Location.Y+ _MainForm.ToolBars_topSandBarDock.Bounds.Height + Convert.ToInt32(Math.Round(_MainForm.ToolBars_ComponentBar.Size.Height/2d,0)));
                intButtonClicked = TheHelpBalloon.ShowHelp(pos,Lang.say("Help.Intro.3.Title"),Lang.say("Help.Intro.3"));
                _MainForm.Focus();
                TheHelpBalloon.Dispose();
                switch (intButtonClicked){
                case 0:
                    intHelpNum = 0;
                    break;
                case 1:
                    intHelpNum--;
                    break;
                case 2:
					intHelpNum++;
                    break;
                }
                break;
            case 4:
                // Song List
                TheHelpBalloon= new MyBalloonWindow();
                TheHelpBalloon.AnchorQuadrant = AnchorQuadrant.Right;
				_MainForm.RightDocks_TopPanel_Songs.Open();
                pos = new Point(_MainForm.Location.X+320,_MainForm.Location.Y+110);
                intButtonClicked = TheHelpBalloon.ShowHelp(pos,Lang.say("Help.Intro.4.Title"),Lang.say("Help.Intro.4"));
                TheHelpBalloon.Dispose();
                switch (intButtonClicked){
				case 0:
                    intHelpNum = 0;
                    break;
                case 1:
                    intHelpNum--;
                    break;
                case 2:
                    intHelpNum++;
                    break;
                }
                break;
            case 5:
                // Play List
                //_MainForm.RightDocks_TopPanel.SelectedTab = _MainForm.RightDocks_TopPanel_PlayList;
                TheHelpBalloon= new MyBalloonWindow();
                TheHelpBalloon.AnchorQuadrant = AnchorQuadrant.Right;
				_MainForm.RightDocks_TopPanel_PlayList.Open();
                pos = new Point(_MainForm.Location.X+320,_MainForm.Location.Y+110);
                intButtonClicked = TheHelpBalloon.ShowHelp(pos,Lang.say("Help.Intro.5.Title"),Lang.say("Help.Intro.5"));
                TheHelpBalloon.Dispose();
                //_MainForm.RightDocks_TopPanel.SelectedTab = _MainForm.RightDocks_TopPanel_Songs;
                switch (intButtonClicked){
                case 0:
                    intHelpNum = 0;
                    break;
                case 1:
                    intHelpNum--;
                    break;
				case 2:
                    intHelpNum++;
                    break;
                }
                break;
            case 6:
                // Play List
                //_MainForm.RightDocks_TopPanel.SelectedTab = _MainForm.RightDocks_TopPanel_PlayList;
                TheHelpBalloon= new MyBalloonWindow();
                TheHelpBalloon.AnchorQuadrant = AnchorQuadrant.Right;
				_MainForm.RightDocks_TopPanel_Search.Open();
                pos = new Point(_MainForm.Location.X+320,_MainForm.Location.Y+110);
                intButtonClicked = TheHelpBalloon.ShowHelp(pos,Lang.say("Help.Intro.6.Title"),Lang.say("Help.Intro.6"));
				TheHelpBalloon.Dispose();
                //_MainForm.RightDocks_TopPanel.SelectedTab = _MainForm.RightDocks_TopPanel_Songs;
                switch (intButtonClicked){
                case 0:
                    intHelpNum = 0;
					_MainForm.RightDocks_TopPanel_Songs.Open();
                    break;
                case 1:
                    intHelpNum--;
                    break;
                case 2:
                    intHelpNum++;
                    break;
                }
                break;
            case 7:
                // Play List
                //_MainForm.RightDocks_TopPanel.SelectedTab = _MainForm.RightDocks_TopPanel_PlayList;
                TheHelpBalloon= new MyBalloonWindow();
                TheHelpBalloon.AnchorQuadrant = AnchorQuadrant.Right;
                TheHelpBalloon.AnchorOffset = -2;
				_MainForm.RightDocks_TopPanel_Songs.Open();
                pos = new Point(_MainForm.Location.X+320,_MainForm.Location.Y+142);
                intButtonClicked = TheHelpBalloon.ShowHelp(pos,Lang.say("Help.Intro.7.Title"),Lang.say("Help.Intro.7"));
                TheHelpBalloon.Dispose();
                //_MainForm.RightDocks_TopPanel.SelectedTab = _MainForm.RightDocks_TopPanel_Songs;
                switch (intButtonClicked){
                case 0:
					intHelpNum = 0;
                    break;
                case 1:
                    intHelpNum--;
                    break;
                case 2:
                    intHelpNum++;
                    break;
                }
                break;
            case 8:
                // Play List
                TheHelpBalloon= new MyBalloonWindow();
				TheHelpBalloon.button1.Text = Lang.say("Help.Buttons.Finish");
                TheHelpBalloon.AnchorQuadrant = AnchorQuadrant.Right;
                TheHelpBalloon.AnchorOffset = -2;
                pos = new Point(_MainForm.Location.X+320,_MainForm.Location.Y+250);
                intButtonClicked = TheHelpBalloon.ShowHelp(pos,Lang.say("Help.Intro.8.Title"),Lang.say("Help.Intro.8"));
                _MainForm.Focus();
                //intButtonClicked = 0;
                TheHelpBalloon.Dispose();
                switch (intButtonClicked){
                case 0:
                    intHelpNum = 0;
                    break;
                case 1:
                    intHelpNum--;
                    break;
                case 2:
                    intHelpNum = 0;
                    break;
                }
                break;

            }
        }while (intHelpNum != 0);

        //   buttonItem1.ButtonBounds);
        //   _MainForm.ToolBars_ComponentBar);

	}
	#endregion

	#region Beambox
	///<summary> Displays Help Balloons - BeamBox Help </summary>
	public void HelpBeamBox() {
		int intHelpNum = 1;
		int intButtonClicked;
		Point pos;
		_MainForm.Size = new Size(790,570);

		do{
			switch(intHelpNum){
			case 1:
				// Start With What is BeamBox
				_ShowBeam.Hide();
                TheHelpBalloon= new MyBalloonWindow();
                TheHelpBalloon.AnchorQuadrant = AnchorQuadrant.Auto;
                TheHelpBalloon.button3.Hide();
                pos = new Point(_MainForm.Location.X+_MainForm.ToolBars_MenuBar_Song.ButtonBounds.X + _MainForm.ToolBars_MenuBar_Edit.ButtonBounds.X +_MainForm.ToolBars_MenuBar_View.ButtonBounds.X + 20  ,_MainForm.Location.Y+30);
                intButtonClicked = TheHelpBalloon.ShowHelp(pos,Lang.say("Help.BeamBox.1.Title"),Lang.say("Help.BeamBox.1"));
                _MainForm.Focus();
                TheHelpBalloon.Dispose();
                switch (intButtonClicked){
                case 0:
                    intHelpNum = 0;
                    break;
                case 1:
                    intHelpNum = 0;
                    break;
                case 2:
                    intHelpNum++;
                    break;
                }
                break;
            case 2:
                _ShowBeam.Show();
                _MainForm.BringToFront();
                TheHelpBalloon= new MyBalloonWindow();
                pos = new Point(_MainForm.Location.X+70,_MainForm.Location.Y+60);
                intButtonClicked = TheHelpBalloon.ShowHelp(pos,Lang.say("Help.BeamBox.2.Title"),Lang.say("Help.BeamBox.2"));
                _MainForm.Focus();
                TheHelpBalloon.Dispose();
                switch (intButtonClicked){
                case 0:
                    intHelpNum = 0;
                    break;
                case 1:
                    intHelpNum--;
                    break;
                case 2:
                    intHelpNum++;
                    break;
                }
                break;
            case 3:
                _ShowBeam.ShowMover();
                _ShowBeam.BringToFront();
                _MainForm.BringToFront();
                TheHelpBalloon= new MyBalloonWindow();
                pos = new Point(_MainForm.Location.X+130,_MainForm.Location.Y+60);
                intButtonClicked = TheHelpBalloon.ShowHelp(pos,Lang.say("Help.BeamBox.3.Title"),Lang.say("Help.BeamBox.3"));
                _MainForm.Focus();
                TheHelpBalloon.Dispose();
                switch (intButtonClicked){
                case 0:
                    intHelpNum = 0;
                    break;
                case 1:
                    intHelpNum--;
                    break;
                case 2:
                    intHelpNum++;
                    break;
                }
                break;
            case 4:
                // Main Toolbar
                _ShowBeam.ShowMover();
                _ShowBeam.BringToFront();
                TheHelpBalloon= new MyBalloonWindow();
                pos = new Point(_ShowBeam.Location.X+140,_ShowBeam.Location.Y+130);
                intButtonClicked = TheHelpBalloon.ShowHelp(pos,Lang.say("Help.BeamBox.4.Title"),Lang.say("Help.BeamBox.4"));
                _MainForm.Focus();
                TheHelpBalloon.Dispose();
                switch (intButtonClicked){
                case 0:
                    intHelpNum = 0;
                    break;
                case 1:
                    intHelpNum--;
                    break;
                case 2:
                    intHelpNum++;
                    break;
                }
                break;
            case 5:
                // Play List
                _ShowBeam.BringToFront();
                TheHelpBalloon= new MyBalloonWindow();
                TheHelpBalloon.button1.Text = Lang.say("Help.Buttons.Finish");
                pos = new Point(_ShowBeam.Location.X+310,_ShowBeam.Location.Y+130);
                intButtonClicked = TheHelpBalloon.ShowHelp(pos,Lang.say("Help.BeamBox.5.Title"),Lang.say("Help.BeamBox.5"));
                _MainForm.Focus();
                TheHelpBalloon.Dispose();
                switch (intButtonClicked){
                case 0:
                    intHelpNum = 0;
                    _ShowBeam.HideMover();
                    _ShowBeam.Hide();
                    break;
                case 1:
                    intHelpNum--;
                    break;
                case 2:
                    intHelpNum = 0;
                    _ShowBeam.HideMover();
                    _ShowBeam.Hide();
                    break;
                }
                break;
            }
        }while (intHelpNum != 0);
    }
#endregion

	#region Options
	public void HelpOptions() {
		int intHelpNum = 1;
        int intButtonClicked;
        Point pos;
        _MainForm.Size = new Size(790,570);

        do{
            switch(intHelpNum){
            case 1:
                TheHelpBalloon= new MyBalloonWindow();
                TheHelpBalloon.AnchorQuadrant = AnchorQuadrant.Auto;
                TheHelpBalloon.button3.Hide();
                pos = new Point(_MainForm.Location.X+70 ,_MainForm.Location.Y+35);
                intButtonClicked = TheHelpBalloon.ShowHelp(pos,Lang.say("Help.Options.1.Title"),Lang.say("Help.Options.1"));
                _MainForm.Focus();
                TheHelpBalloon.Dispose();
                switch (intButtonClicked){
                case 0:
                    intHelpNum = 0;
                    break;
                case 1:
                    intHelpNum = 0;
                    break;
                case 2:
                    intHelpNum++;
                    break;
                }
                break;
            case 2:
				_MainForm.Options.Show();
				_MainForm.Options.Location =  new Point(_MainForm.Location.X+30 ,_MainForm.Location.Y+70);
				_MainForm.Options.tabControl.SelectedTab = _MainForm.Options.Common_Tab;
                TheHelpBalloon= new MyBalloonWindow();
                TheHelpBalloon.AnchorQuadrant = AnchorQuadrant.Top;
                pos = new Point(_MainForm.Location.X+ 180 ,_MainForm.Location.Y+300);
                intButtonClicked = TheHelpBalloon.ShowHelp(pos,Lang.say("Help.Options.2.Title"),Lang.say("Help.Options.2"));
                _MainForm.Focus();
				_MainForm.Options.Focus();
                TheHelpBalloon.Dispose();
                switch (intButtonClicked){
                case 0:
                    intHelpNum = 0;
					_MainForm.Options.Hide();
                    break;
                case 1:
                    intHelpNum--;
					_MainForm.Options.Hide();
                    break;
                case 2:
                    intHelpNum++;
                    break;
                }
                break;
            case 3:
				_MainForm.Options.Show();
				_MainForm.Options.Location =  new Point(_MainForm.Location.X+80 ,_MainForm.Location.Y+70);
				_MainForm.Options.tabControl.SelectedTab = _MainForm.Options.Graphics_tab;
                TheHelpBalloon= new MyBalloonWindow();
                TheHelpBalloon.AnchorQuadrant = AnchorQuadrant.Top;
                pos = new Point(_MainForm.Location.X+ 180 ,_MainForm.Location.Y+300);
                intButtonClicked = TheHelpBalloon.ShowHelp(pos,Lang.say("Help.Options.3.Title"),Lang.say("Help.Options.3"));
                _MainForm.Focus();
				_MainForm.Options.Focus();
                TheHelpBalloon.Dispose();
                switch (intButtonClicked){
                case 0:
                    intHelpNum = 0;
					_MainForm.Options.Hide();
                    break;
                case 1:
                    intHelpNum--;
                    break;
                case 2:
                    intHelpNum++;
                    break;
                }
                break;
            case 4:
				_MainForm.Options.Show();
				_MainForm.Options.Location =  new Point(_MainForm.Location.X+80 ,_MainForm.Location.Y+70);
				_MainForm.Options.tabControl.SelectedTab = _MainForm.Options.BeamBox_tab;
                TheHelpBalloon= new MyBalloonWindow();
                TheHelpBalloon.AnchorQuadrant = AnchorQuadrant.Top;
                pos = new Point(_MainForm.Location.X+ 180 ,_MainForm.Location.Y+300);
                intButtonClicked = TheHelpBalloon.ShowHelp(pos,Lang.say("Help.Options.4.Title"),Lang.say("Help.Options.4"));
                _MainForm.Focus();
				_MainForm.Options.Focus();
                TheHelpBalloon.Dispose();
                switch (intButtonClicked){
                case 0:
                    intHelpNum = 0;
					_MainForm.Options.Hide();
                    break;
                case 1:
                    intHelpNum--;
                    break;
                case 2:
                    intHelpNum++;
                    break;
                }
                break;
            case 5:
				_MainForm.Options.Show();
				_MainForm.Options.Location =  new Point(_MainForm.Location.X+80 ,_MainForm.Location.Y+70);
				_MainForm.Options.tabControl.SelectedTab = _MainForm.Options.Bible_Tab;
                TheHelpBalloon= new MyBalloonWindow();
                TheHelpBalloon.AnchorQuadrant = AnchorQuadrant.Top;
                pos = new Point(_MainForm.Location.X+ 180 ,_MainForm.Location.Y+300);
                intButtonClicked = TheHelpBalloon.ShowHelp(pos,Lang.say("Help.Options.5.Title"),Lang.say("Help.Options.5"));
                _MainForm.Focus();
				_MainForm.Options.Focus();
                TheHelpBalloon.Dispose();
                switch (intButtonClicked){
                case 0:
                    intHelpNum = 0;
					_MainForm.Options.Hide();
                    break;
                case 1:
                    intHelpNum--;
					_MainForm.Options.Hide();
                    break;
                case 2:
                    intHelpNum++;
                    break;
                }
                break;
            case 6:
                TheHelpBalloon= new MyBalloonWindow();
                TheHelpBalloon.AnchorQuadrant = AnchorQuadrant.Top;
                TheHelpBalloon.button1.Text = Lang.say("Help.Buttons.Finish");
                pos = new Point(_MainForm.Location.X+370,_MainForm.Location.Y+370);
                intButtonClicked = TheHelpBalloon.ShowHelp(pos,Lang.say("Help.Options.6.Title"),Lang.say("Help.Options.6"));
                _MainForm.Focus();
				_MainForm.Options.Focus();
                TheHelpBalloon.Dispose();
                switch (intButtonClicked){
                case 0:
                    intHelpNum = 0;
					_MainForm.Options.Hide();
                    break;
                case 1:
                    intHelpNum--;
                    break;
                case 2:
                    intHelpNum = 0;
					_MainForm.Options.Hide();
                    break;
                }
                break;
            }
        }while (intHelpNum != 0);



    }
#endregion

	#region Show Songs
    ///<summary> Displays Help Balloons - ShowSongs </summary>
	public void HelpShowSongs() {
		int intHelpNum = 1;
        int intButtonClicked;
        Point pos;
        _MainForm.Size = new Size(790,570);

        do{
            switch(intHelpNum){
            case 1:
                TheHelpBalloon= new MyBalloonWindow();
                TheHelpBalloon.AnchorQuadrant = AnchorQuadrant.Auto;
                TheHelpBalloon.button3.Hide();
                pos = new Point(_MainForm.Location.X+40 ,_MainForm.Location.Y+100);
                intButtonClicked = TheHelpBalloon.ShowHelp(pos,Lang.say("Help.ShowSongs.1.Title"),Lang.say("Help.ShowSongs.1"));
                _MainForm.Focus();
                TheHelpBalloon.Dispose();
                switch (intButtonClicked){
                case 0:
                    intHelpNum = 0;
                    break;
                case 1:
                    intHelpNum = 0;
                    break;
                case 2:
                    intHelpNum++;
					break;
                }
                break;
			case 2:

                _MainForm.GuiTools.ShowTab(MainTab.ShowSongs);
                TheHelpBalloon= new MyBalloonWindow();
                TheHelpBalloon.AnchorQuadrant = AnchorQuadrant.Auto;
                pos = new Point(_MainForm.Location.X+ 200 ,_MainForm.Location.Y+300);
                intButtonClicked = TheHelpBalloon.ShowHelp(pos,Lang.say("Help.ShowSongs.2.Title"),Lang.say("Help.ShowSongs.2.Title"));
                _MainForm.Focus();
                TheHelpBalloon.Dispose();
                switch (intButtonClicked){
                case 0:
                    intHelpNum = 0;
                    break;
                case 1:
                    intHelpNum--;
                    break;
                case 2:
                    intHelpNum++;
                    break;
                }
                break;
                /*         case 3:
                _MainForm.ShowTab(MainTab.ShowSongs);
                TheHelpBalloon= new MyBalloonWindow();
                TheHelpBalloon.AnchorQuadrant = AnchorQuadrant.Right;
                pos = new Point(_MainForm.Location.X+ 90 ,_MainForm.Location.Y+250);
                intButtonClicked = TheHelpBalloon.ShowHelp(pos,"Enable Alpha Blending","If you enable Alpha Blending, the Song and Strophe changes on the BeamBox will use a nice, but (in the moment) slow, transition effect.");
                _MainForm.Focus();
                TheHelpBalloon.Dispose();
                switch (intButtonClicked){
                case 0:
                intHelpNum = 0;
                break;
                case 1:
                intHelpNum--;
                break;
                case 2:
                intHelpNum++;
                break;
				}
				break;*/
			case 3:
				TheHelpBalloon= new MyBalloonWindow();
				TheHelpBalloon.AnchorQuadrant = AnchorQuadrant.Right;
				TheHelpBalloon.button1.Text = Lang.say("Help.Buttons.Finish");
				pos = new Point(_MainForm.Location.X+90,_MainForm.Location.Y+300);
				intButtonClicked = TheHelpBalloon.ShowHelp(pos,Lang.say("Help.ShowSongs.3.Title"),Lang.say("Help.ShowSongs.3"));
				_MainForm.Focus();
				TheHelpBalloon.Dispose();
				switch (intButtonClicked){
				case 0:
					intHelpNum = 0;
					break;
				case 1:
					intHelpNum--;
					break;
				case 2:
					intHelpNum = 0;
					break;
				}
				break;
			}
		}while (intHelpNum != 0);

	}
	#endregion


	#region EditSongs
    ///<summary> Displays Help Balloons - EditSongs </summary>
		public void HelpEditSongs() {
        int intHelpNum = 1;
        int intButtonClicked;
        Point pos;
        _MainForm.Size = new Size(790,570);

        do{
            switch(intHelpNum){
            case 1:
                TheHelpBalloon= new MyBalloonWindow();
				TheHelpBalloon.AnchorQuadrant = AnchorQuadrant.Auto;
                TheHelpBalloon.button3.Hide();
                pos = new Point(_MainForm.Location.X+40 ,_MainForm.Location.Y+150);
                intButtonClicked = TheHelpBalloon.ShowHelp(pos,Lang.say("Help.EditSongs.1.Title"),Lang.say("Help.EditSongs.1"));
                _MainForm.Focus();
                TheHelpBalloon.Dispose();
                switch (intButtonClicked){
                case 0:
                    intHelpNum = 0;
                    break;
                case 1:
                    intHelpNum = 0;
                    break;
                case 2:
                    intHelpNum++;
                    break;
                }
                break;
            case 2:
                _MainForm.GuiTools.ShowTab(MainTab.EditSongs);
                TheHelpBalloon= new MyBalloonWindow();
                TheHelpBalloon.AnchorQuadrant = AnchorQuadrant.Auto;
                pos = new Point(_MainForm.Location.X+ 200 ,_MainForm.Location.Y+300);
                intButtonClicked = TheHelpBalloon.ShowHelp(pos,Lang.say("Help.EditSongs.2.Title"),Lang.say("Help.EditSongs.2"));
                _MainForm.Focus();
                TheHelpBalloon.Dispose();
                switch (intButtonClicked){
                case 0:
                    intHelpNum = 0;
                    break;
                case 1:
                    intHelpNum--;
                    break;
                case 2:
                    intHelpNum++;
                    break;
                }
                break;
            case 3:
                _MainForm.GuiTools.ShowTab(MainTab.EditSongs);
                TheHelpBalloon= new MyBalloonWindow();
				TheHelpBalloon.AnchorQuadrant = AnchorQuadrant.Auto;
                pos = new Point(_MainForm.Location.X+ 200 ,_MainForm.Location.Y+200);
                intButtonClicked = TheHelpBalloon.ShowHelp(pos,Lang.say("Help.EditSongs.3.Title"),Lang.say("Help.EditSongs.3"));
                _MainForm.Focus();
                TheHelpBalloon.Dispose();
                switch (intButtonClicked){
                case 0:
                    intHelpNum = 0;
                    break;
                case 1:
                    intHelpNum--;
                    break;
                case 2:
                    intHelpNum++;
                    break;
                }
                break;
            case 4:
                _MainForm.GuiTools.ShowTab(MainTab.EditSongs);
                TheHelpBalloon= new MyBalloonWindow();
                TheHelpBalloon.AnchorQuadrant = AnchorQuadrant.Auto;
                pos = new Point(_MainForm.Location.X+ 250 ,_MainForm.Location.Y+175);
                intButtonClicked = TheHelpBalloon.ShowHelp(pos,Lang.say("Help.EditSongs.4.Title"),Lang.say("Help.EditSongs.4"));
                _MainForm.Focus();
                TheHelpBalloon.Dispose();
                switch (intButtonClicked){
                case 0:
                    intHelpNum = 0;
                    break;
                case 1:
                    intHelpNum--;
                    break;
                case 2:
                    intHelpNum++;
                    break;
                }
                break;
            case 5:
                _MainForm.GuiTools.ShowTab(MainTab.EditSongs);
                TheHelpBalloon= new MyBalloonWindow();
                TheHelpBalloon.AnchorQuadrant = AnchorQuadrant.Right;
				pos = new Point(_MainForm.Location.X+102,_MainForm.Location.Y+150);
                intButtonClicked = TheHelpBalloon.ShowHelp(pos,Lang.say("Help.EditSongs.5.Title"),Lang.say("Help.EditSongs.5"));
                _MainForm.Focus();
                TheHelpBalloon.Dispose();
                switch (intButtonClicked){
                case 0:
                    intHelpNum = 0;
                    break;
                case 1:
                    intHelpNum--;
                    break;
                case 2:
                    intHelpNum++;
                    break;
                }
                break;
            case 6:
                _MainForm.GuiTools.ShowTab(MainTab.EditSongs);
                TheHelpBalloon= new MyBalloonWindow();
                TheHelpBalloon.AnchorQuadrant = AnchorQuadrant.Right;
                pos = new Point(_MainForm.Location.X+102,_MainForm.Location.Y+235);
                intButtonClicked = TheHelpBalloon.ShowHelp(pos,Lang.say("Help.EditSongs.6.Title"),Lang.say("Help.EditSongs.6"));
                _MainForm.Focus();
                TheHelpBalloon.Dispose();
                switch (intButtonClicked){
                case 0:
                    intHelpNum = 0;
                    break;
                case 1:
                    intHelpNum--;
                    break;
                case 2:
                    intHelpNum++;
                    break;
                }
                break;
            case 7:
                TheHelpBalloon= new MyBalloonWindow();
                TheHelpBalloon.AnchorQuadrant = AnchorQuadrant.Right;
                TheHelpBalloon.AnchorOffset = -2;
                TheHelpBalloon.button1.Text = Lang.say("Help.Buttons.Finish");
				pos = new Point(_MainForm.Location.X+160,_MainForm.Location.Y+375);
                intButtonClicked = TheHelpBalloon.ShowHelp(pos,Lang.say("Help.EditSongs.7.Title"),Lang.say("Help.EditSongs.7"));
                _MainForm.Focus();
                TheHelpBalloon.Dispose();
                switch (intButtonClicked){
                case 0:
                    intHelpNum = 0;
                    break;
                case 1:
                    intHelpNum--;
                    break;
                case 2:
                    intHelpNum = 0;
                    break;
                }
                break;
            }
        }while (intHelpNum != 0);

	}
	#endregion

	#region Presentation
		public void HelpPresentation(){
        int intHelpNum = 1;
        int intButtonClicked;
        Point pos;
        _MainForm.Size = new Size(790,570);

        do{
            switch(intHelpNum){
            case 1:
                TheHelpBalloon= new MyBalloonWindow();
                TheHelpBalloon.AnchorQuadrant = AnchorQuadrant.Auto;
                TheHelpBalloon.button3.Hide();
                pos = new Point(_MainForm.Location.X+40 ,_MainForm.Location.Y+220);
                intButtonClicked = TheHelpBalloon.ShowHelp(pos,Lang.say("Help.Presentation.1.Title"),Lang.say("Help.Presentation.1"));
                _MainForm.Focus();
                TheHelpBalloon.Dispose();
				switch (intButtonClicked){
                case 0:
                    intHelpNum = 0;
                    break;
                case 1:
                    intHelpNum = 0;
                    break;
                case 2:
                    intHelpNum++;
                    break;
                }
                break;
            case 2:
                _MainForm.GuiTools.ShowTab(MainTab.Presentation);
				if(_MainForm.Sermon_DocManager.TabStrips.Count < 1){
					_MainForm.Sermon_NewDocument();
                }

                TheHelpBalloon= new MyBalloonWindow();
				TheHelpBalloon.AnchorQuadrant = AnchorQuadrant.Auto;
                pos = new Point(_MainForm.Location.X+ 200 ,_MainForm.Location.Y+300);
                intButtonClicked = TheHelpBalloon.ShowHelp(pos,Lang.say("Help.Presentation.2.Title"),Lang.say("Help.Presentation.2"));
                _MainForm.Focus();
                TheHelpBalloon.Dispose();
				switch (intButtonClicked){
				case 0:
                    intHelpNum = 0;
                    break;
                case 1:
                    intHelpNum--;
                    break;
                case 2:
                    intHelpNum++;
                    break;
                }
                break;
            case 3:
                TheHelpBalloon= new MyBalloonWindow();
                TheHelpBalloon.AnchorQuadrant = AnchorQuadrant.Bottom;
                pos = new Point(_MainForm.Location.X+ 300 ,_MainForm.Location.Y+285);
                intButtonClicked = TheHelpBalloon.ShowHelp(pos,Lang.say("Help.Presentation.3.Title"),Lang.say("Help.Presentation.3"));
				_MainForm.Focus();
                TheHelpBalloon.Dispose();
                switch (intButtonClicked){
                case 0:
                    intHelpNum = 0;
                    break;
                case 1:
                    intHelpNum--;
                    break;
                case 2:
                    intHelpNum++;
                    break;
                }
                break;
            case 4:
                TheHelpBalloon= new MyBalloonWindow();
                TheHelpBalloon.AnchorQuadrant = AnchorQuadrant.Right;
				_MainForm.RightDocks_BottomPanel_Media.Open();
                pos = new Point(_MainForm.Location.X+ 300 ,_MainForm.Location.Y+160);
				intButtonClicked = TheHelpBalloon.ShowHelp(pos,Lang.say("Help.Presentation.4.Title"),Lang.say("Help.Presentation.4"));
                _MainForm.Focus();
                TheHelpBalloon.Dispose();
                switch (intButtonClicked){
                case 0:
					intHelpNum = 0;
					break;
                case 1:
                    intHelpNum--;
                    break;
                case 2:
                    intHelpNum++;
                    break;
                }
                break;
			case 5:
				TheHelpBalloon= new MyBalloonWindow();
				TheHelpBalloon.AnchorQuadrant = AnchorQuadrant.Right;
				_MainForm.RightDocks_BottomPanel_MediaLists.Open();
				TheHelpBalloon.button1.Text = Lang.say("Help.Buttons.Finish");
				pos = new Point(_MainForm.Location.X+320,_MainForm.Location.Y+290);
				intButtonClicked = TheHelpBalloon.ShowHelp(pos,Lang.say("Help.Presentation.5.Title"),Lang.say("Help.Presentation.5"));
				_MainForm.Focus();
				TheHelpBalloon.Dispose();
				switch (intButtonClicked){
				case 0:
					intHelpNum = 0;
					_MainForm.RightDocks_BottomPanel_Media.Open();
					break;
				case 1:
					intHelpNum--;
					break;
				case 2:
					intHelpNum = 0;
					_MainForm.RightDocks_BottomPanel_Media.Open();
					break;
				}
				break;
			}
		}while (intHelpNum != 0);
	}
	#endregion

	#region TextTool
		public void HelpTextTool() {
        int intHelpNum = 1;
        int intButtonClicked;
        Point pos;
        _MainForm.Size = new Size(790,570);

        do{
            switch(intHelpNum){
            case 1:
                TheHelpBalloon= new MyBalloonWindow();
                TheHelpBalloon.AnchorQuadrant = AnchorQuadrant.Auto;
                TheHelpBalloon.button3.Hide();
                pos = new Point(_MainForm.Location.X+40 ,_MainForm.Location.Y+270);
                intButtonClicked = TheHelpBalloon.ShowHelp(pos,Lang.say("Help.TextTool.1.Title"),Lang.say("Help.TextTool.1"));
                _MainForm.Focus();
                TheHelpBalloon.Dispose();
                switch (intButtonClicked){
                case 0:
                    intHelpNum = 0;
                    break;
                case 1:
                    intHelpNum = 0;
                    break;
                case 2:
                    intHelpNum++;
                    break;
                }
                break;
            case 2:
                _MainForm.GuiTools.ShowTab(MainTab.SermonTools);
				if(_MainForm.Sermon_DocManager.TabStrips.Count < 1){
                    _MainForm.Sermon_NewDocument();
                }

                TheHelpBalloon= new MyBalloonWindow();
                TheHelpBalloon.AnchorQuadrant = AnchorQuadrant.Auto;
                pos = new Point(_MainForm.Location.X+ 200 ,_MainForm.Location.Y+300);
                intButtonClicked = TheHelpBalloon.ShowHelp(pos,Lang.say("Help.TextTool.2.Title"),Lang.say("Help.TextTool.2"));
                _MainForm.Focus();
                TheHelpBalloon.Dispose();
                switch (intButtonClicked){
                case 0:
                    intHelpNum = 0;
                    break;
                case 1:
                    intHelpNum--;
                    break;
                case 2:
                    intHelpNum++;
                    break;
                }
                break;
            case 3:
                TheHelpBalloon= new MyBalloonWindow();
                TheHelpBalloon.AnchorQuadrant = AnchorQuadrant.Auto;
                pos = new Point(_MainForm.Location.X+ 200 ,_MainForm.Location.Y+115);
                intButtonClicked = TheHelpBalloon.ShowHelp(pos,Lang.say("Help.TextTool.3.Title"),Lang.say("Help.TextTool.3"));
                _MainForm.Focus();
                TheHelpBalloon.Dispose();
                switch (intButtonClicked){
                case 0:
                    intHelpNum = 0;
                    break;
                case 1:
                    intHelpNum--;
                    break;
                case 2:
                    intHelpNum++;
                    break;
                }
                break;
            case 4:
                TheHelpBalloon= new MyBalloonWindow();
                TheHelpBalloon.AnchorQuadrant = AnchorQuadrant.Right;
                pos = new Point(_MainForm.Location.X+ 150 ,_MainForm.Location.Y+160);
                intButtonClicked = TheHelpBalloon.ShowHelp(pos,Lang.say("Help.TextTool.4.Title"),Lang.say("Help.TextTool.4"));
                _MainForm.Focus();
                TheHelpBalloon.Dispose();
                switch (intButtonClicked){
                case 0:
                    intHelpNum = 0;
                    break;
                case 1:
                    intHelpNum--;
                    break;
                case 2:
                    intHelpNum++;
                    break;
                }
                break;
            case 5:
				TheHelpBalloon= new MyBalloonWindow();
                TheHelpBalloon.AnchorQuadrant = AnchorQuadrant.Bottom;
                TheHelpBalloon.button1.Text = Lang.say("Help.Buttons.Finish");
                pos = new Point(_MainForm.Location.X+400,_MainForm.Location.Y+320);
                intButtonClicked = TheHelpBalloon.ShowHelp(pos,Lang.say("Help.TextTool.5.Title"),Lang.say("Help.TextTool.5"));
                _MainForm.Focus();
				TheHelpBalloon.Dispose();
                switch (intButtonClicked){
                case 0:
                    intHelpNum = 0;
                    break;
                case 1:
                    intHelpNum--;
                    break;
                case 2:
                    intHelpNum = 0;
                    break;
                }
                break;
            }
        }while (intHelpNum != 0);




	}
	#endregion


	///<summary>Displays the About Menu </summary>
	public void AboutButton(){
		About.version = _MainForm.version;
        About.ShowDialog();
    }


#endregion




	}
}
