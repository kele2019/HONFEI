using System;
using System.Collections.Generic;

/// <summary>
///多表关系对应字段实体类
/// </summary>
[Serializable]
public class TableRelationFields
{
    private string tableName1;
    /// <summary>
    /// 表1名称
    /// </summary>
    public string TableName1
    {
        get { return tableName1; }
        set { tableName1 = value; }
    }
    private string table1FieldName;
    /// <summary>
    /// 表1字段名称
    /// </summary>
    public string Table1FieldName
    {
        get { return table1FieldName; }
        set { table1FieldName = value; }
    }
    private string fieldRelation;
    /// <summary>
    /// 对应关系
    /// </summary>
    public string FieldRelation
    {
        get { return fieldRelation; }
        set { fieldRelation = value; }
    }
    private string tableName2;
    /// <summary>
    /// 表2名称
    /// </summary>
    public string TableName2
    {
        get { return tableName2; }
        set { tableName2 = value; }
    }
    private string table2FieldName;
    /// <summary>
    /// 表2字段名称
    /// </summary>
    public string Table2FieldName
    {
        get { return table2FieldName; }
        set { table2FieldName = value; }
    }
}