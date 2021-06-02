
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 06/02/2021 18:57:01
-- Generated from EDMX file: C:\Users\SUNIL KUMAR\source\repos\MVCFilters\MVCFilters\chetuAppModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [chetuAutho];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK__forgetPas__useri__239E4DCF]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[forgetPassword] DROP CONSTRAINT [FK__forgetPas__useri__239E4DCF];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[autho]', 'U') IS NOT NULL
    DROP TABLE [dbo].[autho];
GO
IF OBJECT_ID(N'[dbo].[forgetPassword]', 'U') IS NOT NULL
    DROP TABLE [dbo].[forgetPassword];
GO
IF OBJECT_ID(N'[dbo].[products]', 'U') IS NOT NULL
    DROP TABLE [dbo].[products];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'products'
CREATE TABLE [dbo].[products] (
    [id] int IDENTITY(1,1) NOT NULL,
    [title] varchar(100)  NULL,
    [description] varchar(250)  NULL,
    [quantity] int  NULL,
    [price] decimal(19,4)  NULL,
    [imageUrl] varchar(max)  NULL,
    [createOn] datetime  NULL
);
GO

-- Creating table 'authoes'
CREATE TABLE [dbo].[authoes] (
    [id] int IDENTITY(1,1) NOT NULL,
    [email] varchar(200)  NULL,
    [password] varchar(max)  NULL,
    [name] varchar(100)  NULL,
    [mobile] varchar(20)  NULL
);
GO

-- Creating table 'forgetPasswords'
CREATE TABLE [dbo].[forgetPasswords] (
    [id] int IDENTITY(1,1) NOT NULL,
    [userid] int  NULL,
    [requestcode] varchar(100)  NULL,
    [requesttime] datetime  NULL
);
GO

-- Creating table 'ExceptionLoggers'
CREATE TABLE [dbo].[ExceptionLoggers] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ExceptionMessage] nvarchar(max)  NOT NULL,
    [ExceptionStackTrack] nvarchar(max)  NOT NULL,
    [ControllerName] nvarchar(max)  NOT NULL,
    [ActionName] nvarchar(max)  NOT NULL,
    [ExceptionLogTime] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [id] in table 'products'
ALTER TABLE [dbo].[products]
ADD CONSTRAINT [PK_products]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'authoes'
ALTER TABLE [dbo].[authoes]
ADD CONSTRAINT [PK_authoes]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'forgetPasswords'
ALTER TABLE [dbo].[forgetPasswords]
ADD CONSTRAINT [PK_forgetPasswords]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [Id] in table 'ExceptionLoggers'
ALTER TABLE [dbo].[ExceptionLoggers]
ADD CONSTRAINT [PK_ExceptionLoggers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [userid] in table 'forgetPasswords'
ALTER TABLE [dbo].[forgetPasswords]
ADD CONSTRAINT [FK__forgetPas__useri__239E4DCF]
    FOREIGN KEY ([userid])
    REFERENCES [dbo].[authoes]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__forgetPas__useri__239E4DCF'
CREATE INDEX [IX_FK__forgetPas__useri__239E4DCF]
ON [dbo].[forgetPasswords]
    ([userid]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------