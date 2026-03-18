using System;
using System.Threading;

namespace ArbolBinarioBusqueda
{
    class Nodo
    {
        public int Valor;
        public Nodo Izquierdo;
        public Nodo Derecho;

        public Nodo(int valor)
        {
            Valor = valor;
        }
    }

    class BST
    {
        public Nodo Raiz;

        public Nodo Insertar(Nodo raiz, int valor)
        {
            if (raiz == null) return new Nodo(valor);

            if (valor < raiz.Valor)
                raiz.Izquierdo = Insertar(raiz.Izquierdo, valor);
            else if (valor > raiz.Valor)
                raiz.Derecho = Insertar(raiz.Derecho, valor);

            return raiz;
        }

        public bool Buscar(Nodo raiz, int valor)
        {
            if (raiz == null) return false;
            if (raiz.Valor == valor) return true;

            return valor < raiz.Valor
                ? Buscar(raiz.Izquierdo, valor)
                : Buscar(raiz.Derecho, valor);
        }

        public Nodo Minimo(Nodo raiz)
        {
            while (raiz.Izquierdo != null)
                raiz = raiz.Izquierdo;
            return raiz;
        }

        public Nodo Maximo(Nodo raiz)
        {
            while (raiz.Derecho != null)
                raiz = raiz.Derecho;
            return raiz;
        }

        public Nodo Eliminar(Nodo raiz, int valor)
        {
            if (raiz == null) return raiz;

            if (valor < raiz.Valor)
                raiz.Izquierdo = Eliminar(raiz.Izquierdo, valor);
            else if (valor > raiz.Valor)
                raiz.Derecho = Eliminar(raiz.Derecho, valor);
            else
            {
                if (raiz.Izquierdo == null) return raiz.Derecho;
                if (raiz.Derecho == null) return raiz.Izquierdo;

                Nodo temp = Minimo(raiz.Derecho);
                raiz.Valor = temp.Valor;
                raiz.Derecho = Eliminar(raiz.Derecho, temp.Valor);
            }

            return raiz;
        }

        public void Inorden(Nodo raiz)
        {
            if (raiz != null)
            {
                Inorden(raiz.Izquierdo);
                Console.Write(raiz.Valor + " ");
                Inorden(raiz.Derecho);
            }
        }

        public void Preorden(Nodo raiz)
        {
            if (raiz != null)
            {
                Console.Write(raiz.Valor + " ");
                Preorden(raiz.Izquierdo);
                Preorden(raiz.Derecho);
            }
        }

        public void Postorden(Nodo raiz)
        {
            if (raiz != null)
            {
                Postorden(raiz.Izquierdo);
                Postorden(raiz.Derecho);
                Console.Write(raiz.Valor + " ");
            }
        }

        public int Altura(Nodo raiz)
        {
            if (raiz == null) return 0;
            return 1 + Math.Max(Altura(raiz.Izquierdo), Altura(raiz.Derecho));
        }

        public void Limpiar()
        {
            Raiz = null;
        }
    }

    class Program
    {
        static int LeerEntero(string mensaje)
        {
            int valor;
            while (true)
            {
                Console.Write(mensaje);
                if (int.TryParse(Console.ReadLine(), out valor))
                    return valor;

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("⚠ Entrada inválida. Intente de nuevo.");
                Console.ResetColor();
            }
        }

        static void Cargando()
        {
            Console.Write("Procesando");
            for (int i = 0; i < 3; i++)
            {
                Thread.Sleep(300);
                Console.Write(".");
            }
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            BST arbol = new BST();
            int opcion;

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=======================================");
            Console.WriteLine("   ARBOL BINARIO DE BUSQUEDA (BST)");
            Console.WriteLine("   Estudiante: DIANA MARITZA MENENDEZ VELEZ");
            Console.WriteLine("=======================================");
            Console.ResetColor();

            do
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n--- MENU PRINCIPAL ---");
                Console.ResetColor();

                Console.WriteLine("1. Insertar");
                Console.WriteLine("2. Buscar");
                Console.WriteLine("3. Eliminar");
                Console.WriteLine("4. Recorridos");
                Console.WriteLine("5. Minimo y Maximo");
                Console.WriteLine("6. Altura");
                Console.WriteLine("7. Limpiar");
                Console.WriteLine("0. Salir");

                opcion = LeerEntero("Seleccione una opcion: ");

                switch (opcion)
                {
                    case 1:
                        int v1 = LeerEntero("Ingrese valor: ");
                        Cargando();
                        arbol.Raiz = arbol.Insertar(arbol.Raiz, v1);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("✔ Insertado correctamente");
                        Console.ResetColor();
                        break;

                    case 2:
                        int v2 = LeerEntero("Valor a buscar: ");
                        Cargando();
                        bool encontrado = arbol.Buscar(arbol.Raiz, v2);
                        Console.ForegroundColor = encontrado ? ConsoleColor.Green : ConsoleColor.Red;
                        Console.WriteLine(encontrado ? "✔ Encontrado" : "✘ No encontrado");
                        Console.ResetColor();
                        break;

                    case 3:
                        int v3 = LeerEntero("Valor a eliminar: ");
                        Cargando();
                        arbol.Raiz = arbol.Eliminar(arbol.Raiz, v3);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("✔ Eliminado");
                        Console.ResetColor();
                        break;

                    case 4:
                        Console.WriteLine("\nInorden:");
                        arbol.Inorden(arbol.Raiz);

                        Console.WriteLine("\nPreorden:");
                        arbol.Preorden(arbol.Raiz);

                        Console.WriteLine("\nPostorden:");
                        arbol.Postorden(arbol.Raiz);
                        Console.WriteLine();
                        break;

                    case 5:
                        if (arbol.Raiz != null)
                        {
                            Console.WriteLine("Minimo: " + arbol.Minimo(arbol.Raiz).Valor);
                            Console.WriteLine("Maximo: " + arbol.Maximo(arbol.Raiz).Valor);
                        }
                        else
                        {
                            Console.WriteLine("Arbol vacio");
                        }
                        break;

                    case 6:
                        Console.WriteLine("Altura: " + arbol.Altura(arbol.Raiz));
                        break;

                    case 7:
                        arbol.Limpiar();
                        Console.WriteLine("✔ Arbol eliminado");
                        break;
                }

            } while (opcion != 0);

            Console.WriteLine("\nPrograma finalizado.");
        }
    }
}