using System;
using System.Collections.Generic;

/// <summary>
///排序规则实体类
/// </summary>
[Serializable]
public class SortType
{
    private string tableName;
    /// <summary>
    /// 排序表名
    /// </summary>
    public string TableName
    {
        get { return tableName; }
        set { tableName = value; }
    }
    private string fieldName;
    /// <summary>
    /// 排序字段
    /// </summary>
    public string FieldName
    {
        get { return fieldName; }
        set { fieldName = value; }
    }
    private string sortStyle;
    /// <summary>
    /// 排序规则
    /// </summary>
    public string SortStyle
    {
        get { return sortStyle; }
        set { sortStyle = value; }
    }
}