using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace DreamBeam
{

	/// <summary>
	/// Person is the test class defining two properties: first name and last name .
	/// By deriving from GlobalizedObject the displaying of property names are language aware.
	/// GlobalizedObject implements the interface ICustomTypeDescriptor. 
	/// </summary>
	public class SongGrid : GlobalizedObject
	{
		private System.Drawing.Font privFont = new System.Drawing.Font("Times New Roman",22f);
		//private int[] privPosition = int[2]
		private System.Drawing.Color privTextColor = new System.Drawing.Color();
		private System.Drawing.Color privOutlineColor = new System.Drawing.Color();
		private int privPostitionX = 10;
		private int privPostitionY = 100;
		private bool privAntialias = true;
		private bool privOutline = true;

		public SongGrid() {}


		public System.Drawing.Font Font
		{
			get { return privFont; }
			set { privFont = value; }
		}

		public System.Drawing.Color TextColor
		{
			get { return privTextColor; }
			set { privTextColor = value; }
		}

		public bool Antialias
		{
			get { return privAntialias; }
			set { privAntialias = value; }
		}


		public bool Outline
		{
			get { return privOutline; }
			set { privOutline = value; }
		}

		public System.Drawing.Color OutlineColor
		{
			get { return privOutlineColor; }
			set { privOutlineColor = value; }
		}

		public int PositionX
		{
			get { return privPostitionX; }
			set { privPostitionX = value; }
		}

		public int PositionY
		{
			get { return privPostitionY; }
			set { privPostitionY = value; }
		}


		// Uncomment the next line to see the attribute in action:
		// [GlobalizedProperty("Surname",Description="ADescription",Table="GlobalizedPropertyGrid.SpecialStringTable")]
	}
}

