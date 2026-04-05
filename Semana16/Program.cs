using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

namespace ArbolBinarioBusqueda
{
    // Clase Nodo actualizada para evitar advertencias de nulos
    class Nodo
    {
        public int Valor;
        public Nodo? Izquierdo; 
        public Nodo? Derecho;

        public Nodo(int valor)
        {
            Valor = valor;
            Izquierdo = null;
            Derecho = null;
        }
    }

    class BST
    {
        public Nodo? Raiz;

        public void Insertar(int valor)
        {
            Raiz = InsertarRecursivo(Raiz, valor);
        }

        private Nodo InsertarRecursivo(Nodo? raiz, int valor)
        {
            if (raiz == null) return new Nodo(valor);

            if (valor < raiz.Valor)
                raiz.Izquierdo = InsertarRecursivo(raiz.Izquierdo, valor);
            else if (valor > raiz.Valor)
                raiz.Derecho = InsertarRecursivo(raiz.Derecho, valor);

            return raiz;
        }

        // --- GENERACIÓN DE LA VENTANA GRÁFICA ---
        public void DibujarArbol()
        {
            if (Raiz == null)
            {
                Console.WriteLine("⚠ El árbol está vacío, no hay nada que dibujar.");
                return;
            }

            Form formulario = new Form
            {
                Text = "Visualización de Árbol Binario - Diana Menendez",
                Width = 1000,
                Height = 700,
                BackColor = Color.White,
                StartPosition = FormStartPosition.CenterScreen
            };

            formulario.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                DibujarNodo(e.Graphics, Raiz, formulario.Width / 2, 60, formulario.Width / 4);
            };

            formulario.ShowDialog();
        }

        private void DibujarNodo(Graphics g, Nodo? nodo, int x, int y, int dist)
        {
            if (nodo == null) return;

            // Colores y estilo
            Pen lapizArista = new Pen(Color.Gray, 2);
            Pen lapizNodo = new Pen(Color.DarkCyan, 3);
            Font fuente = new Font("Segoe UI", 11, FontStyle.Bold);

            // Dibujar líneas a los hijos
            if (nodo.Izquierdo != null)
            {
                g.DrawLine(lapizArista, x, y, x - dist, y + 70);
                DibujarNodo(g, nodo.Izquierdo, x - dist, y + 70, dist / 2);
            }
            if (nodo.Derecho != null)
            {
                g.DrawLine(lapizArista, x, y, x + dist, y + 70);
                DibujarNodo(g, nodo.Derecho, x + dist, y + 70, dist / 2);
            }

            // Dibujar el círculo del nodo
            Rectangle rect = new Rectangle(x - 22, y - 22, 44, 44);
            g.FillEllipse(Brushes.White, rect);
            g.DrawEllipse(lapizNodo, rect);

            // Dibujar el valor
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            g.DrawString(nodo.Valor.ToString(), fuente, Brushes.Black, x, y, sf);
        }
    }

    class Program
    {
        [STAThread] // Importante para que Windows Forms funcione
        static void Main(string[] args)
        {
            BST arbol = new BST();
            int opcion = -1;

            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("=======================================");
                Console.WriteLine("    ARBOL BINARIO DE BUSQUEDA (BST)");
                Console.WriteLine("    Estudiante: Diana Menendez");
                Console.WriteLine("=======================================");
                Console.ResetColor();

                Console.WriteLine("1. Insertar valor");
                Console.WriteLine("8. VER ÁRBOL EN VENTANA (IMAGEN)");
                Console.WriteLine("0. Salir");
                Console.Write("\nSeleccione una opción: ");

                string? entrada = Console.ReadLine();
                if (int.TryParse(entrada, out opcion))
                {
                    switch (opcion)
                    {
                        case 1:
                            Console.Write("Ingrese el número a insertar: ");
                            if (int.TryParse(Console.ReadLine(), out int valor))
                            {
                                arbol.Insertar(valor);
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("✔ Valor insertado.");
                                Console.ResetColor();
                                Thread.Sleep(500);
                            }
                            break;

                        case 8:
                            Console.WriteLine("\n🚀 Generando visualización gráfica...");
                            arbol.DibujarArbol();
                            break;
                    }
                }
            } while (opcion != 0);

            Console.WriteLine("Programa finalizado.");
        }
    }
}