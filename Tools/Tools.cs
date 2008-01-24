using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.IO;
using System.Runtime.Serialization;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Diagnostics;
using Microsoft.Win32;

namespace DreamBeam {

	/// <summary>
	/// This is used to serialize Color objects to XML, and it indicates the format of the color data.
	/// </summary>
	public enum ColorFormat {
		NamedColor,
		ARGBColor
	}

	[Flags]
	public enum MapFlags {
		MAP_FOLDCZONE = 0x00000010,// fold compatibility zone chars
		MAP_PRECOMPOSED = 0x00000020,// convert to precomposed chars
		MAP_COMPOSITE = 0x00000040, // convert to composite chars
		MAP_FOLDDIGITS = 0x00000080 // all digits to ASCII 0-9
	}



    /// <summary>
    /// Zusammenfassende Beschreibung f�r Class
    /// </summary>
	public class Tools {
		public Tools() {
		}

		/// <summary>
		/// Reverses A String
		/// </summary>
		public static string Reverse(string str) {
			/*termination condition*/
			if (1== str.Length ) {
				return str ;
			} else {
				return Reverse( str.Substring (1) ) + str.Substring (0,1);
			}
		}
		/// <summary>
		/// Counts the Number of Needles in the InputString
		/// </summary>
		public static int Count(string Input, string Needle) {
			int x = 1;
			while (Input.IndexOf(Needle)>= 0) {
				Input=Input.Substring(Input.IndexOf(Needle)+Needle.Length);
				x++;
			}
			return x;
		}

		private static DateTime lastTime = DateTime.Now;
		/// <summary>
		/// This function is used primarily for profiling code. It shows
		/// the current time and the amount of time elapsed since it was last called.
		/// </summary>
		/// <param name="msg">A message to display</param>
		/// <returns>A formatted sentence with all relevant times showsn</returns>
		public static string ElapsedTime(string msg) {
			string res;
			System.TimeSpan span = DateTime.Now.Subtract(Tools.lastTime);
			string last = Tools.lastTime.TimeOfDay.ToString();
			Tools.lastTime = DateTime.Now;
			res = "Last time " + last +	" now " + Tools.lastTime.TimeOfDay +
					"  Elapsed " + span + " -> " + msg;
			Console.WriteLine(res);
			return res;
		}


        public static System.Drawing.Size VideoProportions(System.Drawing.Size WindowSize, System.Drawing.Size VideoSize) {
            double WindowProportion= (double)WindowSize.Height / (double)WindowSize.Width;

            double VideoProportion = (double)VideoSize.Height / (double)VideoSize.Width;

            if (WindowProportion > VideoProportion) {
                return (new System.Drawing.Size(WindowSize.Width,(int)(WindowSize.Width*VideoProportion)));
            } else if(WindowProportion < VideoProportion) {
                return (new System.Drawing.Size((int)(WindowSize.Height/VideoProportion),WindowSize.Height));
            }
            return (WindowSize);
        }

        /// <summary>
        /// This is the directory that contains the song files, backgrounds, etc...
        /// 
        /// In Vista this can not be a subdirectory of "Program Files" due to
        /// permission issues after the SW is installed.
        /// </summary>
        /// <returns></returns>
        public static string GetAppDocPath()
        {
            //return System.Windows.Forms.Application.StartupPath;
            string defaultPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "DreamBeam");
            RegistryKey masterKey = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\DreamBeam");
            if (masterKey == null) {
                // Key doesn't exist and we could not create it (permissions?)
                Directory.CreateDirectory(defaultPath);
                return defaultPath;
            } else {
                defaultPath = masterKey.GetValue("UserFilesDir", defaultPath).ToString();
                masterKey.Close();
                Directory.CreateDirectory(defaultPath);
                return defaultPath;
            }
        }

        /// <summary>
        /// This function should return the same thing that NSIS returns for $APPDATA\${PRODUCT}
        /// </summary>
        /// <returns>A place where the application should store data that is common among machine users.</returns>
        public static string GetCommonAppDataPath() {
            string fullPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            fullPath = Path.Combine(fullPath, "DreamBeam");
            Directory.CreateDirectory(fullPath);
            return fullPath;
        }

        /// <summary>
        /// The cache is probably not compatible between versions, so we use a cache directory
        /// which contains the app version number.
        /// 
        /// Application.CommonAppDataPath contains build and revision number so if
        /// we save settings to that directory we won't be able to read them back
        /// during development because the build and/or revision number keeps
        /// changing.
        /// 
        /// For proper uninstall of DreamBeam, the parent of the directory returned
        /// by this function should be the same as NSIS's $APPDATA\${PRODUCT}\Cache
        /// 
        /// </summary>
        /// <returns>A directory where the application should store cached data.
        /// Basically the Application.CommonAppDataPath with the build and version number removed.</returns>
        public static string GetAppCachePath() {
            Version vrs = new Version(Application.ProductVersion);
            string fullPath = CombinePaths(
                GetCommonAppDataPath(),
                "Cache",
                vrs.Major + "." + vrs.Minor);
            return fullPath;
        }


