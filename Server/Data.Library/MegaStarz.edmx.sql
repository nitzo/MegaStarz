
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 08/07/2011 23:30:57
-- Generated from EDMX file: D:\Projects\MegaStar\Server\Data.Library\MegaStarz.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [MegaStarz];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_ArtistSong]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Songs] DROP CONSTRAINT [FK_ArtistSong];
GO
IF OBJECT_ID(N'[dbo].[FK_StarSongStarLink]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SongStarLinks] DROP CONSTRAINT [FK_StarSongStarLink];
GO
IF OBJECT_ID(N'[dbo].[FK_SongSongStarLink]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SongStarLinks] DROP CONSTRAINT [FK_SongSongStarLink];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Stars]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Stars];
GO
IF OBJECT_ID(N'[dbo].[Songs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Songs];
GO
IF OBJECT_ID(N'[dbo].[Artists]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Artists];
GO
IF OBJECT_ID(N'[dbo].[SongStarLinks]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SongStarLinks];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Stars'
CREATE TABLE [dbo].[Stars] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(max)  NULL,
    [LastName] nvarchar(max)  NULL,
    [InsertDate] datetime  NOT NULL,
    [UpdateDate] datetime  NOT NULL,
    [FacebookId] nvarchar(max)  NOT NULL,
    [Locale] nvarchar(max)  NULL,
    [Gender] nvarchar(max)  NULL,
    [Birthday] datetime  NULL,
    [Email] nvarchar(max)  NULL
);
GO

-- Creating table 'Songs'
CREATE TABLE [dbo].[Songs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [InsertDate] datetime  NOT NULL,
    [ArtistId] int  NOT NULL,
    [Length] int  NOT NULL,
    [PlayUrl] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Artists'
CREATE TABLE [dbo].[Artists] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [InsertDate] datetime  NOT NULL
);
GO

-- Creating table 'SongStarLinks'
CREATE TABLE [dbo].[SongStarLinks] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [InsertDate] datetime  NOT NULL,
    [StarId] int  NOT NULL,
    [SongId] int  NOT NULL,
    [FileGuid] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Stars'
ALTER TABLE [dbo].[Stars]
ADD CONSTRAINT [PK_Stars]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Songs'
ALTER TABLE [dbo].[Songs]
ADD CONSTRAINT [PK_Songs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Artists'
ALTER TABLE [dbo].[Artists]
ADD CONSTRAINT [PK_Artists]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SongStarLinks'
ALTER TABLE [dbo].[SongStarLinks]
ADD CONSTRAINT [PK_SongStarLinks]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [ArtistId] in table 'Songs'
ALTER TABLE [dbo].[Songs]
ADD CONSTRAINT [FK_ArtistSong]
    FOREIGN KEY ([ArtistId])
    REFERENCES [dbo].[Artists]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ArtistSong'
CREATE INDEX [IX_FK_ArtistSong]
ON [dbo].[Songs]
    ([ArtistId]);
GO

-- Creating foreign key on [StarId] in table 'SongStarLinks'
ALTER TABLE [dbo].[SongStarLinks]
ADD CONSTRAINT [FK_StarSongStarLink]
    FOREIGN KEY ([StarId])
    REFERENCES [dbo].[Stars]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_StarSongStarLink'
CREATE INDEX [IX_FK_StarSongStarLink]
ON [dbo].[SongStarLinks]
    ([StarId]);
GO

-- Creating foreign key on [SongId] in table 'SongStarLinks'
ALTER TABLE [dbo].[SongStarLinks]
ADD CONSTRAINT [FK_SongSongStarLink]
    FOREIGN KEY ([SongId])
    REFERENCES [dbo].[Songs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SongSongStarLink'
CREATE INDEX [IX_FK_SongSongStarLink]
ON [dbo].[SongStarLinks]
    ([SongId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------