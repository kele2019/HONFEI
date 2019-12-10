using System;
using System.Collections.Generic;

/// <summary>
///查询字段的规则实体类
/// </summary>
[Serializable]
public class FieldRules
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
    private string fieldName;
    /// <summary>
    /// 字段名
    /// </summary>
    public string FieldName
    {
        get { return fieldName; }
        set { fieldName = value; }
    }
    private string rules;
    /// <summary>
    /// 规则
    /// </summary>
    public string Rules
    {
        get { return rules; }
        set { rules = value; }
    }
}