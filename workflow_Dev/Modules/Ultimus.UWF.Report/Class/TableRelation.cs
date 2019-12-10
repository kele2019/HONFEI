using System;
using System.Collections.Generic;

/// <summary>
///多表之间的关系实体类
/// </summary>
[Serializable]
public class TableRelation
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
    private string relation;
    /// <summary>
    /// 表之间的关系
    /// </summary>
    public string Relation
    {
        get { return relation; }
        set { relation = value; }
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
    private List<TableRelationFields> fields;
    /// <summary>
    /// 表关系对应字段
    /// </summary>
    public List<TableRelationFields> Fields
    {
        get { return fields; }
        set { fields = value; }
    }
}