﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace MatrixMultiplication
{
    /// <summary>
    /// Матрицы типа int размера n * m
    /// </summary>
    public class Matrix
    {
        private int[,] mas;
        private int _strings;
        /// <summary>
        /// Кколичество строк в матрице
        /// </summary>
        public int Strings
        {
            get { return _strings; }
            set { _strings = value; }
        }

        private int _columns;
        /// <summary>
        /// Количество столбцов в матрице
        /// </summary>
        public int Columns
        {
            get { return _columns; }
            set { _columns = value; }
        }
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public Matrix()
        {
            Strings = 0;
            Columns = 0;
            mas = new int[Strings, Columns];
        }
        /// <summary>
        /// Геренрация случайной матрицы заданного размера
        /// </summary>
        /// <param name="numberOfStrings"></param>
        /// <param name="numberOfColumns"></param>
        public Matrix(int numberOfStrings, int numberOfColumns, bool isRandom = false)
        {
            Strings = numberOfStrings;
            Columns = numberOfColumns;
            mas = new int[Strings, Columns];
            if (isRandom)
            {
                var random = new Random();
                for (int i = 0; i < Strings; ++i)
                {
                    for (int j = 0; j < Columns; ++j)
                    {
                        mas[i, j] = random.Next(10);
                    }
                }
            }

        }

        /// <summary>
        /// Чтение из файла; Адрес файла: path
        /// </summary>
        /// <param name="path"></param>
        public Matrix(string path)
        {
            using var sRider = new StreamReader(path);
            var string1 = sRider.ReadLine();
            var strings1 = string1.Split(' ');
            Strings = int.Parse(strings1[0]);
            Columns = int.Parse(strings1[1]);
            mas = new int[Strings, Columns];
            for (int i = 0; i < Strings; ++i)
            {
                string string2 = sRider.ReadLine();
                string[] strings2 = string2.Split(' ');
                for (int j = 0; j < Columns; ++j)
                {
                    mas[i, j] = int.Parse(strings2[j]);
                }
            }
            sRider.Close();
        }

        /// <summary>
        /// Возвращает элемент с индексом i, j
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        public int GetElement(int stringIndex, int columnIndex)
        {
            return mas[stringIndex, columnIndex];
        }
        public void SetElement(int stringIndex, int columnIndex, int value)
        {
            mas[stringIndex, columnIndex] = value;
        }
        /// <summary>
        /// Печатаем матрицу в файл. Адрес файла: path
        /// </summary>
        /// <param name="path"></param>
        public void Print(string path)
        {
            using var sWriter = new StreamWriter(path);
            sWriter.WriteLine(Strings + " " + Columns);
            for (int i = 0; i < Strings; ++i)
            {
                var str = string.Empty;
                for (int j = 0; j < Columns; ++j)
                {
                    str += mas[i, j] + " ";
                }
                sWriter.WriteLine(str);
            }
            sWriter.WriteLine();
            sWriter.Close();
        }

        /// <summary>
        /// Выводим матрицу на экран
        /// </summary>
        public void Print()
        {
            for (int i = 0; i < Strings; ++i)
            {
                var str = string.Empty;
                for (int j = 0; j < Columns; ++j)
                {
                    str += mas[i, j] + " ";
                }
                Console.WriteLine(str);
            }
        }

        /// <summary>
        /// Параллельное умножение матриц
        /// </summary>
        /// <param name="rhsMatrix"></param>
        /// <returns></returns>
        public Matrix ParallelMultiplication(Matrix rhsMatrix)
        {
            var resultMatrix = new Matrix(Strings, rhsMatrix.Columns);
            var numberOfThreads = Environment.ProcessorCount;
            var threads = new Thread[numberOfThreads];
            int chunckSize = this.Strings / numberOfThreads + 1;
            for (int i = 0; i < numberOfThreads; ++i)
            {
                var localI = i;
                threads[i] = new Thread(() =>
                {
                    for (int t = chunckSize * localI; t < chunckSize * (localI + 1) && t < this.Strings; ++t)
                    {
                        for (int j = 0; j < rhsMatrix.Columns; ++j)
                        {
                            var sum = 0;
                            for (int k = 0; k < rhsMatrix.Strings; ++k)
                            {
                                sum += this.GetElement(t, k) * rhsMatrix.GetElement(k, j);
                            }
                            resultMatrix.SetElement(t, j, sum);
                        }
                    }
                });
            }
            foreach (var thread in threads)
            {
                thread.Start();
            }
            foreach (var thread in threads)
            {
                thread.Join();
            }
            return resultMatrix;
        }

        /// <summary>
        /// НЕпараллельное умножение матриц
        /// </summary>
        /// <param name="firstMatrix"></param>
        /// <param name="secondMatrix"></param>
        /// <returns></returns>
        public Matrix NonParallelMultiplication(Matrix rhsMatrix)
        {
            Matrix resultMatrix = new Matrix(Strings, rhsMatrix.Columns);
            for (int i = 0; i < Strings; ++i)
            {
                for (int j = 0; j < rhsMatrix.Columns; ++j)
                {
                    var sum = 0;
                    for (int k = 0; k < rhsMatrix.Strings; ++k)
                    {
                        sum += GetElement(i, k) * rhsMatrix.GetElement(k, j);
                    }
                    resultMatrix.SetElement(i, j, sum);
                }
            }
            return resultMatrix;
        }
    }
}
