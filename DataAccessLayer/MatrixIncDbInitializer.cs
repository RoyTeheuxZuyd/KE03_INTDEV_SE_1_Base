using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public static class MatrixIncDbInitializer
    {
        public static void Initialize(MatrixIncDbContext context)
        {
            // Look for any customers.
            if (context.Customers.Any() || context.Products.Any() || context.Parts.Any())
            {
                return;   // DB has been seeded
            }

            var parts = new Part[]
            {
                new Part { Name = "Tandwiel", Description = "Overdracht van rotatie in bijvoorbeeld de motor of luikmechanismen"},
                new Part { Name = "M5 Boutje", Description = "Bevestiging van panelen, buizen of interne modules"},
                new Part { Name = "Hydraulische cilinder", Description = "Openen/sluiten van zware luchtsluizen of bewegende onderdelen"},
                new Part { Name = "Koelvloeistofpomp", Description = "Koeling van de motor of elektronische systemen."},
                new Part { Name = "Brillendoekje", Description = "Voor het schoonhouden van brillenglazen."}
            };
            context.Parts.AddRange(parts);

            var products = new Product[]
            {
                new Product { Name = "Nebuchadnezzar", Description = "Het schip waarop Neo voor het eerst de echte wereld leert kennen", Price = 10000.00m, ProductImageName = "nebuchadnezzar.jpg"},
                new Product { Name = "Jack-in Chair", Description = "Stoel met een rugsteun en metalen armen waarin mensen zitten om ingeplugd te worden in de Matrix via een kabel in de nekpoort", Price = 500.50m, ProductImageName = "jackinchair.jpg"},
                new Product { Name = "EMP (Electro-Magnetic Pulse) Device", Description = "Wapentuig op de schepen van Zion", Price = 129.99m, ProductImageName = "empdevice.jpg" },
                new Product { Name = "Agent Smith sunglasses", Description = "Speciale zonnebril voor geheime operaties.", Price = 9.99m, ProductImageName = "agentsmithglasses.png"}
            };

            products[0].Parts.Add(parts[0]);
            products[0].Parts.Add(parts[3]);
            products[1].Parts.Add(parts[1]);
            products[2].Parts.Add(parts[0]);
            products[2].Parts.Add(parts[3]);
            products[3].Parts.Add(parts[4]);

            context.Products.AddRange(products);

            context.SaveChanges();

            context.Database.EnsureCreated();
        }
    }
}