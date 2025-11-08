using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms
{
    public static class AlgorithmSuite
    {
        // ===========================
        // ?? 1. 정렬 알고리즘 모음
        // ===========================

        public static void BubbleSort(int[] arr)
        {
            for (int i = 0; i < arr.Length - 1; i++)
            {
                for (int j = 0; j < arr.Length - i - 1; j++)
                {
                    if (arr[j] > arr[j + 1])
                        (arr[j], arr[j + 1]) = (arr[j + 1], arr[j]);
                }
            }
        }

        public static void SelectionSort(int[] arr)
        {
            for (int i = 0; i < arr.Length - 1; i++)
            {
                int minIdx = i;
                for (int j = i + 1; j < arr.Length; j++)
                {
                    if (arr[j] < arr[minIdx])
                        minIdx = j;
                }
                (arr[i], arr[minIdx]) = (arr[minIdx], arr[i]);
            }
        }

        public static void InsertionSort(int[] arr)
        {
            for (int i = 1; i < arr.Length; i++)
            {
                int key = arr[i];
                int j = i - 1;
                while (j >= 0 && arr[j] > key)
                {
                    arr[j + 1] = arr[j];
                    j--;
                }
                arr[j + 1] = key;
            }
        }

        public static void MergeSort(int[] arr)
        {
            if (arr.Length <= 1) return;
            int[] sorted = MergeSortRecursive(arr);
            Array.Copy(sorted, arr, arr.Length);
        }

        private static int[] MergeSortRecursive(int[] arr)
        {
            if (arr.Length <= 1) return arr;
            int mid = arr.Length / 2;
            int[] left = arr.Take(mid).ToArray();
            int[] right = arr.Skip(mid).ToArray();
            return Merge(MergeSortRecursive(left), MergeSortRecursive(right));
        }

        private static int[] Merge(int[] left, int[] right)
        {
            List<int> result = new();
            int i = 0, j = 0;
            while (i < left.Length && j < right.Length)
            {
                if (left[i] <= right[j]) result.Add(left[i++]);
                else result.Add(right[j++]);
            }
            result.AddRange(left.Skip(i));
            result.AddRange(right.Skip(j));
            return result.ToArray();
        }

        public static void QuickSort(int[] arr)
        {
            QuickSortRecursive(arr, 0, arr.Length - 1);
        }

        private static void QuickSortRecursive(int[] arr, int left, int right)
        {
            if (left >= right) return;
            int pivot = arr[(left + right) / 2];
            int index = Partition(arr, left, right, pivot);
            QuickSortRecursive(arr, left, index - 1);
            QuickSortRecursive(arr, index, right);
        }

        private static int Partition(int[] arr, int left, int right, int pivot)
        {
            while (left <= right)
            {
                while (arr[left] < pivot) left++;
                while (arr[right] > pivot) right--;
                if (left <= right)
                {
                    (arr[left], arr[right]) = (arr[right], arr[left]);
                    left++;
                    right--;
                }
            }
            return left;
        }

        // ===========================
        // ?? 2. 그래프 탐색 알고리즘
        // ===========================

        // DFS (깊이 우선 탐색)
        public static void DFS(Dictionary<int, List<int>> graph, int start)
        {
            HashSet<int> visited = new();
            DFSRecursive(graph, start, visited);
            Console.WriteLine();
        }

        private static void DFSRecursive(Dictionary<int, List<int>> graph, int node, HashSet<int> visited)
        {
            if (visited.Contains(node)) return;

            Console.Write(node + " ");
            visited.Add(node);

            if (graph.ContainsKey(node))
            {
                foreach (int neighbor in graph[node])
                {
                    DFSRecursive(graph, neighbor, visited);
                }
            }
        }

        // BFS (너비 우선 탐색)
        public static void BFS(Dictionary<int, List<int>> graph, int start)
        {
            HashSet<int> visited = new();
            Queue<int> queue = new();

            queue.Enqueue(start);
            visited.Add(start);

            while (queue.Count > 0)
            {
                int node = queue.Dequeue();
                Console.Write(node + " ");

                if (graph.ContainsKey(node))
                {
                    foreach (int neighbor in graph[node])
                    {
                        if (!visited.Contains(neighbor))
                        {
                            visited.Add(neighbor);
                            queue.Enqueue(neighbor);
                        }
                    }
                }
            }
            Console.WriteLine();
        }
    }
}
