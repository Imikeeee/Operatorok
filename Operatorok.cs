using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Operatorok
{
    class Program
    {
        static void Main(string[] args)
        {
            var muveletek = new List<Muvelet>();

            //1. feladat
            using (var fajl = new StreamReader("kifejezesek.txt"))
            {
                while (!fajl.EndOfStream)
                {
                    muveletek.Add(new Muvelet(fajl.ReadLine()));
                }
            }

            //2. feladat
            Console.WriteLine($"2. feladat: Kifejezések száma: {muveletek.Count}");

            //3. feladat
            Console.WriteLine($"3. feladat: Kifejezések maradékos osztással: {muveletek.Count(x => x.Operat == "mod")}");

            //4. feladat
            int index = 0;
            while (index < muveletek.Count && (muveletek[index].OperandA % 10 != 0 || muveletek[index].OperandB % 10 != 0))
            {
                index++;
            }
            Console.WriteLine($"4. feladat: {(index < muveletek.Count ? "Van" : "Nincs")} ilyen kifejezés!");

            //5. feladat
            Console.WriteLine("5. feladat: Statisztika");
            muveletek
                .Where(x => x.Operat == "+" || x.Operat == "-" || x.Operat == "*" || x.Operat == "/" || x.Operat == "div" || x.Operat == "mod")
                .GroupBy(x => x.Operat)
                .ToList()
                .ForEach(y => Console.WriteLine($"\t\t{y.Key}\t->\t{y.Count()} db"));

            //7. feladat
            string input;
            do
            {
                if (!string.IsNullOrEmpty(input))
                {
                    try
                    {
                        var muvelet = new Muvelet(input);
                        Console.WriteLine($"\t{muvelet.ToString()}");
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("\tHibásan bevitt adatsor!");
                    }
                }
                Console.Write("7. feladat: Kérek egy kifejezést (pl.: 1 + 1): ");
                input = Console.ReadLine();
            } while (input != "vége");

            //8. feladat
            using (var fajl = new StreamWriter("eredmenyek.txt"))
            {
                muveletek.ForEach(x => fajl.WriteLine(x.ToString()));
            }
            Console.WriteLine("8. feladat: eredmenyek.txt");
            Console.ReadKey();
        }

        class Muvelet
        {
            public int OperandA { get; }
            public int OperandB { get; }
            public string Operat { get; }

            public string Eredmeny
            {
                get
                {
                    try
                    {
                        switch (Operat)
                        {
                            case "mod":
                                return (OperandA % OperandB).ToString();
                            case "/":
                                if (OperandB != 0)
                                    return (OperandA * 1.00 / OperandB).ToString();
                                else
                                    return "Nullával való osztás!";
                            case "div":
                                if (OperandB != 0)
                                    return (OperandA / OperandB).ToString();
                                else
                                    return "Nullával való osztás!";
                            case "-":
                                return (OperandA - OperandB).ToString();
                            case "*":
                                return (OperandA * OperandB).ToString();
                            case "+":
                                return (OperandA + OperandB).ToString();
                            default:
                                return "Hibás operátor!";
                        }
                    }
                    catch (Exception)
                    {
                        return "Egyéb hiba!";
                    }
                }
            }

            public Muvelet(string adatsor)
            {
                var adatok = adatsor.Split(' ');
                OperandA = int.Parse(adatok[0].Trim());
                Operat = adatok[1].Trim();
                OperandB = int.Parse(adatok[2].Trim());
            }

            public override string ToString()
            {
                return $"{OperandA} {Operat} {OperandB} = {Eredmeny}";
            }
        }
    }
}