		/// <summary>
		/// If the FileName is not an absolute path, it assumes it is relative to the application data directory
		/// and generates the full path based on that assumption. This function is used primarily to obtain
		/// the full path of background images that could be stored in various places as relative paths.
		/// </summary>
		/// <param name="FileName"></param>
		/// <returns>The filename (if the file exists), or NULL if the file does not exist.</returns>
		public static string GetFullPath(string FileName) {
			// We need at least 1 char for file name + 4 chars for extension ".jpg"
			if (FileName == null || FileName.Length < 5) return null;
			FileInfo fi = new FileInfo(FileName);
			if ( FileExists(fi.FullName) ) {
				return fi.FullName;
			}

			fi = new FileInfo(Path.Combine(Tools.GetAppDocPath(), FileName));
			if ( FileExists(fi.FullName) ) {
				return fi.FullName;
			}

			return null;
		}

        public static string CombinePaths(params string[] paths) {
            string path = "";
            foreach (string p in paths) {
                path = Path.Combine(path, p);
            }
            return path;
        }

		[DllImport("kernel32.dll")]
		  public static extern bool Beep(int frequency, int duration);

		/// <summary>
		/// TODO: Replace calls to this function with String.IsNullOrEmpty in .NET 2.0
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public static bool StringIsNullOrEmpty(string str) {
			if (str == null || str.Length == 0) {
				return true;
			}
			return false;
		}

		public static bool StringIsNullOrEmptyTrim(string str) {
			if (str == null || str.Trim().Length == 0) return true;
			return false;
		}

		/// <summary>
		/// This function brings a value in range.
		/// </summary>
		/// <param name="min">The minimum value</param>
		/// <param name="max">The maximum value</param>
		/// <param name="val">The value to bring in range</param>
		/// <returns></returns>
		public static int ForceToRange(int min, int max, int val) {
			if (val > max) {
				return max;
			} else if (val < min) {
				return min;
			}
			return val;
		}

		public static decimal ForceToRange(decimal min, decimal max, decimal val) {
			if (val > max) {
				return max;
			} else if (val < min) {
				return min;
			}
			return val;
		}

		/// <summary>
		/// Makes a rectangle the same aspect ratio as a given target by growing one of its dimmensions (W or H).
		/// </summary>
		/// <param name="target">The aspect ratio to achieve</param>
		/// <param name="rect">The rectangle to modify</param>
		/// <returns></returns>
		public static RectangleF GrowToAspectRatio(RectangleF target, RectangleF rect) {
			float targetAR = target.Width / target.Height;
			float rectAR = rect.Width / rect.Height;
			RectangleF result = rect;

			if (rectAR > targetAR) {
				// Rect is wider than target. Need to adjust height.
				result.Height = result.Width / targetAR;
			} else if (rectAR < targetAR) {
				result.Width = result.Height * targetAR;
			}
			return result;
		}

		/// <summary>
		/// Provides a transformation matrix to be applied to a GraphicsPath object with
		/// a bounding box defined by "contents". Upon tranformation, the "contents" will fit in
		/// the "box", and will be aligned as described by the "alignment" argument.
		/// 
		/// The "contents" will be shrunk down if they exceed the box in width or height
		/// (preserving the aspect ratio).
		/// </summary>
		/// <param name="box">The box to fit the contents in</param>
		/// <param name="contents">The contents to fit</param>
		/// <param name="alignment">The horizontal alignment is expressed by .Alignment, and the vertical
		/// alignment by .LineAlignment</param>
		/// <returns>A matrix that can be used as an argument to GraphicsPath.Transform</returns>
		public static Matrix FitContents(RectangleF box, RectangleF contents, StringFormat alignment) {
			RectangleF cont = contents;	// This is the area inside the box where we want to place the contents

			// If the contents are bigger than the box, we first scale them down (maintaining the aspect ratio)
			if (cont.Width > box.Width) {
				cont.Height *= box.Width / cont.Width;
				cont.Width = box.Width;
			}
			if (cont.Height > box.Height) {
				cont.Width *= box.Height / cont.Height;
				cont.Height = box.Height;
			}

			// Now that "cont" fits inside "box", we use the alignment to figure out where to place it
			// Horizontal alignment
			switch (alignment.Alignment) {
				case StringAlignment.Near:
					cont.X = box.X; break;
				case StringAlignment.Center:
					cont.X = box.X + (box.Width - cont.Width)/2; break;
				case StringAlignment.Far:
					cont.X = box.X + (box.Width - cont.Width); break;
			}

			// Vertical alignment
			switch (alignment.LineAlignment) {
				case StringAlignment.Near:
					cont.Y = box.Y; break;
				case StringAlignment.Center:
					cont.Y = box.Y + (box.Height - cont.Height)/2; break;
				case StringAlignment.Far:
					cont.Y = box.Y + (box.Height - cont.Height); break;
			}

			PointF[] dest = {	new PointF(cont.Left, cont.Top),
								new PointF(cont.Right, cont.Top),
								new PointF(cont.Left, cont.Bottom) };

			return new Matrix(contents, dest);
		}

