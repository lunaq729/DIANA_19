using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Programa principal que implementa dos ejercicios de Estructura de Datos usando Stack (Pila LIFO).
/// Autor: Diana Maritza Menéndez Vélez
/// Docente: Ing. Santiago Israel Nogales Guerrero
/// Año: 2026
/// </summary>
class Program
{
    /// <summary>
    /// Menú principal interactivo para seleccionar entre los dos ejercicios.
    /// Utiliza switch-case para ejecutar la función correspondiente.
    /// </summary>
    static void Main()
    {
        int opcion;
        do
        {
            Console.Clear();
            Console.WriteLine("=== EJERCICIOS CON STACKS ===");
            Console.WriteLine("1. Verificar paréntesis balanceados");
            Console.WriteLine("2. Torres de Hanoi");
            Console.WriteLine("0. Salir");
            Console.Write("Seleccione: ");

            string? entrada = Console.ReadLine();
            if (!int.TryParse(entrada, out opcion)) opcion = -1;

            switch (opcion)
            {
                case 1: VerificarParentesis(); break;
                case 2: EjecutarHanoi(); break;
                case 0: Console.WriteLine("Saliendo..."); break;
                default:
                    Console.WriteLine("Opción inválida. Presione una tecla...");
                    Console.ReadKey();
                    break;
            }
        } while (opcion != 0);
    }

    /// <summary>
    /// Verifica si los paréntesis (), llaves {} y corchetes [] están balanceados en una expresión.
    /// Algoritmo Stack LIFO: Aperturas → Push, Cierres → Pop+Validación.
    /// Ignora números, operadores y espacios válidamente.
    /// </summary>
    static void VerificarParentesis()
    {
        Console.Clear();
        // Enunciado completo del ejercicio
        Console.WriteLine("Verificación de paréntesis balanceados en una expresión matemática");
        Console.WriteLine("Implemente un programa que determine si los paréntesis, llaves y corchetes");
        Console.WriteLine("en una expresión matemática están correctamente balanceados.");
        Console.WriteLine("Ejemplo:");
        Console.WriteLine("Entrada: {7 + (8 * 5) - [(9 - 7) + (4 + 1)]}");
        Console.WriteLine("Salida esperada: Fórmula balanceada.\n");
        
        Console.WriteLine("Docente : Ing. Santiago Israel Nogales Guerrero");
        Console.WriteLine("Año     : 2026");
        Console.WriteLine("==========================================================\n");

        Console.Write("Ingrese una expresión: ");
        string? expresion = Console.ReadLine() ?? "";

        /// <summary>
        /// Stack<char> almacena símbolos de apertura: '(', '{', '['
        /// LIFO garantiza que el último abierto sea el primero cerrado
        /// </summary>
        Stack<char> pila = new Stack<char>();
        bool balanceado = true;

        /// <summary>
        /// Recorre cada carácter de la expresión:
        /// 1. Apertura → pila.Push(c)
        /// 2. Cierre → pila.Pop() + validación de pareja
        /// 3. Otros → ignorados
        /// </summary>
        foreach (char c in expresion)
        {
            if (c == '(' || c == '{' || c == '[') 
                pila.Push(c);  // **LÍNEA CLAVE: Empuja aperturas**
            
            else if (c == ')' || c == '}' || c == ']')
            {
                if (pila.Count == 0) 
                { 
                    balanceado = false; 
                    break; 
                }
                
                /// <summary>
                /// **LÍNEA CRÍTICA:** Valida pareja correcta
                /// Pop extrae último abierto, compara con cierre actual
                /// </summary>
                char tope = pila.Pop();
                if (!((tope == '(' && c == ')') || 
                      (tope == '{' && c == '}') || 
                      (tope == '[' && c == ']')))
                {
                    balanceado = false;
                    break;
                }
            }
        }
        
        /// <summary>
        /// **VALIDACIÓN FINAL:** Pila vacía = balanceado perfecto
        /// Si quedan aperturas sin cerrar → desbalanceado
        /// </summary>
        if (pila.Count != 0) balanceado = false;

        Console.WriteLine(balanceado ? "\n✓ Fórmula balanceada" : "\n✗ Fórmula no balanceada");
        Console.WriteLine("\nPresione una tecla para volver al menú...");
        Console.ReadKey();
    }

