USE [WCTCHealthSystem_Dev]
GO
/****** Object:  Table [dbo].[Address]    Script Date: 6/14/2023 12:06:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Address](
    [AddressID] [int] IDENTITY(1,1) NOT NULL,
    [Address1] [varchar](100) NOT NULL,
    [Address2] [varchar](100) NULL,
    [PostalCode] [varchar](10) NOT NULL,
    [City] [varchar](50) NOT NULL,
    [State] [varchar](50) NOT NULL,
    [CountryID] [int] NOT NULL,
    [LastModified] [datetime] NOT NULL,
    [County] [varchar](50) NULL,
    CONSTRAINT [PK_Address] PRIMARY KEY CLUSTERED
(
[AddressID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[PreferredModeOfContact]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[PreferredModeOfContact](
    [ModeOfContactID] [int] IDENTITY(1,1) NOT NULL,
    [Name] [varchar](100) NOT NULL,
    [Description] [varchar](max) NULL,
    [LastModified] [datetime] NOT NULL,
    CONSTRAINT [PK_PreferredModeOfContact] PRIMARY KEY CLUSTERED
(
[ModeOfContactID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[PatientContactDetails]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[PatientContactDetails](
    [PatientContactID] [bigint] IDENTITY(1,1) NOT NULL,
    [MRN] [char](6) NOT NULL,
    [CellPhone] [varchar](10) NULL,
    [HomePhone] [varchar](10) NULL,
    [WorkPhone] [varchar](10) NULL,
    [EmailAddress] [varchar](100) NULL,
    [MailingAddressID] [int] NULL,
    [ResidenceAddressID] [int] NULL,
    [LastModified] [datetime] NOT NULL,
    CONSTRAINT [PK_PatientContactDetails] PRIMARY KEY CLUSTERED
(
[PatientContactID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[Gender]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[Gender](
    [GenderID] [tinyint] IDENTITY(1,1) NOT NULL,
    [Name] [varchar](50) NOT NULL,
    [LastModified] [datetime] NOT NULL,
    CONSTRAINT [PK_Gender] PRIMARY KEY CLUSTERED
(
[GenderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[Encounter]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[Encounter](
    [MRN] [char](6) NOT NULL,
    [EncounterRestrictionID] [bigint] NULL,
    [EncounterID] [bigint] IDENTITY(1,1) NOT NULL,
    [FacilityID] [int] NOT NULL,
    [PatientCurrentAge] [tinyint] NOT NULL,
    [AdmitDateTime] [datetime] NOT NULL,
    [ChiefComplaint] [varchar](max) NOT NULL,
    [EncounterTypeID] [int] NULL,
    [PlaceOfServiceID] [int] NOT NULL,
    [AdmitTypeID] [int] NOT NULL,
    [RoomNumber] [varchar](10) NULL,
    [FacilityRegistryOptInOut] [bit] NOT NULL,
    [DepartmentID] [int] NULL,
    [PointOfOriginID] [int] NOT NULL,
    [PaymentID] [bigint] NULL,
    [DischargeDisposition] [int] NULL,
    [LastModified] [datetime] NOT NULL,
    [DischargeDateTime] [datetime] NULL,
    [DischargeComment] [varchar](max) NULL,
    [PhysicianID] [int] NULL,
    [AdmittingDiagnosis] [varchar](max) NULL,
    [HistoryOfPresentIllness] [varchar](max) NULL,
    [SignificantFindings] [varchar](max) NULL,
    [MedicationsOnDischarge] [varchar](max) NULL,
    [DischargeDietInstructions] [varchar](max) NULL,
    [CoSignature] [int] NULL,
    [WrittenDateTime] [datetime] NULL,
    [LastUpdated] [datetime] NULL,
    [EditedBy] [int] NULL,
    [DischargeDispositionNote] [varchar](max) NULL,
    [AuthoringProvider] [int] NULL,
    [DischargeHospitalCourse] [varchar](max) NULL,
    [DischargeDiagnosis] [varchar](max) NULL,
    CONSTRAINT [PK_Encounter] PRIMARY KEY CLUSTERED
(
[EncounterID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[Patient]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[Patient](
    [MRN] [char](6) NOT NULL,
    [FirstName] [varchar](50) NOT NULL,
    [LastName] [varchar](50) NOT NULL,
    [MiddleName] [varchar](50) NULL,
    [MaidenName] [varchar](50) NULL,
    [ReligionID] [smallint] NULL,
    [AliasFirstName] [varchar](50) NULL,
    [AliasMiddleName] [varchar](50) NULL,
    [AliasLastName] [varchar](50) NULL,
    [MothersMaidenName] [varchar](50) NULL,
    [SSN] [char](9) NULL,
    [DOB] [date] NULL,
    [SexID] [tinyint] NULL,
    [GenderID] [tinyint] NULL,
    [DeceasedLiving] [bit] NOT NULL,
    [InterpreterNeeded] [bit] NOT NULL,
    [MaritalStatusID] [tinyint] NULL,
    [EthnicityID] [tinyint] NULL,
    [EmploymentID] [int] NULL,
    [LastModified] [datetime] NOT NULL,
    [EditedBy] [int] NULL,
    [DeathDate] [date] NULL,
    [IsCurrentMilitaryServiceMember] [bit] NOT NULL,
    [IsVeteran] [bit] NOT NULL,
    [LegalStatusID] [tinyint] NULL,
    [GenderPronounID] [tinyint] NULL,
    CONSTRAINT [PK_Patient] PRIMARY KEY CLUSTERED
(
[MRN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[Country]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[Country](
    [CountryID] [int] IDENTITY(1,1) NOT NULL,
    [Name] [varchar](50) NOT NULL,
    [LastModified] [datetime] NOT NULL,
    CONSTRAINT [PK_Country] PRIMARY KEY CLUSTERED
(
[CountryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]
    GO
/****** Object:  View [dbo].[Patient_General]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO

/*****************************************************************
Patient_General: This view serves as a narrow quick reference for 
important patient information available in one place. 
Author: Brian Duff
Create Date: 04/29/2019
\**************************************************************/


CREATE VIEW [dbo].[Patient_General] AS

    (SELECT p.MRN, p.FirstName AS 'First Name', p.LastName AS 'Last Name',
    p.AliasMiddleName AS 'Middle Initial',
    g.Name AS 'Gender', RIGHT(p.SSN, 4) AS 'Last Four SSN',
    CASE WHEN moc.Name = 'CellPhone' THEN pc.CellPhone
    ELSE ISNULL(pc.HomePhone, pc.WorkPhone) END AS 'Preferred Phone',
    CONCAT(a.Address1, + ' ' + a.Address2, + ', ' + a.City, + ', '+ a.State, + ' ' + a.PostalCode + ', ' + c.Name) AS 'Resident Address',
    i.GroupName AS 'Primary Insurance Provider'
    FROM Patient p
    INNER JOIN Gender g
    ON p.GenderID = g.GenderID
    INNER JOIN PatientContactDetails pc
    ON p.MRN = pc.MRN
    INNER JOIN PreferredModeOfContact moc
    ON pc.PreferredModeOfContact = moc.ModeOfContactID
    LEFT JOIN Address a
    ON pc.ResidenceAddressID = a.AddressID
    INNER JOIN Country c
    ON a.CountryID = a.CountryID
    JOIN Encounter e
    ON p.MRN = e.MRN
    JOIN Payment py
    ON e.PaymentID = py.PaymentID
    JOIN Insurance i
    ON py.PrimaryInsuranceID = i.InsuranceID
)

    GO
/****** Object:  Table [dbo].[Facility]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[Facility](
    [FacilityID] [int] IDENTITY(1,1) NOT NULL,
    [Name] [varchar](100) NOT NULL,
    [Description] [varchar](max) NULL,
    [AddressID] [int] NOT NULL,
    [Phone] [varchar](10) NULL,
    [LastModified] [datetime] NOT NULL,
    CONSTRAINT [PK_Facility] PRIMARY KEY CLUSTERED
(
[FacilityID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[AdmitType]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[AdmitType](
    [AdmitTypeID] [int] IDENTITY(1,1) NOT NULL,
    [WiPopCode] [char](1) NOT NULL,
    [Description] [varchar](25) NOT NULL,
    [Explaination] [varchar](max) NULL,
    [LastModified] [datetime] NOT NULL,
    CONSTRAINT [PK_AdmitType] PRIMARY KEY CLUSTERED
(
[AdmitTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[Departments]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[Departments](
    [DepartmentID] [int] IDENTITY(1,1) NOT NULL,
    [Name] [varchar](100) NOT NULL,
    [Description] [varchar](max) NULL,
    [LastModified] [datetime] NOT NULL,
    CONSTRAINT [PK_Departments] PRIMARY KEY CLUSTERED
(
[DepartmentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
    GO
/****** Object:  View [dbo].[Encounter_History]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO

/*****************************************************************
Patient History: This view lists a patient’s encounter history with 
relevant information pertaining to the encounter instance.
Author: Brian Duff
Create Date: 04/29/2019
\**************************************************************/

CREATE VIEW [dbo].[Encounter_History] AS
    (SELECT e.MRN, e.EncounterID 'Encounter ID', e.ChiefComplaint 'Chief Complaint', at.Description, at.Explaination, DATEPART(YYYY,e.AdmitDateTime) 'Admit Date',
    f.Name AS 'Facility Name', d.Name AS 'Department Name',
    ISNULL(CONVERT(varchar(20), e.DischargeDate), 'Current Patient') AS 'Discharge Date'
    FROM Encounter e
    INNER JOIN AdmitType at
    ON e.AdmitTypeID = e.AdmitTypeID
    INNER JOIN Facility f
    ON e.FacilityID = f.FacilityID
    INNER JOIN Departments d
    ON e.DepartmentID = d.DepartmentID
)



    GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
    [MigrationId] [nvarchar](150) NOT NULL,
    [ProductVersion] [nvarchar](32) NOT NULL,
    CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED
