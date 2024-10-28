using System;
using System.Collections.Generic;

namespace OOP_Bankomat
{
    internal class Program
    {
        // Lista som lagrar alla skapade konton
        static List<Konto> konton = new List<Konto>();

        static void Main(string[] args)
        {
            bool körProgram = true; // Flagga för att hålla programmet igång

            while (körProgram)
            {
                // Visa menyalternativ
                Console.WriteLine("Välj ett alternativ:");
                Console.WriteLine("1. Skapa ett nytt konto");
                Console.WriteLine("2. Gör en insättning på ett konto");
                Console.WriteLine("3. Gör ett uttag på ett konto");
                Console.WriteLine("4. Visa saldot på ett konto");
                Console.WriteLine("5. Skriv ut en lista på alla kontonr och saldon");
                Console.WriteLine("6. Gör utdelning av ränta till alla konton");
                Console.WriteLine("7. Sortera konton");
                Console.WriteLine("8. Avsluta");

                // Läser in användarens val
                string val = Console.ReadLine();

                // Om användaren inte matar in något, fortsätt omstarta menyn
                if (string.IsNullOrEmpty(val))
                {
                    Console.WriteLine("Ogiltigt val, försök igen.");
                    continue;
                }

                // Switch-sats för att välja åtgärd baserat på användarens input
                switch (val)
                {
                    case "1":
                        SkapaNyttKonto(); // Skapa nytt konto
                        break;
                    case "2":
                        GörInsättning(); // Göra en insättning
                        break;
                    case "3":
                        GörUttag(); // Göra ett uttag
                        break;
                    case "4":
                        VisaSaldo(); // Visa saldo för ett konto
                        break;
                    case "5":
                        SkrivUtAllaKonton(); // Skriv ut alla konton
                        break;
                    case "6":
                        GörUtdelningAvRänta(); // Gör räntesutdelning till alla konton
                        break;
                    case "7":
                        SorteraKonton(); // Sortera konton baserat på olika kriterier
                        break;
                    case "8":
                        körProgram = false; // Avslutar programmet
                        break;
                    default:
                        Console.WriteLine("Ogiltigt val, försök igen.");
                        break;
                }
            }
        }

        private static void SorteraKonton()
        {
            Console.WriteLine("Sortera efter (1) Saldo eller (2) Räntesats?");
            string input = Console.ReadLine();

            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Inmatningen kan inte vara null eller tom.");
                return;
            }

            int val;
            if (!int.TryParse(input, out val))
            {
                Console.WriteLine("Ogiltigt val.");
                return;
            }

            if (val == 1)
            {
                // Sortera konton efter saldo (stigande)
                konton.Sort((x, y) => x.Saldo.CompareTo(y.Saldo));
                Console.WriteLine("Konton sorterade efter saldo (stigande):");
                foreach (var konto in konton)
                {
                    Console.WriteLine($"{konto.Namn}: {konto.Saldo} kr");
                }

                // Sortera konton efter saldo (fallande)
                konton.Sort((x, y) => y.Saldo.CompareTo(x.Saldo));
                Console.WriteLine("Konton sorterade efter saldo (fallande):");
                foreach (var konto in konton)
                {
                    Console.WriteLine($"{konto.Namn}: {konto.Saldo} kr");
                }
            }
            else if (val == 2)
            {
                // Sortera konton efter räntesats (stigande)
                konton.Sort((x, y) => x.Räntesats.CompareTo(y.Räntesats));
                Console.WriteLine("Konton sorterade efter räntesats (stigande):");
                foreach (var konto in konton)
                {
                    Console.WriteLine($"{konto.Namn}: {konto.Räntesats}%");
                }

                // Sortera konton efter räntesats (fallande)
                konton.Sort((x, y) => y.Räntesats.CompareTo(x.Räntesats));
                Console.WriteLine("Konton sorterade efter räntesats (fallande):");
                foreach (var konto in konton)
                {
                    Console.WriteLine($"{konto.Namn}: {konto.Räntesats}%");
                }
            }
            else
            {
                Console.WriteLine("Ogiltigt val.");
            }
        }

