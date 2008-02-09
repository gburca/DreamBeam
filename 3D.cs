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
using System.Windows.Forms;


using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;

using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace DreamBeam
{

	public class FXLib
	{

		private Device device = null;
		private bool initialized = false;

		public void Init3D(System.Windows.Forms.Control FXTarget){

			if (initialized == false)
			{
				initialized = true;
				// Set presentation parameters
				PresentParameters presentParams = new PresentParameters();
				presentParams.Windowed = true;
				presentParams.SwapEffect = SwapEffect.Discard;

				// Create our device
				device = new Device(0, DeviceType.Hardware, FXTarget, CreateFlags.SoftwareVertexProcessing, presentParams);
				device.Clear(ClearFlags.Target, Color.CornflowerBlue, 1.0f, 0);
			}

		}


	}

}
