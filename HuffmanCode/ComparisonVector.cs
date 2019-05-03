using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuffmanCode
{
    public class ComparisonVector
    {
        private int SizeVector { set; get; }
        private Dictionary<string, double> Vector { set; get; }

        public ComparisonVector()
        {
            //Определяем каков будет вектор по тексту (его размерность)

        }

        /// <summary>
        /// Получение нулевого вектора, для текста
        /// </summary>
        /// <param name="frequencyDictionary">Частотный словарь по всему тексту</param>
        /// <returns>Нулевой вектор, где каждое слово ассоциируется с конкретной позицией в векторе</returns>
        public static Dictionary<string, double> GetVectorZerosFromFrequencyDictionary(FrequencyDictionary frequencyDictionary)
        {
            Dictionary<string, double> vector = new Dictionary<string, double>();
            //Отсортируем по частоте встречания слова
            frequencyDictionary.SortDescValue();
            //Вормируем ассоциативный 0-вектор
            foreach (var item in frequencyDictionary.Dictionary)
            {
                vector.Add(item.Key, 0.0);
            }
            return vector;
        }

        /// <summary>
        /// Получить вектор с честотами для блока слов
        /// </summary>
        /// <param name="frequencyDictionary">Словарь всех слов текста</param>
        /// <param name="words">Блок слов</param>
        /// <returns>Вектор, с частотами слов в блоке</returns>
        public static Dictionary<string, double> GetVectorFrequencyFromBlock(FrequencyDictionary frequencyDictionary, List<string> words)
        {
            //Получаем нулевой вектор
            Dictionary<string, double> vector = GetVectorZerosFromFrequencyDictionary(frequencyDictionary);
            //подсчитываем количество слов
            foreach (var word in words)
            {
                vector[word] += 1; 
            }

            List<string> keys = new List<string>(vector.Keys);
            //получаем частоту
            foreach (var item in keys)
            {
                vector[item] /= words.Count;
            }
            return vector;
        }

        //Евклидово расстояние
        /// <summary>
        /// Получить Евклидово расстояние между векторами 
        /// </summary>
        /// <param name="p">Вектор</param>
        /// <param name="q">Вектор</param>
        /// <returns>Евклидово растояние</returns>
        public static double GetEuclideanDistance(Dictionary<string, double> p, Dictionary<string, double> q)
        {
            double d = 0;
            foreach (var item in p)
            {
                d += Math.Pow(Math.Abs(p[item.Key] - q[item.Key]), 2);
            }

            return Math.Sqrt(d);
        }

        //растояние Чебышева
        /// <summary>
        /// Получит расстояние Чебышева между векторами 
        /// </summary>
        /// <param name="p">Вектор</param>
        /// <param name="q">Вектор</param>
        /// <returns>Расстояние Чебышево</returns>
        public static double GetChebyshevDistance(Dictionary<string, double> p, Dictionary<string, double> q)
        {
            double d = 0 , max = 0;
            foreach (var item in p)
            {
                d = Math.Abs(p[item.Key] - q[item.Key]);
                if (d > max)
                    max = d;
            }
            return max;
        }

        //Расстояние городских кварталов
        /// <summary>
        /// Получить расстояние городских кварталов
        /// </summary>
        /// <param name="p">Вектор</param>
        /// <param name="q">Вектор</param>
        /// <returns>Расстояние городских кварталов</returns>
        public static double GetCityBlockDistance(Dictionary<string, double> p, Dictionary<string, double> q)
        {
            double d = 0;
            foreach (var item in p)
            {
                d += Math.Abs(p[item.Key] - q[item.Key]);
            }
            return d;
        }

    }
}