        // Funktion för att skapa ett nytt konto
        static void SkapaNyttKonto()
        {
            string kontonummer, namn;
            double räntesats, saldo, maxKredit;

            // Kontroll för kontonummer
            do
            {
                Console.Write("Ange kontonummer: ");
                kontonummer = Console.ReadLine();
                if (string.IsNullOrEmpty(kontonummer))
                {
                    Console.WriteLine("Kontonummer får inte vara tomt.");
                }
                else if (KontonummerFinns(kontonummer))
                {
                    Console.WriteLine("Detta kontonummer finns redan. Välj ett annat.");
                    kontonummer = null;
                }
            } while (string.IsNullOrEmpty(kontonummer)); // Upprepa tills ett giltigt kontonummer anges

            // Kontroll för namn
            do
            {
                Console.Write("Ange namn: ");
                namn = Console.ReadLine();
                if (string.IsNullOrEmpty(namn))
                {
                    Console.WriteLine("Namn får inte vara tomt.");
                }
                else if (KontonamnFinns(namn))
                {
                    Console.WriteLine("Detta kontonamn finns redan. Välj ett annat.");
                    namn = null;
                }
            } while (string.IsNullOrEmpty(namn)); // Upprepa tills ett giltigt namn anges

            // Kontroll för räntesats
            while (true)
            {
                Console.Write("Ange räntesats (%): ");
                if (double.TryParse(Console.ReadLine(), out räntesats))
                {
                    break; // Räntesatsen är giltig
                }
                Console.WriteLine("Ogiltig räntesats, försök igen.");
            }

            // Kontroll för startsaldo
            while (true)
            {
                Console.Write("Ange startsaldo: ");
                if (double.TryParse(Console.ReadLine(), out saldo))
                {
                    break; // Saldot är giltigt
                }
                Console.WriteLine("Ogiltigt startsaldo, försök igen.");
            }

            // Kontroll för maxkredit
            while (true)
            {
                Console.Write("Ange maxkredit: ");
                if (double.TryParse(Console.ReadLine(), out maxKredit))
                {
                    break; // Maxkredit är giltigt
                }
                Console.WriteLine("Ogiltigt maxkredit, försök igen.");
            }

            // Skapa ett nytt konto och lägg till det i kontonlistan
            Konto nyttKonto = new Konto(kontonummer, namn, räntesats, saldo, maxKredit);
            konton.Add(nyttKonto);

            Console.WriteLine("Nytt konto skapat.");
        }

        // Funktion för att göra en insättning på ett konto
        static void GörInsättning()
        {
            Console.Write("Ange kontonummer: ");
            string kontonummer = Console.ReadLine(); // Läser in kontonummer

            // Kontroll om inmatningen är tom
            if (string.IsNullOrEmpty(kontonummer))
            {
                Console.WriteLine("Kontonummer får inte vara tomt.");
                return;
            }

            // Hittar konto baserat på kontonummer
            Konto konto = HittaKonto(kontonummer);

            if (konto != null)
            {
                double belopp;
                // Kontroll för insättningsbelopp
                while (true)
                {
                    Console.Write("Ange belopp att sätta in: ");
                    if (double.TryParse(Console.ReadLine(), out belopp))
                    {
                        break; // Beloppet är giltigt
                    }
                    Console.WriteLine("Ogiltigt belopp, försök igen.");
                }

                konto.Insättning(belopp); // Lägger till beloppet till kontot
                Console.WriteLine("Insättning genomförd.");
            }
            else
            {
                Console.WriteLine("Konto hittades inte.");
            }
        }

        // Funktion för att göra ett uttag från ett konto
        static void GörUttag()
        {
            Console.Write("Ange kontonummer: ");
            string kontonummer = Console.ReadLine(); // Läser in kontonummer

            // Kontroll om inmatningen är tom
            if (string.IsNullOrEmpty(kontonummer))
            {
                Console.WriteLine("Kontonummer får inte vara tomt.");
                return;
            }

            // Hittar konto baserat på kontonummer
            Konto konto = HittaKonto(kontonummer);

            if (konto != null)
            {
                double belopp;
                // Kontroll för uttagsbelopp
                while (true)
                {
                    Console.Write("Ange belopp att ta ut: ");
                    if (double.TryParse(Console.ReadLine(), out belopp))
                    {
                        break; // Beloppet är giltigt
                    }
                    Console.WriteLine("Ogiltigt belopp, försök igen.");
                }

                if (konto.Uttag(belopp)) // Försöker göra uttag
                {
                    Console.WriteLine("Uttag genomfört.");
                }
                else
                {
                    Console.WriteLine("Uttaget kunde inte genomföras.");
                }
            }
            else
            {
                Console.WriteLine("Konto hittades inte.");
            }
        }

