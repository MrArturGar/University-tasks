using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork3
{
    class Program
    {
        class Sorts
        {
            //счетчик сравнения
            public static int C = 0;
            //счетчик обмена
            public static int M = 0;

            //способ генерации случайного числа с логистическим распределением случайного значения
            private static float getRandom(ref System.Random _randomizer, float _A, float _B)
            {
                int tmp; float result, u;
                tmp = _randomizer.Next(int.MaxValue - 1);
                u = (float)tmp / (int.MaxValue);
                result = _A - _B * ((float)Math.Log(1 - u));
                return (result);
            }

            //метод генерации массива с N случайными числами
            public static float[] genArray(int _N, float _A, float _B)
            {
                System.Random randomizer = new System.Random();
                float[] result = new float[_N];
                for (int i = 0; i < _N; i++)
                {
                    result[i] = getRandom(ref randomizer, _A, _B);
                }
                return result;
            }

            //Вставляющая сортировка
            public static void insertionSort(ref float[] _arr)
            {
                for (int i = 1; i < _arr.Length; i++)
                {
                    float key = _arr[i];
                    int j = i - 1;
                    C++;
                    while (j >= 0 && _arr[j] > key)
                    {
                        //подсчет сравнений элементов
                        M++;
                        _arr[j + 1] = _arr[j];
                        j--;
                        //сравнение элементов, не j >= 0
                        C++;
                    }
                    M++;
                    _arr[j + 1] = key;
                }
            }

            //реализация прямой сортировки
            public static void directSort(ref float[] _arr)
            {
                int min;
                float tmp;
                for (int i = 0; i < _arr.Length - 1; i++)
                {
                    min = i;
                    for (int j = i + 1; j < _arr.Length; j++)
                    {
                        //подсчет сравнений элементов
                        C++;
                        if (_arr[j] < _arr[min])
                        {
                            min = j;
                        }
                    }
                    //подсчет элементов swaps
                    M++;
                    tmp = _arr[i];
                    _arr[i] = _arr[min];
                    _arr[min] = tmp;
                }
            }

            //метод реализация сортировки bubble
            public static void bubbleSort(ref float[] _arr)
            {
                for (int i = 0; i < _arr.Length - 1; i++)
                {
                    for (int j = _arr.Length - 1; j > i; j--)
                    {
                        //подсчет сравнений элементов
                        C++;
                        if (_arr[j] < _arr[j - 1])
                        {
                            //подсчет элементов swaps
                            M++;
                            float tmp = _arr[j];
                            _arr[j] = _arr[j - 1];
                            _arr[j - 1] = tmp;
                        }
                    }
                }
            }

            //реализация Shell
            public static void shellSort(ref float[] _arr)
            {
                for (int k = _arr.Length / 2; k > 0; k /= 2)
                {
                    for (int i = k; i < _arr.Length; i++)
                    {
                        float tmp = _arr[i];
                        int j = i;
                        while (j >= k && tmp < _arr[j - k])
                        {
                            _arr[j] = _arr[j - k];
                            j -= k;
                        }
                        _arr[j] = tmp;
                    }
                }
            }

            //быстрая сортировка
            public static void quickSort(ref float[] _arr, int _start, int _end)
            {
                if (_start >= _end)
                {
                    return;
                }
                float tmp;
                int marker = _start;
                for (int i = _start; i <= _end; i++)
                {
                    if (_arr[i] < _arr[_end])
                    {
                        if (i != marker)
                        {
                            tmp = _arr[marker];
                            _arr[marker] = _arr[i];
                            _arr[i] = tmp;
                        }
                        marker++;
                    }
                }
                if (marker != _end)
                {
                    tmp = _arr[marker];
                    _arr[marker] = _arr[_end];
                    _arr[_end] = tmp;
                }
                quickSort(ref _arr, _start, marker - 1);
                quickSort(ref _arr, marker + 1, _end);
            }

            //сортировка слиянием (не эффективно)
            public static float[] mergeSort(ref float[] _arr, int _start, int _end)
            {
                if (_end == _start)
                {
                    return new float[] { _arr[_start] };
                }
                float[] arr1 = mergeSort(ref _arr, _start, _start + (_end - _start + 1) / 2 - 1);
                float[] arr2 = mergeSort(ref _arr, _start + (_end - _start + 1) / 2, _end);
                int i = 0;
                int j = 0;
                float[] result = new float[arr1.Length + arr2.Length];
                while (i + j < arr1.Length + arr2.Length)
                {
                    if (i < arr1.Length)
                    {
                        if (j < arr2.Length && arr2[j] < arr1[i])
                        {
                            result[i + j] = arr2[j];
                            j++;
                        }
                        else
                        {
                            result[i + j] = arr1[i];
                            i++;
                        }
                    }
                    else
                    {
                        result[i + j] = arr2[j];
                        j++;
                    }
                }
                arr1 = null;
                arr2 = null;
                return result;
            }

            //проверка сортировки массива
            public static bool checkSorted(ref float[] _arr)
            {
                for (int i = 0; i < _arr.Length - 1; i++)
                {
                    if (_arr[i + 1] < _arr[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }
        
        static void Main(string[] args)
        {
            //generating array
            float[] array = Sorts.genArray(8000, 543, 12);
            //использование дополнительного массива для сортировки одного массива с разными алгоритмами
            float[] sortingArray = new float[array.Length];
            Array.Copy(array, sortingArray, array.Length);
            //сортировка и отсчет
            long startTime = (long)(DateTime.UtcNow - new DateTime(2000, 1, 1)).TotalMilliseconds;
            Sorts.bubbleSort(ref sortingArray);
            long endTime = (long)(DateTime.UtcNow - new DateTime(2000, 1, 1)).TotalMilliseconds;
            Console.Out.WriteLine("Сортировка «пузырьком»: {0} мс. \t\t\t{1}", endTime - startTime, Sorts.checkSorted(ref sortingArray) ? "Выполнено!" : "Ошибка массива!");
            //отношения печати C/N и M / N для статистики
            Console.Out.WriteLine("C/N = {0:0.00}, M/N = {1:0.00}", (float)Sorts.C / array.Length, (float)Sorts.M / array.Length);
            Array.Copy(array, sortingArray, array.Length);
            //обнулять C и M, потому что они статичны
            Sorts.C = 0;
            Sorts.M = 0;
            //сортировка и отсчет
            startTime = (long)(DateTime.UtcNow - new DateTime(2000, 1, 1)).TotalMilliseconds;
            Sorts.directSort(ref sortingArray);
            endTime = (long)(DateTime.UtcNow - new DateTime(2000, 1, 1)).TotalMilliseconds;
            Console.Out.WriteLine("Сортировка прямым выбором {0} мс. \t\t\t{1}", endTime - startTime, Sorts.checkSorted(ref sortingArray) ? "Выполнено!" : "Ошибка массива!");
            Console.Out.WriteLine("C/N = {0:0.00}, M/N = {1:0.00}", (float)Sorts.C / array.Length, (float)Sorts.M / array.Length);
            Sorts.C = 0;
            Sorts.M = 0;
            Array.Copy(array, sortingArray, array.Length);
            //сортировка и отсчет
            startTime = (long)(DateTime.UtcNow - new DateTime(2000, 1, 1)).TotalMilliseconds;
            Sorts.insertionSort(ref sortingArray);
            endTime = (long)(DateTime.UtcNow - new DateTime(2000, 1, 1)).TotalMilliseconds;
            Console.Out.WriteLine("Сортировка вставками {0} мс. \t\t\t\t{1}", endTime - startTime, Sorts.checkSorted(ref sortingArray) ? "Выполнено!" : "Ошибка массива!");
            Console.Out.WriteLine("C/N = {0:0.00}, M/N = {1:0.00}", (float)Sorts.C / array.Length, (float)Sorts.M / array.Length);
            Array.Copy(array, sortingArray, array.Length);
            //сортировка и отсчет
            startTime = (long)(DateTime.UtcNow - new DateTime(2000, 1, 1)).TotalMilliseconds;
            Sorts.shellSort(ref sortingArray);
            endTime = (long)(DateTime.UtcNow - new DateTime(2000, 1, 1)).TotalMilliseconds;
            Console.Out.WriteLine("Сортировка Шелла {0} мс.  \t\t\t\t{1}", endTime - startTime, Sorts.checkSorted(ref sortingArray) ? "Выполнено!" : "Ошибка массива!");
            Array.Copy(array, sortingArray, array.Length);
            //сортировка и отсчет
            startTime = (long)(DateTime.UtcNow - new DateTime(2000, 1, 1)).TotalMilliseconds;
            sortingArray = Sorts.mergeSort(ref sortingArray, 0, sortingArray.Length - 1);
            endTime = (long)(DateTime.UtcNow - new DateTime(2000, 1, 1)).TotalMilliseconds;
            Console.Out.WriteLine("Сортировка слиянием {0} мс.  \t\t\t\t{1}", endTime - startTime, Sorts.checkSorted(ref sortingArray) ? "Выполнено!" : "Ошибка массива!");
            Array.Copy(array, sortingArray, array.Length);
            //сортировка с использованием быстрой сортировки и подсчет миллисекунд выполнения
            startTime = (long)(DateTime.UtcNow - new DateTime(2000, 1, 1)).TotalMilliseconds;
            Sorts.quickSort(ref sortingArray, 0, sortingArray.Length - 1);
            endTime = (long)(DateTime.UtcNow - new DateTime(2000, 1, 1)).TotalMilliseconds;
            Console.Out.WriteLine("Быстрая сортировка (Хоара) {0} мс.  \t\t\t{1}", endTime - startTime, Sorts.checkSorted(ref sortingArray) ? "Выполнено!" : "Ошибка массива!");
            Console.Out.WriteLine();
            Console.ReadKey();
            //очистка памяти
            sortingArray = null;
            array = null;
        }
    }
}
