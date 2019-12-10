using System;
using System.Collections.Generic;

/// <summary>
///FieldName 的摘要说明
/// </summary>
[Serializable]
public class FieldName
{
    private string columnName;

    public string ColumnName
    {
        get { return columnName; }
        set { columnName = value; }
    }
}