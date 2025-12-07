using System;

// Definición del namespace para organizar las clases
namespace FigurasGeometricas
{
    // Clase para representar un Círculo 🔵
    public class Circulo
    {
        // Atributo privado para almacenar el radio, encapsulado por la propiedad 'Radio'.
        private double _radio;

        // Propiedad pública que encapsula el atributo '_radio'. 
        // Permite la lectura (get) y la escritura (set) del radio.
        // Se utiliza el tipo primitivo 'double' para manejar valores decimales.
        public double Radio
        {
            get { return _radio; }
            set 
            {
                // Validación simple: el radio no puede ser negativo.
                if (value > 0)
                {
                    _radio = value;
                }
                else
                {
                    // Si se intenta asignar un valor inválido, se puede manejar con un error 
                    // o, como en este ejemplo simple, se ignora o se asigna un valor por defecto.
                    Console.WriteLine("El radio debe ser un valor positivo.");
                }
            }
        }

        // Constructor de la clase Circulo. 
        // Inicializa el objeto con el valor del radio.
        public Circulo(double radioInicial)
        {
            this.Radio = radioInicial; // Utiliza la propiedad 'Radio' para la asignación inicial
        }

        // Método para calcular el Área del círculo.
        // Fórmula: Área = π * radio²
        public double CalcularArea()
        {
            // Math.PI es una constante de tipo 'double' que representa π.
            // Math.Pow(Radio, 2) calcula el radio elevado al cuadrado.
            return Math.PI * Math.Pow(Radio, 2);
        }

        // Método para calcular el Perímetro (Circunferencia) del círculo.
        // Fórmula: Perímetro = 2 * π * radio
        public double CalcularPerimetro()
        {
            return 2 * Math.PI * Radio;
        }
    }

    // --- Separación de Clases ---

    // Clase para representar un Rectángulo 🟨
    public class Rectangulo
    {
        // Atributos privados para el ancho y el alto.
        private double _ancho;
        private double _alto;

        // Propiedad pública para el ancho, usando el tipo primitivo 'double'.
        public double Ancho
        {
            get { return _ancho; }
            set 
            {
                if (value > 0)
                {
                    _ancho = value;
                }
            }
        }

        // Propiedad pública para el alto, usando el tipo primitivo 'double'.
        public double Alto
        {
            get { return _alto; }
            set 
            {
                if (value > 0)
                {
                    _alto = value;
                }
            }
        }

        // Constructor de la clase Rectangulo.
        // Inicializa el objeto con los valores de ancho y alto.
        public Rectangulo(double anchoInicial, double altoInicial)
        {
            this.Ancho = anchoInicial; // Utiliza la propiedad 'Ancho'
            this.Alto = altoInicial;   // Utiliza la propiedad 'Alto'
        }

        // Método para calcular el Área del rectángulo.
        // Fórmula: Área = ancho * alto
        public double CalcularArea()
        {
            return Ancho * Alto;
        }

        // Método para calcular el Perímetro del rectángulo.
        // Fórmula: Perímetro = 2 * (ancho + alto)
        public double CalcularPerimetro()
        {
            return 2 * (Ancho + Alto);
        }
    }

    // --- Separación de Clases ---

    // Clase Principal para la ejecución (Main) y demostración de las otras clases.
    public class Program
    {
        public static void Main(string[] args)
        {
            // --- Ejemplo de uso de la clase Circulo ---
            Console.WriteLine("--- Cálculo para el Círculo ---");
            // Se crea una nueva instancia de Circulo con un radio inicial de 5.0.
            var miCirculo = new Circulo(5.0); 

            // Se accede a la propiedad Radio para mostrar el valor inicial.
            Console.WriteLine($"Radio del Círculo: {miCirculo.Radio}"); 

            // Se llama al método para calcular el área y se muestra el resultado.
            Console.WriteLine($"Área del Círculo: {miCirculo.CalcularArea():F2}"); // :F2 formatea a 2 decimales

            // Se llama al método para calcular el perímetro y se muestra el resultado.
            Console.WriteLine($"Perímetro del Círculo: {miCirculo.CalcularPerimetro():F2}");

            Console.WriteLine();

            // --- Ejemplo de uso de la clase Rectangulo ---
            Console.WriteLine("--- Cálculo para el Rectángulo ---");
            // Se crea una nueva instancia de Rectangulo con ancho 4.0 y alto 6.5.
            var miRectangulo = new Rectangulo(4.0, 6.5); 

            // Se accede a las propiedades para mostrar las dimensiones.
            Console.WriteLine($"Ancho del Rectángulo: {miRectangulo.Ancho}");
            Console.WriteLine($"Alto del Rectángulo: {miRectangulo.Alto}");

            // Se llama al método para calcular el área y se muestra el resultado.
            Console.WriteLine($"Área del Rectángulo: {miRectangulo.CalcularArea():F2}");

            // Se llama al método para calcular el perímetro y se muestra el resultado.
            Console.WriteLine($"Perímetro del Rectángulo: {miRectangulo.CalcularPerimetro():F2}");
            
            // Ejemplo de cambio de propiedad (encapsulación con set)
            miRectangulo.Ancho = 10.0;
            Console.WriteLine("\n--- Rectángulo después de cambiar el Ancho a 10.0 ---");
            Console.WriteLine($"Nuevo Ancho: {miRectangulo.Ancho}");
            Console.WriteLine($"Nueva Área: {miRectangulo.CalcularArea():F2}");
        }
    }
}