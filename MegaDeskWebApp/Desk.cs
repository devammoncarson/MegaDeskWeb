using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MegaDesk
{
    public class Desk
    {
        public decimal Width { get; set; }
        public decimal Depth { get; set; }
        public int NumDrawers { get; set; }

        public enum DesktopMaterial
        {
            Rosewood,
            Laminate,
            Veneer,
            Oak,
            Pine,
        }

        public DesktopMaterial Material { get; set; }

    }
}