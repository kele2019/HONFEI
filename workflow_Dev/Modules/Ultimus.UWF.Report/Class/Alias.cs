using System;
using System.Collections.Generic;

/// <summary>
/// 报表中明细列的显示列头实体类
/// </summary>
[Serializable]
public class Alias
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
    private string sliasName;
    /// <summary>
    /// 别名
    /// </summary>
    public string SliasName
    {
        get { return sliasName; }
        set { sliasName = value; }
    }

    /// <summary>
    /// 外部链接
    /// </summary>
    private string openUrl;

    public string OpenUrl
    {
        get { return openUrl; }
        set { openUrl = value; }
    }

    private string isHide;
    /// <summary>
    /// 隐藏列
    /// </summary>
    public string IsHide
    {
        get { return isHide; }
        set { isHide = value; }
    }

    private string width;
    /// <summary>
    /// 列宽
    /// </summary>
    public string Width
    {
        get { return width; }
        set { width = value; }
    }
}