(
[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[AdmitOrder]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[AdmitOrder](
    [AdmitOrderID] [bigint] IDENTITY(1,1) NOT NULL,
    [VisitTypeID] [smallint] NOT NULL,
    [DepartmentID] [int] NOT NULL,
    [LastModified] [datetime] NOT NULL,
    [OrderInfoID] [bigint] NOT NULL,
    [AdmittingDiagnosis] [varchar](200) NULL,
    CONSTRAINT [pk_AdmitOrder] PRIMARY KEY CLUSTERED
(
[AdmitOrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[AdvancedDirectives]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[AdvancedDirectives](
    [AdvancedDirectiveID] [smallint] IDENTITY(1,1) NOT NULL,
    [Name] [varchar](50) NOT NULL,
    CONSTRAINT [pk_AdvancedDirectives] PRIMARY KEY CLUSTERED
(
[AdvancedDirectiveID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[AlertType]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[AlertType](
    [AlertID] [int] IDENTITY(1,1) NOT NULL,
    [Name] [varchar](50) NOT NULL,
    [Description] [varchar](max) NULL,
    [LastModified] [datetime] NOT NULL,
    CONSTRAINT [PK_AlertType] PRIMARY KEY CLUSTERED
(
[AlertID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[Allergen]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[Allergen](
    [AllergenID] [int] IDENTITY(1,1) NOT NULL,
    [AllergenName] [varchar](50) NOT NULL,
    [Description] [varchar](max) NULL,
    [LastModified] [datetime] NOT NULL,
    CONSTRAINT [PK_Allergen] PRIMARY KEY CLUSTERED
(
[AllergenID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[AnestheticType]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[AnestheticType](
    [AnestheticTypeID] [int] IDENTITY(1,1) NOT NULL,
    [AnestheticType] [varchar](50) NOT NULL,
    [LastModified] [datetime] NULL,
    CONSTRAINT [pk_AnestheticType] PRIMARY KEY CLUSTERED
(
[AnestheticTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[AspNetRoleClaims](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [RoleId] [nvarchar](450) NOT NULL,
    [ClaimType] [nvarchar](max) NULL,
    [ClaimValue] [nvarchar](max) NULL,
    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED
(
[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[AspNetRoles](
    [Id] [nvarchar](450) NOT NULL,
    [Name] [nvarchar](256) NULL,
    [NormalizedName] [nvarchar](256) NULL,
    [ConcurrencyStamp] [nvarchar](max) NULL,
    CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED
(
[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[AspNetUserClaims](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [UserId] [nvarchar](450) NOT NULL,
    [ClaimType] [nvarchar](max) NULL,
    [ClaimValue] [nvarchar](max) NULL,
    CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED
(
[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[AspNetUserLogins](
    [LoginProvider] [nvarchar](128) NOT NULL,
    [ProviderKey] [nvarchar](128) NOT NULL,
    [ProviderDisplayName] [nvarchar](max) NULL,
    [UserId] [nvarchar](450) NOT NULL,
    CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED
(
    [LoginProvider] ASC,
[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[AspNetUserRoles](
    [UserId] [nvarchar](450) NOT NULL,
    [RoleId] [nvarchar](450) NOT NULL,
    CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED
(
    [UserId] ASC,
[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[AspNetUsers](
    [Id] [nvarchar](450) NOT NULL,
    [UserName] [nvarchar](256) NULL,
    [NormalizedUserName] [nvarchar](256) NULL,
    [Email] [nvarchar](256) NULL,
    [NormalizedEmail] [nvarchar](256) NULL,
    [EmailConfirmed] [bit] NOT NULL,
    [PasswordHash] [nvarchar](max) NULL,
    [SecurityStamp] [nvarchar](max) NULL,
    [ConcurrencyStamp] [nvarchar](max) NULL,
    [PhoneNumber] [nvarchar](max) NULL,
    [PhoneNumberConfirmed] [bit] NOT NULL,
    [TwoFactorEnabled] [bit] NOT NULL,
    [LockoutEnd] [datetimeoffset](7) NULL,
    [LockoutEnabled] [bit] NOT NULL,
    [AccessFailedCount] [int] NOT NULL,
    CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED
(
[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[AspNetUserTokens](
    [UserId] [nvarchar](450) NOT NULL,
    [LoginProvider] [nvarchar](128) NOT NULL,
    [Name] [nvarchar](128) NOT NULL,
    [Value] [nvarchar](max) NULL,
    CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED
(
    [UserId] ASC,
    [LoginProvider] ASC,
[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[BloodPressureRouteType]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[BloodPressureRouteType](
    [BloodPressureRouteTypeID] [tinyint] IDENTITY(10,1) NOT NULL,
    [Name] [varchar](100) NOT NULL,
    [LastModified] [datetime] NOT NULL,
    CONSTRAINT [pk_BloodPressureRouteType] PRIMARY KEY CLUSTERED
(
[BloodPressureRouteTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[BMIMethod]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[BMIMethod](
    [BMIMethodID] [tinyint] IDENTITY(1,1) NOT NULL,
    [Name] [varchar](100) NOT NULL,
    [LastModified] [datetime] NOT NULL,
    CONSTRAINT [pk_BMIMethod] PRIMARY KEY CLUSTERED
(
[BMIMethodID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[BodyPart]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[BodyPart](
    [PartID] [bigint] IDENTITY(1,1) NOT NULL,
    [Name] [varchar](75) NOT NULL,
    PRIMARY KEY CLUSTERED
(
[PartID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[BodySystemAssessment]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[BodySystemAssessment](
    [BodySystemAssessmentId] [bigint] IDENTITY(1,1) NOT NULL,
    [PhysicianAssessmentID] [bigint] NULL,
    [BodySystemTypeId] [smallint] NULL,
    [IsWithinNormalLimits] [bit] NULL,
    [Comment] [varchar](max) NULL,
    [LastModified] [datetime] NULL,
    [ExamTypeCode] [smallint] NULL,
    CONSTRAINT [pk_PhysicianAssessmentDetail] PRIMARY KEY CLUSTERED
(
[BodySystemAssessmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[BodySystemType]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[BodySystemType](
    [BodySystemTypeId] [smallint] IDENTITY(1,1) NOT NULL,
    [Name] [varchar](100) NOT NULL,
    [NormalLimitsDescription] [varchar](max) NULL,
    [LastModified] [datetime] NULL,
    CONSTRAINT [pk_BodySystemType] PRIMARY KEY CLUSTERED
(
[BodySystemTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[CareSystemAssessment]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[CareSystemAssessment](
    [CareSystemAssessmentID] [int] IDENTITY(1,1) NOT NULL,
    [PCAID] [int] NOT NULL,
    [CareSystemParameterID] [smallint] NOT NULL,
    [IsWithinNormalLimits] [bit] NOT NULL,
    [Comment] [varchar](max) NULL,
    [LastModified] [datetime] NOT NULL,
    CONSTRAINT [PK_CareSystemAssessment] PRIMARY KEY CLUSTERED
(
    [CareSystemAssessmentID] ASC,
[PCAID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[CareSystemParameter]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[CareSystemParameter](
    [CareSystemParameterID] [smallint] IDENTITY(1,1) NOT NULL,
    [Name] [varchar](100) NOT NULL,
    [NormalLimitsDescription] [varchar](max) NULL,
    [CareSystemTypeID] [smallint] NOT NULL,
    [LastModified] [datetime] NOT NULL,
    CONSTRAINT [pk_CareSystemParameter] PRIMARY KEY CLUSTERED
(
[CareSystemParameterID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[CareSystemType]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[CareSystemType](
    [CareSystemTypeID] [smallint] IDENTITY(1,1) NOT NULL,
    [Name] [varchar](100) NOT NULL,
    [LastModified] [datetime] NOT NULL,
    CONSTRAINT [PK_CareSystemType] PRIMARY KEY CLUSTERED
(
[CareSystemTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[ClinicalReminders]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[ClinicalReminders](
    [ClinicalReminderID] [smallint] IDENTITY(1,1) NOT NULL,
    [Name] [varchar](100) NOT NULL,
    [IsActive] [bit] NOT NULL,
    CONSTRAINT [pk_ClinicalReminder] PRIMARY KEY CLUSTERED
(
[ClinicalReminderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[Discharge]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[Discharge](
    [DischargeID] [int] IDENTITY(1,1) NOT NULL,
    [WiPopCode] [varchar](4) NULL,
    [Name] [varchar](200) NOT NULL,
    [Description] [varchar](max) NULL,
    [LastModified] [datetime] NOT NULL,
    CONSTRAINT [PK_Discharge] PRIMARY KEY CLUSTERED
(
[DischargeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[Dosage]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[Dosage](
    [DosageID] [int] IDENTITY(1,1) NOT NULL,
    [Amount] [decimal](18, 0) NOT NULL,
    [UnitOfMeasurement] [varchar](50) NOT NULL,
    PRIMARY KEY CLUSTERED
(
[DosageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[Employment]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[Employment](
    [EmploymentID] [int] IDENTITY(1,1) NOT NULL,
    [EmployerName] [varchar](100) NOT NULL,
    [PhoneNumber] [varchar](10) NOT NULL,
    [Occupation] [varchar](50) NULL,
    [LastModified] [datetime] NOT NULL,
    [AddressID] [int] NULL,
    CONSTRAINT [PK_Employment] PRIMARY KEY CLUSTERED
(
[EmploymentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[EncounterAlerts]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[EncounterAlerts](
    [EncounterID] [bigint] NOT NULL,
    [PatientAlertID] [bigint] NOT NULL,
    [LastModified] [datetime] NOT NULL,
     CONSTRAINT [PK_EncounterAlerts] PRIMARY KEY CLUSTERED
    (
    [EncounterID] ASC,
[PatientAlertID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[EncounterPhysicians]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[EncounterPhysicians](
    [EncounterPhysiciansID] [bigint] IDENTITY(1,1) NOT NULL,
    [PhysicianID] [int] NOT NULL,
    [PhysicianRoleID] [int] NOT NULL,
    [LastModified] [datetime] NOT NULL,
    [EncounterID] [bigint] NULL,
    CONSTRAINT [PK_EncounterPhysicians] PRIMARY KEY CLUSTERED
(
[EncounterPhysiciansID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[EncounterType]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[EncounterType](
    [EncounterTypeID] [int] IDENTITY(1,1) NOT NULL,
    [Name] [varchar](50) NOT NULL,
    [LastModified] [datetime] NOT NULL,
    CONSTRAINT [PK_EncounterType] PRIMARY KEY CLUSTERED
(
[EncounterTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[Ethnicity]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[Ethnicity](
    [EthnicityID] [tinyint] IDENTITY(1,1) NOT NULL,
    [Name] [varchar](50) NOT NULL,
    [LastModified] [datetime] NOT NULL,
    CONSTRAINT [PK_Ethnicity] PRIMARY KEY CLUSTERED
(
[EthnicityID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[ExamType]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[ExamType](
    [ExamTypeCode] [smallint] IDENTITY(1,1) NOT NULL,
    [ExamType] [varchar](100) NOT NULL,
    [LastModified] [datetime] NULL,
    CONSTRAINT [pk_ExamType] PRIMARY KEY CLUSTERED
(
[ExamTypeCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[FallRisks]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[FallRisks](
    [FallRiskID] [tinyint] IDENTITY(1,1) NOT NULL,
    [Name] [varchar](100) NOT NULL,
    [IsActive] [bit] NOT NULL,
    CONSTRAINT [pk_FallRisks] PRIMARY KEY CLUSTERED
(
[FallRiskID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[GenderPronoun]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[GenderPronoun](
    [GenderPronounID] [tinyint] IDENTITY(1,1) NOT NULL,
    [GenderPronouns] [varchar](30) NOT NULL,
    [IsActive] [bit] NOT NULL,
    [LastModified] [datetime] NULL,
    CONSTRAINT [pk_GenderPronoun] PRIMARY KEY CLUSTERED
(
[GenderPronounID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[Language]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[Language](
    [LanguageID] [smallint] IDENTITY(1,1) NOT NULL,
    [Name] [varchar](70) NOT NULL,
    [LastModified] [datetime] NOT NULL,
    CONSTRAINT [PK_LanguageID] PRIMARY KEY CLUSTERED
(
[LanguageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[LegalStatus]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[LegalStatus](
    [LegalStatusID] [tinyint] IDENTITY(1,1) NOT NULL,
    [LegalStatusName] [varchar](50) NOT NULL,
    [RequiresLegalGuardian] [bit] NOT NULL,
    [IsActive] [bit] NOT NULL,
    [LastModified] [datetime] NULL,
    CONSTRAINT [PK_LegalStatus] PRIMARY KEY CLUSTERED
(
[LegalStatusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[MaritalStatus]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[MaritalStatus](
    [MaritalStatusID] [tinyint] IDENTITY(1,1) NOT NULL,
    [Name] [varchar](30) NOT NULL,
    [LastModified] [datetime] NOT NULL,
    CONSTRAINT [PK_MaritalStatus] PRIMARY KEY CLUSTERED
(
[MaritalStatusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[Medication]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[Medication](
    [MedicationID] [int] IDENTITY(1,1) NOT NULL,
    [NDC] [varchar](10) NOT NULL,
    [BrandNameID] [int] NOT NULL,
    [GenericNameID] [int] NOT NULL,
    [ActiveStrength] [varchar](100) NOT NULL,
    [ActiveIngredientUnits] [varchar](200) NOT NULL,
    [DosageFormID] [smallint] NOT NULL,
    [DeliveryRouteID] [smallint] NOT NULL,
    [IsActive] [bit] NOT NULL,
    [ModifiedDate] [datetime] NULL,
    CONSTRAINT [pk_Medication] PRIMARY KEY CLUSTERED
(
[MedicationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[MedicationBrandName]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[MedicationBrandName](
    [MedicationBrandID] [int] IDENTITY(1,1) NOT NULL,
    [BrandName] [varchar](150) NOT NULL,
    [IsActive] [bit] NOT NULL,
    [ModifiedDate] [datetime] NULL,
    CONSTRAINT [pk_MedicationBrandName] PRIMARY KEY CLUSTERED
(
[MedicationBrandID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[MedicationDeliveryRoute]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[MedicationDeliveryRoute](
    [DeliveryRouteID] [smallint] IDENTITY(1,1) NOT NULL,
    [DeliveryRouteName] [varchar](100) NOT NULL,
    [IsActive] [bit] NOT NULL,
    [ModifiedDate] [datetime] NULL,
    CONSTRAINT [pk_MedicationDeliveryRoute] PRIMARY KEY CLUSTERED
(
[DeliveryRouteID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[MedicationDosageForm]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[MedicationDosageForm](
    [DosageFormID] [smallint] IDENTITY(1,1) NOT NULL,
    [DosageForm] [varchar](50) NOT NULL,
    [IsActive] [bit] NOT NULL,
    [DateModified] [datetime] NULL,
    CONSTRAINT [pk_MedicationDosageForm] PRIMARY KEY CLUSTERED
(
[DosageFormID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[MedicationGenericName]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[MedicationGenericName](
    [MedicationGenericID] [int] IDENTITY(1,1) NOT NULL,
    [GenericName] [varchar](300) NOT NULL,
    [IsActive] [bit] NOT NULL,
    [ModifiedDate] [datetime] NULL,
    CONSTRAINT [pk_MedicationGenericName] PRIMARY KEY CLUSTERED
(
[MedicationGenericID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[NoteType]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[NoteType](
    [NoteTypeID] [int] IDENTITY(1,1) NOT NULL,
    [NoteType] [varchar](50) NOT NULL,
    [LastModified] [datetime] NULL,
    CONSTRAINT [pk_NoteType] PRIMARY KEY CLUSTERED
(
[NoteTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[O2DeliveryType]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[O2DeliveryType](
    [O2DeliveryTypeID] [int] IDENTITY(1,1) NOT NULL,
    [O2DeliveryTypeName] [varchar](50) NOT NULL,
    [LastModified] [datetime] NOT NULL,
    CONSTRAINT [PK_O2DeliveryType] PRIMARY KEY CLUSTERED
(
[O2DeliveryTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[OrderInfo]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[OrderInfo](
    [OrderInfoID] [bigint] IDENTITY(1,1) NOT NULL,
    [EncounterID] [bigint] NOT NULL,
    [OrderTypeID] [int] NOT NULL,
    [OrderingAuthor] [int] NOT NULL,
    [OrderDate] [datetime] NOT NULL,
    [PriorityID] [int] NOT NULL,
    [CoAuthorApproved] [bit] NULL,
    [ApprovedDate] [datetime] NOT NULL,
    [Notes] [varchar](2000) NULL,
    [IsOrderComplete] [bit] NOT NULL,
    [OrderCompletedDateTime] [datetime] NULL,
    [OrderCompletedByID] [int] NULL,
    [IsVerbalOrder] [bit] NOT NULL,
    [AuthenticatingProviderID] [int] NULL,
    [AuthorESignature] [varchar](100) NULL,
    [AuthenticatingProviderESignature] [varchar](100) NULL,
    PRIMARY KEY CLUSTERED
(
[OrderInfoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[OrderType]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[OrderType](
    [OrderTypeID] [int] IDENTITY(1,1) NOT NULL,
    [OrderName] [varchar](50) NOT NULL,
    PRIMARY KEY CLUSTERED
(
[OrderTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[PainParameter]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[PainParameter](
    [PainParameterID] [int] IDENTITY(1,1) NOT NULL,
    [PainScaleTypeID] [int] NOT NULL,
    [ParameterName] [varchar](200) NOT NULL,
    [Description] [varchar](200) NOT NULL,
    [LastModified] [datetime] NOT NULL,
    CONSTRAINT [pk_PainParameter] PRIMARY KEY CLUSTERED
(
[PainParameterID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[PainRating]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[PainRating](
    [PainRatingID] [int] IDENTITY(1,1) NOT NULL,
    [PainParameterID] [int] NOT NULL,
    [Value] [tinyint] NOT NULL,
    [Description] [varchar](200) NOT NULL,
    [LastModified] [datetime] NOT NULL,
    CONSTRAINT [pk_PainScaleRating] PRIMARY KEY CLUSTERED
(
[PainRatingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[PainRatingImage]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[PainRatingImage](
    [PainRatingID] [int] NOT NULL,
    [Image] [varbinary](max) NULL,
    [LastModified] [datetime] NOT NULL,
    CONSTRAINT [pk_PainRatingImage] PRIMARY KEY CLUSTERED
(
[PainRatingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[PainScaleType]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[PainScaleType](
    [PainScaleTypeID] [int] IDENTITY(1,1) NOT NULL,
    [PainScaleTypeName] [varchar](50) NOT NULL,
    [UseDescription] [varchar](500) NULL,
    [LastModified] [datetime] NOT NULL,
    CONSTRAINT [PK_PainScaleType] PRIMARY KEY CLUSTERED
(
[PainScaleTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[PatientAdvancedDirectives]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[PatientAdvancedDirectives](
    [PatientAdvancedDirectiveID] [bigint] IDENTITY(1,1) NOT NULL,
    [PatientAlertID] [bigint] NOT NULL,
    [AdvancedDirectiveID] [smallint] NOT NULL,
    [LastModified] [datetime] NOT NULL,
    CONSTRAINT [pk_PatientAdvancedDirectives] PRIMARY KEY CLUSTERED
(
[PatientAdvancedDirectiveID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[PatientAlerts]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[PatientAlerts](
    [PatientAlertID] [bigint] IDENTITY(1,1) NOT NULL,
    [AlertTypeID] [int] NULL,
    [MRN] [char](6) NOT NULL,
    [LastModified] [datetime] NOT NULL,
    [IsActive] [bit] NOT NULL,
    [StartDate] [datetime] NOT NULL,
    [EndDate] [datetime] NULL,
    [Comments] [varchar](max) NULL,
    CONSTRAINT [PK_PatientAlerts] PRIMARY KEY CLUSTERED
(
[PatientAlertID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[PatientAlias]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[PatientAlias](
    [PatientAliasID] [int] IDENTITY(1,1) NOT NULL,
    [PatientMRN] [char](6) NOT NULL,
    [AliasFirstName] [varchar](50) NULL,
    [AliasMiddleName] [varchar](50) NULL,
    [AliasLastName] [varchar](50) NULL,
    [AliasPriority] [tinyint] NULL,
    [LastModified] [datetime] NOT NULL,
    [EditedBy] [int] NULL,
    CONSTRAINT [pk_PatientAlias] PRIMARY KEY CLUSTERED
(
[PatientAliasID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[PatientAllergy]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[PatientAllergy](
    [PatientAllergyID] [bigint] IDENTITY(1,1) NOT NULL,
    [AllergenID] [int] NULL,
    [ReactionID] [int] NULL,
    [LastModified] [datetime] NOT NULL,
    [PatientAlertID] [bigint] NOT NULL,
    [GenericMedicationID] [int] NULL,
    CONSTRAINT [PK_PatientAllergy] PRIMARY KEY CLUSTERED
(
[PatientAllergyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[PatientClinicalReminders]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[PatientClinicalReminders](
    [PatientClinicalReminderID] [bigint] IDENTITY(1,1) NOT NULL,
    [PatientAlertID] [bigint] NOT NULL,
    [ClinicalReminderID] [smallint] NOT NULL,
    [LastModified] [datetime] NOT NULL,
    CONSTRAINT [pk_PatientClinicalReminders] PRIMARY KEY CLUSTERED
(
[PatientClinicalReminderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[PatientContactTimes]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[PatientContactTimes](
    [PatientContactTimeID] [bigint] IDENTITY(1,1) NOT NULL,
    [PatientContactID] [bigint] NOT NULL,
    [ContactTimeID] [int] NOT NULL,
    [LastModified] [datetime] NULL,
    CONSTRAINT [pk_PatientContactTimes] PRIMARY KEY CLUSTERED
(
[PatientContactTimeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[PatientEmergencyContact]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[PatientEmergencyContact](
    [EmergencyContactID] [bigint] IDENTITY(1,1) NOT NULL,
    [MRN] [char](6) NOT NULL,
    [ContactName] [varchar](50) NOT NULL,
    [ContactPhone] [varchar](10) NULL,
    [ContactAddressID] [int] NOT NULL,
    [ContactRelationshipID] [int] NOT NULL,
    [LastModified] [datetime] NOT NULL,
    [Email] [varchar](100) NULL,
    CONSTRAINT [PK_PatientEmergencyContact] PRIMARY KEY CLUSTERED
(
[EmergencyContactID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[PatientFallRisks]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[PatientFallRisks](
    [PatientFallRiskID] [bigint] IDENTITY(1,1) NOT NULL,
    [LastModified] [datetime] NOT NULL,
    [PatientAlertID] [bigint] NOT NULL,
    [FallRiskID] [tinyint] NOT NULL,
    CONSTRAINT [PK_PatientFallRisks] PRIMARY KEY CLUSTERED
(
[PatientFallRiskID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[PatientInsurance]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[PatientInsurance](
    [PatientInsuranceID] [int] IDENTITY(1,1) NOT NULL,
    [MRN] [char](6) NOT NULL,
    [InsuranceOrder] [tinyint] NOT NULL,
    [Guarantor] [varchar](100) NOT NULL,
    [InsuranceProviderName] [varchar](150) NULL,
    [Subscriber] [varchar](100) NULL,
    [SubscriberRelationshipID] [int] NULL,
    [SubscriberNumber] [varchar](50) NULL,
    [GroupNumber] [varchar](20) NULL,
    [PlanName] [varchar](100) NULL,
    [PlanNumber] [varchar](20) NULL,
    [EffectiveDate] [date] NULL,
    [Notes] [varchar](max) NULL,
    CONSTRAINT [pk_PatientInsurance] PRIMARY KEY CLUSTERED
(
[PatientInsuranceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[PatientLanguage]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[PatientLanguage](
    [LanguageID] [smallint] NOT NULL,
    [MRN] [char](6) NOT NULL,
    [IsPrimary] [tinyint] NOT NULL,
    [LastModified] [datetime] NOT NULL,
    CONSTRAINT [PK_PatientLanguage] PRIMARY KEY CLUSTERED
(
    [LanguageID] ASC,
[MRN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[PatientModeOfContacts]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[PatientModeOfContacts](
    [PatientModeOfContactID] [bigint] IDENTITY(1,1) NOT NULL,
    [PatientContactID] [bigint] NOT NULL,
    [ModeOfContactID] [int] NOT NULL,
    [LastModified] [datetime] NULL,
    CONSTRAINT [pk_PatientModeOfContacts] PRIMARY KEY CLUSTERED
(
[PatientModeOfContactID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[PatientRace]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[PatientRace](
    [RaceID] [tinyint] NOT NULL,
    [MRN] [char](6) NOT NULL,
    [LastModified] [datetime] NOT NULL,
    CONSTRAINT [PK_PatientRace] PRIMARY KEY CLUSTERED
(
    [RaceID] ASC,
[MRN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[PatientRestrictions]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[PatientRestrictions](
    [RestrictionID] [bigint] IDENTITY(1,1) NOT NULL,
    [LastModified] [datetime] NOT NULL,
    [RestrictionTypeID] [smallint] NOT NULL,
    [PatientAlertID] [bigint] NOT NULL,
    CONSTRAINT [PK_PatientRestrictions] PRIMARY KEY CLUSTERED
(
[RestrictionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[PaymentPlan]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[PaymentPlan](
    [PaymentPlanID] [int] IDENTITY(1,1) NOT NULL,
    [WiPopCode] [char](2) NOT NULL,
    [Description] [varchar](100) NULL,
    [LastModified] [datetime] NOT NULL,
    CONSTRAINT [PK_PaymentPlan] PRIMARY KEY CLUSTERED
(
[PaymentPlanID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[PaymentSource]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[PaymentSource](
    [PaymentSourceID] [int] IDENTITY(1,1) NOT NULL,
    [WiPopCode] [varchar](5) NOT NULL,
    [Name] [varchar](50) NOT NULL,
    [Description] [varchar](max) NULL,
    [LastModified] [datetime] NOT NULL,
    CONSTRAINT [PK_PaymentSource] PRIMARY KEY CLUSTERED
(
[PaymentSourceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[PCAComment]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[PCAComment](
    [PCACommentID] [int] IDENTITY(1,1) NOT NULL,
    [PCAID] [int] NOT NULL,
    [PCACommentTypeID] [int] NOT NULL,
    [PCAComment] [varchar](max) NULL,
    [DateCommentAdded] [datetime] NULL,
    [LastModified] [datetime] NOT NULL,
    CONSTRAINT [PK_PCAComment] PRIMARY KEY CLUSTERED
(
[PCACommentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[PCACommentType]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[PCACommentType](
    [PCACommentTypeID] [int] IDENTITY(1,1) NOT NULL,
    [PCACommentTypeName] [varchar](50) NOT NULL,
    [LastModified] [datetime] NOT NULL,
    CONSTRAINT [PK_PCACommentType] PRIMARY KEY CLUSTERED
(
[PCACommentTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[PCAPainAssessment]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[PCAPainAssessment](
    [PainAssessmentID] [bigint] IDENTITY(1,1) NOT NULL,
    [PCAID] [int] NOT NULL,
    [PainParameterID] [int] NOT NULL,
    [PainRatingID] [int] NOT NULL,
    [LastModified] [datetime] NOT NULL,
    CONSTRAINT [pk_PCAPainAssessment] PRIMARY KEY CLUSTERED
(
[PainAssessmentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[PCARecord]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[PCARecord](
    [PCAID] [int] IDENTITY(1,1) NOT NULL,
    [EncounterID] [bigint] NOT NULL,
    [PainLevelGoal] [tinyint] NULL,
    [PainScaleTypeID] [int] NULL,
    [Temperature] [decimal](5, 2) NULL,
    [TempRouteTypeID] [int] NULL,
    [Pulse] [tinyint] NULL,
    [PulseRouteTypeID] [int] NULL,
    [Respiration] [tinyint] NULL,
    [SystolicBloodPressure] [smallint] NULL,
    [DiastolicBloodPressure] [smallint] NULL,
    [BloodPressureRouteTypeID] [tinyint] NULL,
    [PulseOximetry] [tinyint] NULL,
    [O2DeliveryTypeID] [int] NULL,
    [OxygenFlow] [varchar](50) NULL,
    [PercentOxygenDelivered] [decimal](5, 4) NULL,
    [Weight] [decimal](10, 2) NULL,
    [WeightUnits] [varchar](50) NULL,
    [Height] [decimal](6, 2) NULL,
    [HeightUnits] [varchar](50) NULL,
    [BodyMassIndex] [decimal](7, 3) NULL,
    [BMIMethodID] [tinyint] NULL,
    [HeadCircumference] [decimal](7, 3) NULL,
    [HeadCircumferenceUnits] [varchar](50) NULL,
    [DateVitalsAdded] [datetime] NULL,
    [LastModified] [datetime] NOT NULL,
    [EditedBy] [int] NULL,
    [TempUnits] [varchar](10) NULL,
    CONSTRAINT [PK_PCARecord] PRIMARY KEY CLUSTERED
(
[PCAID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[Physician]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[Physician](
    [PhysicianID] [int] IDENTITY(1,1) NOT NULL,
    [FirstName] [varchar](100) NOT NULL,
    [LastName] [varchar](100) NOT NULL,
    [Credentials] [varchar](100) NULL,
    [License] [varchar](50) NULL,
    [AddressID] [int] NULL,
    [PhoneNumber] [varchar](20) NULL,
    [EmailAddress] [varchar](50) NULL,
    [SpecialtyID] [int] NULL,
    [ProviderTypeID] [int] NULL,
    [ProviderStatusID] [tinyint] NULL,
    [LastModified] [datetime] NOT NULL,
    [Pin] [smallint] NULL,
    CONSTRAINT [PK_Physician] PRIMARY KEY CLUSTERED
(
[PhysicianID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[PhysicianAssessment]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[PhysicianAssessment](
    [PhysicianAssessmentID] [bigint] IDENTITY(1,1) NOT NULL,
    [EncounterID] [bigint] NOT NULL,
    [PhysicianAssessmentDate] [date] NULL,
    [PhysicianAssessmentTypeID] [smallint] NOT NULL,
    [ReferringProvider] [int] NULL,
    [ChiefComplaint] [varchar](max) NULL,
    [HistoryOfPresentIllness] [varchar](max) NULL,
    [SocialHistory] [varchar](max) NULL,
    [FamilyHistory] [varchar](max) NULL,
    [SignificantDiagnosticTests] [varchar](max) NULL,
    [Assessment] [varchar](max) NULL,
    [Plan] [varchar](max) NULL,
    [CoSignature] [int] NULL,
    [AuthoringProvider] [int] NOT NULL,
    [WrittenDateTime] [datetime] NULL,
    [LastUpdated] [datetime] NULL,
    [EditedBy] [int] NOT NULL,
    CONSTRAINT [pk_PhysicianAssessment] PRIMARY KEY CLUSTERED
(
[PhysicianAssessmentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[PhysicianAssessmentAllergies]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[PhysicianAssessmentAllergies](
    [PhysicianAssessmentID] [bigint] NOT NULL,
    [AllergenID] [int] NOT NULL,
     CONSTRAINT [PK_PhysicianAssessmentAllergies] PRIMARY KEY CLUSTERED
    (
    [PhysicianAssessmentID] ASC,
[AllergenID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[PhysicianAssessmentType]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[PhysicianAssessmentType](
    [PhysicianAssessmentTypeID] [smallint] IDENTITY(1,1) NOT NULL,
    [Name] [varchar](100) NOT NULL,
    [LastModified] [datetime] NULL,
    CONSTRAINT [pk_PhysicianAssessmentType] PRIMARY KEY CLUSTERED
(
[PhysicianAssessmentTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[PhysicianRole]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[PhysicianRole](
    [PhysicianRoleID] [int] IDENTITY(1,1) NOT NULL,
    [Name] [varchar](50) NOT NULL,
    [Description] [varchar](max) NULL,
    [LastModified] [datetime] NOT NULL,
    CONSTRAINT [PK_PhysicianRole] PRIMARY KEY CLUSTERED
(
[PhysicianRoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[PlaceOfServiceOutPatient]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[PlaceOfServiceOutPatient](
    [PlaceOfServiceID] [int] IDENTITY(1,1) NOT NULL,
    [WiPopCode] [tinyint] NOT NULL,
    [Description] [varchar](70) NULL,
    [LastModified] [datetime] NOT NULL,
    CONSTRAINT [PK_PlaceOfServiceOutPatient] PRIMARY KEY CLUSTERED
(
[PlaceOfServiceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[PointOfOrigin]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[PointOfOrigin](
    [PointOfOriginID] [int] IDENTITY(1,1) NOT NULL,
    [WiPopCode] [varchar](3) NOT NULL,
    [Description] [varchar](150) NULL,
    [Explaination] [varchar](max) NULL,
    [LastModified] [datetime] NOT NULL,
    CONSTRAINT [PK_PointOfOrigin] PRIMARY KEY CLUSTERED
(
[PointOfOriginID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[PreferredContactTime]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[PreferredContactTime](
    [ContactTimeID] [int] IDENTITY(1,1) NOT NULL,
    [Name] [varchar](50) NOT NULL,
    [Description] [varchar](max) NULL,
    [LastModified] [datetime] NOT NULL,
    CONSTRAINT [PK_PreferredContactTime] PRIMARY KEY CLUSTERED
(
[ContactTimeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[Priority]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[Priority](
    [PriorityID] [int] IDENTITY(1,1) NOT NULL,
    [PriorityName] [varchar](50) NOT NULL,
    PRIMARY KEY CLUSTERED
(
[PriorityID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[ProcedureReport]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[ProcedureReport](
    [EncounterID] [bigint] NOT NULL,
    [ProcedureReportID] [bigint] IDENTITY(1,1) NOT NULL,
    [ProcedureDate] [date] NULL,
    [PreoperativeDiagonsis] [varchar](max) NULL,
    [PostoperativeDiagnosis] [varchar](max) NULL,
    [OperativeIndications] [varchar](max) NULL,
    [ProcedurePerformed] [varchar](max) NULL,
    [DescriptionOfProcedure] [varchar](max) NULL,
    [Complications] [varchar](max) NULL,
    [EstiamtedBloodLoss] [decimal](18, 0) NULL,
    [Drains] [decimal](18, 0) NULL,
    [CoSignature] [int] NULL,
    [AuthoringProvider] [int] NOT NULL,
    [WrittenDateTime] [datetime] NULL,
    [LastUpdated] [datetime] NULL,
    [EditedBy] [int] NOT NULL,
    CONSTRAINT [pk_ProcedureReport] PRIMARY KEY CLUSTERED
(
[ProcedureReportID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[ProcedureReportAnestheticType]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[ProcedureReportAnestheticType](
    [ProcedureReportID] [bigint] NOT NULL,
    [AnestheticTypeID] [int] NOT NULL,
    [LastModified] [datetime] NULL,
     CONSTRAINT [pk_ProcedureReportAnestheticType] PRIMARY KEY CLUSTERED
    (
    [ProcedureReportID] ASC,
[AnestheticTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[ProcedureReportPhysician]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[ProcedureReportPhysician](
    [ProcedureReportID] [bigint] NOT NULL,
    [PhysicianID] [int] NOT NULL,
    [PhysicianRoleID] [int] NOT NULL,
    [LastModified] [datetime] NULL,
     CONSTRAINT [pk_ProcedureReportPhysician] PRIMARY KEY CLUSTERED
    (
    [ProcedureReportID] ASC,
[PhysicianID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[Program]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[Program](
    [ProgramID] [int] IDENTITY(1,1) NOT NULL,
    [Name] [varchar](255) NOT NULL,
    [IsActive] [bit] NOT NULL,
    CONSTRAINT [pk_Program] PRIMARY KEY CLUSTERED
(
[ProgramID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[ProgramFacilities]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[ProgramFacilities](
    [ProgramFacilitiesID] [int] IDENTITY(1,1) NOT NULL,
    [ProgramID] [int] NULL,
    [FacilityID] [int] NULL,
    CONSTRAINT [pk_ProgramFacilities] PRIMARY KEY CLUSTERED
(
[ProgramFacilitiesID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
    CONSTRAINT [uk_ProgramFacilities] UNIQUE NONCLUSTERED
(
    [ProgramID] ASC,
[FacilityID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[ProgressNote]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[ProgressNote](
    [ProgressNoteID] [bigint] IDENTITY(1,1) NOT NULL,
    [EncounterID] [bigint] NOT NULL,
    [NoteTypeID] [int] NOT NULL,
    [WrittenDate] [datetime] NULL,
    [Note] [varchar](max) NULL,
    [CoPhysicianID] [int] NULL,
    [PhysicianID] [int] NOT NULL,
    [LastUpdated] [datetime] NULL,
    [EditedBy] [int] NOT NULL,
    CONSTRAINT [pk_ProgressNoteID] PRIMARY KEY CLUSTERED
(
[ProgressNoteID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[ProviderStatus]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[ProviderStatus](
    [ProviderStatusID] [tinyint] IDENTITY(1,1) NOT NULL,
    [Status] [varchar](50) NOT NULL,
    [LastModified] [datetime] NULL,
    CONSTRAINT [pk_ProviderStatus] PRIMARY KEY CLUSTERED
(
[ProviderStatusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[ProviderType]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[ProviderType](
    [ProviderTypeID] [int] IDENTITY(1,1) NOT NULL,
    [Name] [varchar](100) NOT NULL,
    [Description] [varchar](max) NULL,
    [LastModified] [datetime] NOT NULL,
    [CosignRequired] [bit] NULL,
    CONSTRAINT [PK_ProviderType] PRIMARY KEY CLUSTERED
(
[ProviderTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[PulseRouteType]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[PulseRouteType](
    [PulseRouteTypeID] [int] IDENTITY(1,1) NOT NULL,
    [PulseRouteTypeName] [varchar](50) NOT NULL,
    [LastModified] [datetime] NOT NULL,
    CONSTRAINT [PK_PulseRouteType] PRIMARY KEY CLUSTERED
(
[PulseRouteTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[Race]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[Race](
    [RaceID] [tinyint] IDENTITY(1,1) NOT NULL,
    [Name] [varchar](50) NOT NULL,
    [Category] [varchar](30) NULL,
    [LastModified] [datetime] NOT NULL,
    CONSTRAINT [PK_Race] PRIMARY KEY CLUSTERED
(
[RaceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[Reaction]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[Reaction](
    [ReactionID] [int] IDENTITY(1,1) NOT NULL,
    [Name] [varchar](50) NOT NULL,
    [Description] [varchar](max) NULL,
    [LastModified] [datetime] NOT NULL,
    CONSTRAINT [PK_Reaction] PRIMARY KEY CLUSTERED
(
[ReactionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[Relationship]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[Relationship](
    [RelationshipID] [int] IDENTITY(1,1) NOT NULL,
    [Name] [varchar](50) NOT NULL,
    [Description] [varchar](max) NULL,
    [LastModified] [datetime] NOT NULL,
    CONSTRAINT [PK_Relationship] PRIMARY KEY CLUSTERED
(
[RelationshipID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[Religion]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[Religion](
    [ReligionID] [smallint] IDENTITY(1,1) NOT NULL,
    [Name] [varchar](100) NOT NULL,
    [Description] [varchar](max) NULL,
    [LastModified] [datetime] NOT NULL,
    CONSTRAINT [PK_Religion] PRIMARY KEY CLUSTERED
(
[ReligionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[Restrictions]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[Restrictions](
    [RestrictionID] [smallint] IDENTITY(1,1) NOT NULL,
    [Name] [varchar](100) NOT NULL,
    [IsActive] [bit] NOT NULL,
    CONSTRAINT [pk_Restriction] PRIMARY KEY CLUSTERED
(
[RestrictionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[SecurityQuestion]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[SecurityQuestion](
    [SecurityQuestionID] [int] IDENTITY(1,1) NOT NULL,
    [QuestionText] [varchar](255) NOT NULL,
    CONSTRAINT [pk_SecurityQuestion] PRIMARY KEY CLUSTERED
(
[SecurityQuestionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[Sex]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[Sex](
    [SexID] [tinyint] IDENTITY(1,1) NOT NULL,
    [Name] [varchar](25) NOT NULL,
    [LastModified] [datetime] NOT NULL,
    CONSTRAINT [PK_Sex] PRIMARY KEY CLUSTERED
(
[SexID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[SideEffect]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[SideEffect](
    [SideEffectID] [int] IDENTITY(1,1) NOT NULL,
    [SideEffectDescription] [varchar](100) NULL,
    PRIMARY KEY CLUSTERED
(
[SideEffectID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[Specialty]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[Specialty](
    [SpecialtyID] [int] IDENTITY(1,1) NOT NULL,
    [Name] [varchar](100) NOT NULL,
    [Description] [varchar](max) NULL,
    [LastModified] [datetime] NOT NULL,
    CONSTRAINT [PK_Specialty] PRIMARY KEY CLUSTERED
(
[SpecialtyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[TempRouteType]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[TempRouteType](
    [TempRouteTypeID] [int] IDENTITY(1,1) NOT NULL,
    [TempRouteTypeName] [varchar](50) NOT NULL,
    [LastModified] [datetime] NOT NULL,
    CONSTRAINT [PK_TempRouteType] PRIMARY KEY CLUSTERED
(
[TempRouteTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[Test]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[Test](
    [TestID] [bigint] IDENTITY(1,1) NOT NULL,
    [TestCategoryID] [bigint] NOT NULL,
    [TestName] [varchar](100) NOT NULL,
    [Description] [varchar](5000) NULL,
    PRIMARY KEY CLUSTERED
(
[TestID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[TestCategory]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[TestCategory](
    [TestCategoryID] [bigint] IDENTITY(1,1) NOT NULL,
    [CategoryDescription] [varchar](5000) NULL,
    [TestCategoryName] [varchar](50) NOT NULL,
    PRIMARY KEY CLUSTERED
(
[TestCategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[TestedBodyParts]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[TestedBodyParts](
    [TestID] [bigint] NOT NULL,
    [PartID] [bigint] NOT NULL,
     PRIMARY KEY CLUSTERED
    (
    [TestID] ASC,
[PartID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[UserFacility]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[UserFacility](
    [UserID] [int] NOT NULL,
    [FacilityID] [int] NOT NULL,
    [LastModified] [datetime] NOT NULL,
     CONSTRAINT [PK_UserFacility] PRIMARY KEY CLUSTERED
    (
    [UserID] ASC,
[FacilityID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[UserPrograms]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[UserPrograms](
    [UserID] [int] NOT NULL,
    [ProgramID] [int] NOT NULL,
     CONSTRAINT [pk_UserPrograms] PRIMARY KEY CLUSTERED
    (
    [UserID] ASC,
[ProgramID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[UserSecurityQuestion]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[UserSecurityQuestion](
    [UserID] [int] NOT NULL,
    [SecurityQuestionID] [int] NOT NULL,
    [AnswerHash] [varchar](max) NOT NULL,
    CONSTRAINT [pk_UserSecurityQuestion] PRIMARY KEY CLUSTERED
(
    [UserID] ASC,
[SecurityQuestionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[UserTable]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[UserTable](
    [UserID] [int] IDENTITY(1,1) NOT NULL,
    [FirstName] [varchar](50) NOT NULL,
    [LastName] [varchar](50) NOT NULL,
    [Email] [varchar](100) NOT NULL,
    [ProgramEnrolledIn] [varchar](100) NULL,
    [StartDate] [datetime] NOT NULL,
    [EndDate] [datetime] NOT NULL,
    [LastModified] [datetime] NOT NULL,
    [AspNetUsersID] [nvarchar](450) NOT NULL,
    [InstructorID] [int] NULL,
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED
(
[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[VisitType]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[VisitType](
    [VisitTypeID] [smallint] IDENTITY(1,1) NOT NULL,
    [Name] [varchar](100) NOT NULL,
    [LastModified] [datetime] NOT NULL,
    CONSTRAINT [pk_VisitType] PRIMARY KEY CLUSTERED
(
[VisitTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]
    GO
ALTER TABLE [dbo].[Address] ADD  DEFAULT (getdate()) FOR [LastModified]
    GO
ALTER TABLE [dbo].[AdmitOrder] ADD  DEFAULT (getdate()) FOR [LastModified]
    GO
ALTER TABLE [dbo].[AdmitType] ADD  DEFAULT (getdate()) FOR [LastModified]
    GO
ALTER TABLE [dbo].[AlertType] ADD  DEFAULT (getdate()) FOR [LastModified]
    GO
ALTER TABLE [dbo].[Allergen] ADD  DEFAULT (getdate()) FOR [LastModified]
    GO
ALTER TABLE [dbo].[AnestheticType] ADD  DEFAULT (getdate()) FOR [LastModified]
    GO
ALTER TABLE [dbo].[BodySystemAssessment] ADD  DEFAULT (getdate()) FOR [LastModified]
    GO
ALTER TABLE [dbo].[BodySystemType] ADD  DEFAULT (getdate()) FOR [LastModified]
    GO
ALTER TABLE [dbo].[CareSystemAssessment] ADD  CONSTRAINT [DF_CareSystemAssessment_IsWithinNormalLimits]  DEFAULT ((1)) FOR [IsWithinNormalLimits]
    GO
ALTER TABLE [dbo].[CareSystemAssessment] ADD  CONSTRAINT [DF__CareSyste__LastM__634EBE90]  DEFAULT (getdate()) FOR [LastModified]
    GO
ALTER TABLE [dbo].[CareSystemType] ADD  CONSTRAINT [DF__CareSyste__LastM__607251E5]  DEFAULT (getdate()) FOR [LastModified]
    GO
ALTER TABLE [dbo].[ClinicalReminders] ADD  DEFAULT ((0)) FOR [IsActive]
    GO
ALTER TABLE [dbo].[Country] ADD  DEFAULT (getdate()) FOR [LastModified]
    GO
ALTER TABLE [dbo].[Departments] ADD  DEFAULT (getdate()) FOR [LastModified]
    GO
ALTER TABLE [dbo].[Discharge] ADD  DEFAULT (getdate()) FOR [LastModified]
    GO
ALTER TABLE [dbo].[Employment] ADD  DEFAULT (getdate()) FOR [LastModified]
    GO
ALTER TABLE [dbo].[Encounter] ADD  DEFAULT (getdate()) FOR [AdmitDateTime]
    GO
ALTER TABLE [dbo].[Encounter] ADD  DEFAULT (getdate()) FOR [LastModified]
    GO
ALTER TABLE [dbo].[Encounter] ADD  DEFAULT (getdate()) FOR [WrittenDateTime]
    GO
ALTER TABLE [dbo].[Encounter] ADD  DEFAULT (getdate()) FOR [LastUpdated]
    GO
ALTER TABLE [dbo].[EncounterAlerts] ADD  DEFAULT (getdate()) FOR [LastModified]
    GO
ALTER TABLE [dbo].[EncounterPhysicians] ADD  DEFAULT (getdate()) FOR [LastModified]
    GO
ALTER TABLE [dbo].[EncounterType] ADD  DEFAULT (getdate()) FOR [LastModified]
    GO
ALTER TABLE [dbo].[Ethnicity] ADD  DEFAULT (getdate()) FOR [LastModified]
    GO
ALTER TABLE [dbo].[ExamType] ADD  DEFAULT (getdate()) FOR [LastModified]
    GO
ALTER TABLE [dbo].[Facility] ADD  DEFAULT (getdate()) FOR [LastModified]
    GO
ALTER TABLE [dbo].[FallRisks] ADD  DEFAULT ((0)) FOR [IsActive]
    GO
ALTER TABLE [dbo].[Gender] ADD  DEFAULT (getdate()) FOR [LastModified]
    GO
ALTER TABLE [dbo].[GenderPronoun] ADD  DEFAULT ((1)) FOR [IsActive]
    GO
ALTER TABLE [dbo].[Language] ADD  DEFAULT (getdate()) FOR [LastModified]
    GO
ALTER TABLE [dbo].[LegalStatus] ADD  DEFAULT ((0)) FOR [RequiresLegalGuardian]
    GO
ALTER TABLE [dbo].[LegalStatus] ADD  DEFAULT ((1)) FOR [IsActive]
    GO
ALTER TABLE [dbo].[MaritalStatus] ADD  DEFAULT (getdate()) FOR [LastModified]
    GO
ALTER TABLE [dbo].[Medication] ADD  DEFAULT ((1)) FOR [IsActive]
    GO
ALTER TABLE [dbo].[MedicationBrandName] ADD  DEFAULT ((1)) FOR [IsActive]
    GO
ALTER TABLE [dbo].[MedicationDeliveryRoute] ADD  DEFAULT ((1)) FOR [IsActive]
    GO
ALTER TABLE [dbo].[MedicationDosageForm] ADD  DEFAULT ((1)) FOR [IsActive]
    GO
ALTER TABLE [dbo].[MedicationGenericName] ADD  DEFAULT ((1)) FOR [IsActive]
    GO
ALTER TABLE [dbo].[NoteType] ADD  DEFAULT (getdate()) FOR [LastModified]
    GO
ALTER TABLE [dbo].[O2DeliveryType] ADD  DEFAULT (getdate()) FOR [LastModified]
    GO
ALTER TABLE [dbo].[OrderInfo] ADD  CONSTRAINT [df_OrderInfo_CoauthorApproved]  DEFAULT ((0)) FOR [CoAuthorApproved]
    GO
ALTER TABLE [dbo].[OrderInfo] ADD  DEFAULT ((0)) FOR [IsOrderComplete]
    GO
ALTER TABLE [dbo].[OrderInfo] ADD  DEFAULT ((0)) FOR [IsVerbalOrder]
    GO
ALTER TABLE [dbo].[PainScaleType] ADD  CONSTRAINT [DF__PainScale__LastM__3493CFA7]  DEFAULT (getdate()) FOR [LastModified]
    GO
ALTER TABLE [dbo].[Patient] ADD  CONSTRAINT [df_Patient_DeceasedLiving]  DEFAULT ((0)) FOR [DeceasedLiving]
    GO
ALTER TABLE [dbo].[Patient] ADD  CONSTRAINT [df_Patient_InterpreterNeeded]  DEFAULT ((0)) FOR [InterpreterNeeded]
    GO
ALTER TABLE [dbo].[Patient] ADD  DEFAULT (getdate()) FOR [LastModified]
    GO
ALTER TABLE [dbo].[Patient] ADD  DEFAULT ((0)) FOR [IsCurrentMilitaryServiceMember]
    GO
ALTER TABLE [dbo].[Patient] ADD  DEFAULT ((0)) FOR [IsVeteran]
    GO
ALTER TABLE [dbo].[PatientAdvancedDirectives] ADD  DEFAULT ((0)) FOR [LastModified]
    GO
ALTER TABLE [dbo].[PatientAlerts] ADD  DEFAULT (getdate()) FOR [LastModified]
    GO
ALTER TABLE [dbo].[PatientAlerts] ADD  DEFAULT ((1)) FOR [IsActive]
    GO
ALTER TABLE [dbo].[PatientAlerts] ADD  DEFAULT (getdate()) FOR [StartDate]
    GO
ALTER TABLE [dbo].[PatientAllergy] ADD  DEFAULT (getdate()) FOR [LastModified]
    GO
ALTER TABLE [dbo].[PatientClinicalReminders] ADD  DEFAULT (getdate()) FOR [LastModified]
    GO
ALTER TABLE [dbo].[PatientContactDetails] ADD  DEFAULT (getdate()) FOR [LastModified]
    GO
ALTER TABLE [dbo].[PatientEmergencyContact] ADD  DEFAULT (getdate()) FOR [LastModified]
    GO
ALTER TABLE [dbo].[PatientFallRisks] ADD  DEFAULT (getdate()) FOR [LastModified]
    GO
ALTER TABLE [dbo].[PatientLanguage] ADD  DEFAULT (getdate()) FOR [LastModified]
    GO
ALTER TABLE [dbo].[PatientRace] ADD  DEFAULT (getdate()) FOR [LastModified]
    GO
ALTER TABLE [dbo].[PatientRestrictions] ADD  DEFAULT (getdate()) FOR [LastModified]
    GO
ALTER TABLE [dbo].[PaymentPlan] ADD  DEFAULT (getdate()) FOR [LastModified]
    GO
ALTER TABLE [dbo].[PaymentSource] ADD  DEFAULT (getdate()) FOR [LastModified]
    GO
ALTER TABLE [dbo].[PCAComment] ADD  DEFAULT (getdate()) FOR [LastModified]
    GO
ALTER TABLE [dbo].[PCACommentType] ADD  DEFAULT (getdate()) FOR [LastModified]
    GO
ALTER TABLE [dbo].[PCARecord] ADD  CONSTRAINT [DF__PCARecord__LastM__51300E55]  DEFAULT (getdate()) FOR [LastModified]
    GO
ALTER TABLE [dbo].[Physician] ADD  DEFAULT (getdate()) FOR [LastModified]
    GO
ALTER TABLE [dbo].[PhysicianAssessment] ADD  DEFAULT (getdate()) FOR [WrittenDateTime]
    GO
ALTER TABLE [dbo].[PhysicianAssessment] ADD  DEFAULT (getdate()) FOR [LastUpdated]
    GO
ALTER TABLE [dbo].[PhysicianAssessmentType] ADD  DEFAULT (getdate()) FOR [LastModified]
    GO
ALTER TABLE [dbo].[PhysicianRole] ADD  DEFAULT (getdate()) FOR [LastModified]
    GO
ALTER TABLE [dbo].[PlaceOfServiceOutPatient] ADD  DEFAULT (getdate()) FOR [LastModified]
    GO
ALTER TABLE [dbo].[PointOfOrigin] ADD  DEFAULT (getdate()) FOR [LastModified]
    GO
ALTER TABLE [dbo].[PreferredContactTime] ADD  DEFAULT (getdate()) FOR [LastModified]
    GO
ALTER TABLE [dbo].[PreferredModeOfContact] ADD  DEFAULT (getdate()) FOR [LastModified]
    GO
ALTER TABLE [dbo].[ProcedureReport] ADD  DEFAULT (CONVERT([date],getdate())) FOR [ProcedureDate]
    GO
ALTER TABLE [dbo].[ProcedureReport] ADD  DEFAULT (getdate()) FOR [WrittenDateTime]
    GO
ALTER TABLE [dbo].[ProcedureReport] ADD  DEFAULT (getdate()) FOR [LastUpdated]
    GO
ALTER TABLE [dbo].[ProcedureReportAnestheticType] ADD  DEFAULT (getdate()) FOR [LastModified]
    GO
ALTER TABLE [dbo].[ProcedureReportPhysician] ADD  DEFAULT (getdate()) FOR [LastModified]
    GO
ALTER TABLE [dbo].[Program] ADD  DEFAULT ((1)) FOR [IsActive]
    GO
ALTER TABLE [dbo].[ProgressNote] ADD  DEFAULT (getdate()) FOR [WrittenDate]
    GO
ALTER TABLE [dbo].[ProgressNote] ADD  DEFAULT (getdate()) FOR [LastUpdated]
    GO
ALTER TABLE [dbo].[ProviderType] ADD  DEFAULT (getdate()) FOR [LastModified]
    GO
ALTER TABLE [dbo].[PulseRouteType] ADD  DEFAULT (getdate()) FOR [LastModified]
    GO
ALTER TABLE [dbo].[Race] ADD  DEFAULT (getdate()) FOR [LastModified]
    GO
ALTER TABLE [dbo].[Reaction] ADD  DEFAULT (getdate()) FOR [LastModified]
    GO
ALTER TABLE [dbo].[Relationship] ADD  DEFAULT (getdate()) FOR [LastModified]
    GO
ALTER TABLE [dbo].[Religion] ADD  DEFAULT (getdate()) FOR [LastModified]
    GO
ALTER TABLE [dbo].[Restrictions] ADD  DEFAULT ((0)) FOR [IsActive]
    GO
ALTER TABLE [dbo].[Sex] ADD  DEFAULT (getdate()) FOR [LastModified]
    GO
ALTER TABLE [dbo].[Specialty] ADD  DEFAULT (getdate()) FOR [LastModified]
    GO
ALTER TABLE [dbo].[TempRouteType] ADD  DEFAULT (getdate()) FOR [LastModified]
    GO
ALTER TABLE [dbo].[UserFacility] ADD  DEFAULT (getdate()) FOR [LastModified]
    GO
ALTER TABLE [dbo].[UserTable] ADD  DEFAULT (getdate()) FOR [StartDate]
    GO
ALTER TABLE [dbo].[UserTable] ADD  DEFAULT ('12/31/9999') FOR [EndDate]
    GO
ALTER TABLE [dbo].[UserTable] ADD  DEFAULT (getdate()) FOR [LastModified]
    GO
ALTER TABLE [dbo].[VisitType] ADD  DEFAULT (getdate()) FOR [LastModified]
    GO
ALTER TABLE [dbo].[Address]  WITH CHECK ADD  CONSTRAINT [fk_Address_CountryID] FOREIGN KEY([CountryID])
    REFERENCES [dbo].[Country] ([CountryID])
    GO
ALTER TABLE [dbo].[Address] CHECK CONSTRAINT [fk_Address_CountryID]
    GO
ALTER TABLE [dbo].[AdmitOrder]  WITH CHECK ADD  CONSTRAINT [fk_AdmitOrder_Department] FOREIGN KEY([DepartmentID])
    REFERENCES [dbo].[Departments] ([DepartmentID])
    GO
ALTER TABLE [dbo].[AdmitOrder] CHECK CONSTRAINT [fk_AdmitOrder_Department]
    GO
ALTER TABLE [dbo].[AdmitOrder]  WITH CHECK ADD  CONSTRAINT [fk_AdmitOrder_OrderInfoID] FOREIGN KEY([OrderInfoID])
    REFERENCES [dbo].[OrderInfo] ([OrderInfoID])
    GO
ALTER TABLE [dbo].[AdmitOrder] CHECK CONSTRAINT [fk_AdmitOrder_OrderInfoID]
    GO
ALTER TABLE [dbo].[AdmitOrder]  WITH CHECK ADD  CONSTRAINT [fk_AdmitOrder_VisitType] FOREIGN KEY([VisitTypeID])
    REFERENCES [dbo].[VisitType] ([VisitTypeID])
    GO
ALTER TABLE [dbo].[AdmitOrder] CHECK CONSTRAINT [fk_AdmitOrder_VisitType]
    GO
ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
    REFERENCES [dbo].[AspNetRoles] ([Id])
    GO
ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
    GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
    REFERENCES [dbo].[AspNetUsers] ([Id])
    GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
    GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
    REFERENCES [dbo].[AspNetUsers] ([Id])
    GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
    GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
    REFERENCES [dbo].[AspNetRoles] ([Id])
    GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
    GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
    REFERENCES [dbo].[AspNetUsers] ([Id])
    GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
    GO
ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])
    REFERENCES [dbo].[AspNetUsers] ([Id])
    GO
ALTER TABLE [dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
    GO
ALTER TABLE [dbo].[BodySystemAssessment]  WITH CHECK ADD  CONSTRAINT [fk_CareSystemType] FOREIGN KEY([BodySystemTypeId])
    REFERENCES [dbo].[BodySystemType] ([BodySystemTypeId])
    GO
ALTER TABLE [dbo].[BodySystemAssessment] CHECK CONSTRAINT [fk_CareSystemType]
    GO
ALTER TABLE [dbo].[BodySystemAssessment]  WITH CHECK ADD  CONSTRAINT [fk_ExamType] FOREIGN KEY([ExamTypeCode])
    REFERENCES [dbo].[ExamType] ([ExamTypeCode])
    GO
ALTER TABLE [dbo].[BodySystemAssessment] CHECK CONSTRAINT [fk_ExamType]
    GO
ALTER TABLE [dbo].[BodySystemAssessment]  WITH CHECK ADD  CONSTRAINT [fk_PhysicianAssessment] FOREIGN KEY([PhysicianAssessmentID])
    REFERENCES [dbo].[PhysicianAssessment] ([PhysicianAssessmentID])
    GO
ALTER TABLE [dbo].[BodySystemAssessment] CHECK CONSTRAINT [fk_PhysicianAssessment]
    GO
ALTER TABLE [dbo].[CareSystemAssessment]  WITH CHECK ADD  CONSTRAINT [fk_CareSystemAssessment_CareSystemParameterID] FOREIGN KEY([CareSystemParameterID])
    REFERENCES [dbo].[CareSystemParameter] ([CareSystemParameterID])
    GO
ALTER TABLE [dbo].[CareSystemAssessment] CHECK CONSTRAINT [fk_CareSystemAssessment_CareSystemParameterID]
    GO
ALTER TABLE [dbo].[CareSystemAssessment]  WITH CHECK ADD  CONSTRAINT [fk_CareSystemAssessment_PCAID] FOREIGN KEY([PCAID])
    REFERENCES [dbo].[PCARecord] ([PCAID])
    GO
ALTER TABLE [dbo].[CareSystemAssessment] CHECK CONSTRAINT [fk_CareSystemAssessment_PCAID]
    GO
ALTER TABLE [dbo].[CareSystemParameter]  WITH CHECK ADD  CONSTRAINT [fk_CareSystemParameter_CareSystemTypeID] FOREIGN KEY([CareSystemTypeID])
    REFERENCES [dbo].[CareSystemType] ([CareSystemTypeID])
    GO
ALTER TABLE [dbo].[CareSystemParameter] CHECK CONSTRAINT [fk_CareSystemParameter_CareSystemTypeID]
    GO
ALTER TABLE [dbo].[Employment]  WITH CHECK ADD  CONSTRAINT [fk_Employment_AddressID] FOREIGN KEY([AddressID])
    REFERENCES [dbo].[Address] ([AddressID])
    GO
ALTER TABLE [dbo].[Employment] CHECK CONSTRAINT [fk_Employment_AddressID]
    GO
ALTER TABLE [dbo].[Encounter]  WITH CHECK ADD  CONSTRAINT [fk_Encounter_AdmitTypeID] FOREIGN KEY([AdmitTypeID])
    REFERENCES [dbo].[AdmitType] ([AdmitTypeID])
    GO
ALTER TABLE [dbo].[Encounter] CHECK CONSTRAINT [fk_Encounter_AdmitTypeID]
    GO
ALTER TABLE [dbo].[Encounter]  WITH CHECK ADD  CONSTRAINT [fk_Encounter_DepartmentID] FOREIGN KEY([DepartmentID])
    REFERENCES [dbo].[Departments] ([DepartmentID])
    GO
ALTER TABLE [dbo].[Encounter] CHECK CONSTRAINT [fk_Encounter_DepartmentID]
    GO
ALTER TABLE [dbo].[Encounter]  WITH CHECK ADD  CONSTRAINT [fk_Encounter_DischargeDisposition] FOREIGN KEY([DischargeDisposition])
    REFERENCES [dbo].[Discharge] ([DischargeID])
    GO
ALTER TABLE [dbo].[Encounter] CHECK CONSTRAINT [fk_Encounter_DischargeDisposition]
    GO
ALTER TABLE [dbo].[Encounter]  WITH CHECK ADD  CONSTRAINT [fk_Encounter_EncounterTypeID] FOREIGN KEY([EncounterTypeID])
    REFERENCES [dbo].[EncounterType] ([EncounterTypeID])
    GO
ALTER TABLE [dbo].[Encounter] CHECK CONSTRAINT [fk_Encounter_EncounterTypeID]
    GO
ALTER TABLE [dbo].[Encounter]  WITH CHECK ADD  CONSTRAINT [fk_Encounter_FacilityID] FOREIGN KEY([FacilityID])
    REFERENCES [dbo].[Facility] ([FacilityID])
    GO
ALTER TABLE [dbo].[Encounter] CHECK CONSTRAINT [fk_Encounter_FacilityID]
    GO
ALTER TABLE [dbo].[Encounter]  WITH CHECK ADD  CONSTRAINT [fk_Encounter_MRN] FOREIGN KEY([MRN])
    REFERENCES [dbo].[Patient] ([MRN])
    GO
ALTER TABLE [dbo].[Encounter] CHECK CONSTRAINT [fk_Encounter_MRN]
    GO
ALTER TABLE [dbo].[Encounter]  WITH CHECK ADD  CONSTRAINT [fk_Encounter_PlaceOfServiceID] FOREIGN KEY([PlaceOfServiceID])
    REFERENCES [dbo].[PlaceOfServiceOutPatient] ([PlaceOfServiceID])
    GO
ALTER TABLE [dbo].[Encounter] CHECK CONSTRAINT [fk_Encounter_PlaceOfServiceID]
    GO
ALTER TABLE [dbo].[Encounter]  WITH CHECK ADD  CONSTRAINT [fk_Encounter_PointOfOriginID] FOREIGN KEY([PointOfOriginID])
    REFERENCES [dbo].[PointOfOrigin] ([PointOfOriginID])
    GO
ALTER TABLE [dbo].[Encounter] CHECK CONSTRAINT [fk_Encounter_PointOfOriginID]
    GO
ALTER TABLE [dbo].[Encounter]  WITH CHECK ADD  CONSTRAINT [fk_EncounterAuthoringProvider] FOREIGN KEY([AuthoringProvider])
    REFERENCES [dbo].[Physician] ([PhysicianID])
    GO
ALTER TABLE [dbo].[Encounter] CHECK CONSTRAINT [fk_EncounterAuthoringProvider]
    GO
ALTER TABLE [dbo].[Encounter]  WITH CHECK ADD  CONSTRAINT [fk_EncounterCoSignature] FOREIGN KEY([CoSignature])
    REFERENCES [dbo].[Physician] ([PhysicianID])
    GO
ALTER TABLE [dbo].[Encounter] CHECK CONSTRAINT [fk_EncounterCoSignature]
    GO
ALTER TABLE [dbo].[Encounter]  WITH CHECK ADD  CONSTRAINT [fk_EncounterPhysicianID] FOREIGN KEY([PhysicianID])
    REFERENCES [dbo].[Physician] ([PhysicianID])
    GO
ALTER TABLE [dbo].[Encounter] CHECK CONSTRAINT [fk_EncounterPhysicianID]
    GO
ALTER TABLE [dbo].[EncounterAlerts]  WITH CHECK ADD  CONSTRAINT [fk_EncounterAlerts_EncounterID] FOREIGN KEY([EncounterID])
    REFERENCES [dbo].[Encounter] ([EncounterID])
    ON DELETE CASCADE
GO
ALTER TABLE [dbo].[EncounterAlerts] CHECK CONSTRAINT [fk_EncounterAlerts_EncounterID]
    GO
ALTER TABLE [dbo].[EncounterAlerts]  WITH CHECK ADD  CONSTRAINT [fk_EncounterAlerts_PatientAlertID] FOREIGN KEY([PatientAlertID])
    REFERENCES [dbo].[PatientAlerts] ([PatientAlertID])
    ON DELETE CASCADE
GO
ALTER TABLE [dbo].[EncounterAlerts] CHECK CONSTRAINT [fk_EncounterAlerts_PatientAlertID]
    GO
ALTER TABLE [dbo].[EncounterPhysicians]  WITH CHECK ADD  CONSTRAINT [fk_EncounterPhysicians_EncounterID] FOREIGN KEY([EncounterID])
    REFERENCES [dbo].[Encounter] ([EncounterID])
    GO
ALTER TABLE [dbo].[EncounterPhysicians] CHECK CONSTRAINT [fk_EncounterPhysicians_EncounterID]
    GO
ALTER TABLE [dbo].[EncounterPhysicians]  WITH CHECK ADD  CONSTRAINT [fk_EncounterPhysicians_PhysicianID] FOREIGN KEY([PhysicianID])
    REFERENCES [dbo].[Physician] ([PhysicianID])
    GO
ALTER TABLE [dbo].[EncounterPhysicians] CHECK CONSTRAINT [fk_EncounterPhysicians_PhysicianID]
    GO
ALTER TABLE [dbo].[EncounterPhysicians]  WITH CHECK ADD  CONSTRAINT [fk_EncounterPhysicians_PhysicianRoleID] FOREIGN KEY([PhysicianRoleID])
    REFERENCES [dbo].[PhysicianRole] ([PhysicianRoleID])
    GO
ALTER TABLE [dbo].[EncounterPhysicians] CHECK CONSTRAINT [fk_EncounterPhysicians_PhysicianRoleID]
    GO
ALTER TABLE [dbo].[Facility]  WITH CHECK ADD  CONSTRAINT [fk_Facility_AddressID] FOREIGN KEY([AddressID])
    REFERENCES [dbo].[Address] ([AddressID])
    GO
ALTER TABLE [dbo].[Facility] CHECK CONSTRAINT [fk_Facility_AddressID]
    GO
ALTER TABLE [dbo].[Medication]  WITH CHECK ADD  CONSTRAINT [fk_Medication_BrandNameID] FOREIGN KEY([BrandNameID])
    REFERENCES [dbo].[MedicationBrandName] ([MedicationBrandID])
    GO
ALTER TABLE [dbo].[Medication] CHECK CONSTRAINT [fk_Medication_BrandNameID]
    GO
ALTER TABLE [dbo].[Medication]  WITH CHECK ADD  CONSTRAINT [fk_Medication_DeliveryRouteID] FOREIGN KEY([DeliveryRouteID])
    REFERENCES [dbo].[MedicationDeliveryRoute] ([DeliveryRouteID])
    GO
ALTER TABLE [dbo].[Medication] CHECK CONSTRAINT [fk_Medication_DeliveryRouteID]
    GO
ALTER TABLE [dbo].[Medication]  WITH CHECK ADD  CONSTRAINT [fk_Medication_DosageFormID] FOREIGN KEY([DosageFormID])
    REFERENCES [dbo].[MedicationDosageForm] ([DosageFormID])
    GO
ALTER TABLE [dbo].[Medication] CHECK CONSTRAINT [fk_Medication_DosageFormID]
    GO
ALTER TABLE [dbo].[Medication]  WITH CHECK ADD  CONSTRAINT [fk_Medication_GenericNameID] FOREIGN KEY([GenericNameID])
    REFERENCES [dbo].[MedicationGenericName] ([MedicationGenericID])
    GO
ALTER TABLE [dbo].[Medication] CHECK CONSTRAINT [fk_Medication_GenericNameID]
    GO
ALTER TABLE [dbo].[OrderInfo]  WITH CHECK ADD  CONSTRAINT [FK__OrderInfo__Encou__62AFA012] FOREIGN KEY([EncounterID])
    REFERENCES [dbo].[Encounter] ([EncounterID])
    ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OrderInfo] CHECK CONSTRAINT [FK__OrderInfo__Encou__62AFA012]
    GO
ALTER TABLE [dbo].[OrderInfo]  WITH CHECK ADD  CONSTRAINT [FK__OrderInfo__Order__63A3C44B] FOREIGN KEY([OrderTypeID])
    REFERENCES [dbo].[OrderType] ([OrderTypeID])
    GO
ALTER TABLE [dbo].[OrderInfo] CHECK CONSTRAINT [FK__OrderInfo__Order__63A3C44B]
    GO
ALTER TABLE [dbo].[OrderInfo]  WITH CHECK ADD  CONSTRAINT [FK__OrderInfo__Order__6497E884] FOREIGN KEY([OrderingAuthor])
    REFERENCES [dbo].[Physician] ([PhysicianID])
    GO
ALTER TABLE [dbo].[OrderInfo] CHECK CONSTRAINT [FK__OrderInfo__Order__6497E884]
    GO
ALTER TABLE [dbo].[OrderInfo]  WITH CHECK ADD  CONSTRAINT [FK__OrderInfo__Prior__658C0CBD] FOREIGN KEY([PriorityID])
    REFERENCES [dbo].[Priority] ([PriorityID])
    GO
ALTER TABLE [dbo].[OrderInfo] CHECK CONSTRAINT [FK__OrderInfo__Prior__658C0CBD]
    GO
ALTER TABLE [dbo].[OrderInfo]  WITH CHECK ADD  CONSTRAINT [fk_OrderInfo_AuthenticatingProviderID] FOREIGN KEY([AuthenticatingProviderID])
    REFERENCES [dbo].[Physician] ([PhysicianID])
    GO
ALTER TABLE [dbo].[OrderInfo] CHECK CONSTRAINT [fk_OrderInfo_AuthenticatingProviderID]
    GO
ALTER TABLE [dbo].[OrderInfo]  WITH CHECK ADD  CONSTRAINT [fk_OrderInfo_OrderCompletedByID] FOREIGN KEY([OrderCompletedByID])
    REFERENCES [dbo].[Physician] ([PhysicianID])
    GO
ALTER TABLE [dbo].[OrderInfo] CHECK CONSTRAINT [fk_OrderInfo_OrderCompletedByID]
    GO
ALTER TABLE [dbo].[PainParameter]  WITH CHECK ADD  CONSTRAINT [fk_PainParameter_PainScaleTypeID] FOREIGN KEY([PainScaleTypeID])
    REFERENCES [dbo].[PainScaleType] ([PainScaleTypeID])
    GO
ALTER TABLE [dbo].[PainParameter] CHECK CONSTRAINT [fk_PainParameter_PainScaleTypeID]
    GO
ALTER TABLE [dbo].[PainRating]  WITH CHECK ADD  CONSTRAINT [fk_PainRating_PainParameterID] FOREIGN KEY([PainParameterID])
    REFERENCES [dbo].[PainParameter] ([PainParameterID])
    GO
ALTER TABLE [dbo].[PainRating] CHECK CONSTRAINT [fk_PainRating_PainParameterID]
    GO
ALTER TABLE [dbo].[PainRatingImage]  WITH CHECK ADD  CONSTRAINT [fk_PainRatingImage_PainRatingID] FOREIGN KEY([PainRatingID])
    REFERENCES [dbo].[PainRating] ([PainRatingID])
    GO
ALTER TABLE [dbo].[PainRatingImage] CHECK CONSTRAINT [fk_PainRatingImage_PainRatingID]
    GO
ALTER TABLE [dbo].[Patient]  WITH CHECK ADD  CONSTRAINT [fk_Patient_EmploymentID] FOREIGN KEY([EmploymentID])
    REFERENCES [dbo].[Employment] ([EmploymentID])
    GO
ALTER TABLE [dbo].[Patient] CHECK CONSTRAINT [fk_Patient_EmploymentID]
    GO
ALTER TABLE [dbo].[Patient]  WITH CHECK ADD  CONSTRAINT [fk_Patient_EthnicityID] FOREIGN KEY([EthnicityID])
    REFERENCES [dbo].[Ethnicity] ([EthnicityID])
    GO
ALTER TABLE [dbo].[Patient] CHECK CONSTRAINT [fk_Patient_EthnicityID]
    GO
ALTER TABLE [dbo].[Patient]  WITH CHECK ADD  CONSTRAINT [fk_Patient_GenderID] FOREIGN KEY([GenderID])
    REFERENCES [dbo].[Gender] ([GenderID])
    GO
ALTER TABLE [dbo].[Patient] CHECK CONSTRAINT [fk_Patient_GenderID]
    GO
ALTER TABLE [dbo].[Patient]  WITH CHECK ADD  CONSTRAINT [fk_Patient_GenderPronounID] FOREIGN KEY([GenderPronounID])
    REFERENCES [dbo].[GenderPronoun] ([GenderPronounID])
    GO
ALTER TABLE [dbo].[Patient] CHECK CONSTRAINT [fk_Patient_GenderPronounID]
    GO
ALTER TABLE [dbo].[Patient]  WITH CHECK ADD  CONSTRAINT [fk_Patient_LegalStatusID] FOREIGN KEY([LegalStatusID])
    REFERENCES [dbo].[LegalStatus] ([LegalStatusID])
    GO
ALTER TABLE [dbo].[Patient] CHECK CONSTRAINT [fk_Patient_LegalStatusID]
    GO
ALTER TABLE [dbo].[Patient]  WITH CHECK ADD  CONSTRAINT [fk_Patient_MaritalStatusID] FOREIGN KEY([MaritalStatusID])
    REFERENCES [dbo].[MaritalStatus] ([MaritalStatusID])
    GO
ALTER TABLE [dbo].[Patient] CHECK CONSTRAINT [fk_Patient_MaritalStatusID]
    GO
ALTER TABLE [dbo].[Patient]  WITH CHECK ADD  CONSTRAINT [fk_Patient_ReligionID] FOREIGN KEY([ReligionID])
    REFERENCES [dbo].[Religion] ([ReligionID])
    GO
ALTER TABLE [dbo].[Patient] CHECK CONSTRAINT [fk_Patient_ReligionID]
    GO
ALTER TABLE [dbo].[Patient]  WITH CHECK ADD  CONSTRAINT [fk_Patient_SexID] FOREIGN KEY([SexID])
    REFERENCES [dbo].[Sex] ([SexID])
    GO
ALTER TABLE [dbo].[Patient] CHECK CONSTRAINT [fk_Patient_SexID]
    GO
ALTER TABLE [dbo].[PatientAdvancedDirectives]  WITH CHECK ADD  CONSTRAINT [fk_PatientAdvancedDirectives_AdvancedDirectiveID] FOREIGN KEY([AdvancedDirectiveID])
    REFERENCES [dbo].[AdvancedDirectives] ([AdvancedDirectiveID])
    GO
ALTER TABLE [dbo].[PatientAdvancedDirectives] CHECK CONSTRAINT [fk_PatientAdvancedDirectives_AdvancedDirectiveID]
    GO
ALTER TABLE [dbo].[PatientAdvancedDirectives]  WITH CHECK ADD  CONSTRAINT [fk_PatientAdvancedDirectives_PatientAlertID] FOREIGN KEY([PatientAlertID])
    REFERENCES [dbo].[PatientAlerts] ([PatientAlertID])
    ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PatientAdvancedDirectives] CHECK CONSTRAINT [fk_PatientAdvancedDirectives_PatientAlertID]
    GO
ALTER TABLE [dbo].[PatientAlerts]  WITH CHECK ADD  CONSTRAINT [fk_PatientAlerts_AlertTypeID] FOREIGN KEY([AlertTypeID])
    REFERENCES [dbo].[AlertType] ([AlertID])
    GO
ALTER TABLE [dbo].[PatientAlerts] CHECK CONSTRAINT [fk_PatientAlerts_AlertTypeID]
    GO
ALTER TABLE [dbo].[PatientAlerts]  WITH CHECK ADD  CONSTRAINT [fk_PatientAlerts_MRN] FOREIGN KEY([MRN])
    REFERENCES [dbo].[Patient] ([MRN])
    GO
ALTER TABLE [dbo].[PatientAlerts] CHECK CONSTRAINT [fk_PatientAlerts_MRN]
    GO
ALTER TABLE [dbo].[PatientAlias]  WITH CHECK ADD  CONSTRAINT [fk_PatientAlias_PatientMRN] FOREIGN KEY([PatientMRN])
    REFERENCES [dbo].[Patient] ([MRN])
    GO
ALTER TABLE [dbo].[PatientAlias] CHECK CONSTRAINT [fk_PatientAlias_PatientMRN]
    GO
ALTER TABLE [dbo].[PatientAllergy]  WITH CHECK ADD  CONSTRAINT [fk_PatientAllergy_AllergenID] FOREIGN KEY([AllergenID])
    REFERENCES [dbo].[Allergen] ([AllergenID])
    GO
ALTER TABLE [dbo].[PatientAllergy] CHECK CONSTRAINT [fk_PatientAllergy_AllergenID]
    GO
ALTER TABLE [dbo].[PatientAllergy]  WITH CHECK ADD  CONSTRAINT [fk_PatientAllergy_GenericaMedicationID] FOREIGN KEY([GenericMedicationID])
    REFERENCES [dbo].[MedicationGenericName] ([MedicationGenericID])
    GO
ALTER TABLE [dbo].[PatientAllergy] CHECK CONSTRAINT [fk_PatientAllergy_GenericaMedicationID]
    GO
ALTER TABLE [dbo].[PatientAllergy]  WITH CHECK ADD  CONSTRAINT [fk_PatientAllergy_PatientAlertID] FOREIGN KEY([PatientAlertID])
    REFERENCES [dbo].[PatientAlerts] ([PatientAlertID])
    ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PatientAllergy] CHECK CONSTRAINT [fk_PatientAllergy_PatientAlertID]
    GO
ALTER TABLE [dbo].[PatientAllergy]  WITH CHECK ADD  CONSTRAINT [fk_PatientAllergy_ReactionID] FOREIGN KEY([ReactionID])
    REFERENCES [dbo].[Reaction] ([ReactionID])
    GO
ALTER TABLE [dbo].[PatientAllergy] CHECK CONSTRAINT [fk_PatientAllergy_ReactionID]
    GO
ALTER TABLE [dbo].[PatientClinicalReminders]  WITH CHECK ADD  CONSTRAINT [fk_PatientClinicalReminders_ClinicalReminderID] FOREIGN KEY([ClinicalReminderID])
    REFERENCES [dbo].[ClinicalReminders] ([ClinicalReminderID])
    GO
ALTER TABLE [dbo].[PatientClinicalReminders] CHECK CONSTRAINT [fk_PatientClinicalReminders_ClinicalReminderID]
    GO
ALTER TABLE [dbo].[PatientClinicalReminders]  WITH CHECK ADD  CONSTRAINT [fk_PatientClinicalReminders_PatientAlertID] FOREIGN KEY([PatientAlertID])
    REFERENCES [dbo].[PatientAlerts] ([PatientAlertID])
    ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PatientClinicalReminders] CHECK CONSTRAINT [fk_PatientClinicalReminders_PatientAlertID]
    GO
ALTER TABLE [dbo].[PatientContactDetails]  WITH CHECK ADD  CONSTRAINT [fk_PatientContactDetails_MailingAddressID] FOREIGN KEY([MailingAddressID])
    REFERENCES [dbo].[Address] ([AddressID])
    GO
ALTER TABLE [dbo].[PatientContactDetails] CHECK CONSTRAINT [fk_PatientContactDetails_MailingAddressID]
    GO
ALTER TABLE [dbo].[PatientContactDetails]  WITH CHECK ADD  CONSTRAINT [fk_PatientContactDetails_MRN] FOREIGN KEY([MRN])
    REFERENCES [dbo].[Patient] ([MRN])
    GO
ALTER TABLE [dbo].[PatientContactDetails] CHECK CONSTRAINT [fk_PatientContactDetails_MRN]
    GO
ALTER TABLE [dbo].[PatientContactDetails]  WITH CHECK ADD  CONSTRAINT [fk_PatientContactDetails_ResidenceAddressID] FOREIGN KEY([ResidenceAddressID])
    REFERENCES [dbo].[Address] ([AddressID])
    GO
ALTER TABLE [dbo].[PatientContactDetails] CHECK CONSTRAINT [fk_PatientContactDetails_ResidenceAddressID]
    GO
ALTER TABLE [dbo].[PatientContactTimes]  WITH CHECK ADD  CONSTRAINT [fk_PatientContactTimes_ModeOfContact] FOREIGN KEY([ContactTimeID])
    REFERENCES [dbo].[PreferredContactTime] ([ContactTimeID])
    GO
ALTER TABLE [dbo].[PatientContactTimes] CHECK CONSTRAINT [fk_PatientContactTimes_ModeOfContact]
    GO
ALTER TABLE [dbo].[PatientContactTimes]  WITH CHECK ADD  CONSTRAINT [fk_PatientContactTimes_PatientContactID] FOREIGN KEY([PatientContactID])
    REFERENCES [dbo].[PatientContactDetails] ([PatientContactID])
    GO
ALTER TABLE [dbo].[PatientContactTimes] CHECK CONSTRAINT [fk_PatientContactTimes_PatientContactID]
    GO
ALTER TABLE [dbo].[PatientEmergencyContact]  WITH CHECK ADD  CONSTRAINT [fk_PatientEmergencyContact_ContactAddressID] FOREIGN KEY([ContactAddressID])
    REFERENCES [dbo].[Address] ([AddressID])
    GO
ALTER TABLE [dbo].[PatientEmergencyContact] CHECK CONSTRAINT [fk_PatientEmergencyContact_ContactAddressID]
    GO
ALTER TABLE [dbo].[PatientEmergencyContact]  WITH CHECK ADD  CONSTRAINT [fk_PatientEmergencyContact_ContactRelationshipID] FOREIGN KEY([ContactRelationshipID])
    REFERENCES [dbo].[Relationship] ([RelationshipID])
    GO
ALTER TABLE [dbo].[PatientEmergencyContact] CHECK CONSTRAINT [fk_PatientEmergencyContact_ContactRelationshipID]
    GO
ALTER TABLE [dbo].[PatientEmergencyContact]  WITH CHECK ADD  CONSTRAINT [fk_PatientEmergencyContact_MRN] FOREIGN KEY([MRN])
    REFERENCES [dbo].[Patient] ([MRN])
    GO
ALTER TABLE [dbo].[PatientEmergencyContact] CHECK CONSTRAINT [fk_PatientEmergencyContact_MRN]
    GO
ALTER TABLE [dbo].[PatientFallRisks]  WITH CHECK ADD  CONSTRAINT [fk_PatientFallRisks_FallRiskID] FOREIGN KEY([FallRiskID])
    REFERENCES [dbo].[FallRisks] ([FallRiskID])
    GO
ALTER TABLE [dbo].[PatientFallRisks] CHECK CONSTRAINT [fk_PatientFallRisks_FallRiskID]
    GO
ALTER TABLE [dbo].[PatientFallRisks]  WITH CHECK ADD  CONSTRAINT [fk_PatientFallRisks_PatientAlertID] FOREIGN KEY([PatientAlertID])
    REFERENCES [dbo].[PatientAlerts] ([PatientAlertID])
    ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PatientFallRisks] CHECK CONSTRAINT [fk_PatientFallRisks_PatientAlertID]
    GO
ALTER TABLE [dbo].[PatientInsurance]  WITH CHECK ADD  CONSTRAINT [fk_PatientInsurance_MRN] FOREIGN KEY([MRN])
    REFERENCES [dbo].[Patient] ([MRN])
    GO
ALTER TABLE [dbo].[PatientInsurance] CHECK CONSTRAINT [fk_PatientInsurance_MRN]
    GO
ALTER TABLE [dbo].[PatientInsurance]  WITH CHECK ADD  CONSTRAINT [fk_PatientInsurance_SubscriberRelationshipID] FOREIGN KEY([SubscriberRelationshipID])
    REFERENCES [dbo].[Relationship] ([RelationshipID])
    GO
ALTER TABLE [dbo].[PatientInsurance] CHECK CONSTRAINT [fk_PatientInsurance_SubscriberRelationshipID]
    GO
ALTER TABLE [dbo].[PatientLanguage]  WITH CHECK ADD  CONSTRAINT [fk_PatientLanguage_LanguageID] FOREIGN KEY([LanguageID])
    REFERENCES [dbo].[Language] ([LanguageID])
    GO
ALTER TABLE [dbo].[PatientLanguage] CHECK CONSTRAINT [fk_PatientLanguage_LanguageID]
    GO
ALTER TABLE [dbo].[PatientLanguage]  WITH CHECK ADD  CONSTRAINT [fk_PatientLanguage_MRN] FOREIGN KEY([MRN])
    REFERENCES [dbo].[Patient] ([MRN])
    GO
ALTER TABLE [dbo].[PatientLanguage] CHECK CONSTRAINT [fk_PatientLanguage_MRN]
    GO
ALTER TABLE [dbo].[PatientModeOfContacts]  WITH CHECK ADD  CONSTRAINT [fk_PatientModeOfContacts_ModeOfContact] FOREIGN KEY([ModeOfContactID])
    REFERENCES [dbo].[PreferredModeOfContact] ([ModeOfContactID])
    GO
ALTER TABLE [dbo].[PatientModeOfContacts] CHECK CONSTRAINT [fk_PatientModeOfContacts_ModeOfContact]
    GO
ALTER TABLE [dbo].[PatientModeOfContacts]  WITH CHECK ADD  CONSTRAINT [fk_PatientModeOfContacts_PatientContactID] FOREIGN KEY([PatientContactID])
    REFERENCES [dbo].[PatientContactDetails] ([PatientContactID])
    GO
ALTER TABLE [dbo].[PatientModeOfContacts] CHECK CONSTRAINT [fk_PatientModeOfContacts_PatientContactID]
    GO
ALTER TABLE [dbo].[PatientRace]  WITH CHECK ADD  CONSTRAINT [fk_PatientRace_MRN] FOREIGN KEY([MRN])
    REFERENCES [dbo].[Patient] ([MRN])
    GO
ALTER TABLE [dbo].[PatientRace] CHECK CONSTRAINT [fk_PatientRace_MRN]
    GO
ALTER TABLE [dbo].[PatientRace]  WITH CHECK ADD  CONSTRAINT [fk_PatientRace_RaceID] FOREIGN KEY([RaceID])
    REFERENCES [dbo].[Race] ([RaceID])
    GO
ALTER TABLE [dbo].[PatientRace] CHECK CONSTRAINT [fk_PatientRace_RaceID]
    GO
ALTER TABLE [dbo].[PatientRestrictions]  WITH CHECK ADD  CONSTRAINT [fk_PatientRestrictions_PatientAlertID] FOREIGN KEY([PatientAlertID])
    REFERENCES [dbo].[PatientAlerts] ([PatientAlertID])
    ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PatientRestrictions] CHECK CONSTRAINT [fk_PatientRestrictions_PatientAlertID]
    GO
ALTER TABLE [dbo].[PatientRestrictions]  WITH CHECK ADD  CONSTRAINT [fk_PatientRestrictions_RestrictionID] FOREIGN KEY([RestrictionTypeID])
    REFERENCES [dbo].[Restrictions] ([RestrictionID])
    GO
ALTER TABLE [dbo].[PatientRestrictions] CHECK CONSTRAINT [fk_PatientRestrictions_RestrictionID]
    GO
ALTER TABLE [dbo].[PCAComment]  WITH CHECK ADD  CONSTRAINT [fk_PCAComment_PCACommentTypeID] FOREIGN KEY([PCACommentTypeID])
    REFERENCES [dbo].[PCACommentType] ([PCACommentTypeID])
    GO
ALTER TABLE [dbo].[PCAComment] CHECK CONSTRAINT [fk_PCAComment_PCACommentTypeID]
    GO
ALTER TABLE [dbo].[PCAComment]  WITH CHECK ADD  CONSTRAINT [fk_PCAComment_PCAID] FOREIGN KEY([PCAID])
    REFERENCES [dbo].[PCARecord] ([PCAID])
    GO
ALTER TABLE [dbo].[PCAComment] CHECK CONSTRAINT [fk_PCAComment_PCAID]
    GO
ALTER TABLE [dbo].[PCAPainAssessment]  WITH CHECK ADD  CONSTRAINT [fk_PCAPainAssessment_PainParameterID] FOREIGN KEY([PainParameterID])
    REFERENCES [dbo].[PainParameter] ([PainParameterID])
    GO
ALTER TABLE [dbo].[PCAPainAssessment] CHECK CONSTRAINT [fk_PCAPainAssessment_PainParameterID]
    GO
ALTER TABLE [dbo].[PCAPainAssessment]  WITH CHECK ADD  CONSTRAINT [fk_PCAPainAssessment_PainRatingID] FOREIGN KEY([PainRatingID])
    REFERENCES [dbo].[PainRating] ([PainRatingID])
    GO
ALTER TABLE [dbo].[PCAPainAssessment] CHECK CONSTRAINT [fk_PCAPainAssessment_PainRatingID]
    GO
ALTER TABLE [dbo].[PCAPainAssessment]  WITH CHECK ADD  CONSTRAINT [fk_PCAPainAssessment_PCAID] FOREIGN KEY([PCAID])
    REFERENCES [dbo].[PCARecord] ([PCAID])
    GO
ALTER TABLE [dbo].[PCAPainAssessment] CHECK CONSTRAINT [fk_PCAPainAssessment_PCAID]
    GO
ALTER TABLE [dbo].[PCARecord]  WITH CHECK ADD  CONSTRAINT [fk_PCARecord_BloodPressureRouteTypeID] FOREIGN KEY([BloodPressureRouteTypeID])
    REFERENCES [dbo].[BloodPressureRouteType] ([BloodPressureRouteTypeID])
    GO
ALTER TABLE [dbo].[PCARecord] CHECK CONSTRAINT [fk_PCARecord_BloodPressureRouteTypeID]
    GO
ALTER TABLE [dbo].[PCARecord]  WITH CHECK ADD  CONSTRAINT [fk_PCARecord_BMIMethodID] FOREIGN KEY([BMIMethodID])
    REFERENCES [dbo].[BMIMethod] ([BMIMethodID])
    GO
ALTER TABLE [dbo].[PCARecord] CHECK CONSTRAINT [fk_PCARecord_BMIMethodID]
    GO
ALTER TABLE [dbo].[PCARecord]  WITH CHECK ADD  CONSTRAINT [fk_PCARecord_EncounterID] FOREIGN KEY([EncounterID])
    REFERENCES [dbo].[Encounter] ([EncounterID])
    ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PCARecord] CHECK CONSTRAINT [fk_PCARecord_EncounterID]
    GO
ALTER TABLE [dbo].[PCARecord]  WITH CHECK ADD  CONSTRAINT [fk_PCARecord_O2DeliveryTypeID] FOREIGN KEY([O2DeliveryTypeID])
    REFERENCES [dbo].[O2DeliveryType] ([O2DeliveryTypeID])
    GO
ALTER TABLE [dbo].[PCARecord] CHECK CONSTRAINT [fk_PCARecord_O2DeliveryTypeID]
    GO
ALTER TABLE [dbo].[PCARecord]  WITH CHECK ADD  CONSTRAINT [fk_PCARecord_PainScaleTypeID] FOREIGN KEY([PainScaleTypeID])
    REFERENCES [dbo].[PainScaleType] ([PainScaleTypeID])
    GO
ALTER TABLE [dbo].[PCARecord] CHECK CONSTRAINT [fk_PCARecord_PainScaleTypeID]
    GO
ALTER TABLE [dbo].[PCARecord]  WITH CHECK ADD  CONSTRAINT [fk_PCARecord_PulseRouteTypeID] FOREIGN KEY([PulseRouteTypeID])
    REFERENCES [dbo].[PulseRouteType] ([PulseRouteTypeID])
    GO
ALTER TABLE [dbo].[PCARecord] CHECK CONSTRAINT [fk_PCARecord_PulseRouteTypeID]
    GO
ALTER TABLE [dbo].[PCARecord]  WITH CHECK ADD  CONSTRAINT [fk_PCARecord_TempRouteTypeID] FOREIGN KEY([TempRouteTypeID])
    REFERENCES [dbo].[TempRouteType] ([TempRouteTypeID])
    GO
ALTER TABLE [dbo].[PCARecord] CHECK CONSTRAINT [fk_PCARecord_TempRouteTypeID]
    GO
ALTER TABLE [dbo].[Physician]  WITH CHECK ADD  CONSTRAINT [FK_Physician_AddressID] FOREIGN KEY([AddressID])
    REFERENCES [dbo].[Address] ([AddressID])
    GO
ALTER TABLE [dbo].[Physician] CHECK CONSTRAINT [FK_Physician_AddressID]
    GO
ALTER TABLE [dbo].[Physician]  WITH CHECK ADD  CONSTRAINT [FK_Physician_ProviderStatusID] FOREIGN KEY([ProviderStatusID])
    REFERENCES [dbo].[ProviderStatus] ([ProviderStatusID])
    GO
ALTER TABLE [dbo].[Physician] CHECK CONSTRAINT [FK_Physician_ProviderStatusID]
    GO
ALTER TABLE [dbo].[Physician]  WITH CHECK ADD  CONSTRAINT [FK_Physician_ProviderTypeID] FOREIGN KEY([ProviderTypeID])
    REFERENCES [dbo].[ProviderType] ([ProviderTypeID])
    GO
ALTER TABLE [dbo].[Physician] CHECK CONSTRAINT [FK_Physician_ProviderTypeID]
    GO
ALTER TABLE [dbo].[Physician]  WITH CHECK ADD  CONSTRAINT [FK_Physician_SpecialtyID] FOREIGN KEY([SpecialtyID])
    REFERENCES [dbo].[Specialty] ([SpecialtyID])
    GO
ALTER TABLE [dbo].[Physician] CHECK CONSTRAINT [FK_Physician_SpecialtyID]
    GO
ALTER TABLE [dbo].[PhysicianAssessment]  WITH CHECK ADD  CONSTRAINT [fk_PAAuthoringProvider] FOREIGN KEY([AuthoringProvider])
    REFERENCES [dbo].[Physician] ([PhysicianID])
    GO
ALTER TABLE [dbo].[PhysicianAssessment] CHECK CONSTRAINT [fk_PAAuthoringProvider]
    GO
ALTER TABLE [dbo].[PhysicianAssessment]  WITH CHECK ADD  CONSTRAINT [fk_PACoSignature] FOREIGN KEY([CoSignature])
    REFERENCES [dbo].[Physician] ([PhysicianID])
    GO
ALTER TABLE [dbo].[PhysicianAssessment] CHECK CONSTRAINT [fk_PACoSignature]
    GO
ALTER TABLE [dbo].[PhysicianAssessment]  WITH CHECK ADD  CONSTRAINT [fk_PAEncounterID] FOREIGN KEY([EncounterID])
    REFERENCES [dbo].[Encounter] ([EncounterID])
    ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PhysicianAssessment] CHECK CONSTRAINT [fk_PAEncounterID]
    GO
ALTER TABLE [dbo].[PhysicianAssessment]  WITH CHECK ADD  CONSTRAINT [fk_PAReferringProvider] FOREIGN KEY([ReferringProvider])
    REFERENCES [dbo].[Physician] ([PhysicianID])
    GO
ALTER TABLE [dbo].[PhysicianAssessment] CHECK CONSTRAINT [fk_PAReferringProvider]
    GO
ALTER TABLE [dbo].[PhysicianAssessment]  WITH CHECK ADD  CONSTRAINT [fk_PhysicianAssessmentTypeID] FOREIGN KEY([PhysicianAssessmentTypeID])
    REFERENCES [dbo].[PhysicianAssessmentType] ([PhysicianAssessmentTypeID])
    GO
ALTER TABLE [dbo].[PhysicianAssessment] CHECK CONSTRAINT [fk_PhysicianAssessmentTypeID]
    GO
ALTER TABLE [dbo].[PhysicianAssessmentAllergies]  WITH CHECK ADD  CONSTRAINT [FK_PhysicianAssessmentAllergies_Allergen] FOREIGN KEY([AllergenID])
    REFERENCES [dbo].[Allergen] ([AllergenID])
    GO
ALTER TABLE [dbo].[PhysicianAssessmentAllergies] CHECK CONSTRAINT [FK_PhysicianAssessmentAllergies_Allergen]
    GO
ALTER TABLE [dbo].[PhysicianAssessmentAllergies]  WITH CHECK ADD  CONSTRAINT [FK_PhysicianAssessmentAllergies_PhysicianAssessment] FOREIGN KEY([PhysicianAssessmentID])
    REFERENCES [dbo].[PhysicianAssessment] ([PhysicianAssessmentID])
    GO
ALTER TABLE [dbo].[PhysicianAssessmentAllergies] CHECK CONSTRAINT [FK_PhysicianAssessmentAllergies_PhysicianAssessment]
    GO
ALTER TABLE [dbo].[ProcedureReport]  WITH CHECK ADD  CONSTRAINT [fk_AuthoringProvider] FOREIGN KEY([AuthoringProvider])
    REFERENCES [dbo].[Physician] ([PhysicianID])
    GO
ALTER TABLE [dbo].[ProcedureReport] CHECK CONSTRAINT [fk_AuthoringProvider]
    GO
ALTER TABLE [dbo].[ProcedureReport]  WITH CHECK ADD  CONSTRAINT [fk_PRCoSignature] FOREIGN KEY([CoSignature])
    REFERENCES [dbo].[Physician] ([PhysicianID])
    GO
ALTER TABLE [dbo].[ProcedureReport] CHECK CONSTRAINT [fk_PRCoSignature]
    GO
ALTER TABLE [dbo].[ProcedureReport]  WITH CHECK ADD  CONSTRAINT [fk_PREncounterID] FOREIGN KEY([EncounterID])
    REFERENCES [dbo].[Encounter] ([EncounterID])
    ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProcedureReport] CHECK CONSTRAINT [fk_PREncounterID]
    GO
ALTER TABLE [dbo].[ProcedureReportAnestheticType]  WITH CHECK ADD  CONSTRAINT [fk_AnestheticTypeID] FOREIGN KEY([AnestheticTypeID])
    REFERENCES [dbo].[AnestheticType] ([AnestheticTypeID])
    GO
ALTER TABLE [dbo].[ProcedureReportAnestheticType] CHECK CONSTRAINT [fk_AnestheticTypeID]
    GO
ALTER TABLE [dbo].[ProcedureReportAnestheticType]  WITH CHECK ADD  CONSTRAINT [fk_PRATProcedureReportID] FOREIGN KEY([ProcedureReportID])
    REFERENCES [dbo].[ProcedureReport] ([ProcedureReportID])
    GO
ALTER TABLE [dbo].[ProcedureReportAnestheticType] CHECK CONSTRAINT [fk_PRATProcedureReportID]
    GO
ALTER TABLE [dbo].[ProcedureReportPhysician]  WITH CHECK ADD  CONSTRAINT [fk_PRPPhysicianID] FOREIGN KEY([PhysicianID])
    REFERENCES [dbo].[Physician] ([PhysicianID])
    GO
ALTER TABLE [dbo].[ProcedureReportPhysician] CHECK CONSTRAINT [fk_PRPPhysicianID]
    GO
ALTER TABLE [dbo].[ProcedureReportPhysician]  WITH CHECK ADD  CONSTRAINT [fk_PRPProcedureReportID] FOREIGN KEY([ProcedureReportID])
    REFERENCES [dbo].[ProcedureReport] ([ProcedureReportID])
    GO
ALTER TABLE [dbo].[ProcedureReportPhysician] CHECK CONSTRAINT [fk_PRPProcedureReportID]
    GO
ALTER TABLE [dbo].[ProgramFacilities]  WITH CHECK ADD  CONSTRAINT [fk_ProgramFacilities_FacilityID] FOREIGN KEY([FacilityID])
    REFERENCES [dbo].[Facility] ([FacilityID])
    GO
ALTER TABLE [dbo].[ProgramFacilities] CHECK CONSTRAINT [fk_ProgramFacilities_FacilityID]
    GO
ALTER TABLE [dbo].[ProgramFacilities]  WITH CHECK ADD  CONSTRAINT [fk_ProgramFacilities_ProgramID] FOREIGN KEY([ProgramID])
    REFERENCES [dbo].[Program] ([ProgramID])
    GO
ALTER TABLE [dbo].[ProgramFacilities] CHECK CONSTRAINT [fk_ProgramFacilities_ProgramID]
    GO
ALTER TABLE [dbo].[ProgressNote]  WITH CHECK ADD  CONSTRAINT [fk_CoPhysicanID] FOREIGN KEY([CoPhysicianID])
    REFERENCES [dbo].[Physician] ([PhysicianID])
    GO
ALTER TABLE [dbo].[ProgressNote] CHECK CONSTRAINT [fk_CoPhysicanID]
    GO
ALTER TABLE [dbo].[ProgressNote]  WITH CHECK ADD  CONSTRAINT [fk_NoteTypeID] FOREIGN KEY([NoteTypeID])
    REFERENCES [dbo].[NoteType] ([NoteTypeID])
    GO
ALTER TABLE [dbo].[ProgressNote] CHECK CONSTRAINT [fk_NoteTypeID]
    GO
ALTER TABLE [dbo].[ProgressNote]  WITH CHECK ADD  CONSTRAINT [fk_PNEncounterID] FOREIGN KEY([EncounterID])
    REFERENCES [dbo].[Encounter] ([EncounterID])
    ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProgressNote] CHECK CONSTRAINT [fk_PNEncounterID]
    GO
ALTER TABLE [dbo].[ProgressNote]  WITH CHECK ADD  CONSTRAINT [fk_PNPhysicanID] FOREIGN KEY([PhysicianID])
    REFERENCES [dbo].[Physician] ([PhysicianID])
    GO
ALTER TABLE [dbo].[ProgressNote] CHECK CONSTRAINT [fk_PNPhysicanID]
    GO
ALTER TABLE [dbo].[Test]  WITH CHECK ADD  CONSTRAINT [FK__Test__TestCatego__6D2D2E85] FOREIGN KEY([TestCategoryID])
    REFERENCES [dbo].[TestCategory] ([TestCategoryID])
    GO
ALTER TABLE [dbo].[Test] CHECK CONSTRAINT [FK__Test__TestCatego__6D2D2E85]
    GO
ALTER TABLE [dbo].[TestedBodyParts]  WITH CHECK ADD  CONSTRAINT [FK__TestedBod__PartI__70FDBF69] FOREIGN KEY([PartID])
    REFERENCES [dbo].[BodyPart] ([PartID])
    GO
ALTER TABLE [dbo].[TestedBodyParts] CHECK CONSTRAINT [FK__TestedBod__PartI__70FDBF69]
    GO
ALTER TABLE [dbo].[TestedBodyParts]  WITH CHECK ADD  CONSTRAINT [FK__TestedBod__TestI__70099B30] FOREIGN KEY([TestID])
    REFERENCES [dbo].[Test] ([TestID])
    GO
ALTER TABLE [dbo].[TestedBodyParts] CHECK CONSTRAINT [FK__TestedBod__TestI__70099B30]
    GO
ALTER TABLE [dbo].[UserFacility]  WITH CHECK ADD  CONSTRAINT [fk_UserFacility_FacilityID] FOREIGN KEY([FacilityID])
    REFERENCES [dbo].[Facility] ([FacilityID])
    GO
ALTER TABLE [dbo].[UserFacility] CHECK CONSTRAINT [fk_UserFacility_FacilityID]
    GO
ALTER TABLE [dbo].[UserFacility]  WITH CHECK ADD  CONSTRAINT [fk_UserFacility_UserID] FOREIGN KEY([UserID])
    REFERENCES [dbo].[UserTable] ([UserID])
    GO
ALTER TABLE [dbo].[UserFacility] CHECK CONSTRAINT [fk_UserFacility_UserID]
    GO
ALTER TABLE [dbo].[UserPrograms]  WITH CHECK ADD  CONSTRAINT [fk_UserPrograms_ProgramID] FOREIGN KEY([ProgramID])
    REFERENCES [dbo].[Program] ([ProgramID])
    GO
ALTER TABLE [dbo].[UserPrograms] CHECK CONSTRAINT [fk_UserPrograms_ProgramID]
    GO
ALTER TABLE [dbo].[UserPrograms]  WITH CHECK ADD  CONSTRAINT [fk_UserPrograms_UserID] FOREIGN KEY([UserID])
    REFERENCES [dbo].[UserTable] ([UserID])
    GO
ALTER TABLE [dbo].[UserPrograms] CHECK CONSTRAINT [fk_UserPrograms_UserID]
    GO
ALTER TABLE [dbo].[UserSecurityQuestion]  WITH CHECK ADD  CONSTRAINT [fk_UserSecurityQuestion_QuestionID] FOREIGN KEY([SecurityQuestionID])
    REFERENCES [dbo].[SecurityQuestion] ([SecurityQuestionID])
    GO
ALTER TABLE [dbo].[UserSecurityQuestion] CHECK CONSTRAINT [fk_UserSecurityQuestion_QuestionID]
    GO
ALTER TABLE [dbo].[UserSecurityQuestion]  WITH CHECK ADD  CONSTRAINT [fk_UserSecurityQuestion_UserID] FOREIGN KEY([UserID])
    REFERENCES [dbo].[UserTable] ([UserID])
    GO
ALTER TABLE [dbo].[UserSecurityQuestion] CHECK CONSTRAINT [fk_UserSecurityQuestion_UserID]
    GO
ALTER TABLE [dbo].[UserTable]  WITH CHECK ADD  CONSTRAINT [fk_InstructorID] FOREIGN KEY([InstructorID])
    REFERENCES [dbo].[UserTable] ([UserID])
    GO
ALTER TABLE [dbo].[UserTable] CHECK CONSTRAINT [fk_InstructorID]
    GO
ALTER TABLE [dbo].[UserTable]  WITH CHECK ADD  CONSTRAINT [fk_UserTable_AspNetUsersID] FOREIGN KEY([AspNetUsersID])
    REFERENCES [dbo].[AspNetUsers] ([Id])
    GO
ALTER TABLE [dbo].[UserTable] CHECK CONSTRAINT [fk_UserTable_AspNetUsersID]
    GO
ALTER TABLE [dbo].[PatientContactDetails]  WITH CHECK ADD  CONSTRAINT [ck_PatientContactDetails_EmailCheck] CHECK  (([EmailAddress] like '%_@__%.__%'))
    GO
ALTER TABLE [dbo].[PatientContactDetails] CHECK CONSTRAINT [ck_PatientContactDetails_EmailCheck]
    GO
ALTER TABLE [dbo].[UserTable]  WITH CHECK ADD  CONSTRAINT [ck_UserTable_EmailCheck] CHECK  (([Email] like '%_@__%.__%'))
    GO
ALTER TABLE [dbo].[UserTable] CHECK CONSTRAINT [ck_UserTable_EmailCheck]
    GO
/****** Object:  StoredProcedure [dbo].[GetNextMRN]    Script Date: 6/14/2023 12:06:44 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
/*************************************************
* Procedure: GetNextMRN
* Created by: R Koch
* Created Date: 5/7/2019
*
* This procedure gets the next MRN from the MRN sequence
* and adds the necessary leading zeros and returns
* a char(6).
*
* Change Log:
* --------------------------------------------
* 
*************************************************/
CREATE     PROCEDURE [dbo].[GetNextMRN]
as begin
DECLARE @MRN varchar(6)
DECLARE @MRNNumber int
DECLARE @Counter int

SET @Counter = 1
SET @MRNNumber = NEXT VALUE FOR dbo.MRN_ID


Select Cast(Concat(Replicate('0', 6-LEN(@MRNNumber)), @MRNNumber) as Char(6)) as MRN,
       '' as FirstName, '' as LastName, '' as MiddleName, '' as MaidenName,
       Cast(0 as SmallInt) as ReligionID, '' as AliasFirstName,
       '' as AliasMiddleName, '' as AliasLastName, '' as MothersMaidenName, cast(0 as Char(9)) as SSN,
       Cast('1/1/1900' as DateTime) as DOB,
       Cast('1/1/1900' as DateTime) as DeathDate,
       Cast(0 as TinyInt) as SexID, Cast(0 as TinyInt) as GenderID, cast(0 as bit) as DeceasedLiving,
       cast(0 as bit) as InterpreterNeeded, Cast(0 as TinyInt) as MaritalStatusID,
       Cast(0 as TinyInt) as EthnicityID, 0 as EmploymentID, GetDate() as LastModified, 0 as EditedBy,
       Cast(0 as bit) as IsCurrentMilitaryServiceMember,
       Cast(0 as bit) as IsVeteran,
       Cast(0 as tinyint) as LegalStatusID,
       Cast(0 as tinyint) as GenderPronounID

END
GO
