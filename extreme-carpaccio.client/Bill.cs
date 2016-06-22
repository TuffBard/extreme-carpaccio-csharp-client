using System;
using System.Collections.Generic;

namespace xCarpaccio.client
{

    class Bill
    {

        public decimal total { get; set; }

        public void calculTotal(decimal[] prices,int[] quantities)
        {
            if (prices.Length == quantities.Length)
            {
                //Calcul du total brut de la commande
                for (var i = 0; i < prices.Length; i++)
                {
                    this.total += prices[i]*quantities[i];
                }
            }
        }

        public void applyTaxes(String country)
        {
            //Creation d'un dictionnaire contenant les taxes(%) selon les pays
            var taxes = new Dictionary<String, int>();
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

            //Application de la taxes
            if (taxes.ContainsKey(country))
                this.total *= 1m + (taxes[country] / 100m);
        }

        public void applyReduceStandard()
        {
            if (this.total >= 50000)
            {
                this.total *= 0.85m;
            }
            else if (this.total >= 10000)
            {
                this.total *= 0.90m;
            }
            else if (this.total >= 7000)
            {
                this.total *= 0.93m;
            }
            else if (this.total >= 5000)
            {
                this.total *= 0.95m;
            }
            else if (this.total >= 1000)
            {
                this.total *= 0.97m;
            }
        }
    }
}
