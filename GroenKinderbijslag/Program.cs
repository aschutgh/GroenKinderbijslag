using System;
using System.Collections.Generic;


namespace GroenKinderbijslag
{
    public class Kind
    {
        public DateTime Gebdat { get; set; }
        public DateTime Peildat { get; set; }
        public bool Jongertwaalf { get; set; }

        public Kind() { }
        public Kind(DateTime gebdat, DateTime peildat)
        {
            Gebdat = gebdat;
            Peildat = peildat;
            if (DateTime.Compare(Gebdat.AddYears(12), Peildat) <= 0)
            {
                Jongertwaalf = false;
            }
            else
            {
                Jongertwaalf = true;
            }
        }
        public override string ToString()
        {
            return "Geboren: " + Gebdat + " Peildatum: " + Peildat + " Jonger dan twaalf? " + Jongertwaalf;
        }

    }



    class Program
    {
        static string vraagGebdat()
        {
            Console.Write("Geef geboortedatum van kind in formaat jjjj/mm/dd: ");
            return Console.ReadLine();
        }

        static string vraagPeildat()
        {
            Console.Write("Geef peildatum in formaat jjjj/mm/dd: ");
            return Console.ReadLine();
        }

        static void vraagKindInfo(ref List<Kind> tkinderen)
        {
            bool minderjarig = true;

            DateTime gebdat = DateTime.Parse(vraagGebdat());
            DateTime peildat = DateTime.Parse(vraagPeildat());

            if (DateTime.Compare(gebdat.AddYears(18), peildat) <= 0)
            {
                minderjarig = false;
            }
            else
            {
                minderjarig = true;
            }

            //Console.WriteLine("Minderjarig? : {0}", minderjarig);
            //Console.WriteLine("Geboortedatum + 18 jaar: {0}", gebdat.AddYears(18));
            if (minderjarig == true)
            {
                var nieuwkind = new Kind(gebdat, peildat);
                tkinderen.Add(nieuwkind);
            }
            else
            {
                Console.WriteLine("Dit is een volwassene!");
            }

        }

        static Double berekenKinderBijslag(List<Kind> tkinderen)
        {
            Double kinderbijslag = 0.0;
            int ak = tkinderen.Count; // aantal kinderen

            foreach(Kind individu in tkinderen)
            {
                if (individu.Jongertwaalf == true)
                {
                    kinderbijslag = kinderbijslag + 150;
                }
                else
                {
                    kinderbijslag = kinderbijslag + 235;
                }
            }

            if (ak == 3 || ak == 4)
            {
                kinderbijslag = kinderbijslag * 1.02;
            }

            if (ak == 5)
            {
                kinderbijslag = kinderbijslag * 1.03;
            }

            if (ak >= 6)
            {
                kinderbijslag = kinderbijslag * 1.035;
            }

            return kinderbijslag;

        }

        static void Main(string[] args)
        {
            List<Kind> kinderen = new List<Kind>();

            Console.WriteLine("Uitrekenen kinderbijslag.");
            var meerkinderen = true;
            while (meerkinderen == true)
            {
                vraagKindInfo(ref kinderen);
                Console.WriteLine("Wil je meer kinderen invoeren? (ja of nee)");
                if (Console.ReadLine() == "nee")
                {
                    meerkinderen = false;
                }
            }

            Console.WriteLine("Opgegeven kinderen: ");
            foreach(Kind individu in kinderen)
            {
                Console.WriteLine(individu);
            }

            Console.WriteLine("De totale kinderbijslag bedraagt: {0}", berekenKinderBijslag(kinderen));
            
        }
    }
}
