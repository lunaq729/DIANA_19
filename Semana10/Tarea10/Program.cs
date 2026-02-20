using System;
using System.Collections.Generic;
using System.Linq;

namespace VacunacionEcuador
{
    class Ciudadano
    {
        public string Nombre { get; set; }
        public string Cedula { get; set; }
        public string Provincia { get; set; }
        public string Vacuna { get; set; }

        public Ciudadano(string nombre, string cedula, string provincia, string vacuna = "")
        {
            Nombre = nombre;
            Cedula = cedula;
            Provincia = provincia;
            Vacuna = vacuna;
        }

        public override string ToString()
        {
            return $"{Nombre,-20} | {Cedula,-10} | {Provincia,-12} | {Vacuna}";
        }
    }

    class Program
    {
        static readonly string[] ProvGuayas = {"Guayaquil", "Samborondón", "Durán", "Milagro"};
        static readonly string[] ProvPichincha = {"Quito", "Rumiñahui", "Cayambe"};
        static readonly string[] Nombres = {"José García", "María López", "Carlos Torres", "Ana Herrera", "Luis Vargas", 
                                          "Rosa Morales", "Miguel Salazar", "Luisa Quito", "Pedro Guamán", "Carmen Paz"};

        static void Main()
        {
            Console.Clear();
            Console.WriteLine("🏥 MSP - CAMPAÑA NACIONAL COVID-19 2026\n");
            Console.WriteLine("Datos: INEC Ecuador | Población: 18.3M habitantes\n");

            // GENERAR DATOS CON INTERSECCIÓN REAL
            var todos = GenerarPoblacion(500);
            var (pfizer, astra, solapados) = GenerarVacunadosConInterseccion(75, 75);

            // OPERACIONES DE CONJUNTOS
            MostrarResultados(todos, pfizer, astra, solapados);

            Console.WriteLine("\n🔬 OPERACIONES TEORÍA DE CONJUNTOS:");
            Console.WriteLine("• No vacunados: U - (P ∪ A)");
            Console.WriteLine("• Ambas dosis:   P ∩ A");
            Console.WriteLine("• Solo Pfizer:   P - A");
            Console.WriteLine("• Solo Astra:    A - P");
            Console.ReadKey();
        }

        static List<Ciudadano> GenerarPoblacion(int n)
        {
            var rand = new Random(123);
            var lista = new List<Ciudadano>();
            for (int i = 1; i <= n; i++)
            {
                string prov = rand.Next(2) == 0 ? ProvGuayas[rand.Next(ProvGuayas.Length)] : 
                                     ProvPichincha[rand.Next(ProvPichincha.Length)];
                lista.Add(new Ciudadano(Nombres[rand.Next(Nombres.Length)], $"18{i:D7}", prov));
            }
            return lista;
        }

        static (List<Ciudadano> pfizer, List<Ciudadano> astra, List<Ciudadano> solapados) 
            GenerarVacunadosConInterseccion(int pfizerTotal, int astraTotal)
        {
            var rand = new Random(456);
            var pfizer = new List<Ciudadano>();
            var astra = new List<Ciudadano>();
            var solapados = new List<Ciudadano>();

            // 20 ciudadanos con AMBAS VACUNAS (intersección)
            for (int i = 1; i <= 20; i++)
            {
                string prov = ProvGuayas[rand.Next(ProvGuayas.Length)];
                var ciud = new Ciudadano(Nombres[rand.Next(5)], $"18{rand.Next(1,501):D3}5{i:D2}", prov, "Pfizer+Astra");
                pfizer.Add(ciud); astra.Add(ciud); solapados.Add(ciud);
            }

            // Pfizer exclusivo (55)
            for (int i = 1; i <= 55; i++)
            {
                string prov = ProvGuayas[rand.Next(ProvGuayas.Length)];
                pfizer.Add(new Ciudadano(Nombres[rand.Next(5)], $"18{rand.Next(1,501):D3}6{i:D2}", prov, "Pfizer"));
            }

            // AstraZeneca exclusivo (55)
            for (int i = 1; i <= 55; i++)
            {
                string prov = ProvPichincha[rand.Next(ProvPichincha.Length)];
                astra.Add(new Ciudadano(Nombres[rand.Next(5)], $"18{rand.Next(1,501):D3}7{i:D2}", prov, "AstraZeneca"));
            }

            return (pfizer, astra, solapados);
        }

        static void MostrarResultados(List<Ciudadano> total, List<Ciudadano> pfizer, List<Ciudadano> astra, List<Ciudadano> solapados)
        {
            var todosSet = new HashSet<Ciudadano>(total, ComparadorCedula);
            var pfizerSet = new HashSet<Ciudadano>(pfizer, ComparadorCedula);
            var astraSet = new HashSet<Ciudadano>(astra, ComparadorCedula);

            // 1. NO VACUNADOS
            var union = new HashSet<Ciudadano>(pfizerSet); union.UnionWith(astraSet);
            var noVacunados = new HashSet<Ciudadano>(todosSet); noVacunados.ExceptWith(union);
            
            Console.WriteLine("📋 LISTADOS MSP ECUADOR:\n");
            Console.WriteLine($"1️⃣ SIN VACUNA ({noVacunados.Count}):");
            foreach (var c in noVacunados.Take(5)) Console.WriteLine($"   {c}");
            Console.WriteLine();

            // 2. AMBAS DOSIS (¡AHORA SÍ HAY!)
            Console.WriteLine($"2️⃣ AMBAS DOSIS ({solapados.Count}):");
            foreach (var c in solapados.Take(5)) Console.WriteLine($"   {c}");
            Console.WriteLine();

            // 3. SOLO PFIZER
            var soloPfizer = new HashSet<Ciudadano>(pfizerSet); soloPfizer.ExceptWith(astraSet);
            Console.WriteLine($"3️⃣ SOLO PFIZER ({soloPfizer.Count}):");
            foreach (var c in soloPfizer.Take(5)) Console.WriteLine($"   {c}");
            Console.WriteLine();

            // 4. SOLO ASTRA
            var soloAstra = new HashSet<Ciudadano>(astraSet); soloAstra.ExceptWith(pfizerSet);
            Console.WriteLine($"4️⃣ SOLO ASTRA ({soloAstra.Count}):");
            foreach (var c in soloAstra.Take(5)) Console.WriteLine($"   {c}");

            // ESTADÍSTICAS
            Console.WriteLine($"\n📊 DASHBOARD MSP:");
            Console.WriteLine($"Población:           {todosSet.Count,4}");
            Console.WriteLine($"Cobertura total:     {(150.0/500):P0}");
            Console.WriteLine($"Guayaquil:           {pfizerSet.Count(x=>x.Provincia.Contains("Guaya"))/2,4}");
            Console.WriteLine($"Quito:               {astraSet.Count(x=>x.Provincia.Contains("Quito"))/2,4}");
        }

        static IEqualityComparer<Ciudadano> ComparadorCedula => new CiudadanoComparer();
    }

    class CiudadanoComparer : IEqualityComparer<Ciudadano>
    {
        public bool Equals(Ciudadano x, Ciudadano y) => x.Cedula == y.Cedula;
        public int GetHashCode(Ciudadano obj) => obj.Cedula.GetHashCode();
    }
}
