CREATE TABLE [dbo].[test] (
    [id]           INT          IDENTITY (1, 1) NOT NULL,
    [varchar10]      VARCHAR (10) NOT NULL,
    [intC]          INT          NOT NULL,
    [datetime]     DATETIME     NOT NULL,
    [varcharNull10]  VARCHAR (10) NULL,
    [intNull]      INT          NULL,
    [datetimeNull] DATETIME     NULL
);

