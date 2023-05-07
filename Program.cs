using System;

class Neuron /*Суть этого нейрона заключается в том, что нейрон должен обучиться по одному вашему примеру.*/
{
    // Эти поля пригодяться на стадии обучения нейрона.
   
    public double smoothing = 0.0001;
    public bool showResults = false;
    //Внутри этого массива храняться последние данные обученых весов.
    public double[] lastWeights;

    // Этот метод нужен для сложения всех входных данных умноженых на их веса.
    protected double Summator(double[] inputs, double[] weights)
    {
        double allSum = 0;

        for (int i = 0; i < inputs.Length; i++)
        {
            allSum = allSum + inputs[i] * weights[i];
        }

        return allSum;
    }
    // Этот метод нужен для активации функции сигмоида.
    public double Sigmoida(double[] inputs, double[] weights)
    {
        double x = Summator(inputs, weights);

        return 1 / (1 + (1 / Math.Pow(Math.E, x)));

    }
    // Этот метод нужен для обучения неейрона.
    public double[] Training(double[] inputs, double result)
    {
        double[] readyWeigts = new double[inputs.Length];
        int operationWeight = 0;
        double nowResult = Sigmoida(inputs, readyWeigts);

        while(nowResult<result)
        {
            if (operationWeight>=inputs.Length) {operationWeight=0;}

            nowResult = Sigmoida(inputs, readyWeigts);
            double error = result - nowResult;
            double corection = (error / nowResult) * smoothing;
            readyWeigts[operationWeight] += corection;
            if (showResults || nowResult<result)
            {
                Console.ForegroundColor= ConsoleColor.Green;
                Console.WriteLine($"\rВес: {operationWeight}\tТекущий результат веса: {readyWeigts[operationWeight]}\tТекущий результат: {nowResult}\tПредпологаемый результат: {result}");
            }
            operationWeight++;
        }

        lastWeights = readyWeigts;
        return readyWeigts;
    }
}

// Класс для теста нейрона.
class Program
{
    static void Main(string[] args)
    {
        double[] inputDateForTest = new double[3];
        inputDateForTest[0] = 7000000.0;
        inputDateForTest[1] = 13.0;
        inputDateForTest[2] = 11.2;

        double[] inputDatePack = new double[3];
        inputDatePack[0] = 4000000.0;
        inputDatePack[1] = 5.0;
        inputDatePack[2] = 6.0;

        Neuron neuron = new Neuron();
        //Параметр каторый будет выводить данные в консоль
        neuron.showResults = true;
        //Обучаю нейрон
        double[] weight = neuron.Training(inputDateForTest, 2.0);
        //Тест на данных переданых в процес обучения
        Console.WriteLine($"\tFirst test:{neuron.Sigmoida(inputDateForTest, weight)}");
        //Второй тест
        Console.WriteLine($"\tSecond test:{neuron.Sigmoida(inputDatePack, neuron.lastWeights)}");
        
    }
}//Проблема в том, что нейрон не может себя нормально обучить и он почему то работает не коректно. Просто нажмите f5 и сами всё поймёте.