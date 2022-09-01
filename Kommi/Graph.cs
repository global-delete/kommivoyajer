using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Kommi
{
    struct pathDescription
    {
        public List<GraphVertex> sequence;
        public int length;
        public pathDescription(int length = 0)
        {
            this.length = length;
            this.sequence = new List<GraphVertex>();
        }
    }

    class Graph
    {
        public List<GraphVertex> vertices { get; set; }
        public List<GraphEdge> edges { get; set; }
        public int getVerticesLength()
        {
            return vertices.Count;
        }
        public int getEdgesLength()
        {
            return edges.Count;
        }
        public Graph()
        {
            vertices = new List<GraphVertex>();
            edges = new List<GraphEdge>();
        }
        public GraphVertex AddVertex(string vertexName) //добаление вершины
        {
            GraphVertex vertex = new GraphVertex(vertexName);
            vertices.Add(vertex);
            return vertex;
        }
        public bool RemoveVertex(string vertexName) //удаление вершины
        {
            bool success = false;
            GraphVertex vertex = FindVertex(vertexName);
            if (vertex != null)
            {
                for (int i = vertex.edges.Count - 1; i >= 0; i--)
                {
                    if (RemoveEdgeHelper(vertex, vertex.edges[i].EndVertex))
                        success = true;
                }
                vertices.Remove(vertex);

                return success;

            }
            return false;

        }

        public GraphVertex FindVertex(string vertexName) // поиск вершины
        {
            foreach (GraphVertex v in vertices)
            {
                if (v.name.Equals(vertexName))
                {
                    return v;
                }

            }
            return null;
        }
        public void AddEdge(string firstName, string secondName, int weight) //добавление ребра
        {
            GraphVertex v1 = FindVertex(firstName);
            GraphVertex v2 = FindVertex(secondName);
            if (v2 != null && v1 != null)
            {
                v1.AddEdge(v1, v2, weight);
                v2.AddEdge(v2, v1, weight);
                v1.neighbours.Add(v2);
                v2.neighbours.Add(v1);
                edges.Add(FindEdge(v1, v2));
            }
        }
        public bool RemoveEdge(string firstName, string secondName) //удаление ребра
        {
            GraphVertex v1 = FindVertex(firstName);
            GraphVertex v2 = FindVertex(secondName);
            if (v2 != null && v1 != null)
            {
                if (RemoveEdgeHelper(v2, v1) && RemoveEdgeHelper(v1, v2))
                    return true;
                else
                    return false;
            }
            return false;
        }
        //Вспомогательный метод удаления ребра, удаляет ребро сразу у обоих вершин. Возвращает true если удаление успешно, false если есть ошибки

        public bool RemoveEdgeHelper(GraphVertex vertex, GraphVertex vertex2)
        {
            GraphEdge edge = FindEdge(vertex, vertex2);

            if (edge != null)
            {
                vertex.edges.Remove(edge);
                vertex.neighbours.Remove(vertex2);
                return true;
            }
            else
                return false;
        }

        public GraphEdge FindEdge(GraphVertex vertex, GraphVertex vertex2) //поиск ребра
        {
            foreach (GraphEdge e in vertex.edges)
            {
                if (e.EndVertex.Equals(vertex2))
                {
                    return e;
                }
            }
            return null;
        }

        public GraphVertex BFS(string start, string goal)
        {
            GraphVertex v1 = FindVertex(start);
            GraphVertex v2 = FindVertex(goal);

            if (v2 != null && v1 != null)
            {
                Queue<GraphVertex> Q = new Queue<GraphVertex>();
                HashSet<GraphVertex> S = new HashSet<GraphVertex>();
                Q.Enqueue(v1);
                S.Add(v1);
                while (Q.Count > 0)
                {
                    GraphVertex e = Q.Dequeue();
                    if (e == v2)
                        return e;
                    foreach (GraphVertex v in e.neighbours)
                    {
                        if (!S.Contains(v))
                        {
                            Q.Enqueue(v);
                            S.Add(v);
                        }
                    }
                }
            }
            return null;
        }
        public LinkedList<GraphVertex> DFS(GraphVertex start, GraphVertex goal)  //Поиск в глубину


        {

            HashSet<GraphVertex> visited = new HashSet<GraphVertex>();

            LinkedList<GraphVertex> path = new LinkedList<GraphVertex>();

            DFS(start, goal, visited, path);

            if (path.Count > 0)

            {

                path.AddFirst(start);

            }

            return path;

        }

        //Вспомогательный рекурсивный метод поиска в глубину

        private bool DFS(GraphVertex node, GraphVertex goal, HashSet<GraphVertex> visited, LinkedList<GraphVertex> path)

        {

            if (node == goal)

            {

                return true;

            }

            visited.Add(node);

            foreach (var child in node.neighbours.Where(x => !visited.Contains(x)))

            {

                if (DFS(child, goal, visited, path))

                {

                    path.AddFirst(child);

                    return true;

                }

            }

            return false;

        }

        //Поиск самых отдаленных друг от друга вершин графа

        public int SearchLongest()

        {

            int longest = 0;

            for (int i = 0; i < vertices.Count - 1; i++)

            {

                var primaryVertex = vertices[i];

                for (int j = 0; j < vertices.Count - 1; j++)

                {

                    var secondaryVertex = vertices[j];

                    if (primaryVertex == secondaryVertex)

                        continue;

                    var path = DFS(primaryVertex, secondaryVertex);

                    int length = 0;

                    GraphVertex prev = primaryVertex;

                    foreach (var e in path)

                    {

                        var edge = FindEdge(prev, e);

                        if (edge != null)

                        {

                            if (length < edge.EdgeWeight)

                                length += edge.EdgeWeight;

                        }

                        prev = e;

                    }

                    if (length > longest)

                        longest = length;

                }

            }

            return longest;

        }

        public void TSP() //Travelling salesman problem

        {

            //Выбор случайного ребра для дальнейшего сравнения

            GraphEdge min = this.FindEdge(vertices[0], vertices[1]);

            //Поиск самого короткого ребра

            foreach (GraphEdge edge in this.edges)

            {

                if (edge.EdgeWeight < min.EdgeWeight)

                    min = edge;

            }

            //Объявление итогового значения

            pathDescription path = new pathDescription();

            path.sequence = new List<GraphVertex>() { min.BeginVertex, min.EndVertex, min.BeginVertex };

            for (int i = 0; i < vertices.Count - 1; i++)

            {

                //Черновик для поиска пути

                pathDescription minPath = new pathDescription(Int32.MaxValue);

                //Пробегаемся по соседям

                foreach (GraphVertex neighbour in path.sequence.Last<GraphVertex>().neighbours)

                {

                    //Копируем текущую последовательность в отдельную переменную

                    List<GraphVertex> sequenceProbe = new List<GraphVertex>(path.sequence);

                    //Если последовательность содержит такую вершину, то пропускаем итерацию

                    if (sequenceProbe.Contains(neighbour))

                        continue;

                    //Поиск оптимального пути

                    for (int j = 1; j < sequenceProbe.Count - 1; j++)

                    {

                        //Если первая итерация тычемся только в 3ю позицию, иначе куда угодно

                        if (i != 0)

                            sequenceProbe.Insert(j, neighbour);

                        else

                            sequenceProbe.Insert(sequenceProbe.Count - 1, neighbour);

                        //Считаем длину пути

                        int pathLength = 0;

                        for (int k = 0; k < sequenceProbe.Count - 1; k++)

                        {

                            pathLength += this.FindEdge(sequenceProbe[k], sequenceProbe[k + 1]).EdgeWeight;

                        }

                        //Обновляем данные minPath

                        if (pathLength < minPath.length)

                        {

                            minPath.length = pathLength;

                            minPath.sequence = new List<GraphVertex>(sequenceProbe);

                        }

                        //Удаляем пробуемый элемент из тестовой последовательности

                        sequenceProbe.RemoveAt(j);

                    }

                }

                //Обновление итогового значения

                if (minPath.sequence.Count > 0)

                {

                    path.sequence = new List<GraphVertex>(minPath.sequence);

                    path.length = minPath.length;

                }

            }

            //Вывод на экран

            Console.WriteLine(string.Join(" -> ", path.sequence.Select(x => x.name)));

            Console.WriteLine("" + path.length);

        }

         void ViewGraph()
        {

            foreach (var vertex in vertices)

            {

                Console.Write("\nВершина: {0}", vertex.name + " Ребра:");

                foreach (var e in vertex.edges)

                    Console.Write(" " + e.EdgeWeight);

            }

        }

    }
    }
