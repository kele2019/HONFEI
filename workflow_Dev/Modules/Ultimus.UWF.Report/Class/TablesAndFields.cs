using System;
using System.Collections.Generic;

/// <summary>
/// 选择的表名和字段集合实体类
/// </summary>
[Serializable]
public class TablesAndFields
{
    private string tableName;
    /// <summary>
    /// 表名
    /// </summary>
    public string TableName
    {
        get { return tableName; }
        set { tableName = value; }
    }
    private List<FieldName> data;
    /// <summary>
    /// 字段集合
    /// </summary>
    public List<FieldName> Data
    {
        get { return data; }
        set { data = value; }
    }
}