    /// <summary>
    /// Ejecuta Torres de Hanoi recursivo con 3 stacks representando las torres.
    /// Inicializa discos del 1(al más pequeño) al n(más grande) en torre Origen.
    /// </summary>
    static void EjecutarHanoi()
    {
        Console.Clear();
        Console.WriteLine("Resolución del problema de las Torres de Hanoi");
        Console.WriteLine("Desarrolle un algoritmo que resuelva el problema de las Torres de Hanoi");
        Console.WriteLine("utilizando pilas. El programa debe mostrar paso a paso cómo se mueven");
        Console.WriteLine("los discos entre las torres.\n");

        Console.Write("Número de discos: ");
        if (!int.TryParse(Console.ReadLine(), out int n)) return;

        /// <summary>
        /// **INICIALIZACIÓN DE TORRES:** 3 Stacks LIFO
        /// origen: [n, n-1, ..., 1] (mayor abajo)
        /// destino/auxiliar: vacíos inicialmente
        /// </summary>
        Stack<int> origen = new Stack<int>(Enumerable.Range(1, n).Reverse());
        Stack<int> destino = new Stack<int>();
        Stack<int> auxiliar = new Stack<int>();

        int movimientos = 0;
        ResolverHanoi(n, origen, destino, auxiliar, "Origen", "Destino", "Auxiliar", ref movimientos);

        Console.WriteLine("-------------------------------------------");
        Console.WriteLine($"Total de movimientos realizados: {movimientos} (2^n - 1)");
        Console.WriteLine("\nPresione una tecla para volver al menú...");
        Console.ReadKey();
    }

    /// <summary>
    /// Algoritmo recursivo clásico de Torres de Hanoi.
    /// Precondición: n > 0, origen tiene n discos ordenados.
    /// 1. Mover n-1 a auxiliar
    /// 2. Mover disco n a destino  
    /// 3. Mover n-1 de auxiliar a destino
    /// </summary>
    /// <param name="n">Número de discos a mover</param>
    /// <param name="o">Stack origen</param>
    /// <param name="d">Stack destino</param>
    /// <param name="a">Stack auxiliar</param>
    /// <param name="sO">Nombre torre origen</param>
    /// <param name="sD">Nombre torre destino</param>
    /// <param name="sA">Nombre torre auxiliar</param>
    /// <param name="mov">Contador movimientos (referencia)</param>
    static void ResolverHanoi(int n, Stack<int> o, Stack<int> d, Stack<int> a, 
        string sO, string sD, string sA, ref int mov)
    {
        if (n > 0)
        {
            /// <summary>
            /// PASO 1: Recursivo n-1 → auxiliar (usando destino como temp)
            /// </summary>
            ResolverHanoi(n - 1, o, a, d, sO, sA, sD, ref mov);
            
            /// <summary>
            /// PASO 2: **MOVIMIENTO PRINCIPAL** - Disco más grande
            /// Pop origen → Push destino (regla: mayor NUNCA sobre menor garantizada por recursión)
            /// </summary>
            mov++;
            int disco = o.Pop();
            d.Push(disco);

            Console.WriteLine("-------------------------------------------");
            Console.WriteLine($"Movimiento {mov}: Disco {disco} de {sO} a {sD}");
            
            /// <summary>
            /// **VISUALIZACIÓN:** Estado actual de las 3 torres (Reverse para mostrar menor arriba)
            /// </summary>
            Console.WriteLine($"Origen   : {string.Join(", Disco ", o.Reverse().Select(x => x.ToString()))}");
            Console.WriteLine($"Destino  : {string.Join(", Disco ", d.Reverse().Select(x => x.ToString()))}");
            Console.WriteLine($"Auxiliar : {string.Join(", Disco ", a.Reverse().Select(x => x.ToString()))}");

            /// <summary>
            /// PASO 3: Recursivo n-1 → destino (usando origen como temp)
            /// </summary>
            ResolverHanoi(n - 1, a, d, o, sA, sD, sO, ref mov);
        }
    }
}



