using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace DreamBeam
{
	/// <summary>
	/// Zusammenfassende Beschreibung f�r WinForm
	/// </summary>
	public class Previewer : SlideDialog
	{
		/// <summary>
		/// Erforderliche Designervariable
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Previewer(Form poOwner, float pfStep) : base(poOwner, pfStep)
		{
			//
			// Erforderlich f�r die Unterst�tzung des Windows Forms-Designer
			//
			InitializeComponent();

			//
			// TODO: F�gen Sie nach dem Aufruf von InitializeComponent() Konstruktorcode hinzu.
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
		/// Erforderliche Methode zur Unterst�tzung des Designers -
		/// �ndern Sie die Methode nicht mit dem Quelltext-Editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.Size = new System.Drawing.Size(300,300);
			this.Text = "WinForm";
		}
		#endregion
	}
}
