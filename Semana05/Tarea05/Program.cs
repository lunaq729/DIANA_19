using System;
using System.Collections.Generic;
using System.Linq;

namespace DeberSemana05
{
    class Program
    {
        static void Main(string[] args)
        {
            MenuPrincipal menu = new MenuPrincipal();
            menu.Mostrar();
        }
    }

    public class MenuPrincipal
    {
        public void Mostrar()
        {
            bool continuar = true;
            while (continuar)
            {
                Console.Clear();
                Console.WriteLine("====================================================");
                Console.WriteLine("       DEBER SEMANA 05 - PROGRAMACIÓN EN C#         ");
                Console.WriteLine("====================================================");
                Console.WriteLine("1. Ejercicio 1: Asignaturas de un curso");
                Console.WriteLine("2. Ejercicio 2: Lotería Primitiva");
                Console.WriteLine("3. Ejercicio 3: Números 1-10 Inverso");
                Console.WriteLine("4. Ejercicio 4: Abecedario (Múltiplos de 3)");
                Console.WriteLine("5. Ejercicio 5: Verificador de Palíndromos");
                Console.WriteLine("0. Salir");
                Console.WriteLine("----------------------------------------------------");
                Console.Write("Seleccione una opción: ");

                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1": Ejercicio1(); break;
                    case "2": Ejercicio2(); break;
                    case "3": Ejercicio3(); break;
                    case "4": Ejercicio4(); break;
                    case "5": Ejercicio5(); break;
                    case "0": continuar = false; break;
                    default: Console.WriteLine("\nOpción no válida. Intente de nuevo."); break;
                }

                if (continuar)
                {
                    Console.WriteLine("\n----------------------------------------------------");
                    Console.WriteLine("Presione cualquier tecla para volver al menú...");
                    Console.ReadKey();
                }
            }
        }

        private void MostrarEnunciado(int num, string enunciado)
        {
            Console.Clear();
            Console.WriteLine($"EJERCICIO {num}");
            Console.WriteLine($"Pregunta: {enunciado}");
            Console.WriteLine(new string('-', 50) + "\n");
        }

        private void Ejercicio1()
        {
            MostrarEnunciado(1, "Escribir un programa que almacene las asignaturas de un curso (Matemáticas, Física, Química, Historia y Lengua) en una lista y la muestre por pantalla.");
            
            List<string> asignaturas = new List<string> { "Matematicaa", "Física", "Química", "Estructuras de Datos", "Investigacion" };
            Console.WriteLine("Las asignaturas almacenadas son:");
            foreach (var materia in asignaturas)
            {
                Console.WriteLine($"- {materia}");
            }
        }

        private void Ejercicio2()
        {
            MostrarEnunciado(2, "Escribir un programa que pregunte al usuario los números ganadores de la lotería primitiva, los almacene en una lista y los muestre por pantalla ordenados de menor a mayor.");
            
            List<int> numerosLoteria = new List<int>();
            Console.WriteLine("Ingrese los 6 números ganadores:");
            for (int i = 0; i < 6; i++)
            {
                Console.Write($"[{i + 1}/6] Ingrese número: ");
                if (int.TryParse(Console.ReadLine(), out int num))
                    numerosLoteria.Add(num);
                else
                {
                    Console.WriteLine("Entrada no válida, intente de nuevo.");
                    i--;
                }
            }
            numerosLoteria.Sort();
            Console.WriteLine("\nResultado: Números ordenados de menor a mayor:");
            Console.WriteLine(string.Join(" - ", numerosLoteria));
        }

        private void Ejercicio3()
        {
            MostrarEnunciado(3, "Escribir un programa que almacene en una lista los números del 1 al 10 y los muestre por pantalla en orden inverso separados por comas.");
            
            List<int> numeros = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            numeros.Reverse();
            Console.WriteLine("Resultado (Orden inverso):");
            Console.WriteLine(string.Join(", ", numeros));
        }
    
        private void Ejercicio4()
        {
            MostrarEnunciado(4, "Escribir un programa que almacene el abecedario en una lista, elimine de la lista las letras que ocupen posiciones múltiplos de 3, y muestre por pantalla la lista resultante.");
            
            List<char> abecedario = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToList();
            
            // Eliminamos múltiplos de 3 (posiciones 3, 6, 9...)
            for (int i = abecedario.Count - 1; i >= 0; i--)
            {
                if ((i + 1) % 3 == 0)
                {
                    abecedario.RemoveAt(i);
                }
            }
            Console.WriteLine("Resultado (Letras restantes):");
            Console.WriteLine(string.Join(" ", abecedario));
        }

        private void Ejercicio5()
        {
            MostrarEnunciado(5, "Escribir un programa que pida al usuario una palabra y muestre por pantalla si es un palíndromo.");
            
            Console.Write("Ingrese la palabra a verificar: ");
            string original = Console.ReadLine() ?? "";
            string procesada = original.ToLower().Replace(" ", "");
            
            char[] arr = procesada.ToCharArray();
            Array.Reverse(arr);
            string invertida = new string(arr);

            Console.WriteLine($"\nPalabra original: {original}");
            if (procesada == invertida && procesada != "")
                Console.WriteLine("RESULTADO: ¡SÍ es un palíndromo!");
            else
                Console.WriteLine("RESULTADO: NO es un palíndromo.");
        }
    }
}