        // Funktion för att visa saldo för ett konto
        static void VisaSaldo()
        {
            Console.Write("Ange kontonummer: ");
            string kontonummer = Console.ReadLine(); // Läser in kontonummer

            // Kontroll om inmatningen är tom
            if (string.IsNullOrEmpty(kontonummer))
            {
                Console.WriteLine("Kontonummer får inte vara tomt.");
                return;
            }

            // Hittar konto baserat på kontonummer
            Konto konto = HittaKonto(kontonummer);

            if (konto != null)
            {
                Console.WriteLine($"Saldo för konto {konto.Kontonummer}: {konto.Saldo} kr"); // Visar saldo
            }
            else
            {
                Console.WriteLine("Konto hittades inte.");
            }
        }

        // Funktion för att skriva ut alla konton
        static void SkrivUtAllaKonton()
        {
            Console.WriteLine("Lista över alla konton:");

            foreach (var konto in konton)
            {
                Console.WriteLine($"{konto.Kontonummer}, {konto.Namn}: {konto.Saldo} kr");
            }
        }

        // Funktion för att göra utdelning av ränta till alla konton
        static void GörUtdelningAvRänta()
        {
            foreach (var konto in konton)
            {
                konto.UtdelningAvRänta(); // Utdelar ränta
            }
            Console.WriteLine("Räntan har utdelats till alla konton.");
        }

        // Funktion för att hitta ett konto baserat på kontonummer
        static Konto HittaKonto(string kontonummer)
        {
            foreach (var konto in konton)
            {
                if (konto.Kontonummer == kontonummer)
                {
                    return konto; // Returnera kontot om det hittas
                }
            }
            return null; // Returnera null om kontot inte hittades
        }

        // Funktion för att kontrollera om ett kontonummer redan finns
        static bool KontonummerFinns(string kontonummer)
        {
            foreach (var konto in konton)
            {
                if (konto.Kontonummer == kontonummer)
                {
                    return true; // Kontonumret finns redan
                }
            }
            return false; // Kontonumret finns inte
        }

        // Funktion för att kontrollera om ett kontonamn redan finns
        static bool KontonamnFinns(string namn)
        {
            foreach (var konto in konton)
            {
                if (konto.Namn == namn)
                {
                    return true; // Kontonamnet finns redan
                }
            }
            return false; // Kontonamnet finns inte
        }
    }

    // Klass som representerar ett konto
    public class Konto
    {
        public string Kontonummer { get; private set; }
        public string Namn { get; private set; }
        public double Räntesats { get; private set; }
        public double Saldo { get; private set; }
        public double MaxKredit { get; private set; }

        public Konto(string kontonummer, string namn, double räntesats, double saldo, double maxKredit)
        {
            Kontonummer = kontonummer;
            Namn = namn;
            Räntesats = räntesats;
            Saldo = saldo;
            MaxKredit = maxKredit;
        }

        // Metod för att göra en insättning
        public void Insättning(double belopp)
        {
            Saldo += belopp; // Lägger till beloppet till saldot
        }

        // Metod för att göra ett uttag
        public bool Uttag(double belopp)
        {
            if (Saldo + MaxKredit >= belopp) // Kollar om uttaget kan göras
            {
                Saldo -= belopp; // Minskar saldot med uttagsbeloppet
                return true; // Uttaget lyckades
            }
            return false; // Uttaget misslyckades
        }

        // Metod för att utdela ränta
        public void UtdelningAvRänta()
        {
            Saldo += Saldo * (Räntesats / 100); // Lägger till räntan till saldot
        }
    }
}
