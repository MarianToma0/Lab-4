using System;
using System.Collections.Generic;
using System.IO;

public class ManagerMasini
{
    public static List<Masina> masiniDisponibile = new List<Masina>();
    public const string caleFisier = "masini.txt";
    //adaugarea masinilor in program(model,an,pret)
    static ManagerMasini()
    {
        CitesteMasiniDinFisier();
    }
    public static void CitesteMasiniDinFisier()
    {
        /*if (File.Exists("masini.txt"))
        {
            string[] linii = File.ReadAllLines("masini.txt");
            foreach (var linie in linii)
            {
                var detalii = linie.Split(',');
                string model = detalii[0];
                int an = int.Parse(detalii[1]);
                double pret = double.Parse(detalii[2]);

                Masina masina = new Masina(model, an, pret);
                masiniDisponibile.Add(masina);
            }
        }*/
        if (File.Exists(caleFisier))
        {
            string[] linii = File.ReadAllLines("masini.txt");
            foreach (var linie in linii)
            {
                var detalii = linie.Split(',');

                if (detalii.Length >= 4) // Verificăm dacă avem cel puțin 4 valori în linie
                {
                    string model = detalii[0].Trim();
                    int an;
                    double pret;
                    bool inchiriata;

                    if (int.TryParse(detalii[1].Trim(), out an) &&
                        double.TryParse(detalii[2].Trim(), out pret) &&
                        bool.TryParse(detalii[3].Trim(), out inchiriata))
                    {
                        Masina masina = new Masina(model, an, pret, Culoare.Alb, Optiuni.AerConditionat | Optiuni.Navigatie) { Inchiriata = inchiriata };
                        masiniDisponibile.Add(masina);
                    }
                    else
                    {
                        Console.WriteLine($"Eroare la citirea detaliilor pentru mașina: {linie}. Linia va fi ignorată.");
                    }
                }
                else
                {
                    Console.WriteLine($"Linia '{linie}' din fișierul {caleFisier} nu conține detaliile necesare. Linia va fi ignorată.");
                }
            }
        }

    }
    /*public static void AdaugaMasina()
    {
        Console.WriteLine("Introduceti detalii despre masina:");
        Console.Write("Model: ");
        string model = Console.ReadLine();
        Console.Write("An: ");
        int an = int.Parse(Console.ReadLine());
        Console.Write("Pret: ");
        double pret = double.Parse(Console.ReadLine());

        Masina masina = new Masina(model, an, pret);
        masiniDisponibile.Add(masina);

        // Salvăm detaliile mașinii în fișier
        SalveazaDetaliiMasinaInFisier(masina);

        Console.WriteLine("Detalii despre masina adaugate cu succes.");
    }*/
    public static void AdaugaMasina()
    {
        Console.WriteLine("Introduceti detalii despre masina:");
        Console.Write("Model: ");
        string model = Console.ReadLine();

        Console.Write("An: ");
        int an;
        while (!int.TryParse(Console.ReadLine(), out an)) // Verificăm dacă anul este un număr valid
        {
            Console.WriteLine("Introduceti un an valid.");
            Console.Write("An: ");
        }

        Console.Write("Pret: ");
        double pret;
        while (!double.TryParse(Console.ReadLine(), out pret)) // Verificăm dacă prețul este un număr valid
        {
            Console.WriteLine("Introduceti un pret valid.");
            Console.Write("Pret: ");
        }

        // Culoare
        Console.WriteLine("Culori disponibile: ");
        foreach (var culoare in Enum.GetValues(typeof(Culoare)))
        {
            Console.WriteLine($"- {culoare}");
        }
        Console.Write("Culoare: ");
        Culoare culoareSelectata;
        while (!Enum.TryParse(Console.ReadLine(), true, out culoareSelectata))
        {
            Console.WriteLine("Introduceti o culoare valida.");
            Console.Write("Culoare: ");
        }

        // Optiuni
        Console.WriteLine("Optiuni disponibile (separate prin virgula, fara spatii): ");
        foreach (var optiune in Enum.GetValues(typeof(Optiuni)))
        {
            Console.WriteLine($"- {optiune}");
        }
        Console.Write("Optiuni: ");
        string optiuniStr = Console.ReadLine();
        var optiuniArray = optiuniStr.Split(',');
        Optiuni optiuniSelectate = 0;
        foreach (var optiune in optiuniArray)
        {
            Optiuni optiuneEnum;
            if (Enum.TryParse(optiune.Trim(), true, out optiuneEnum))
            {
                optiuniSelectate |= optiuneEnum;
            }
        }

        Masina masina = new Masina(model, an, pret, culoareSelectata, optiuniSelectate);
        masiniDisponibile.Add(masina);

        Console.WriteLine("Detalii despre masina adaugate cu succes.");
        SalveazaDetaliiMasinaInFisier(masina);
    }


