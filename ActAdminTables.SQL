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
, [OhHold] bit  NOT NULL  
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
