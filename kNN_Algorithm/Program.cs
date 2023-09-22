using CsvHelper;
using System.Globalization;

string dataSetPath = "C:\\Users\\aliev\\Downloads\\DataSet.csv";

using (var reader = new StreamReader(dataSetPath))

using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
{
    var records = csv.GetRecords<Point>();

    foreach (var record in records)
        Console.WriteLine(record.Region);
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

}

class Point_Class
{

    public Point_Class()
    {
        
    }
}


class Point
{
    public string Class { get; set; }
    public string Sex { get; set; }
    public string Sport { get; set; }
    public string Job { get; set; }
    public string HeartDisease { get; set; }
    public string OwlOrLark { get; set; }
    public string Milk { get; set; }
    public string WakeUpTime { get; set; }
    public string Region { get; set; }
    public string SleepTime { get; set; }
}