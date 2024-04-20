using System;

//constrcutor masina
[Flags]
public enum Optiuni
{
    AerConditionat = 1,
    Navigatie = 2,
    CutieAutomata = 4,
    CruiseControl = 8
    // Poți adăuga și alte opțiuni aici
}

public enum Culoare
{
    Rosu,
    Alb,
    Negru
}
public class Masina
{
    public string Model { get; set; }
    public int An { get; set; }
    public double Pret { get; set; }
    public bool Inchiriata { get; set; }
    public Culoare Culoare { get; set; }
    public Optiuni Optiuni { get; set; }

    public Masina()
    {

    }
    public Masina(string model, int an, double pret, Culoare culoare, Optiuni optiuni)
    {
        Model = model;
        An = an;
        Pret = pret;
        Culoare = culoare;
        Optiuni = optiuni;
    }

    public override string ToString()
    {
        return $"{Model} - An: {An}, Pret: {Pret}, Culoare: {Culoare}, Optiuni: {Optiuni}";
    }
}