    public static void SalveazaDetaliiMasinaInFisier(Masina masina)
    {
        using (StreamWriter sw = new StreamWriter(caleFisier, true))
        {
            sw.WriteLine($"{masina.Model},{masina.An},{masina.Pret},{masina.Inchiriata},{masina.Culoare},{masina.Optiuni}");
        }
        Console.WriteLine("Detalii despre masina, salvate cu succes în fisier.");
    }
    //Inchirierea lor
    public static void InchiriazaMasina()
    {
        
        if (masiniDisponibile.Count == 0)
        {
            Console.WriteLine("Nu exista masini disponibile pentru inchiriere.");
            return;
        }

        AfiseazaMasini();
        Console.Write("Introduceti numarul masinii pe care doriti sa o inchiriati: ");
        int numarMasina;

        while (!int.TryParse(Console.ReadLine(), out numarMasina) || numarMasina < 0 || numarMasina >= masiniDisponibile.Count)
        {
            Console.WriteLine("Introduceti un număr de masina valid.");
            Console.Write("Introduceti numarul masinii pe care doriti sa o inchiriati: ");
        }

        Masina masinaSelectata = masiniDisponibile[numarMasina];
        if (!masinaSelectata.Inchiriata)
        {
            masinaSelectata.Inchiriata = true;
            Console.WriteLine("Masina a fost inchiriata cu succes.");
        }
        else
        {
            Console.WriteLine("Masina este deja inchiriata.");
        }
    }
    //returnarea lor
    public static void ReturneazaMasina()
    {
        AfiseazaMasini();
        Console.Write("Introduceti numarul masinii pe care doriti sa o returnati: ");
        int numarMasina = int.Parse(Console.ReadLine());

        if (numarMasina >= 0 && numarMasina < masiniDisponibile.Count)
        {
            Masina masinaSelectata = masiniDisponibile[numarMasina];
            if (masinaSelectata.Inchiriata)
            {
                masinaSelectata.Inchiriata = false;
                Console.WriteLine("Masina a fost returnata cu succes.");
            }
            else
            {
                Console.WriteLine("Masina nu este inchiriata.");
            }
        }
        else
        {
            Console.WriteLine("Numarul masinii introdus nu este valid.");
        }
    }
    //stock-ul de masini
    public static void AfiseazaMasini()
    {
        /* Console.WriteLine("Masini disponibile pentru inchiriere:");
         for (int i = 0; i < masiniDisponibile.Count; i++)
         {
             Console.WriteLine($"{i}. {masiniDisponibile[i]}");
         }*/
        Console.WriteLine("Listă masini disponibile pentru închiriere:");
        Console.WriteLine("-------------------------------------------------------------");
        Console.WriteLine("Nr. | Model\t| An\t| Pret\t| Inchiriat");
        Console.WriteLine("-------------------------------------------------------------");

        for (int i = 0; i < masiniDisponibile.Count; i++)
        {
            Masina masina = masiniDisponibile[i];
            Console.WriteLine($"{i}.  | {masina.Model}\t| {masina.An}\t| {masina.Pret}\t| {(masina.Inchiriata ? "Da" : "Nu")}");
        }
        Console.WriteLine("-------------------------------------------------------------");
    }
    //eliminarea masinilor din program
    public static void EliminaMasina()
    {
        AfiseazaMasini();
        Console.Write("Introduceti numarul masinii pe care doriti sa o eliminati: ");
        int numarMasina = int.Parse(Console.ReadLine());

        if (numarMasina >= 0 && numarMasina < masiniDisponibile.Count)
        {
            masiniDisponibile.RemoveAt(numarMasina);
            Console.WriteLine("Masina a fost eliminata cu succes.");
        }
        else
        {
            Console.WriteLine("Numarul masinii introdus nu este valid.");
        }
    }
    //cauta masina dupa denumirea introdusa in partea de adaugare
    public static List<Masina> CautaMasinaDupaNume(string numeCautat)
    {
        List<Masina> rezultat = new List<Masina>();

        foreach (Masina masina in masiniDisponibile)
        {
            if (masina.Model.Equals(numeCautat, StringComparison.OrdinalIgnoreCase))
            {
                rezultat.Add(masina);
            }
        }

        return rezultat;
    }

    ////////////////tablouri
    ///////
    ///////
    //////
    //////
    ///////
    //////
    ///
    public static Masina[] CitesteMasini(string caleFisier)
    {
        Masina[] masini;


        try
        {
            string[] linii = File.ReadAllLines("masini.txt");
            masini = new Masina[linii.Length];

            for (int i = 0; i < linii.Length; i++)
            {
                string[] detalii = linii[i].Split(',');
                string model = detalii[0].Trim();
                int an = int.Parse(detalii[1].Trim());
                double pret = double.Parse(detalii[2].Trim());
                bool inchiriata = bool.Parse(detalii[3].Trim());

                Masina masina = new Masina(model, an, pret, Culoare.Alb, Optiuni.AerConditionat | Optiuni.Navigatie) { Inchiriata = inchiriata };
                masini[i] = masina;
            }
        }
        catch (Exception ex)
        {
            //Console.WriteLine($"A apărut o eroare la citirea fișierului: {ex.Message}");
            masini = new Masina[0];
        }

        return masini;
    }

    public static string[][] ClasificaMasiniDinFisier()
    {
        string[][] tablouMasini = new string[26][];
        for (int i = 0; i < 26; i++)
        {
            tablouMasini[i] = new string[0];
        }

        foreach (Masina masina in masiniDisponibile)
        {
            if (masina != null)
            {
                char primaLitera = char.ToUpper(masina.Model[0]);
                int index = primaLitera - 'A';

                tablouMasini[index] = ArrayAdd(tablouMasini[index], masina.ToString());
            }
        }

        return tablouMasini;
    }

    public static string[] ArrayAdd(string[] array, string value)
    {
        Array.Resize(ref array, array.Length + 1);
        array[array.Length - 1] = value;
        return array;
    }

    public static void AfiseazaTablouMasini()
    {
        string[][] tablouMasini = ClasificaMasiniDinFisier();

        for (int i = 0; i < 26; i++)
        {
            char litera = (char)('A' + i);
            Console.WriteLine($"Masini cu model începând cu '{litera}' sau '{char.ToLower(litera)}':");
            Console.WriteLine(string.Join(", ", tablouMasini[i]));
            Console.WriteLine("-------------------------------");
        }
    }

}


