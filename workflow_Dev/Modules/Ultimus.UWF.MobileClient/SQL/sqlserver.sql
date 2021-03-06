
/****** OBJECT:  TABLE [DBO].[MOBILECLIENT_CLASSIFICATION]    SCRIPT DATE: 06/19/2013 10:48:03 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [DBO].[MOBILECLIENT_CLASSIFICATION](
	[ID] [INT]  NOT NULL,
	[CATEGORYCNAME] [NVARCHAR](156) NULL,
	[CATEGORYENAME] [NVARCHAR](156) NULL,
	[PROCESSNAME] [NVARCHAR](156) NULL,
	[ISACTION] [NCHAR](10) NULL,
 CONSTRAINT [PK_MOBILECLIENT_CLASSIFICATION] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

EXEC SYS.SP_ADDEXTENDEDPROPERTY @NAME=N'MS_DESCRIPTION', @VALUE=N'自增列' , @LEVEL0TYPE=N'SCHEMA',@LEVEL0NAME=N'DBO', @LEVEL1TYPE=N'TABLE',@LEVEL1NAME=N'MOBILECLIENT_CLASSIFICATION', @LEVEL2TYPE=N'COLUMN',@LEVEL2NAME=N'ID'
GO

EXEC SYS.SP_ADDEXTENDEDPROPERTY @NAME=N'MS_DESCRIPTION', @VALUE=N'类别中文名称' , @LEVEL0TYPE=N'SCHEMA',@LEVEL0NAME=N'DBO', @LEVEL1TYPE=N'TABLE',@LEVEL1NAME=N'MOBILECLIENT_CLASSIFICATION', @LEVEL2TYPE=N'COLUMN',@LEVEL2NAME=N'CATEGORYCNAME'
GO

EXEC SYS.SP_ADDEXTENDEDPROPERTY @NAME=N'MS_DESCRIPTION', @VALUE=N'类别英文名称' , @LEVEL0TYPE=N'SCHEMA',@LEVEL0NAME=N'DBO', @LEVEL1TYPE=N'TABLE',@LEVEL1NAME=N'MOBILECLIENT_CLASSIFICATION', @LEVEL2TYPE=N'COLUMN',@LEVEL2NAME=N'CATEGORYENAME'
GO

EXEC SYS.SP_ADDEXTENDEDPROPERTY @NAME=N'MS_DESCRIPTION', @VALUE=N'流程名称' , @LEVEL0TYPE=N'SCHEMA',@LEVEL0NAME=N'DBO', @LEVEL1TYPE=N'TABLE',@LEVEL1NAME=N'MOBILECLIENT_CLASSIFICATION', @LEVEL2TYPE=N'COLUMN',@LEVEL2NAME=N'PROCESSNAME'
GO

EXEC SYS.SP_ADDEXTENDEDPROPERTY @NAME=N'MS_DESCRIPTION', @VALUE=N'是否启用' , @LEVEL0TYPE=N'SCHEMA',@LEVEL0NAME=N'DBO', @LEVEL1TYPE=N'TABLE',@LEVEL1NAME=N'MOBILECLIENT_CLASSIFICATION', @LEVEL2TYPE=N'COLUMN',@LEVEL2NAME=N'ISACTION'
GO

/****** OBJECT:  TABLE [DBO].[MOBILECLIENT_CONTROL]    SCRIPT DATE: 06/19/2013 10:48:03 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [DBO].[MOBILECLIENT_CONTROL](
	[ID] [INT]  NOT NULL,
	[CONTROLCNAME] [NVARCHAR](156) NULL,
	[CONTROLENAME] [NVARCHAR](156) NULL,
	[CONTROLNAME] [NVARCHAR](156) NULL,
	[ISACTION] [NCHAR](10) NULL,
 CONSTRAINT [PK_MOBILECLIENT_CONTROL] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

EXEC SYS.SP_ADDEXTENDEDPROPERTY @NAME=N'MS_DESCRIPTION', @VALUE=N'自增列' , @LEVEL0TYPE=N'SCHEMA',@LEVEL0NAME=N'DBO', @LEVEL1TYPE=N'TABLE',@LEVEL1NAME=N'MOBILECLIENT_CONTROL', @LEVEL2TYPE=N'COLUMN',@LEVEL2NAME=N'ID'
GO

EXEC SYS.SP_ADDEXTENDEDPROPERTY @NAME=N'MS_DESCRIPTION', @VALUE=N'控件中文名称' , @LEVEL0TYPE=N'SCHEMA',@LEVEL0NAME=N'DBO', @LEVEL1TYPE=N'TABLE',@LEVEL1NAME=N'MOBILECLIENT_CONTROL', @LEVEL2TYPE=N'COLUMN',@LEVEL2NAME=N'CONTROLCNAME'
GO

EXEC SYS.SP_ADDEXTENDEDPROPERTY @NAME=N'MS_DESCRIPTION', @VALUE=N'控件英文名称' , @LEVEL0TYPE=N'SCHEMA',@LEVEL0NAME=N'DBO', @LEVEL1TYPE=N'TABLE',@LEVEL1NAME=N'MOBILECLIENT_CONTROL', @LEVEL2TYPE=N'COLUMN',@LEVEL2NAME=N'CONTROLENAME'
GO

EXEC SYS.SP_ADDEXTENDEDPROPERTY @NAME=N'MS_DESCRIPTION', @VALUE=N'是否启用' , @LEVEL0TYPE=N'SCHEMA',@LEVEL0NAME=N'DBO', @LEVEL1TYPE=N'TABLE',@LEVEL1NAME=N'MOBILECLIENT_CONTROL', @LEVEL2TYPE=N'COLUMN',@LEVEL2NAME=N'ISACTION'
GO


/****** OBJECT:  TABLE [DBO].[MOBILECLIENT_PROCESS]    SCRIPT DATE: 06/19/2013 10:48:03 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [DBO].[MOBILECLIENT_PROCESS](
	[ID] [INT]  NOT NULL,
	[PROCESSNAME] [NVARCHAR](156) NULL,
	[LOGO] [NVARCHAR](256) NULL,
	[CREATETIME] [DATETIME] NULL,
	[UPDATETIME] [DATETIME] NULL,
	[ISCREATEPAGE] [NVARCHAR](50) NULL,
 CONSTRAINT [PK_MOBILECLIENT] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

EXEC SYS.SP_ADDEXTENDEDPROPERTY @NAME=N'MS_DESCRIPTION', @VALUE=N'自增列' , @LEVEL0TYPE=N'SCHEMA',@LEVEL0NAME=N'DBO', @LEVEL1TYPE=N'TABLE',@LEVEL1NAME=N'MOBILECLIENT_PROCESS', @LEVEL2TYPE=N'COLUMN',@LEVEL2NAME=N'ID'
GO

EXEC SYS.SP_ADDEXTENDEDPROPERTY @NAME=N'MS_DESCRIPTION', @VALUE=N'流程名称' , @LEVEL0TYPE=N'SCHEMA',@LEVEL0NAME=N'DBO', @LEVEL1TYPE=N'TABLE',@LEVEL1NAME=N'MOBILECLIENT_PROCESS', @LEVEL2TYPE=N'COLUMN',@LEVEL2NAME=N'PROCESSNAME'
GO

EXEC SYS.SP_ADDEXTENDEDPROPERTY @NAME=N'MS_DESCRIPTION', @VALUE=N'页面LOGO路径' , @LEVEL0TYPE=N'SCHEMA',@LEVEL0NAME=N'DBO', @LEVEL1TYPE=N'TABLE',@LEVEL1NAME=N'MOBILECLIENT_PROCESS', @LEVEL2TYPE=N'COLUMN',@LEVEL2NAME=N'LOGO'
GO

EXEC SYS.SP_ADDEXTENDEDPROPERTY @NAME=N'MS_DESCRIPTION', @VALUE=N'创建时间' , @LEVEL0TYPE=N'SCHEMA',@LEVEL0NAME=N'DBO', @LEVEL1TYPE=N'TABLE',@LEVEL1NAME=N'MOBILECLIENT_PROCESS', @LEVEL2TYPE=N'COLUMN',@LEVEL2NAME=N'CREATETIME'
GO

EXEC SYS.SP_ADDEXTENDEDPROPERTY @NAME=N'MS_DESCRIPTION', @VALUE=N'更新时间' , @LEVEL0TYPE=N'SCHEMA',@LEVEL0NAME=N'DBO', @LEVEL1TYPE=N'TABLE',@LEVEL1NAME=N'MOBILECLIENT_PROCESS', @LEVEL2TYPE=N'COLUMN',@LEVEL2NAME=N'UPDATETIME'
GO

EXEC SYS.SP_ADDEXTENDEDPROPERTY @NAME=N'MS_DESCRIPTION', @VALUE=N'是否生成页面' , @LEVEL0TYPE=N'SCHEMA',@LEVEL0NAME=N'DBO', @LEVEL1TYPE=N'TABLE',@LEVEL1NAME=N'MOBILECLIENT_PROCESS', @LEVEL2TYPE=N'COLUMN',@LEVEL2NAME=N'ISCREATEPAGE'
GO


/****** OBJECT:  TABLE [DBO].[MOBILECLIENT_STEP]    SCRIPT DATE: 06/19/2013 10:48:03 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [DBO].[MOBILECLIENT_STEP](
	[ID] [INT]  NOT NULL,
	[FK_ID] [INT] NULL,
	[STEPNAME] [NVARCHAR](156) NULL,
	[STEPCNAME] [NVARCHAR](156) NULL,
	[STEPENAME] [NVARCHAR](156) NULL,
 CONSTRAINT [PK_MOBILECLIENT_ITEM] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

EXEC SYS.SP_ADDEXTENDEDPROPERTY @NAME=N'MS_DESCRIPTION', @VALUE=N'自增列' , @LEVEL0TYPE=N'SCHEMA',@LEVEL0NAME=N'DBO', @LEVEL1TYPE=N'TABLE',@LEVEL1NAME=N'MOBILECLIENT_STEP', @LEVEL2TYPE=N'COLUMN',@LEVEL2NAME=N'ID'
GO

EXEC SYS.SP_ADDEXTENDEDPROPERTY @NAME=N'MS_DESCRIPTION', @VALUE=N'主表外键' , @LEVEL0TYPE=N'SCHEMA',@LEVEL0NAME=N'DBO', @LEVEL1TYPE=N'TABLE',@LEVEL1NAME=N'MOBILECLIENT_STEP', @LEVEL2TYPE=N'COLUMN',@LEVEL2NAME=N'FK_ID'
GO

EXEC SYS.SP_ADDEXTENDEDPROPERTY @NAME=N'MS_DESCRIPTION', @VALUE=N'步骤名称' , @LEVEL0TYPE=N'SCHEMA',@LEVEL0NAME=N'DBO', @LEVEL1TYPE=N'TABLE',@LEVEL1NAME=N'MOBILECLIENT_STEP', @LEVEL2TYPE=N'COLUMN',@LEVEL2NAME=N'STEPNAME'
GO

EXEC SYS.SP_ADDEXTENDEDPROPERTY @NAME=N'MS_DESCRIPTION', @VALUE=N'步骤中文名称' , @LEVEL0TYPE=N'SCHEMA',@LEVEL0NAME=N'DBO', @LEVEL1TYPE=N'TABLE',@LEVEL1NAME=N'MOBILECLIENT_STEP', @LEVEL2TYPE=N'COLUMN',@LEVEL2NAME=N'STEPCNAME'
GO

EXEC SYS.SP_ADDEXTENDEDPROPERTY @NAME=N'MS_DESCRIPTION', @VALUE=N'步骤英文名称' , @LEVEL0TYPE=N'SCHEMA',@LEVEL0NAME=N'DBO', @LEVEL1TYPE=N'TABLE',@LEVEL1NAME=N'MOBILECLIENT_STEP', @LEVEL2TYPE=N'COLUMN',@LEVEL2NAME=N'STEPENAME'
GO


/****** OBJECT:  TABLE [DBO].[MOBILECLIENT_STEPCONTROL]    SCRIPT DATE: 06/19/2013 10:48:03 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [DBO].[MOBILECLIENT_STEPCONTROL](
	[ID] [INT]  NOT NULL,
	[FK_ID] [INT] NULL,
	[COLUMNNAME] [NVARCHAR](156) NULL,
	[CONTROLID] [INT] NULL,
	[FORMAT] [NVARCHAR](1000) NULL,
	[ISWILLFILL] [NVARCHAR](50) NULL,
	[READONLY] [NVARCHAR](50) NULL,
	[EXTERNALLINKS] [NVARCHAR](1000) NULL,
	[ISSHOW] [NVARCHAR](50) NULL,
	[ORDERBY] [NVARCHAR](256) NULL,
	[SOURCETYPE] [NVARCHAR](50) NULL,
	[SOURCECONNECTIONSTRING] [NVARCHAR](256) NULL,
	[SOURCETABLENAME] [NVARCHAR](256) NULL,
	[SOURCECOLUMNNAME] [NVARCHAR](256) NULL,
	[SOURCEWHERE] [NVARCHAR](1500) NULL,
	[SOURCEVARIABLENAME] [NVARCHAR](50) NULL,
	[ISMASTERTABLE] [NVARCHAR](50) NULL,
	[ISSUBLIST] [NVARCHAR](50) NULL,
	[DESTTYPE] [NVARCHAR](50) NULL,
	[DESTCONNECTIONSTRING] [NVARCHAR](256) NULL,
	[DESTTABLENAME] [NVARCHAR](256) NULL,
	[DESTCOLUMNNAME] [NVARCHAR](50) NULL,
	[DESTVARIABLENAME] [NVARCHAR](50) NULL,
 CONSTRAINT [PK_MOBILECLIENT_STEPCONTROL] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

EXEC SYS.SP_ADDEXTENDEDPROPERTY @NAME=N'MS_DESCRIPTION', @VALUE=N'自增列' , @LEVEL0TYPE=N'SCHEMA',@LEVEL0NAME=N'DBO', @LEVEL1TYPE=N'TABLE',@LEVEL1NAME=N'MOBILECLIENT_STEPCONTROL', @LEVEL2TYPE=N'COLUMN',@LEVEL2NAME=N'ID'
GO

EXEC SYS.SP_ADDEXTENDEDPROPERTY @NAME=N'MS_DESCRIPTION', @VALUE=N'步骤表主键' , @LEVEL0TYPE=N'SCHEMA',@LEVEL0NAME=N'DBO', @LEVEL1TYPE=N'TABLE',@LEVEL1NAME=N'MOBILECLIENT_STEPCONTROL', @LEVEL2TYPE=N'COLUMN',@LEVEL2NAME=N'FK_ID'
GO

EXEC SYS.SP_ADDEXTENDEDPROPERTY @NAME=N'MS_DESCRIPTION', @VALUE=N'字段名称' , @LEVEL0TYPE=N'SCHEMA',@LEVEL0NAME=N'DBO', @LEVEL1TYPE=N'TABLE',@LEVEL1NAME=N'MOBILECLIENT_STEPCONTROL', @LEVEL2TYPE=N'COLUMN',@LEVEL2NAME=N'COLUMNNAME'
GO

EXEC SYS.SP_ADDEXTENDEDPROPERTY @NAME=N'MS_DESCRIPTION', @VALUE=N'页面控件' , @LEVEL0TYPE=N'SCHEMA',@LEVEL0NAME=N'DBO', @LEVEL1TYPE=N'TABLE',@LEVEL1NAME=N'MOBILECLIENT_STEPCONTROL', @LEVEL2TYPE=N'COLUMN',@LEVEL2NAME=N'CONTROLID'
GO

EXEC SYS.SP_ADDEXTENDEDPROPERTY @NAME=N'MS_DESCRIPTION', @VALUE=N'显示格式' , @LEVEL0TYPE=N'SCHEMA',@LEVEL0NAME=N'DBO', @LEVEL1TYPE=N'TABLE',@LEVEL1NAME=N'MOBILECLIENT_STEPCONTROL', @LEVEL2TYPE=N'COLUMN',@LEVEL2NAME=N'FORMAT'
GO

EXEC SYS.SP_ADDEXTENDEDPROPERTY @NAME=N'MS_DESCRIPTION', @VALUE=N'是否必填' , @LEVEL0TYPE=N'SCHEMA',@LEVEL0NAME=N'DBO', @LEVEL1TYPE=N'TABLE',@LEVEL1NAME=N'MOBILECLIENT_STEPCONTROL', @LEVEL2TYPE=N'COLUMN',@LEVEL2NAME=N'ISWILLFILL'
GO

EXEC SYS.SP_ADDEXTENDEDPROPERTY @NAME=N'MS_DESCRIPTION', @VALUE=N'是否只读' , @LEVEL0TYPE=N'SCHEMA',@LEVEL0NAME=N'DBO', @LEVEL1TYPE=N'TABLE',@LEVEL1NAME=N'MOBILECLIENT_STEPCONTROL', @LEVEL2TYPE=N'COLUMN',@LEVEL2NAME=N'READONLY'
GO

EXEC SYS.SP_ADDEXTENDEDPROPERTY @NAME=N'MS_DESCRIPTION', @VALUE=N'外部链接' , @LEVEL0TYPE=N'SCHEMA',@LEVEL0NAME=N'DBO', @LEVEL1TYPE=N'TABLE',@LEVEL1NAME=N'MOBILECLIENT_STEPCONTROL', @LEVEL2TYPE=N'COLUMN',@LEVEL2NAME=N'EXTERNALLINKS'
GO

EXEC SYS.SP_ADDEXTENDEDPROPERTY @NAME=N'MS_DESCRIPTION', @VALUE=N'是否显示' , @LEVEL0TYPE=N'SCHEMA',@LEVEL0NAME=N'DBO', @LEVEL1TYPE=N'TABLE',@LEVEL1NAME=N'MOBILECLIENT_STEPCONTROL', @LEVEL2TYPE=N'COLUMN',@LEVEL2NAME=N'ISSHOW'
GO

EXEC SYS.SP_ADDEXTENDEDPROPERTY @NAME=N'MS_DESCRIPTION', @VALUE=N'显示顺序' , @LEVEL0TYPE=N'SCHEMA',@LEVEL0NAME=N'DBO', @LEVEL1TYPE=N'TABLE',@LEVEL1NAME=N'MOBILECLIENT_STEPCONTROL', @LEVEL2TYPE=N'COLUMN',@LEVEL2NAME=N'ORDERBY'
GO

EXEC SYS.SP_ADDEXTENDEDPROPERTY @NAME=N'MS_DESCRIPTION', @VALUE=N'数据源类型' , @LEVEL0TYPE=N'SCHEMA',@LEVEL0NAME=N'DBO', @LEVEL1TYPE=N'TABLE',@LEVEL1NAME=N'MOBILECLIENT_STEPCONTROL', @LEVEL2TYPE=N'COLUMN',@LEVEL2NAME=N'SOURCETYPE'
GO

EXEC SYS.SP_ADDEXTENDEDPROPERTY @NAME=N'MS_DESCRIPTION', @VALUE=N'连接字符串' , @LEVEL0TYPE=N'SCHEMA',@LEVEL0NAME=N'DBO', @LEVEL1TYPE=N'TABLE',@LEVEL1NAME=N'MOBILECLIENT_STEPCONTROL', @LEVEL2TYPE=N'COLUMN',@LEVEL2NAME=N'SOURCECONNECTIONSTRING'
GO

EXEC SYS.SP_ADDEXTENDEDPROPERTY @NAME=N'MS_DESCRIPTION', @VALUE=N'表名称' , @LEVEL0TYPE=N'SCHEMA',@LEVEL0NAME=N'DBO', @LEVEL1TYPE=N'TABLE',@LEVEL1NAME=N'MOBILECLIENT_STEPCONTROL', @LEVEL2TYPE=N'COLUMN',@LEVEL2NAME=N'SOURCETABLENAME'
GO

EXEC SYS.SP_ADDEXTENDEDPROPERTY @NAME=N'MS_DESCRIPTION', @VALUE=N'字段名称' , @LEVEL0TYPE=N'SCHEMA',@LEVEL0NAME=N'DBO', @LEVEL1TYPE=N'TABLE',@LEVEL1NAME=N'MOBILECLIENT_STEPCONTROL', @LEVEL2TYPE=N'COLUMN',@LEVEL2NAME=N'SOURCECOLUMNNAME'
GO

EXEC SYS.SP_ADDEXTENDEDPROPERTY @NAME=N'MS_DESCRIPTION', @VALUE=N'电子表格变量名称' , @LEVEL0TYPE=N'SCHEMA',@LEVEL0NAME=N'DBO', @LEVEL1TYPE=N'TABLE',@LEVEL1NAME=N'MOBILECLIENT_STEPCONTROL', @LEVEL2TYPE=N'COLUMN',@LEVEL2NAME=N'SOURCEVARIABLENAME'
GO

EXEC SYS.SP_ADDEXTENDEDPROPERTY @NAME=N'MS_DESCRIPTION', @VALUE=N'电子表格变量名称' , @LEVEL0TYPE=N'SCHEMA',@LEVEL0NAME=N'DBO', @LEVEL1TYPE=N'TABLE',@LEVEL1NAME=N'MOBILECLIENT_STEPCONTROL', @LEVEL2TYPE=N'COLUMN',@LEVEL2NAME=N'DESTVARIABLENAME'
GO


INSERT INTO  MOBILECLIENT_CONTROL (ID, CONTROLCNAME, CONTROLENAME, CONTROLNAME, ISACTION) VALUES (1, N'文本输入框', N'TEXTBOX', N'TEXTBOX', N'1         ');
INSERT INTO  MOBILECLIENT_CONTROL (ID, CONTROLCNAME, CONTROLENAME, CONTROLNAME, ISACTION) VALUES (2, N'复选按钮', N'CHECKBOX', N'CHECKBOX', N'1         ');
INSERT INTO  MOBILECLIENT_CONTROL (ID, CONTROLCNAME, CONTROLENAME, CONTROLNAME, ISACTION) VALUES (3, N'文本标签', N'LABEL', N'LABEL', N'1         ');
INSERT INTO  MOBILECLIENT_CONTROL (ID, CONTROLCNAME, CONTROLENAME, CONTROLNAME, ISACTION) VALUES (4, N'单选按钮', N'RADIOBUTTON', N'RADIOBUTTON', N'1         ');
INSERT INTO  MOBILECLIENT_CONTROL (ID, CONTROLCNAME, CONTROLENAME, CONTROLNAME, ISACTION) VALUES (5, N'表格', N'TABLE', N'GRIDVIEW', N'1         ');
INSERT INTO  MOBILECLIENT_CONTROL (ID, CONTROLCNAME, CONTROLENAME, CONTROLNAME, ISACTION) VALUES (6, N'下拉列表', N'DROPDOWNLIST', N'DROPDOWNLIST', N'1         ');
INSERT INTO  MOBILECLIENT_CONTROL (ID, CONTROLCNAME, CONTROLENAME, CONTROLNAME, ISACTION) VALUES (7, N'超链接', N'HYPERLINKS', N'HYPERLINKS', N'1         ');
INSERT INTO  MOBILECLIENT_CONTROL (ID, CONTROLCNAME, CONTROLENAME, CONTROLNAME, ISACTION) VALUES (8, N'文本域输入框', N'TEXTAREA', N'TEXTAREA', N'1         ');
INSERT INTO  MOBILECLIENT_CONTROL (ID, CONTROLCNAME, CONTROLENAME, CONTROLNAME, ISACTION) VALUES (9, N'按钮', N'BUTTON', N'BUTTON', N'1         ');
INSERT INTO  MOBILECLIENT_CONTROL (ID, CONTROLCNAME, CONTROLENAME, CONTROLNAME, ISACTION) VALUES (10, N'同意按钮', N'APPROVEBUTTON', N'APPROVEBUTTON', N'1         ');
INSERT INTO  MOBILECLIENT_CONTROL (ID, CONTROLCNAME, CONTROLENAME, CONTROLNAME, ISACTION) VALUES (11, N'退回按钮', N'RETURNBUTTON', N'RETURNBUTTON', N'1         ');
INSERT INTO  MOBILECLIENT_CONTROL (ID, CONTROLCNAME, CONTROLENAME, CONTROLNAME, ISACTION) VALUES (12, N'日期控件', N'DATE', N'LABEL', N'1         ');
INSERT INTO  MOBILECLIENT_CONTROL (ID, CONTROLCNAME, CONTROLENAME, CONTROLNAME, ISACTION) VALUES (13, N'提交按钮', N'SUBMITBUTTON', N'SUBMITBUTTON', N'1         ');
INSERT INTO  MOBILECLIENT_CONTROL (ID, CONTROLCNAME, CONTROLENAME, CONTROLNAME, ISACTION) VALUES (14, N'审批意见', N'APPROVALREMARK', N'TEXTBOX', N'1         ');

