using System;


public class TableInformation
{
    private string columnName;
    /// <summary>
    /// 字段名
    /// </summary>
    public string ColumnName
    {
        get { return columnName; }
        set { columnName = value; }
    }
    private string dataType;
    /// <summary>
    /// 数据类型
    /// </summary>
    public string DataType
    {
        get { return dataType; }
        set { dataType = value; }
    }
    private string dataLength;
    /// <summary>
    /// 长度
    /// </summary>
    public string DataLength
    {
        get { return dataLength; }
        set { dataLength = value; }
    }
    private string dataPrecision;
    /// <summary>
    /// 备注
    /// </summary>
    public string DataPrecision
    {
        get { return dataPrecision; }
        set { dataPrecision = value; }
    }
    private string nullLable;
    /// <summary>
    /// 允许空值
    /// </summary>
    public string NullLable
    {
        get { return nullLable; }
        set { nullLable = value; }
    }
    private string dataDefault;
    /// <summary>
    /// 缺省值
    /// </summary>
    public string DataDefault
    {
        get { return dataDefault; }
        set { dataDefault = value; }
    }
}
