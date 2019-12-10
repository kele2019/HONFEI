using System;
using System.Collections.Generic;

/// <summary>
///报表中的查询条件实体类
/// </summary>
[Serializable]
public class SelectWhere
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
    private string tableField;
    /// <summary>
    /// 表字段名
    /// </summary>
    public string TableField
    {
        get { return tableField; }
        set { tableField = value; }
    }
    private string displayListing;
    /// <summary>
    /// 页面显示文字
    /// </summary>
    public string DisplayListing
    {
        get { return displayListing; }
        set { displayListing = value; }
    }
    private string fuzzyInquires;
    /// <summary>
    /// 是否启用模糊查询
    /// </summary>
    public string FuzzyInquires
    {
        get { return fuzzyInquires; }
        set { fuzzyInquires = value; }
    }
    private string timeSelect;
    /// <summary>
    /// 起始结束日期查询
    /// </summary>
    public string TimeSelect
    {
        get { return timeSelect; }
        set { timeSelect = value; }
    }
    private string moneySelect;
    /// <summary>
    /// 金额区域查询
    /// </summary>
    public string MoneySelect
    {
        get { return moneySelect; }
        set { moneySelect = value; }
    }
}