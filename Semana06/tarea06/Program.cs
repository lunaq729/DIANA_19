using System;

public class Nodo
{
    public object Dato { get; set; }  // object para int y string
    public Nodo Siguiente { get; set; }
    public Nodo(object dato)
    {
        Dato = dato;
        Siguiente = null;
    }
}

public class ListaEnlazada
{
    public Nodo Cabeza { get; set; }

    public ListaEnlazada()
    {
        Cabeza = null;
    }

    public void AgregarFinal(object dato)
    {
        Nodo nuevo = new Nodo(dato);
        if (Cabeza == null)
        {
            Cabeza = nuevo;
            return;
        }
        Nodo actual = Cabeza;
        while (actual.Siguiente != null)
            actual = actual.Siguiente;
        actual.Siguiente = nuevo;
    }

    public void EliminarFueraRango(int min, int max)
    {
        if (Cabeza == null) return;

        // Eliminar desde cabeza si está fuera de rango
        while (Cabeza != null && (int)Cabeza.Dato < min || (int)Cabeza.Dato > max)
            Cabeza = Cabeza.Siguiente;

        if (Cabeza == null) return;

        Nodo actual = Cabeza;
        while (actual.Siguiente != null)
        {
            if ((int)actual.Siguiente.Dato < min || (int)actual.Siguiente.Dato > max)
                actual.Siguiente = actual.Siguiente.Siguiente;
            else
                actual = actual.Siguiente;
        }
    }

    public void InsertarOrdenado(string placa)
    {
        Nodo nuevo = new Nodo(placa);
        if (Cabeza == null || string.Compare(placa, (string)Cabeza.Dato) < 0)
        {
            nuevo.Siguiente = Cabeza;
            Cabeza = nuevo;
            return;
        }
        Nodo actual = Cabeza;
        while (actual.Siguiente != null && string.Compare((string)actual.Siguiente.Dato, placa) < 0)
            actual = actual.Siguiente;
        nuevo.Siguiente = actual.Siguiente;
        actual.Siguiente = nuevo;
    }

    public void Mostrar()
    {
        Nodo actual = Cabeza;
        int i = 1;
        while (actual != null)
        {
            Console.WriteLine($"{i}. {actual.Dato}");
            actual = actual.Siguiente;
            i++;
        }
        if (Cabeza == null) Console.WriteLine("Lista vacía");
    }

    public int Contar()
    {
        int conteo = 0;
        Nodo actual = Cabeza;
        while (actual != null)
        {
            conteo++;
            actual = actual.Siguiente;
        }
        return conteo;
    }
}

class Program
{
    static Random rnd = new Random();

    static void Main()
    {
        // EJERCICIO 1: Lista con 50 números aleatorios 1-999, eliminar fuera de rango
        Console.WriteLine("=== EJERCICIO 1 ===");
        Console.WriteLine("Crea una lista enlazada con 50 números enteros del 1 al 999 generados aleatoriamente.");
        ListaEnlazada listaNumeros = new ListaEnlazada();
        for (int i = 0; i < 50; i++)
        {
            int num = rnd.Next(1, 1000);
            listaNumeros.AgregarFinal(num);
        }
        Console.WriteLine("Lista original generada (primeros 10):");
        Nodo temp = listaNumeros.Cabeza;
        for (int i = 0; i < 10 && temp != null; i++)
        {
            Console.Write(temp.Dato + " ");
            temp = temp.Siguiente;
        }
        Console.WriteLine("... (50 total)");

        Console.WriteLine("\nUna vez creada la lista, se debe eliminar los nodos que estén fuera de un rango de valores leído desde el teclado.");
        Console.Write("Ingrese el rango MINIMO: ");
        int min = int.Parse(Console.ReadLine());
        Console.Write("Ingrese el rango MAXIMO: ");
        int max = int.Parse(Console.ReadLine());
        listaNumeros.EliminarFueraRango(min, max);

        Console.WriteLine($"\nLista después de eliminar fuera de [{min}-{max}] ({listaNumeros.Contar()} elementos restantes):");
        listaNumeros.Mostrar();
        Console.WriteLine();

        // EJERCICIO 2: Registro de vehículos por placas
        Console.WriteLine("=== EJERCICIO 2 ===");
        Console.WriteLine("Crearon un programa que permita llevar el registro de los vehículos del estacionamiento del área de Ingeniería de Sistemas de la universidad.");
        Console.WriteLine("Utilizando como estructura de almacenamiento listas enlazadas. Los dos Datos solicitados por cada vehículo son: placas.");
        ListaEnlazada estacionamiento = new ListaEnlazada();

        Console.WriteLine("\nIngrese placas de vehículos (ingrese 'FIN' para terminar):");
        while (true)
        {
            Console.Write("Placa: ");
            string placa = Console.ReadLine().Trim().ToUpper();
            if (placa == "FIN") break;
            if (!string.IsNullOrEmpty(placa))
            {
                estacionamiento.InsertarOrdenado(placa);
                Console.WriteLine($"  -> Agregado: {placa}");
            }
        }

        Console.WriteLine($"\nRegistro de vehículos ordenado alfabéticamente ({estacionamiento.Contar()} total):");
        estacionamiento.Mostrar();

        Console.WriteLine("\nPresiona cualquier tecla para salir...");
        Console.ReadKey();
    }
}

