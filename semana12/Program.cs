using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static List<string> participantes = new List<string>();

    static Dictionary<string, List<string>> disciplinas =
        new Dictionary<string, List<string>>();

    static Dictionary<string, List<(string disciplina, string medalla)>> medallero =
        new Dictionary<string, List<(string, string)>>();

    static Random rnd = new Random();

    static void Main()
    {
        CrearParticipantes();
        AsignarDisciplinas();
        AsignarMedallas();

        int opcion;

        do
        {
            Console.Clear();
            Console.WriteLine("===== SISTEMA DE PREMIACION DEPORTIVA =====");
            Console.WriteLine("1. Ver lista de participantes");
            Console.WriteLine("2. Ver participantes por disciplina");
            Console.WriteLine("3. Ver medallero general");
            Console.WriteLine("4. Ver deportistas en varias disciplinas");
            Console.WriteLine("5. Ver deportistas sin participacion");
            Console.WriteLine("6. Ver deportistas con mas medallas");
            Console.WriteLine("0. Salir");
            Console.WriteLine("--------------------------------------------");
            Console.Write("Seleccione una opcion: ");

            opcion = int.Parse(Console.ReadLine());
            Console.WriteLine();

            switch (opcion)
            {
                case 1:
                    VerParticipantes();
                    break;

                case 2:
                    VerPorDisciplina();
                    break;

                case 3:
                    VerMedallero();
                    break;

                case 4:
                    VerVariasDisciplinas();
                    break;

                case 5:
                    VerSinParticipacion();
                    break;

                case 6:
                    VerMasMedallas();
                    break;
            }

            if (opcion != 0)
            {
                Console.WriteLine("\nPresione una tecla para continuar...");
                Console.ReadKey();
            }

        } while (opcion != 0);
    }

    // CREAR PARTICIPANTES
    static void CrearParticipantes()
    {
        string[] nombresBase =
        {
            "Ana","Carlos","Jose","Luis","Maria","Pedro",
            "Lucia","Andres","Sofia","Miguel",
            "Daniela","Jorge","Valentina","David",
            "Camila","Fernando","Elena","Diego",
            "Paula","Ricardo"
        };

        for (int i = 0; i < 100; i++)
        {
            string nombre = nombresBase[rnd.Next(nombresBase.Length)] + "_" + (i + 1);
            participantes.Add(nombre);
            medallero[nombre] = new List<(string, string)>();
        }
    }

    // ASIGNAR DISCIPLINAS
    static void AsignarDisciplinas()
    {
        disciplinas["Natacion"] = participantes.Take(25).ToList();
        disciplinas["Ciclismo"] = participantes.Skip(15).Take(30).ToList();
        disciplinas["Atletismo"] = participantes.Skip(30).Take(20).ToList();
    }

    // ASIGNAR MEDALLAS
    static void AsignarMedallas()
    {
        string[] tipos = { "Oro", "Plata", "Bronce" };

        foreach (var dep in participantes.Take(20))
        {
            medallero[dep].Add(("Natacion", tipos[rnd.Next(3)]));
            medallero[dep].Add(("Ciclismo", tipos[rnd.Next(3)]));
        }

        foreach (var dep in participantes.Skip(20).Take(30))
        {
            medallero[dep].Add(("Atletismo", tipos[rnd.Next(3)]));
        }
    }

    // VER PARTICIPANTES
    static void VerParticipantes()
    {
        Console.WriteLine("==== LISTA DE PARTICIPANTES ====\n");

        foreach (var p in participantes)
        {
            Console.WriteLine(p);
        }

        Console.WriteLine("\nTotal participantes: " + participantes.Count);
    }

    // PARTICIPANTES POR DISCIPLINA
    static void VerPorDisciplina()
    {
        Console.WriteLine("==== PARTICIPANTES POR DISCIPLINA ====\n");

        foreach (var d in disciplinas)
        {
            Console.WriteLine("Disciplina: " + d.Key);
            Console.WriteLine("----------------------------------");

            foreach (var p in d.Value)
            {
                Console.WriteLine(p);
            }

            Console.WriteLine("\nTotal: " + d.Value.Count + "\n");
        }
    }

    // MEDALLERO
    static void VerMedallero()
    {
        Console.WriteLine("==== MEDALLERO GENERAL ====\n");

        foreach (var dep in medallero)
        {
            if (dep.Value.Count > 0)
            {
                Console.Write(dep.Key + " -> ");

                foreach (var m in dep.Value)
                {
                    Console.Write($"{m.disciplina}:{m.medalla} ");
                }

                Console.WriteLine();
            }
        }
    }

    // VARIAS DISCIPLINAS (MODIFICADO)
    static void VerVariasDisciplinas()
    {
        Console.WriteLine("==== DEPORTISTAS EN VARIAS DISCIPLINAS ====\n");

        var lista = participantes
            .Where(p => disciplinas.Count(d => d.Value.Contains(p)) > 1)
            .ToList();

        foreach (var dep in lista)
        {
            var dis = disciplinas
                .Where(d => d.Value.Contains(dep))
                .Select(d => d.Key);

            Console.WriteLine(dep.PadRight(20) + " -> " + string.Join(", ", dis));
        }

        Console.WriteLine("\nTotal: " + lista.Count);
    }

    // SIN PARTICIPACION
    static void VerSinParticipacion()
    {
        Console.WriteLine("==== DEPORTISTAS SIN PARTICIPACION ====\n");

        var sin = participantes
            .Where(p => !disciplinas.Any(d => d.Value.Contains(p)))
            .ToList();

        foreach (var p in sin)
        {
            Console.WriteLine(p);
        }

        Console.WriteLine("\nTotal: " + sin.Count);
    }

    // MAS MEDALLAS
    static void VerMasMedallas()
    {
        Console.WriteLine("==== DEPORTISTAS CON MAS MEDALLAS ====\n");

        var top = medallero
            .OrderByDescending(x => x.Value.Count)
            .Take(10);

        foreach (var dep in top)
        {
            int oro = dep.Value.Count(m => m.medalla == "Oro");
            int plata = dep.Value.Count(m => m.medalla == "Plata");
            int bronce = dep.Value.Count(m => m.medalla == "Bronce");

            Console.WriteLine(dep.Key.PadRight(20) +
                $"Total:{dep.Value.Count}  Oro:{oro}  Plata:{plata}  Bronce:{bronce}");
        }
    }
}




