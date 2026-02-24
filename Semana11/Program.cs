#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    private static readonly Dictionary<string, string> diccionario = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
    {
        {"tiempo", "Time"},
        {"persona", "Person"},
        {"año", "Year"},
        {"camino", "Way"},
        {"día", "Day"},
        {"cosa", "Thing"},
        {"hombre", "Man"},
        {"mundo", "World"},
        {"vida", "Life"},
        {"mano", "Hand"},
        {"parte", "Part"},
        {"niño", "Child"},
        {"ojo", "Eye"},
        {"mujer", "Woman"},
        {"lugar", "Place"},
        {"trabajo", "Work"},
        {"semana", "Week"},
        {"caso", "Case"},
        {"punto", "Point"},
        {"gobierno", "Government"},
        {"compañía", "Company"}
    };

    static void Main()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("==================== MENÚ ====================");
            Console.WriteLine("1. Traducir una frase");
            Console.WriteLine("2. Agregar palabras al diccionario");
            Console.WriteLine("3. Ver todas las palabras");
            Console.WriteLine("0. Salir");
            Console.WriteLine("");
            Console.WriteLine("Ejemplo: En el mundo cada persona necesita trabajar cada día");
            Console.WriteLine("Traducción esperada: En el World cada Person necesita Work cada Day");
            Console.WriteLine("==============================================");
            Console.Write("Seleccione una opción: ");
            string opcion = Console.ReadLine() ?? "";

            if (opcion == "1") TraducirFrase();
            else if (opcion == "2") AgregarPalabra();
            else if (opcion == "3") MostrarDiccionario();
            else if (opcion == "0")
            {
                Console.WriteLine("Saliendo del programa...");
                break;
            }
            else
            {
                Console.WriteLine("Opción inválida. Presione Enter...");
                Console.ReadLine();
            }
        }
    }

    static void TraducirFrase()
    {
        Console.Clear();
        Console.WriteLine("--- TRADUCTOR ---");
        string fraseEjemplo = "En el mundo cada persona necesita trabajar cada día";
        Console.WriteLine($"\nFrase de ejemplo ya cargada:");
        Console.WriteLine($"\"{fraseEjemplo}\"");
        Console.WriteLine("\n¿Desea traducir esta frase? (ENTER para SÍ / cualquier tecla para escribir otra)");
        
        Console.ReadKey();
        string frase = fraseEjemplo; // Usa el ejemplo por defecto
        
        string resultado = Traducir(frase);
        Console.WriteLine("\nRESULTADO");
        Console.WriteLine($"Frase original: {frase}");
        Console.WriteLine($"Traducción:     {resultado}");
        Console.WriteLine("\nPresione Enter para volver al menú...");
        Console.ReadLine();
    }

    static void MostrarDiccionario()
    {
        Console.Clear();
        Console.WriteLine("--- DICCIONARIO COMPLETO ---");
        Console.WriteLine($"Total de palabras: {diccionario.Count}");
        Console.WriteLine(new string('=', 50));
        Console.WriteLine("ESPAÑOL".PadRight(20) + " | INGLÉS");
        Console.WriteLine(new string('=', 50));

        foreach (var kvp in diccionario.OrderBy(x => x.Key))
        {
            Console.WriteLine($"{kvp.Key.PadRight(20)} | {kvp.Value}");
        }
        
        Console.WriteLine(new string('=', 50));
        Console.WriteLine("\nPresione Enter para volver al menú...");
        Console.ReadLine();
    }

    static void AgregarPalabra()
    {
        Console.Clear();
        Console.WriteLine("--- AGREGAR PALABRA ---");
        Console.Write("Palabra en español: ");
        string espanol = Console.ReadLine() ?? "";
        Console.Write("Palabra en inglés: ");
        string ingles = Console.ReadLine() ?? "";

        if (!string.IsNullOrEmpty(espanol) && !string.IsNullOrEmpty(ingles) && !diccionario.ContainsKey(espanol))
        {
            diccionario[espanol] = ingles;
            Console.WriteLine($"¡Palabra guardada! ({espanol} -> {ingles})");
        }
        else
        {
            Console.WriteLine("Error: Palabra ya existe o campos vacíos.");
        }
        Console.WriteLine("\nPresione Enter para volver al menú...");
        Console.ReadLine();
    }

    static string Traducir(string frase)
    {
        if (string.IsNullOrEmpty(frase)) return "";
        string[] palabras = frase.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        List<string> resultado = new List<string>();
        
        foreach (string palabraOriginal in palabras)
        {
            string limpia = LimpiarPuntuacion(palabraOriginal);
            if (diccionario.ContainsKey(limpia))
            {
                resultado.Add(diccionario[limpia] + ExtraerPuntuacion(palabraOriginal));
            }
            else
            {
                resultado.Add(palabraOriginal);
            }
        }
        return string.Join(" ", resultado);
    }

    static string LimpiarPuntuacion(string palabra)
    {
        return new string(palabra.Where(c => char.IsLetter(c)).ToArray()).ToLower();
    }

    static string ExtraerPuntuacion(string palabra)
    {
        return new string(palabra.Where(c => !char.IsLetter(c)).ToArray());
    }
}






