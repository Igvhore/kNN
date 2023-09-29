using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using System.Linq;
using System.Collections.Immutable;

string dataSetPath = "C:\\Users\\aliev\\Downloads\\DataSetCode.csv";
List<itemInDataSet> items = new List<itemInDataSet>();

using (var reader = new StreamReader(dataSetPath))

using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
{
    var records = csv.GetRecords<itemInDataSet>();
    
    foreach (var record in records)
        items.Add(record);
}

List<itemInDataSet>[] testAndClassifiedData = DataSet.SplitDataSet(items, 0.2);

foreach (var data in testAndClassifiedData[0])
{
    List<itemInDataSet> neighbours = DataSet.GetNeighbours(data, testAndClassifiedData[1], 7);
    DataSet.PredictItemClass(data, neighbours);
}


class DataSet
{
    private string _title;
    private string _path;
    private List<itemInDataSet> _items;

    public DataSet()
    {
        _title = "Empty";
        _path = "Empty";
        _items = new List<itemInDataSet>();
    }

    public DataSet(string title, string path, List<itemInDataSet> items)
    {
        _title = title;
        _path = path;
        _items = items;
    }

    public DataSet(string title, string path)
    {
        _title = title;
        _path = path;
        _items = new List<itemInDataSet>();
    }

    public static double CalculateEuclideanDistanceFree(itemInDataSet a, itemInDataSet b) 
        =>Math.Pow(a.Sex - b.Sex, 2)
        + Math.Pow(a.Sport - b.Sport, 2) 
        + Math.Pow(a.Job - b.Job, 2) 
        + Math.Pow(a.HeartDisease - b.HeartDisease, 2) 
        + Math.Pow(a.OwlOrLark - b.OwlOrLark, 2) 
        + Math.Pow(a.Milk - b.Milk, 2) 
        + Math.Pow(a.WakeUpTime - b.WakeUpTime, 2) 
        + Math.Pow(a.Region - b.Region, 2) 
        + Math.Pow(a.SleepTime - b.SleepTime, 2);  

    public static double CalculateEuclideanDistance(itemInDataSet a, itemInDataSet b) => Math.Sqrt(CalculateEuclideanDistanceFree(a, b));

    /// <summary>
    /// Метод, разделяющий DataSet на 2 для обучения модели
    /// </summary>
    /// <param name="itemsInDataSet"></param>
    /// <param name="percent"></param>
    /// <returns> Возвращает два DataSet. Индекс [0] - DataSet для тренирвоки, [1] -  DataSet для тестирования работы алгоритма</returns>
    public static List<itemInDataSet>[] SplitDataSet(List<itemInDataSet> itemsInDataSet, double percent)
    {
        List<itemInDataSet>[] dataSets = new List<itemInDataSet>[2];
        int count = (int)Math.Ceiling(percent * itemsInDataSet.Count());
        List<itemInDataSet> testDataSet = new List<itemInDataSet>();
        Random rnd = new Random();

        while(count != 0)
        {
            int temp = rnd.Next(0, itemsInDataSet.Count());
            testDataSet.Add(itemsInDataSet[temp]);
            itemsInDataSet.Remove(itemsInDataSet[temp]);
            count--;
        }

        dataSets[0] = testDataSet;
        dataSets[1] = itemsInDataSet;

        return dataSets;
    }
    
    public static List<itemInDataSet> GetNeighbours(itemInDataSet testData, List<itemInDataSet> classifiedData, int k)
    {
        Dictionary<int, double> distances = new Dictionary<int, double>();
        List<int> sortedID = new List<int>();
        List<itemInDataSet> neighbours = new List<itemInDataSet>();

        for (int i = 0; i < classifiedData.Count(); i++)
            distances.Add(i, DataSet.CalculateEuclideanDistanceFree(testData, classifiedData[i]));

        sortedID = distances.OrderBy(n => n.Value).Select(n => n.Key).ToList();

        for (int i = 0; i < k; i++)
            neighbours.Add(classifiedData[sortedID[i]]);

        return neighbours;
    }

    public static void PredictItemClass(itemInDataSet itemForClassification, List<itemInDataSet> neighbours)
    {
        int tea = 0;
        int coffee = 0;

        foreach (itemInDataSet neighbour in neighbours)
        {
            if (neighbour.Class == "Coffee")
                coffee++;
            else if (neighbour.Class == "Tea")
                tea++;
        }

        Console.WriteLine($"Item ID: {itemForClassification.GetHashCode()}");
        Console.WriteLine($"Item Class: {itemForClassification.Class}");

        itemForClassification.Class = tea > coffee ? "Tea" : "Coffee";       

        Console.WriteLine($"Class based on model prediction: {itemForClassification.Class}");
        Console.WriteLine();    
    }

}


class itemInDataSet
{
    public string Class { get; set; }
    public double Sex { get; set; }
    public double Sport { get; set; }
    public double Job { get; set; }
    public double HeartDisease { get; set; }
    public double OwlOrLark { get; set; }
    public double Milk { get; set; }
    public double WakeUpTime { get; set; }
    public double Region { get; set; }
    public double SleepTime { get; set; }
}