CREATE TABLE [dbo].[test] (
    [id]           INT          IDENTITY (1, 1) NOT NULL,
    [varchar]      VARCHAR (10) NOT NULL,
    [int]          INT          NOT NULL,
    [datetime]     DATETIME     NOT NULL,
    [varcharNull]  VARCHAR (10) NULL,
    [intNull]      INT          NULL,
    [datetimeNull] DATETIME     NULL
);

