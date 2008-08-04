using System;
using System.Collections.Generic;

using System.Text;

namespace DreamBeam
{
    interface ISongEditor
    {        
        Song Song { get; set; }
        string[] Collections { get; set; }
    }
}
