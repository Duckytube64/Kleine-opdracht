using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SolverFoundation;
using Microsoft.SolverFoundation.Common;
using Microsoft.SolverFoundation.Services;

class Program
{
    static void Main(string[] args)
    {
        Program p = new Program();
        p.Initialize();
        Console.ReadLine();
    }

    //Voorbeeld LP uit de opgave
    void Initialize()
    {
        //Introduceer de keuzes
        SolverContext context = SolverContext.GetContext();
        Model model = context.CreateModel();
        Decision aardappel = new Decision(Domain.RealNonnegative,
        "ingredient_aardappel");
        Decision vlees = new Decision(Domain.RealNonnegative,
        "ingredient_vlees");
        Decision groente = new Decision(Domain.RealNonnegative,
        "ingredient_groente");
        model.AddDecisions(aardappel, vlees, groente);

        //Constraints toevoegen
        model.AddConstraint("calorie", 800 * aardappel + 1000 * vlees + 5 * groente == 600);        model.AddConstraints("prot_vit", 150 <= 5 * aardappel + 500 * vlees <= 250, 10 * aardappel + 100 * groente >= 200);

        //Introduceer doelstelling
        model.AddGoal("prijs", GoalKind.Minimize, 5 * aardappel + 20 * vlees + 7 * groente);
        //Roep solver aan
        Solution solution = context.Solve(new SimplexDirective());
        Report report = solution.GetReport();
        Console.Write(report);
    }
}
