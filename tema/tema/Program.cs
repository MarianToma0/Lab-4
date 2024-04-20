using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    public static List<Masina> masiniDisponibile = new List<Masina>();
    Masina masinaNoua = new Masina();
    static void Main(string[] args)
    {
        
        Masina[] masini = ManagerMasini.CitesteMasini("masini.txt");
        string[][] tablouMasini = ManagerMasini.ClasificaMasiniDinFisier();
        while (true)
        {
            
            Console.WriteLine("1. Adaugare masina");
            Console.WriteLine("2. Eliminare masina");
            Console.WriteLine("3. Afisare masini");
            Console.WriteLine("4. Inchiriere masina");
            Console.WriteLine("5. Returnare masina");
            Console.WriteLine("6. Cautare masina dupa nume");
            Console.WriteLine("9. Iesire");
            Console.WriteLine("\n");
            Console.Write("Introduceti optiunea: ");
            int optiune = int.Parse(Console.ReadLine());

            switch (optiune)
            {
                case 1:
                    ManagerMasini.AdaugaMasina();
                    ManagerMasini.CitesteMasini("masini.txt");
                    ManagerMasini.CitesteMasiniDinFisier();


                    break;
                case 2:
                    ManagerMasini.EliminaMasina();
                    break;
                case 3:
                    ManagerMasini.AfiseazaMasini();
                    ManagerMasini.AfiseazaTablouMasini();
                    break;
                case 4:
                    ManagerMasini.InchiriazaMasina();
                    break;
                case 5:
                    ManagerMasini.ReturneazaMasina();
                    break;
                case 6:
                    Console.Write("Introduceti numele masinii pentru cautare: ");
                    string numeCautat = Console.ReadLine();
                    List<Masina> masiniGasite = ManagerMasini.CautaMasinaDupaNume(numeCautat);
                    if (masiniGasite.Count > 0)
                    {
                        Console.WriteLine("Masini gasite:");
                        foreach (Masina masina in masiniGasite)
                        {
                            Console.WriteLine(masina);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Nu s-au gasit masini cu acest nume.");
                    }
                    break;

                case 7:
                    
                    break;

                case 8:

                    break;
                case 9:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Optiune invalida. Reincercati.");
                    break;
            }
        }
    }
    
}
