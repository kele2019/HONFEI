
CREATE TABLE MOBILECLIENT_CLASSIFICATION(
	ID NUMBER  NOT NULL,
	CATEGORYCNAME NVARCHAR2(156) NULL,
	CATEGORYENAME NVARCHAR2(156) NULL,
	PROCESSNAME NVARCHAR2(156) NULL,
	ISACTION NCHAR(10) NULL
	
	);


CREATE TABLE MOBILECLIENT_CONTROL(
	ID NUMBER  NOT NULL,
	CONTROLCNAME NVARCHAR2(156) NULL,
	CONTROLENAME NVARCHAR2(156) NULL,
	CONTROLNAME NVARCHAR2(156) NULL,
	ISACTION NCHAR(10) NULL);

CREATE TABLE MOBILECLIENT_PROCESS(
	ID NUMBER  NOT NULL,
	PROCESSNAME NVARCHAR2(156) NULL,
	LOGO NVARCHAR2(256) NULL,
	CREATETIME DATE NULL,
	UPDATETIME DATE NULL,
	ISCREATEPAGE NVARCHAR2(50) NULL);

CREATE TABLE MOBILECLIENT_STEP(
	ID NUMBER  NOT NULL,
	FK_ID NUMBER NULL,
	STEPNAME NVARCHAR2(156) NULL,
	STEPCNAME NVARCHAR2(156) NULL,
	STEPENAME NVARCHAR2(156) NULL
	);

CREATE TABLE MOBILECLIENT_STEPCONTROL(
	ID NUMBER  NOT NULL,
	FK_ID NUMBER NULL,
	COLUMNNAME NVARCHAR2(156) NULL,
	CONTROLID NUMBER NULL,
	FORMAT NVARCHAR2(1000) NULL,
	ISWILLFILL NVARCHAR2(50) NULL,
	READONLY NVARCHAR2(50) NULL,
	EXTERNALLINKS NVARCHAR2(1000) NULL,
	ISSHOW NVARCHAR2(50) NULL,
	ORDERBY NVARCHAR2(256) NULL,
	SOURCETYPE NVARCHAR2(50) NULL,
	SOURCECONNECTIONSTRING NVARCHAR2(256) NULL,
	SOURCETABLENAME NVARCHAR2(256) NULL,
	SOURCECOLUMNNAME NVARCHAR2(256) NULL,
	SOURCEWHERE NVARCHAR2(1500) NULL,
	SOURCEVARIABLENAME NVARCHAR2(50) NULL,
	ISMASTERTABLE NVARCHAR2(50) NULL,
	ISSUBLIST NVARCHAR2(50) NULL,
	DESTTYPE NVARCHAR2(50) NULL,
	DESTCONNECTIONSTRING NVARCHAR2(256) NULL,
	DESTTABLENAME NVARCHAR2(256) NULL,
	DESTCOLUMNNAME NVARCHAR2(50) NULL,
	DESTVARIABLENAME NVARCHAR2(50) NULL);




INSERT INTO  MOBILECLIENT_CONTROL (ID, CONTROLCNAME, CONTROLENAME, CONTROLNAME, ISACTION) VALUES (1, N'�ı������', N'TEXTBOX', N'TEXTBOX', N'1         ');
INSERT INTO  MOBILECLIENT_CONTROL (ID, CONTROLCNAME, CONTROLENAME, CONTROLNAME, ISACTION) VALUES (2, N'��ѡ��ť', N'CHECKBOX', N'CHECKBOX', N'1         ');
INSERT INTO  MOBILECLIENT_CONTROL (ID, CONTROLCNAME, CONTROLENAME, CONTROLNAME, ISACTION) VALUES (3, N'�ı���ǩ', N'LABEL', N'LABEL', N'1         ');
INSERT INTO  MOBILECLIENT_CONTROL (ID, CONTROLCNAME, CONTROLENAME, CONTROLNAME, ISACTION) VALUES (4, N'��ѡ��ť', N'RADIOBUTTON', N'RADIOBUTTON', N'1         ');
INSERT INTO  MOBILECLIENT_CONTROL (ID, CONTROLCNAME, CONTROLENAME, CONTROLNAME, ISACTION) VALUES (5, N'����', N'TABLE', N'GRIDVIEW', N'1         ');
INSERT INTO  MOBILECLIENT_CONTROL (ID, CONTROLCNAME, CONTROLENAME, CONTROLNAME, ISACTION) VALUES (6, N'�����б�', N'DROPDOWNLIST', N'DROPDOWNLIST', N'1         ');
INSERT INTO  MOBILECLIENT_CONTROL (ID, CONTROLCNAME, CONTROLENAME, CONTROLNAME, ISACTION) VALUES (7, N'������', N'HYPERLINKS', N'HYPERLINKS', N'1         ');
INSERT INTO  MOBILECLIENT_CONTROL (ID, CONTROLCNAME, CONTROLENAME, CONTROLNAME, ISACTION) VALUES (8, N'�ı��������', N'TEXTAREA', N'TEXTAREA', N'1         ');
INSERT INTO  MOBILECLIENT_CONTROL (ID, CONTROLCNAME, CONTROLENAME, CONTROLNAME, ISACTION) VALUES (9, N'��ť', N'BUTTON', N'BUTTON', N'1         ');
INSERT INTO  MOBILECLIENT_CONTROL (ID, CONTROLCNAME, CONTROLENAME, CONTROLNAME, ISACTION) VALUES (10, N'ͬ�ⰴť', N'APPROVEBUTTON', N'APPROVEBUTTON', N'1         ');
INSERT INTO  MOBILECLIENT_CONTROL (ID, CONTROLCNAME, CONTROLENAME, CONTROLNAME, ISACTION) VALUES (11, N'�˻ذ�ť', N'RETURNBUTTON', N'RETURNBUTTON', N'1         ');
INSERT INTO  MOBILECLIENT_CONTROL (ID, CONTROLCNAME, CONTROLENAME, CONTROLNAME, ISACTION) VALUES (12, N'���ڿؼ�', N'DATE', N'LABEL', N'1         ');
INSERT INTO  MOBILECLIENT_CONTROL (ID, CONTROLCNAME, CONTROLENAME, CONTROLNAME, ISACTION) VALUES (13, N'�ύ��ť', N'SUBMITBUTTON', N'SUBMITBUTTON', N'1         ');
INSERT INTO  MOBILECLIENT_CONTROL (ID, CONTROLCNAME, CONTROLENAME, CONTROLNAME, ISACTION) VALUES (14, N'�������', N'APPROVALREMARK', N'TEXTBOX', N'1         ');