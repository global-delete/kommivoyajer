using System;

namespace Kommi
{
    class Program
    {
        static void Main(string[] args)
        {
            // Задача коммивояжера (вариант ближайшего соседа)

            var g = new Graph(); // Создаем неориентированный граф

            // Добавление вершин

            g.AddVertex("A");

            g.AddVertex("B");

            g.AddVertex("C");

            g.AddVertex("D");

            g.AddVertex("E");

            // Добавление ребер

            g.AddEdge("A", "B", 8);

            g.AddEdge("B", "C", 4);

            g.AddEdge("C", "D", 3);

            g.AddEdge("D", "E", 9);

            g.AddEdge("E", "A", 2);

            g.AddEdge("A", "C", 6);

            g.AddEdge("A", "D", 9);

            g.AddEdge("B", "D", 5);

            g.AddEdge("B", "E", 15);

            g.AddEdge("C", "E", 4);

            Console.WriteLine("Задача коммивояжера (вариант ближайшего соседа)");

            g.TSP();

            Console.WriteLine();
        }
    }
}
