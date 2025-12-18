using System;
using System.Collections.Generic;

class Contacto
{
    public string Nombre { get; set; }
    public string Telefono { get; set; }

    public Contacto(string nombre, string telefono)
    {
        Nombre = nombre;
        Telefono = telefono;
    }
}

class Program
{
    static List<Contacto> agenda = new List<Contacto>();

    static void Main(string[] args)
    {
        int opcion = 0;

        while (opcion != 4)
        {
            Console.Clear();
            Console.WriteLine("AGENDA TELEFONICA");
            Console.WriteLine("1. Agregar contacto");
            Console.WriteLine("2. Mostrar contactos");
            Console.WriteLine("3. Buscar por nombre");
            Console.WriteLine("4. Salir");
            Console.Write("Elija una opcion: ");

            int.TryParse(Console.ReadLine(), out opcion);

            switch (opcion)
            {
                case 1:
                    AgregarContacto();
                    break;
                case 2:
                    MostrarContactos();
                    break;
                case 3:
                    BuscarContacto();
                    break;
                case 4:
                    Console.WriteLine("Saliendo...");
                    break;
                default:
                    Console.WriteLine("Opcion no valida. Presione una tecla...");
                    Console.ReadKey();
                    break;
            }
        }
    }

    static void AgregarContacto()
    {
        Console.Clear();
        Console.WriteLine("AGREGAR CONTACTO\n");

        Console.Write("Nombre: ");
        string nombre = Console.ReadLine();

        Console.Write("Telefono: ");
        string telefono = Console.ReadLine();

        agenda.Add(new Contacto(nombre, telefono));

        Console.WriteLine("\nContacto agregado. Presione una tecla para continuar...");
        Console.ReadKey();
    }

    static void MostrarContactos()
    {
        Console.Clear();
        Console.WriteLine("LISTA DE CONTACTOS\n");

        if (agenda.Count == 0)
        {
            Console.WriteLine("No hay contactos registrados.");
        }
        else
        {
            foreach (var c in agenda)
                Console.WriteLine($"Nombre: {c.Nombre} | Tel: {c.Telefono}");
        }

        Console.WriteLine("\nPresione una tecla para continuar...");
        Console.ReadKey();
    }

    static void BuscarContacto()
    {
        Console.Clear();
        Console.WriteLine("BUSCAR CONTACTO\n");
        Console.Write("Nombre a buscar: ");
        string nombreBuscado = Console.ReadLine();

        bool encontrado = false;
        Console.WriteLine();

        foreach (var c in agenda)
        {
            if (c.Nombre.ToLower().Contains(nombreBuscado.ToLower()))
            {
                Console.WriteLine($"Nombre: {c.Nombre} | Tel: {c.Telefono}");
                encontrado = true;
            }
        }

        if (!encontrado)
            Console.WriteLine("No se encontraron contactos con ese nombre.");

        Console.WriteLine("\nPresione una tecla para continuar...");
        Console.ReadKey();
    }
}



