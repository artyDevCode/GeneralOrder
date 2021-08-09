/*
    //SQL statement to drop and create the DB tables

ALTER TABLE [dbo].[GeneralOrderPortfolioModel] DROP CONSTRAINT [GeneralOrderImportHeader_GeneralOrderPortfolioModel_FK1]
ALTER TABLE [dbo].[GeneralOrderPortfolioModel] DROP CONSTRAINT [ParagraphModel_GeneralOrderPortfolioModel_FK1]
ALTER TABLE [dbo].[GeneralOrderActCommentModel] DROP CONSTRAINT [GeneralOrderActModel_GeneralOrderActCommentModel_FK1]
ALTER TABLE [dbo].[GeneralOrderActCommentModel] DROP CONSTRAINT [ParagraphModel_GeneralOrderActCommentModel_FK1]
ALTER TABLE [dbo].[GeneralOrderActModel] DROP CONSTRAINT [ParagraphModel_GeneralOrderActModel_FK1] 
ALTER TABLE [dbo].[GeneralOrderActModel] DROP CONSTRAINT [GeneralOrderPortfolioModel_GeneralOrderActModel_FK1] 
ALTER TABLE [dbo].[ParagraphModel] DROP CONSTRAINT [ParagraphModelType_ParagraphModel_FK1] 
GO
ALTER TABLE [dbo].[GeneralOrderImportHeader] DROP CONSTRAINT [GeneralOrderImportHeader_PK]
ALTER TABLE [dbo].[GeneralOrderPortfolioModel] DROP CONSTRAINT [GeneralOrderPortfolioModel_PK]
ALTER TABLE [dbo].[GeneralOrderActCommentModel] DROP CONSTRAINT [GeneralOrderActCommentModel_PK]
ALTER TABLE [dbo].[GeneralOrderActModel] DROP CONSTRAINT [GeneralOrderActModel_PK]
ALTER TABLE [dbo].[ParagraphModel] DROP CONSTRAINT [ParagraphModel_PK]
GO
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GeneralOrderImportHeader]') AND type in (N'U'))
    DROP TABLE [dbo].[GeneralOrderImportHeader]
GO
CREATE TABLE [dbo].[GeneralOrderImportHeader] (
[GeneralOrderImportHeaderID] int identity  NOT NULL  
, [GeneralOrderImportFile] nvarchar(max)  NOT NULL  
, [GeneralOrderEffectiveDate] date  NOT NULL  
, [GeneralOrderFull] bit  NOT NULL  
, [OriginalXML] nvarchar(max)  NOT NULL  
, [ModifiedXML] nvarchar(max)  NOT NULL  
, [GeneralOrderImportDatetime] datetime  NOT NULL  
, [GeneralOrderProcesseddatetime] datetime  NULL  
, [MarginLeft] float  NOT NULL  
, [MarginRight] float  NOT NULL  
, [DefaultTabStop] float  NOT NULL  
, [PageHeaderMargin] float  NOT NULL  
, [PageFooterMargin] float  NOT NULL  
, [IndentationSpacing] bit  NOT NULL  
)
GO

ALTER TABLE [dbo].[GeneralOrderImportHeader] ADD CONSTRAINT [GeneralOrderImportHeader_PK] PRIMARY KEY CLUSTERED (
[GeneralOrderImportHeaderID]
)
GO
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GeneralOrderPortfolioModel]') AND type in (N'U'))
    DROP TABLE [dbo].[GeneralOrderPortfolioModel]
GO
CREATE TABLE [dbo].[GeneralOrderPortfolioModel] (
    [GeneralOrderPortfolioModelID] INT            IDENTITY (1, 1) NOT NULL,
    [GeneralOrderImportHeaderID]   INT            NOT NULL,
    [GeneralOrderPortfolioName]    NVARCHAR (100) NOT NULL,
    [GeneralOrderPortfolioID]      INT            NULL,
    [ParagraphModelID]             INT            NOT NULL
);


ALTER TABLE [dbo].[GeneralOrderPortfolioModel] ADD CONSTRAINT [GeneralOrderPortfolioModel_PK] PRIMARY KEY CLUSTERED (
[GeneralOrderPortfolioModelID]
)
GO
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GeneralOrderActCommentModel]') AND type in (N'U'))
    DROP TABLE [dbo].[GeneralOrderActCommentModel]
GO
CREATE TABLE [dbo].[GeneralOrderActCommentModel] (
[ParagraphModelID] int  NOT NULL  
, [GeneralOrderActModelID] int  NOT NULL  
, [GeneralOrderActCommentModelID] int identity  NOT NULL  
, [GeneralOrderActComment] nvarchar(max)  NULL  
)
GO

ALTER TABLE [dbo].[GeneralOrderActCommentModel] ADD CONSTRAINT [GeneralOrderActCommentModel_PK] PRIMARY KEY CLUSTERED (
[GeneralOrderActCommentModelID]
)
GO
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GeneralOrderActModel]') AND type in (N'U'))
    DROP TABLE [dbo].[GeneralOrderActModel]
GO
CREATE TABLE [dbo].[GeneralOrderActModel] (
[GeneralOrderActModelID] int identity  NOT NULL  
, [GeneralOrderActTitle] nvarchar(255)  NOT NULL  
, [ILDNumber] int  NULL  
, [GeneralOrderPortfolioModelID] int  NOT NULL  
, [ParagraphModelID] int  NOT NULL  
, [EffectiveDate] date  NULL  
, [DepartmentPortfolioIDs] nvarchar(255)  NULL  
, [IsExcept] BIT NOT NULL DEFAULT 0
)
GO



ALTER TABLE [dbo].[GeneralOrderActModel] ADD CONSTRAINT [GeneralOrderActModel_PK] PRIMARY KEY CLUSTERED (
[GeneralOrderActModelID]
)
GO
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ParagraphModel]') AND type in (N'U'))
    DROP TABLE [dbo].[ParagraphModel]
GO
CREATE TABLE [dbo].[ParagraphModel] (
[ParagraphModelTypeID] int  NOT NULL  
, [ParagraphModelID] int identity  NOT NULL  
, [FontBold] bit  NOT NULL  
, [BulletChar] char(1)  NULL  
, [BulletASCII] smallint  NOT NULL  
, [TabStopPosition] float  NOT NULL  
, [PageBreakBefore] bit  NOT NULL  
, [ListSymbolFont] nvarchar(50)  NULL  
, [IndentationLeft] float  NOT NULL  
, [IndentationRight] float  NOT NULL  
, [IndentationBy] float  NOT NULL  
, [TabHangingIndent] smallint  NOT NULL  
, [AlignmentType] tinyint  NOT NULL  
)
GO

ALTER TABLE [dbo].[ParagraphModel] ADD CONSTRAINT [ParagraphModel_PK] PRIMARY KEY CLUSTERED (
[ParagraphModelID]
)
GO
GO

ALTER TABLE [dbo].[GeneralOrderPortfolioModel] WITH CHECK ADD CONSTRAINT [GeneralOrderImportHeader_GeneralOrderPortfolioModel_FK1] FOREIGN KEY (
[GeneralOrderImportHeaderID]
)
REFERENCES [dbo].[GeneralOrderImportHeader] (
[GeneralOrderImportHeaderID]
)
ALTER TABLE [dbo].[GeneralOrderPortfolioModel] WITH CHECK ADD CONSTRAINT [ParagraphModel_GeneralOrderPortfolioModel_FK1] FOREIGN KEY (
[ParagraphModelID]
)
REFERENCES [dbo].[ParagraphModel] (
[ParagraphModelID]
)
GO

ALTER TABLE [dbo].[GeneralOrderActCommentModel] WITH CHECK ADD CONSTRAINT [GeneralOrderActModel_GeneralOrderActCommentModel_FK1] FOREIGN KEY (
[GeneralOrderActModelID]
)
REFERENCES [dbo].[GeneralOrderActModel] (
[GeneralOrderActModelID]
)
ALTER TABLE [dbo].[GeneralOrderActCommentModel] WITH CHECK ADD CONSTRAINT [ParagraphModel_GeneralOrderActCommentModel_FK1] FOREIGN KEY (
[ParagraphModelID]
)
REFERENCES [dbo].[ParagraphModel] (
[ParagraphModelID]
)
GO

ALTER TABLE [dbo].[GeneralOrderActModel] WITH CHECK ADD CONSTRAINT [ParagraphModel_GeneralOrderActModel_FK1] FOREIGN KEY (
[ParagraphModelID]
)
REFERENCES [dbo].[ParagraphModel] (
[ParagraphModelID]
)
ALTER TABLE [dbo].[GeneralOrderActModel] WITH CHECK ADD CONSTRAINT [GeneralOrderPortfolioModel_GeneralOrderActModel_FK1] FOREIGN KEY (
[GeneralOrderPortfolioModelID]
)
REFERENCES [dbo].[GeneralOrderPortfolioModel] (
[GeneralOrderPortfolioModelID]
)
GO

ALTER TABLE [dbo].[ParagraphModel] WITH CHECK ADD CONSTRAINT [ParagraphModelType_ParagraphModel_FK1] FOREIGN KEY (
[ParagraphModelTypeID]
)
REFERENCES [dbo].[ParagraphModelType] (
[ParagraphModelTypeID]
)
GO
GO

ALTER TABLE [dbo].[ActAdministration] DROP CONSTRAINT [ILD_CurrentDocuments_ActAdministration_FK1] 
ALTER TABLE [dbo].[ActAdminComment] DROP CONSTRAINT [ActAdministration_ActAdminComment_FK1] 
ALTER TABLE [dbo].[ActAdministration] DROP CONSTRAINT [DepartmentPortfolio_ActAdministration_FK1] 
ALTER TABLE [dbo].[ActAdminComment] DROP CONSTRAINT [ActAdminComment_PK] 
ALTER TABLE [dbo].[ActAdministration] DROP CONSTRAINT [ActAdministration_PK] 
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ActAdministration]') AND type in (N'U'))
    DROP TABLE [dbo].[ActAdministration]
GO
CREATE TABLE [dbo].[ActAdministration] (
[ActAdministrationID] int identity  NOT NULL  
, [ILDNumber] int  NOT NULL  
, [DepartmentPortfolioID] int  NOT NULL  
, [ActAdministrationComment] ntext  NULL  
, [IsCurrent] bit  NOT NULL  
, [IsExcept] bit  NOT NULL  
, [PendingEditType] smallint  NOT NULL  
, [PendingEditID] int  NULL  
, [OnHold] bit  NOT NULL  
, [StartDate] date  NOT NULL  
, [EndDate] date  NULL  
)
GO

ALTER TABLE [dbo].[ActAdministration] ADD CONSTRAINT [ActAdministration_PK] PRIMARY KEY CLUSTERED (
[ActAdministrationID]
)
GO
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ActAdminComment]') AND type in (N'U'))
    DROP TABLE [dbo].[ActAdminComment]
GO
CREATE TABLE [dbo].[ActAdminComment] (
[ActAdminCommentID] int identity  NOT NULL  
, [ActAdminComment] nvarchar(max)  NOT NULL  
, [FontBold] bit  NOT NULL  
, [BulletChar] char(1)  NULL  
, [BulletASCII] smallint  NOT NULL  
, [TabStopPosition] float  NOT NULL  
, [PageBreakBefore] bit  NOT NULL  
, [ListSymbolFont] nvarchar(50)  NULL  
, [IndentationLeft] float  NOT NULL  
, [IndentationRight] float  NOT NULL  
, [IndentationBy] float  NOT NULL  
, [TabHangingIndent] smallint  NOT NULL  
, [AlignmentType] tinyint  NOT NULL  
, [ActAdministrationID] int  NOT NULL  
)
GO

ALTER TABLE [dbo].[ActAdminComment] ADD CONSTRAINT [ActAdminComment_PK] PRIMARY KEY CLUSTERED (
[ActAdminCommentID]
)
GO
GO



ALTER TABLE [dbo].[ActAdministration] WITH CHECK ADD CONSTRAINT [ILD_CurrentDocuments_ActAdministration_FK1] FOREIGN KEY (
[ILDNumber]
)
REFERENCES [dbo].[ILD_CurrentDocuments] (
[ILDNumber]
)
ALTER TABLE [dbo].[ActAdministration] WITH CHECK ADD CONSTRAINT [DepartmentPortfolio_ActAdministration_FK1] FOREIGN KEY (
[DepartmentPortfolioID]
)
REFERENCES [dbo].[DepartmentPortfolio] (
[DepartmentPortfolioID]
)
GO

ALTER TABLE [dbo].[ActAdminComment] WITH CHECK ADD CONSTRAINT [ActAdministration_ActAdminComment_FK1] FOREIGN KEY (
[ActAdministrationID]
)
REFERENCES [dbo].[ActAdministration] (
[ActAdministrationID]
)
GO


*/
