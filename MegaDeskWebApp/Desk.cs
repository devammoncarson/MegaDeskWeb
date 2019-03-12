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
        public int MaterialId { get; set; }
        public Desk(decimal width, decimal depth, int numDrawers, int materialId)
        {
            Width = width;
            Depth = depth;
            NumDrawers = numDrawers;
            MaterialId = materialId;
        }
    }
}