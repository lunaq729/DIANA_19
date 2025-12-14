using System;

// Clase que representa a un estudiante
class Estudiante
{
    // Atributos del estudiante
    public int ID;
    public string Nombres;
    public string Apellidos;
    public string Direccion;

    // Array para almacenar tres números de teléfono
    public string[] Telefonos = new string[3];

    // Método para mostrar la información del estudiante
    public void MostrarDatos()
    {
        Console.WriteLine("\n--- DATOS DEL ESTUDIANTE ---");
        Console.WriteLine("ID: " + ID);
        Console.WriteLine("Nombres: " + Nombres);
        Console.WriteLine("Apellidos: " + Apellidos);
        Console.WriteLine("Dirección: " + Direccion);

        Console.WriteLine("Teléfonos:");
        for (int i = 0; i < Telefonos.Length; i++)
        {
            Console.WriteLine(" Teléfono " + (i + 1) + ": " + Telefonos[i]);
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Crear objeto estudiante
        Estudiante estudiante = new Estudiante();

        // Asignación directa de datos
        estudiante.ID = 1;
        estudiante.Nombres = "Diana";
        estudiante.Apellidos = "Menéndez";
        estudiante.Direccion = "Quito - Ecuador";

        // Asignación de teléfonos usando array
        estudiante.Telefonos[0] = "0991234567";
        estudiante.Telefonos[1] = "0987654321";
        estudiante.Telefonos[2] = "022345678";

        // Mostrar los datos del estudiante
        estudiante.MostrarDatos();

        Console.WriteLine("\nPresione cualquier tecla para salir...");
        Console.ReadKey();
    }
}