		/// <summary>
		/// Ensures the file is not null before calling FileInfo.Exists to see if it exists.
		/// </summary>
		/// <param name="file"></param>
		/// <returns></returns>
		public static bool FileExists(string file) {
			if (file == null || file.Length == 0) return false;
			try {
				FileInfo fi = new FileInfo(file);
				return fi.Exists;
			} catch { return false; }
		}

		/// <summary>
		/// XML serialization does not handle Color. We convert it to a string.
		/// </summary>
		/// <param name="color"></param>
		/// <returns></returns>
		public static string SerializeColor(Color color) {
			if( color.IsNamedColor )
				return string.Format("{0}:{1}", 
					ColorFormat.NamedColor, color.Name);
			else
				return string.Format("{0}:{1}:{2}:{3}:{4}", 
					ColorFormat.ARGBColor, 
					color.A, color.R, color.G, color.B);
		}

		/// <summary>
		/// This function takes a string generated by SerializeColor and returns a Color object.
		/// </summary>
		/// <param name="color"></param>
		/// <returns></returns>
		public static Color DeserializeColor(string color) {
			byte a, r, g, b;

			string [] pieces = color.Split(new char[] {':'});
		
			ColorFormat colorType = (ColorFormat) 
				Enum.Parse(typeof(ColorFormat), pieces[0], true);

			switch(colorType) {
				case ColorFormat.NamedColor:
					return Color.FromName(pieces[1]);

				case ColorFormat.ARGBColor:
					a = byte.Parse(pieces[1]);
					r = byte.Parse(pieces[2]);
					g = byte.Parse(pieces[3]);
					b = byte.Parse(pieces[4]);
			
					return Color.FromArgb(a, r, g, b);
			}
			return Color.Empty;
		}


		[DllImport("kernel32.dll", SetLastError=true)]
		static extern int FoldString(MapFlags dwMapFlags, string lpSrcStr, int cchSrc,
			[Out] StringBuilder lpDestStr, int cchDest);


		public static string RemoveDiacritics(string stIn) {
			// "sb" must be large enough to store the converted string, else we'll get an exception.
			StringBuilder sb = new StringBuilder(stIn.Length * 2 + 10);

			int ret = FoldString(MapFlags.MAP_COMPOSITE , stIn, stIn.Length, sb, stIn.Length * 2);
			sb.Length = ret;	// Otherwise we end up with garbage at the end because of "Length * 2" above
			return Regex.Replace(sb.ToString(), @"\p{Sk}", "");
		}

        public static XmlNode RenameXmlNode(XmlNode node, string namespaceURI, string qualifiedName) {
            if (node.NodeType == XmlNodeType.Element) {
                XmlElement oldElement = (XmlElement)node;
                XmlElement newElement = node.OwnerDocument.CreateElement(qualifiedName, namespaceURI);

                while (oldElement.HasAttributes) {
                    newElement.SetAttributeNode(oldElement.RemoveAttributeNode(oldElement.Attributes[0]));
                }

                while (oldElement.HasChildNodes) {
                    newElement.AppendChild(oldElement.RemoveChild(oldElement.FirstChild));
                }

                if (oldElement.ParentNode != null) {
                    oldElement.ParentNode.ReplaceChild(newElement, oldElement);
                }

                return newElement;
            } else {
                return null;
            }
        }

		// We use this to open or close the console programatically
		[DllImport("kernel32.dll")] public static extern Boolean AllocConsole();
		[DllImport("kernel32.dll")] public static extern Boolean FreeConsole();

    }


	[Serializable]
	public class SerializableHashtable : Hashtable,
		System.Xml.Serialization.IXmlSerializable {
		
		#region IXmlSerializable Members

		#region Node class
		/// 
		/// This class would be for custom serialization
		/// 
		[Serializable]
		public class Node {
			public object key;
			public object val;

			public Node() { }

			public Node(object k, object v) {
				key = k;
				val = v;
			}
		}

		#endregion Node class for XML Serialization

		/// <summary>
		/// Write the xml using an array list
		/// using the Node to store key value pairs
		/// </summary>
		public void WriteXml(System.Xml.XmlWriter writer) {
			XmlSerializer xs = new XmlSerializer(typeof(System.Collections.ArrayList),
				new System.Type[]{typeof(Node)});
			ArrayList list = new ArrayList();
			foreach(object key in this.Keys) {
				list.Add(new Node(key, this[key]));
			}
			xs.Serialize(writer,list);
		}

		public System.Xml.Schema.XmlSchema GetSchema() {
			// TODO:  Add SerializableHashtable.GetSchema implementation
			return null;
		}

		/// <summary>
		/// Deserialization using array list
		/// and the node(key,value) pairs
		/// </summary>
		public void ReadXml(System.Xml.XmlReader reader) {
			XmlSerializer xs = new XmlSerializer(typeof(System.Collections.ArrayList),
				new System.Type[]{typeof(Node)});

			//Move the reader into the ArrayList element.
			reader.Read();
			ArrayList list  = xs.Deserialize(reader) as ArrayList;

			if(list == null)
				return;

			//Reload the hashTable.
			foreach (Node node in list) {
				this.Add(node.key, node.val);
			}
		}
		#endregion
	}

}
