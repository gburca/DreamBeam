using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace DreamBeam {
    /// <summary>
    /// Zusammenfassende Beschreibung für Class
    /// </summary>
    public class Tools {
        public Tools() {



            //
            // TODO: Hier die Konstruktorlogik einfügen
            //
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

        public static string DreamBeamPath() {
            return System.Windows.Forms.Application.StartupPath;
        }

    }
}
