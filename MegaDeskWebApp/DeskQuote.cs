using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MegaDesk
{
    public class DeskQuote
    {
        const decimal BASE_DESK_PRICE = 200.00M;

        public string CustomerName { get; set; }
        public int NumShippingDays { get; set; }
        public DateTime QuoteDate { get; set; }
        public decimal Total { get; set; }
        public Desk Desk { get; set; }

        public DeskQuote()
        {
        }

        public decimal GetQuote(Desk desk, int NumShippingDays)
        {
            decimal totalQuote = BASE_DESK_PRICE;
            decimal surfaceArea = desk.Width * desk.Depth;

            if (surfaceArea > 1000)
            {
                totalQuote += (surfaceArea - 1000);
            }

            if (desk.NumDrawers!= 0)
            {
                totalQuote += (desk.NumDrawers * 50);
            }

            var db = WebMatrix.Data.Database.Open("MegaDeskDB2");

            var surfaceQuery = db.Query("SELECT MaterialCost FROM SurfaceMaterial WHERE MaterialId = @0", desk.MaterialId);
            foreach (var row in surfaceQuery)
            {
                totalQuote += row.MaterialCost;
            }

            var shippingQuery = db.Query("SELECT * FROM Shipping WHERE ShippingId = @0", NumShippingDays);
            foreach (var row in shippingQuery)
            {
                if (surfaceArea < 1000)
                {
                    totalQuote += row.LessThan1000;
                }
                else if (surfaceArea > 2000)
                {
                    totalQuote += row.MoreThan2000;
                }
                else
                {
                    totalQuote += row.LessThan2000;
                }
            }            
            return totalQuote;
        }
    }


        
}