using System.Collections.Generic;
using System.IO;
using System.Text;

namespace xCarpaccio.client
{
    using Nancy;
    using System;
    using Nancy.ModelBinding;

    public class IndexModule : NancyModule
    {
        public IndexModule()
        {
            Get["/"] = _ => "It works !!! You need to register your server on main server.";

            Post["/order"] = _ =>
            {
                using (var reader = new StreamReader(Request.Body, Encoding.UTF8))
                {
                    Console.WriteLine("Order received: {0}", reader.ReadToEnd());
                }

                var order = this.Bind<Order>();
                Bill bill = new Bill();
                bill.total = 0;
                //TODO: do something with order and return a bill if possible
                //Creation d'un dictionnaire contenant les taxes(%) selon les pays
                var taxes = new Dictionary<String,int>();
                taxes.Add("DE", 20);
                taxes.Add("UK", 21);
                taxes.Add("FR", 20);
                taxes.Add("IT", 25);
                taxes.Add("ES", 19);
                taxes.Add("PL", 21);
                taxes.Add("RO", 20);
                taxes.Add("NL", 20);
                taxes.Add("BE", 24);
                taxes.Add("EL", 20);
                taxes.Add("CZ", 19);
                taxes.Add("PT", 23);
                taxes.Add("HU", 27);
                taxes.Add("SE", 23);
                taxes.Add("AT", 22);
                taxes.Add("BG", 21);
                taxes.Add("DK", 21);
                taxes.Add("FI", 17);
                taxes.Add("SK", 18);
                taxes.Add("IE", 21);
                taxes.Add("HR", 23);
                taxes.Add("LT", 23);
                taxes.Add("SI", 24);
                taxes.Add("LV", 20);
                taxes.Add("EE", 22);
                taxes.Add("CY", 21);
                taxes.Add("LU", 25);
                taxes.Add("MT", 20);

                //Calcul du total brut de la commande
                for (var i = 0; i < order.Prices.Length; i++)
                {
                    bill.total += order.Prices[i]*order.Quantities[i];
                }

                //Application de la taxes
                bill.total *= 1m+(taxes[order.Country] / 100m);

                //Application d'une reducion standard selon le montant total
                if (bill.total >= 50000)
                {
                    bill.total *= 0.85m;
                }
                else if (bill.total >= 10000)
                {
                    bill.total *= 0.90m;
                }
                else if (bill.total >= 7000)
                {
                    bill.total *= 0.93m;
                }
                else if (bill.total >= 5000)
                {
                    bill.total *= 0.95m;
                }
                else if (bill.total >= 1000)
                {
                    bill.total *= 0.97m;
                }

                // If you manage to get the result, return a Bill object (JSON serialization is done automagically)
                // Else return a HTTP 404 error : return Negotiate.WithStatusCode(HttpStatusCode.NotFound);
                Console.WriteLine("Total: " + bill.total);
                return bill;
            };

            Post["/feedback"] = _ =>
            {
                var feedback = this.Bind<Feedback>();
                Console.Write("Type: {0}: ", feedback.Type);
                Console.WriteLine(feedback.Content);
                return Negotiate.WithStatusCode(HttpStatusCode.OK);
            };
        }
    }
}