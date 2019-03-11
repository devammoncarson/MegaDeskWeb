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
        public decimal Quote { get; set; }
        public Desk Desk { get; set; }

         public DeskQuote(Desk desk, int time, string name, DateTime date)
        {
            Desk = desk;
            Shipping = time;
            Customer = name;
            Date = date;
            Price = GetQuote(desk, time);
        }

        public decimal GetQuote(Desk desk, int time)
        {
            decimal totalQuote = BASE_DESK_PRICE;
            decimal surfaceArea = desk.Width * desk.Depth;

            if (surfaceArea > 1000)
            {
                totalQuote += (surfaceArea - 1000);
            }

            if (desk.Drawers != 0)
            {
                totalQuote += (desk.Drawers * 50);
            }

            var db = WebMatrix.Data.Database.Open("MegaDeskWeb");

            var surfaceQuery = db.Query("SELECT Cost FROM SurfaceMaterial WHERE MaterialId = @0", desk.MaterialId);
            foreach (var row in surfaceQuery)
            {
                totalQuote += row.Cost;
            }

            var shippingQuery = db.Query("SELECT * FROM Shipping WHERE ShippingId = @0", time);
            foreach (var row in shippingQuery)
            {
                if (surfaceArea < 1000)
                {
                    totalQuote += row.CostLess1000;
                }
                else if (surfaceArea > 2000)
                {
                    totalQuote += row.CostGreater2000;
                }
                else
                {
                    totalQuote += row.Cost1000to2000;
                }
            }            
            return totalQuote;
        }
    }


        
}