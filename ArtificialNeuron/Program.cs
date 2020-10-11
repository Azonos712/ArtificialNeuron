using System;

namespace ArtificialNeuron
{
    class Program
    {
        public class Neuron
        {
            private decimal _weight = 0.5m; //случайное значение весов
            public decimal LastError { get; private set; }
            public decimal Smoothing { get; set; } = 0.00001m; // сглаживание или шаг коррекции

            public decimal ProcessInputData(decimal input)
            {
                return input * _weight;
            }

            public decimal RestoreInputData(decimal output)
            {
                return output / _weight;
            }

            public void Train(decimal input, decimal expectedResult)
            {
                var actualResult = input * _weight; //текущий результат
                LastError = expectedResult - actualResult; //вычисление ошибки
                var correction = (LastError / actualResult) * Smoothing; //значение корректировки
                _weight += correction;
            }
        }

        static void Main(string[] args)
        {
            decimal km = 100;
            decimal miles = 62.1371m;

            Neuron neuron = new Neuron();
            int i = 0;
            do
            {
                i++;
                neuron.Train(km, miles);
                Console.WriteLine($"Итерация №{i}\tОшибка:\t{neuron.LastError}");
            } while (neuron.LastError > neuron.Smoothing || neuron.LastError < -neuron.Smoothing);

            Console.WriteLine("Обучение завершено!");

            Console.WriteLine($"{neuron.ProcessInputData(100)} миль в {100} км");
            Console.WriteLine($"{neuron.ProcessInputData(541)} миль в {541} км");
            Console.WriteLine($"{neuron.RestoreInputData(10)} км в {10} милях");
            Console.WriteLine($"{neuron.ProcessInputData(10)} миль в {10} км");
            Console.WriteLine($"{neuron.ProcessInputData(1)} миль в {1} км");
        }
    }
}
