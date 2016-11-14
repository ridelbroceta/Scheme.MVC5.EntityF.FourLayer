
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/12/2016 18:25:55
-- Generated from EDMX file: C:\ridel\nettoGit\ModelExample\Apl.Data\Model\Model.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Database];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'roles'
CREATE TABLE [dbo].[roles] (
    [Id] int  NOT NULL,
    [Desc] nvarchar(20)  NOT NULL,
    [Level] tinyint  NOT NULL
);
GO

-- Creating table 'users'
CREATE TABLE [dbo].[users] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Email] nvarchar(100)  NOT NULL,
    [Name] nvarchar(100)  NOT NULL,
    [LastName] nvarchar(50)  NULL,
    [Pass] nvarchar(255)  NOT NULL,
    [Phone] nvarchar(20)  NOT NULL,
    [IsLocked] bit  NOT NULL,
    [IsConnected] bit  NOT NULL,
    [IsActive] bit  NOT NULL,
    [CountFailedPassAttempt] smallint  NOT NULL,
    [CountAfterPassAttempt] smallint  NOT NULL,
    [UserCreated] int  NULL,
    [DateCreated] datetime  NOT NULL,
    [DateLastLogin] datetime  NULL,
    [DateLastLockout] datetime  NULL,
    [UserLastLockout] int  NULL,
    [DateLastPassChange] datetime  NULL,
    [UserUpdated] int  NULL,
    [DateUpdated] datetime  NULL,
    [UserResetPass] int  NULL,
    [DateResetPass] datetime  NULL,
    [Cmnt] nvarchar(255)  NULL
);
GO

-- Creating table 'users_admin'
CREATE TABLE [dbo].[users_admin] (
    [Id] int  NOT NULL
);
GO

-- Creating table 'users_ouser'
CREATE TABLE [dbo].[users_ouser] (
    [Id] int  NOT NULL
);
GO

-- Creating table 'role_user'
CREATE TABLE [dbo].[role_user] (
    [Roles_Id] int  NOT NULL,
    [Users_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'roles'
ALTER TABLE [dbo].[roles]
ADD CONSTRAINT [PK_roles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'users'
ALTER TABLE [dbo].[users]
ADD CONSTRAINT [PK_users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'users_admin'
ALTER TABLE [dbo].[users_admin]
ADD CONSTRAINT [PK_users_admin]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'users_ouser'
ALTER TABLE [dbo].[users_ouser]
ADD CONSTRAINT [PK_users_ouser]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Roles_Id], [Users_Id] in table 'role_user'
ALTER TABLE [dbo].[role_user]
ADD CONSTRAINT [PK_role_user]
    PRIMARY KEY CLUSTERED ([Roles_Id], [Users_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Roles_Id] in table 'role_user'
ALTER TABLE [dbo].[role_user]
ADD CONSTRAINT [FK_role_user_role]
    FOREIGN KEY ([Roles_Id])
    REFERENCES [dbo].[roles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Users_Id] in table 'role_user'
ALTER TABLE [dbo].[role_user]
ADD CONSTRAINT [FK_role_user_user]
    FOREIGN KEY ([Users_Id])
    REFERENCES [dbo].[users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_role_user_user'
CREATE INDEX [IX_FK_role_user_user]
ON [dbo].[role_user]
    ([Users_Id]);
GO

-- Creating foreign key on [Id] in table 'users_admin'
ALTER TABLE [dbo].[users_admin]
ADD CONSTRAINT [FK_admin_inherits_user]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[users]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'users_ouser'
ALTER TABLE [dbo].[users_ouser]
ADD CONSTRAINT [FK_ouser_inherits_user]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[users]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------