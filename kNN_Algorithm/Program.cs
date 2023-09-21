


class DataSet
{
    private string name;
    private int size;

    public DataSet()
    {
        this.name = "";
        this.size = 0;
    }

    public DataSet (string name, int size)
    {
        this.name = name;
        this.size = size;
    }
    
}

class Point_Class
{

}


class Point
{
    private DataSet dataSet;
    private int point_id;
    private Point_Class p_class;

    public Point() 
    { 
        dataSet = new DataSet();
        point_id = 0;
        p_class = new Point_Class();
    }
}