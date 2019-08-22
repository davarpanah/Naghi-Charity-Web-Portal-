
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 06/07/2017 09:53:34
-- Generated from EDMX file: C:\Users\Plus\Documents\Visual Studio 2013\Projects\NaghiInistitutePro\NaghiInistitutePro\NaghiEDM.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [NaghiInistitute];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_CityUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserSet] DROP CONSTRAINT [FK_CityUser];
GO
IF OBJECT_ID(N'[dbo].[FK_RelationTypeHelper]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserSet_Donator] DROP CONSTRAINT [FK_RelationTypeHelper];
GO
IF OBJECT_ID(N'[dbo].[FK_ContactTypeDoneContact]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DoneContactSet] DROP CONSTRAINT [FK_ContactTypeDoneContact];
GO
IF OBJECT_ID(N'[dbo].[FK_DonatorDoneContact]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DoneContactSet] DROP CONSTRAINT [FK_DonatorDoneContact];
GO
IF OBJECT_ID(N'[dbo].[FK_UserGroupUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserSet] DROP CONSTRAINT [FK_UserGroupUser];
GO
IF OBJECT_ID(N'[dbo].[FK_RoleAssignedRole]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AssignedRoleSet] DROP CONSTRAINT [FK_RoleAssignedRole];
GO
IF OBJECT_ID(N'[dbo].[FK_UserLog]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[LogSet] DROP CONSTRAINT [FK_UserLog];
GO
IF OBJECT_ID(N'[dbo].[FK_RoleLog]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[LogSet] DROP CONSTRAINT [FK_RoleLog];
GO
IF OBJECT_ID(N'[dbo].[FK_GoodGroupGood]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[GoodSet] DROP CONSTRAINT [FK_GoodGroupGood];
GO
IF OBJECT_ID(N'[dbo].[FK_DonatorPayment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DonateSet] DROP CONSTRAINT [FK_DonatorPayment];
GO
IF OBJECT_ID(N'[dbo].[FK_DonateDonateRow]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DonateRowSet] DROP CONSTRAINT [FK_DonateDonateRow];
GO
IF OBJECT_ID(N'[dbo].[FK_GoodHelp_Row]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DonateRowSet] DROP CONSTRAINT [FK_GoodHelp_Row];
GO
IF OBJECT_ID(N'[dbo].[FK_PaymentTypeHelpRow]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DonateRowSet] DROP CONSTRAINT [FK_PaymentTypeHelpRow];
GO
IF OBJECT_ID(N'[dbo].[FK_UserGroupAssignedRole]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AssignedRoleSet] DROP CONSTRAINT [FK_UserGroupAssignedRole];
GO
IF OBJECT_ID(N'[dbo].[FK_UserDailyPoints]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DailyPointsSet] DROP CONSTRAINT [FK_UserDailyPoints];
GO
IF OBJECT_ID(N'[dbo].[FK_UserNews]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[NewsSet] DROP CONSTRAINT [FK_UserNews];
GO
IF OBJECT_ID(N'[dbo].[FK_GallaryPhoto]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PhotoSet] DROP CONSTRAINT [FK_GallaryPhoto];
GO
IF OBJECT_ID(N'[dbo].[FK_NewsPhoto]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PhotoSet] DROP CONSTRAINT [FK_NewsPhoto];
GO
IF OBJECT_ID(N'[dbo].[FK_CountryCity]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CitySet] DROP CONSTRAINT [FK_CountryCity];
GO
IF OBJECT_ID(N'[dbo].[FK_Donator_inherits_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserSet_Donator] DROP CONSTRAINT [FK_Donator_inherits_User];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[UserSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserSet];
GO
IF OBJECT_ID(N'[dbo].[CountrySet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CountrySet];
GO
IF OBJECT_ID(N'[dbo].[CitySet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CitySet];
GO
IF OBJECT_ID(N'[dbo].[ContactTypeSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ContactTypeSet];
GO
IF OBJECT_ID(N'[dbo].[DoneContactSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DoneContactSet];
GO
IF OBJECT_ID(N'[dbo].[UserGroupSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserGroupSet];
GO
IF OBJECT_ID(N'[dbo].[RoleSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RoleSet];
GO
IF OBJECT_ID(N'[dbo].[AssignedRoleSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AssignedRoleSet];
GO
IF OBJECT_ID(N'[dbo].[LogSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LogSet];
GO
IF OBJECT_ID(N'[dbo].[GoodGroupSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[GoodGroupSet];
GO
IF OBJECT_ID(N'[dbo].[GoodSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[GoodSet];
GO
IF OBJECT_ID(N'[dbo].[DonateSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DonateSet];
GO
IF OBJECT_ID(N'[dbo].[DonateRowSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DonateRowSet];
GO
IF OBJECT_ID(N'[dbo].[PaymentTypeSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PaymentTypeSet];
GO
IF OBJECT_ID(N'[dbo].[InistituteSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[InistituteSet];
GO
IF OBJECT_ID(N'[dbo].[DailyPointsSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DailyPointsSet];
GO
IF OBJECT_ID(N'[dbo].[NewsSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[NewsSet];
GO
IF OBJECT_ID(N'[dbo].[GallarySet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[GallarySet];
GO
IF OBJECT_ID(N'[dbo].[PhotoSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PhotoSet];
GO
IF OBJECT_ID(N'[dbo].[UserSet_Donator]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserSet_Donator];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'UserSet'
CREATE TABLE [dbo].[UserSet] (
    [Us_ID] int IDENTITY(1,1) NOT NULL,
    [Us_Name] nvarchar(max)  NOT NULL,
    [Us_LName] nvarchar(max)  NOT NULL,
    [Us_Active] bit  NOT NULL,
    [Us_BirthDay] datetime  NULL,
    [Us_Phone] bigint  NULL,
    [Us_Mob] bigint  NULL,
    [Us_HomeAddress] nvarchar(max)  NULL,
    [Us_WorkAddress] nvarchar(max)  NULL,
    [Us_Email] nvarchar(max)  NULL,
    [Us_Reg_Date] datetime  NOT NULL,
    [City_Ct_Id] smallint  NOT NULL,
    [UserGroup_Gr_Id] tinyint  NOT NULL
);
GO

-- Creating table 'CountrySet'
CREATE TABLE [dbo].[CountrySet] (
    [Co_Id] tinyint IDENTITY(1,1) NOT NULL,
    [Co_Name] nvarchar(max)  NOT NULL,
    [Co_Latin] nvarchar(max)  NULL,
    [Co_Tel_Code] tinyint  NULL
);
GO

-- Creating table 'CitySet'
CREATE TABLE [dbo].[CitySet] (
    [Ct_Id] smallint IDENTITY(1,1) NOT NULL,
    [Ct_Name] nvarchar(max)  NOT NULL,
    [Ct_Latin] nvarchar(max)  NULL,
    [Ct_Tel_Code] tinyint  NULL,
    [State_St_Id] smallint  NOT NULL
);
GO

-- Creating table 'ContactTypeSet'
CREATE TABLE [dbo].[ContactTypeSet] (
    [CT_Code] tinyint IDENTITY(1,1) NOT NULL,
    [CT_Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'DoneContactSet'
CREATE TABLE [dbo].[DoneContactSet] (
    [DC_Id] int IDENTITY(1,1) NOT NULL,
    [DC_Date] nvarchar(max)  NOT NULL,
    [ContactType_CT_Code] tinyint  NOT NULL,
    [Donator_Us_ID] int  NOT NULL
);
GO

-- Creating table 'UserGroupSet'
CREATE TABLE [dbo].[UserGroupSet] (
    [Gr_Id] tinyint IDENTITY(1,1) NOT NULL,
    [Gr_Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'RoleSet'
CREATE TABLE [dbo].[RoleSet] (
    [Rol_Id] tinyint IDENTITY(1,1) NOT NULL,
    [Ro_Name] nvarchar(max)  NOT NULL,
    [Ro_Active] bit  NOT NULL
);
GO

-- Creating table 'AssignedRoleSet'
CREATE TABLE [dbo].[AssignedRoleSet] (
    [As_Active] bit  NOT NULL,
    [As_Id] smallint IDENTITY(1,1) NOT NULL,
    [Role_Rol_Id] tinyint  NOT NULL,
    [UserGroup_Gr_Id] tinyint  NOT NULL
);
GO

-- Creating table 'LogSet'
CREATE TABLE [dbo].[LogSet] (
    [Lg_Id] bigint IDENTITY(1,1) NOT NULL,
    [Lg_Date] datetime  NOT NULL,
    [User_Us_ID] int  NOT NULL,
    [Role_Rol_Id] tinyint  NOT NULL
);
GO

-- Creating table 'GoodGroupSet'
CREATE TABLE [dbo].[GoodGroupSet] (
    [GG_Id] tinyint IDENTITY(1,1) NOT NULL,
    [GG_Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'GoodSet'
CREATE TABLE [dbo].[GoodSet] (
    [Gd_Id] smallint IDENTITY(1,1) NOT NULL,
    [Gd_Name] nvarchar(max)  NOT NULL,
    [Gd_RequiredNo] smallint  NOT NULL,
    [GoodGroup_GG_Id] tinyint  NOT NULL
);
GO

-- Creating table 'DonateSet'
CREATE TABLE [dbo].[DonateSet] (
    [Do_Id] int IDENTITY(1,1) NOT NULL,
    [Do_Date] datetime  NOT NULL,
    [Do_Type] tinyint  NOT NULL,
    [Donator_Us_ID] int  NOT NULL
);
GO

-- Creating table 'DonateRowSet'
CREATE TABLE [dbo].[DonateRowSet] (
    [DR_Id] tinyint IDENTITY(1,1) NOT NULL,
    [DR_Amount] smallint  NOT NULL,
    [Donate_Do_Id] int  NOT NULL,
    [Good_Gd_Id] smallint  NULL,
    [PaymentType_PT_Id] tinyint  NULL
);
GO

-- Creating table 'PaymentTypeSet'
CREATE TABLE [dbo].[PaymentTypeSet] (
    [PT_Id] tinyint IDENTITY(1,1) NOT NULL,
    [PT_Description] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'InistituteSet'
CREATE TABLE [dbo].[InistituteSet] (
    [In_Id] tinyint IDENTITY(1,1) NOT NULL,
    [In_Name] nvarchar(max)  NOT NULL,
    [In_BankCard] nvarchar(max)  NULL,
    [In_BankAccount] bigint  NULL,
    [In_Address] nvarchar(max)  NULL,
    [In_Mng_Mob] bigint  NULL,
    [In_Tel] nvarchar(max)  NULL,
    [In_Fax] nvarchar(max)  NULL,
    [In_Email] nvarchar(max)  NULL,
    [In_WbAddress] nvarchar(max)  NULL
);
GO

-- Creating table 'DailyPointsSet'
CREATE TABLE [dbo].[DailyPointsSet] (
    [DP_Id] smallint IDENTITY(1,1) NOT NULL,
    [DP_Sayer] nvarchar(max)  NULL,
    [DP_Point] nvarchar(max)  NOT NULL,
    [User_Us_ID] int  NULL
);
GO

-- Creating table 'NewsSet'
CREATE TABLE [dbo].[NewsSet] (
    [Nw_Id] int IDENTITY(1,1) NOT NULL,
    [Nw_Description] nvarchar(max)  NOT NULL,
    [Nw_Title] nvarchar(max)  NOT NULL,
    [Nw_Type] tinyint  NOT NULL,
    [User_Us_ID] int  NULL
);
GO

-- Creating table 'GallarySet'
CREATE TABLE [dbo].[GallarySet] (
    [Ga_Id] tinyint IDENTITY(1,1) NOT NULL,
    [Ga_Title] nvarchar(max)  NOT NULL,
    [Ga_Description] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'PhotoSet'
CREATE TABLE [dbo].[PhotoSet] (
    [Ph_Id] int IDENTITY(1,1) NOT NULL,
    [Ph_Description] nvarchar(max)  NOT NULL,
    [Ph_Title] nvarchar(max)  NOT NULL,
    [Ph_Address] nvarchar(max)  NOT NULL,
    [Ph_Showable] bit  NOT NULL,
    [Gallary_Ga_Id] tinyint  NULL,
    [News_Nw_Id] int  NULL
);
GO

-- Creating table 'StateSet'
CREATE TABLE [dbo].[StateSet] (
    [St_Id] smallint IDENTITY(1,1) NOT NULL,
    [St_Name] nvarchar(max)  NOT NULL,
    [St_Latin] nvarchar(max)  NULL,
    [Country_Co_Id] tinyint  NOT NULL
);
GO

-- Creating table 'UserSet_Donator'
CREATE TABLE [dbo].[UserSet_Donator] (
    [Dr_HelpDay] tinyint  NOT NULL,
    [Us_ID] int  NOT NULL,
    [PreferReType_CT_Code] tinyint  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Us_ID] in table 'UserSet'
ALTER TABLE [dbo].[UserSet]
ADD CONSTRAINT [PK_UserSet]
    PRIMARY KEY CLUSTERED ([Us_ID] ASC);
GO

-- Creating primary key on [Co_Id] in table 'CountrySet'
ALTER TABLE [dbo].[CountrySet]
ADD CONSTRAINT [PK_CountrySet]
    PRIMARY KEY CLUSTERED ([Co_Id] ASC);
GO

-- Creating primary key on [Ct_Id] in table 'CitySet'
ALTER TABLE [dbo].[CitySet]
ADD CONSTRAINT [PK_CitySet]
    PRIMARY KEY CLUSTERED ([Ct_Id] ASC);
GO

-- Creating primary key on [CT_Code] in table 'ContactTypeSet'
ALTER TABLE [dbo].[ContactTypeSet]
ADD CONSTRAINT [PK_ContactTypeSet]
    PRIMARY KEY CLUSTERED ([CT_Code] ASC);
GO

-- Creating primary key on [DC_Id] in table 'DoneContactSet'
ALTER TABLE [dbo].[DoneContactSet]
ADD CONSTRAINT [PK_DoneContactSet]
    PRIMARY KEY CLUSTERED ([DC_Id] ASC);
GO

-- Creating primary key on [Gr_Id] in table 'UserGroupSet'
ALTER TABLE [dbo].[UserGroupSet]
ADD CONSTRAINT [PK_UserGroupSet]
    PRIMARY KEY CLUSTERED ([Gr_Id] ASC);
GO

-- Creating primary key on [Rol_Id] in table 'RoleSet'
ALTER TABLE [dbo].[RoleSet]
ADD CONSTRAINT [PK_RoleSet]
    PRIMARY KEY CLUSTERED ([Rol_Id] ASC);
GO

-- Creating primary key on [As_Id] in table 'AssignedRoleSet'
ALTER TABLE [dbo].[AssignedRoleSet]
ADD CONSTRAINT [PK_AssignedRoleSet]
    PRIMARY KEY CLUSTERED ([As_Id] ASC);
GO

-- Creating primary key on [Lg_Id] in table 'LogSet'
ALTER TABLE [dbo].[LogSet]
ADD CONSTRAINT [PK_LogSet]
    PRIMARY KEY CLUSTERED ([Lg_Id] ASC);
GO

-- Creating primary key on [GG_Id] in table 'GoodGroupSet'
ALTER TABLE [dbo].[GoodGroupSet]
ADD CONSTRAINT [PK_GoodGroupSet]
    PRIMARY KEY CLUSTERED ([GG_Id] ASC);
GO

-- Creating primary key on [Gd_Id] in table 'GoodSet'
ALTER TABLE [dbo].[GoodSet]
ADD CONSTRAINT [PK_GoodSet]
    PRIMARY KEY CLUSTERED ([Gd_Id] ASC);
GO

-- Creating primary key on [Do_Id] in table 'DonateSet'
ALTER TABLE [dbo].[DonateSet]
ADD CONSTRAINT [PK_DonateSet]
    PRIMARY KEY CLUSTERED ([Do_Id] ASC);
GO

-- Creating primary key on [DR_Id] in table 'DonateRowSet'
ALTER TABLE [dbo].[DonateRowSet]
ADD CONSTRAINT [PK_DonateRowSet]
    PRIMARY KEY CLUSTERED ([DR_Id] ASC);
GO

-- Creating primary key on [PT_Id] in table 'PaymentTypeSet'
ALTER TABLE [dbo].[PaymentTypeSet]
ADD CONSTRAINT [PK_PaymentTypeSet]
    PRIMARY KEY CLUSTERED ([PT_Id] ASC);
GO

-- Creating primary key on [In_Id] in table 'InistituteSet'
ALTER TABLE [dbo].[InistituteSet]
ADD CONSTRAINT [PK_InistituteSet]
    PRIMARY KEY CLUSTERED ([In_Id] ASC);
GO

-- Creating primary key on [DP_Id] in table 'DailyPointsSet'
ALTER TABLE [dbo].[DailyPointsSet]
ADD CONSTRAINT [PK_DailyPointsSet]
    PRIMARY KEY CLUSTERED ([DP_Id] ASC);
GO

-- Creating primary key on [Nw_Id] in table 'NewsSet'
ALTER TABLE [dbo].[NewsSet]
ADD CONSTRAINT [PK_NewsSet]
    PRIMARY KEY CLUSTERED ([Nw_Id] ASC);
GO

-- Creating primary key on [Ga_Id] in table 'GallarySet'
ALTER TABLE [dbo].[GallarySet]
ADD CONSTRAINT [PK_GallarySet]
    PRIMARY KEY CLUSTERED ([Ga_Id] ASC);
GO

-- Creating primary key on [Ph_Id] in table 'PhotoSet'
ALTER TABLE [dbo].[PhotoSet]
ADD CONSTRAINT [PK_PhotoSet]
    PRIMARY KEY CLUSTERED ([Ph_Id] ASC);
GO

-- Creating primary key on [St_Id] in table 'StateSet'
ALTER TABLE [dbo].[StateSet]
ADD CONSTRAINT [PK_StateSet]
    PRIMARY KEY CLUSTERED ([St_Id] ASC);
GO

-- Creating primary key on [Us_ID] in table 'UserSet_Donator'
ALTER TABLE [dbo].[UserSet_Donator]
ADD CONSTRAINT [PK_UserSet_Donator]
    PRIMARY KEY CLUSTERED ([Us_ID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [City_Ct_Id] in table 'UserSet'
ALTER TABLE [dbo].[UserSet]
ADD CONSTRAINT [FK_CityUser]
    FOREIGN KEY ([City_Ct_Id])
    REFERENCES [dbo].[CitySet]
        ([Ct_Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CityUser'
CREATE INDEX [IX_FK_CityUser]
ON [dbo].[UserSet]
    ([City_Ct_Id]);
GO

-- Creating foreign key on [PreferReType_CT_Code] in table 'UserSet_Donator'
ALTER TABLE [dbo].[UserSet_Donator]
ADD CONSTRAINT [FK_RelationTypeHelper]
    FOREIGN KEY ([PreferReType_CT_Code])
    REFERENCES [dbo].[ContactTypeSet]
        ([CT_Code])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RelationTypeHelper'
CREATE INDEX [IX_FK_RelationTypeHelper]
ON [dbo].[UserSet_Donator]
    ([PreferReType_CT_Code]);
GO

-- Creating foreign key on [ContactType_CT_Code] in table 'DoneContactSet'
ALTER TABLE [dbo].[DoneContactSet]
ADD CONSTRAINT [FK_ContactTypeDoneContact]
    FOREIGN KEY ([ContactType_CT_Code])
    REFERENCES [dbo].[ContactTypeSet]
        ([CT_Code])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ContactTypeDoneContact'
CREATE INDEX [IX_FK_ContactTypeDoneContact]
ON [dbo].[DoneContactSet]
    ([ContactType_CT_Code]);
GO

-- Creating foreign key on [Donator_Us_ID] in table 'DoneContactSet'
ALTER TABLE [dbo].[DoneContactSet]
ADD CONSTRAINT [FK_DonatorDoneContact]
    FOREIGN KEY ([Donator_Us_ID])
    REFERENCES [dbo].[UserSet_Donator]
        ([Us_ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DonatorDoneContact'
CREATE INDEX [IX_FK_DonatorDoneContact]
ON [dbo].[DoneContactSet]
    ([Donator_Us_ID]);
GO

-- Creating foreign key on [UserGroup_Gr_Id] in table 'UserSet'
ALTER TABLE [dbo].[UserSet]
ADD CONSTRAINT [FK_UserGroupUser]
    FOREIGN KEY ([UserGroup_Gr_Id])
    REFERENCES [dbo].[UserGroupSet]
        ([Gr_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserGroupUser'
CREATE INDEX [IX_FK_UserGroupUser]
ON [dbo].[UserSet]
    ([UserGroup_Gr_Id]);
GO

-- Creating foreign key on [Role_Rol_Id] in table 'AssignedRoleSet'
ALTER TABLE [dbo].[AssignedRoleSet]
ADD CONSTRAINT [FK_RoleAssignedRole]
    FOREIGN KEY ([Role_Rol_Id])
    REFERENCES [dbo].[RoleSet]
        ([Rol_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RoleAssignedRole'
CREATE INDEX [IX_FK_RoleAssignedRole]
ON [dbo].[AssignedRoleSet]
    ([Role_Rol_Id]);
GO

-- Creating foreign key on [User_Us_ID] in table 'LogSet'
ALTER TABLE [dbo].[LogSet]
ADD CONSTRAINT [FK_UserLog]
    FOREIGN KEY ([User_Us_ID])
    REFERENCES [dbo].[UserSet]
        ([Us_ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserLog'
CREATE INDEX [IX_FK_UserLog]
ON [dbo].[LogSet]
    ([User_Us_ID]);
GO

-- Creating foreign key on [Role_Rol_Id] in table 'LogSet'
ALTER TABLE [dbo].[LogSet]
ADD CONSTRAINT [FK_RoleLog]
    FOREIGN KEY ([Role_Rol_Id])
    REFERENCES [dbo].[RoleSet]
        ([Rol_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RoleLog'
CREATE INDEX [IX_FK_RoleLog]
ON [dbo].[LogSet]
    ([Role_Rol_Id]);
GO

-- Creating foreign key on [GoodGroup_GG_Id] in table 'GoodSet'
ALTER TABLE [dbo].[GoodSet]
ADD CONSTRAINT [FK_GoodGroupGood]
    FOREIGN KEY ([GoodGroup_GG_Id])
    REFERENCES [dbo].[GoodGroupSet]
        ([GG_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GoodGroupGood'
CREATE INDEX [IX_FK_GoodGroupGood]
ON [dbo].[GoodSet]
    ([GoodGroup_GG_Id]);
GO

-- Creating foreign key on [Donator_Us_ID] in table 'DonateSet'
ALTER TABLE [dbo].[DonateSet]
ADD CONSTRAINT [FK_DonatorPayment]
    FOREIGN KEY ([Donator_Us_ID])
    REFERENCES [dbo].[UserSet_Donator]
        ([Us_ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DonatorPayment'
CREATE INDEX [IX_FK_DonatorPayment]
ON [dbo].[DonateSet]
    ([Donator_Us_ID]);
GO

-- Creating foreign key on [Donate_Do_Id] in table 'DonateRowSet'
ALTER TABLE [dbo].[DonateRowSet]
ADD CONSTRAINT [FK_DonateDonateRow]
    FOREIGN KEY ([Donate_Do_Id])
    REFERENCES [dbo].[DonateSet]
        ([Do_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DonateDonateRow'
CREATE INDEX [IX_FK_DonateDonateRow]
ON [dbo].[DonateRowSet]
    ([Donate_Do_Id]);
GO

-- Creating foreign key on [Good_Gd_Id] in table 'DonateRowSet'
ALTER TABLE [dbo].[DonateRowSet]
ADD CONSTRAINT [FK_GoodHelp_Row]
    FOREIGN KEY ([Good_Gd_Id])
    REFERENCES [dbo].[GoodSet]
        ([Gd_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GoodHelp_Row'
CREATE INDEX [IX_FK_GoodHelp_Row]
ON [dbo].[DonateRowSet]
    ([Good_Gd_Id]);
GO

-- Creating foreign key on [PaymentType_PT_Id] in table 'DonateRowSet'
ALTER TABLE [dbo].[DonateRowSet]
ADD CONSTRAINT [FK_PaymentTypeHelpRow]
    FOREIGN KEY ([PaymentType_PT_Id])
    REFERENCES [dbo].[PaymentTypeSet]
        ([PT_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PaymentTypeHelpRow'
CREATE INDEX [IX_FK_PaymentTypeHelpRow]
ON [dbo].[DonateRowSet]
    ([PaymentType_PT_Id]);
GO

-- Creating foreign key on [UserGroup_Gr_Id] in table 'AssignedRoleSet'
ALTER TABLE [dbo].[AssignedRoleSet]
ADD CONSTRAINT [FK_UserGroupAssignedRole]
    FOREIGN KEY ([UserGroup_Gr_Id])
    REFERENCES [dbo].[UserGroupSet]
        ([Gr_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserGroupAssignedRole'
CREATE INDEX [IX_FK_UserGroupAssignedRole]
ON [dbo].[AssignedRoleSet]
    ([UserGroup_Gr_Id]);
GO

-- Creating foreign key on [User_Us_ID] in table 'DailyPointsSet'
ALTER TABLE [dbo].[DailyPointsSet]
ADD CONSTRAINT [FK_UserDailyPoints]
    FOREIGN KEY ([User_Us_ID])
    REFERENCES [dbo].[UserSet]
        ([Us_ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserDailyPoints'
CREATE INDEX [IX_FK_UserDailyPoints]
ON [dbo].[DailyPointsSet]
    ([User_Us_ID]);
GO

-- Creating foreign key on [User_Us_ID] in table 'NewsSet'
ALTER TABLE [dbo].[NewsSet]
ADD CONSTRAINT [FK_UserNews]
    FOREIGN KEY ([User_Us_ID])
    REFERENCES [dbo].[UserSet]
        ([Us_ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserNews'
CREATE INDEX [IX_FK_UserNews]
ON [dbo].[NewsSet]
    ([User_Us_ID]);
GO

-- Creating foreign key on [Gallary_Ga_Id] in table 'PhotoSet'
ALTER TABLE [dbo].[PhotoSet]
ADD CONSTRAINT [FK_GallaryPhoto]
    FOREIGN KEY ([Gallary_Ga_Id])
    REFERENCES [dbo].[GallarySet]
        ([Ga_Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GallaryPhoto'
CREATE INDEX [IX_FK_GallaryPhoto]
ON [dbo].[PhotoSet]
    ([Gallary_Ga_Id]);
GO

-- Creating foreign key on [News_Nw_Id] in table 'PhotoSet'
ALTER TABLE [dbo].[PhotoSet]
ADD CONSTRAINT [FK_NewsPhoto]
    FOREIGN KEY ([News_Nw_Id])
    REFERENCES [dbo].[NewsSet]
        ([Nw_Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_NewsPhoto'
CREATE INDEX [IX_FK_NewsPhoto]
ON [dbo].[PhotoSet]
    ([News_Nw_Id]);
GO

-- Creating foreign key on [Country_Co_Id] in table 'StateSet'
ALTER TABLE [dbo].[StateSet]
ADD CONSTRAINT [FK_CountryState]
    FOREIGN KEY ([Country_Co_Id])
    REFERENCES [dbo].[CountrySet]
        ([Co_Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CountryState'
CREATE INDEX [IX_FK_CountryState]
ON [dbo].[StateSet]
    ([Country_Co_Id]);
GO

-- Creating foreign key on [State_St_Id] in table 'CitySet'
ALTER TABLE [dbo].[CitySet]
ADD CONSTRAINT [FK_StateCity]
    FOREIGN KEY ([State_St_Id])
    REFERENCES [dbo].[StateSet]
        ([St_Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StateCity'
CREATE INDEX [IX_FK_StateCity]
ON [dbo].[CitySet]
    ([State_St_Id]);
GO

-- Creating foreign key on [Us_ID] in table 'UserSet_Donator'
ALTER TABLE [dbo].[UserSet_Donator]
ADD CONSTRAINT [FK_Donator_inherits_User]
    FOREIGN KEY ([Us_ID])
    REFERENCES [dbo].[UserSet]
        ([Us_ID])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------