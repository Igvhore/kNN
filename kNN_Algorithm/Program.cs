﻿using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;

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
    

}

/*class itemInDataSet
{
    private string _item_class;
    private List<double> _parameters;

    public itemInDataSet() 
    {
        _item_class = "Empty DataSet";
        _parameters = new List<double>();
    }

    public itemInDataSet(string title, List<double> parameters)
    {
        _item_class = title;
        _parameters = parameters;
    }
}

string dataSetPath = "C:\\Users\\aliev\\Downloads\\DataSetCode.csv";

using (var reader = new StreamReader(dataSetPath))

using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
{
    var records = csv.GetRecords<Point>();
}


class DataSet
{
    private string _name { get; set; }
    private int _size { get; set; }
    private Point[] _points { get; set; }

    public DataSet()
    {
        _name = "";
        _size = 0;
        _points = new Point[0];
    }

    public DataSet (string name, int size)
    {
        _name = name;
        _size = size;
        _points = new Point[size];
    }

}*/


class itemInDataSet
{
    public string Class { get; set; }
    public int Sex { get; set; }
    public int Sport { get; set; }
    public int Job { get; set; }
    public int HeartDisease { get; set; }
    public int OwlOrLark { get; set; }
    public int Milk { get; set; }
    public int WakeUpTime { get; set; }
    public int Region { get; set; }
    public int SleepTime { get; set; }
}