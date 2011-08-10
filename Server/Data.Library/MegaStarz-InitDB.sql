INSERT INTO [MegaStarz].[dbo].[Artists]
		   ([Name]
		   ,[InsertDate])
	 VALUES
		   ('Lady GA GA'
		   ,'08/08/2011')
GO

INSERT INTO [MegaStarz].[dbo].[Artists]
		   ([Name]
		   ,[InsertDate])
	 VALUES
		   ('ABBA'
		   ,'08/08/2011')
GO


INSERT INTO [MegaStarz].[dbo].[Songs]
		   ([Name]
		   ,[InsertDate]
		   ,[ArtistId]
		   ,[Length]
		   ,[PlayUrl])
	 VALUES
		   ('Paparazzi'
		   ,'08/08/2011'
		   ,1
		   ,219
		   ,'http://megastarz.blob.core.windows.net/songs/paparazzi.wmv')
GO

INSERT INTO [MegaStarz].[dbo].[Songs]
		   ([Name]
		   ,[InsertDate]
		   ,[ArtistId]
		   ,[Length]
		   ,[PlayUrl])
	 VALUES
		   ('Poker Face'
		   ,'08/08/2011'
		   ,1
		   ,253
		   ,'http://megastarz.blob.core.windows.net/songs/pokerface.wmv')
GO

INSERT INTO [MegaStarz].[dbo].[Songs]
		   ([Name]
		   ,[InsertDate]
		   ,[ArtistId]
		   ,[Length]
		   ,[PlayUrl])
	 VALUES
		   ('Mamma Mia'
		   ,'08/08/2011'
		   ,2
		   ,253 --Fix Length!!
		   ,'http://megastarz.blob.core.windows.net/songs/mammamia.wmv')
GO

INSERT INTO [MegaStarz].[dbo].[Songs]
		   ([Name]
		   ,[InsertDate]
		   ,[ArtistId]
		   ,[Length]
		   ,[PlayUrl])
	 VALUES
		   ('The Winner Takes It All'
		   ,'08/08/2011'
		   ,2
		   ,253 --Fix Length!!
		   ,'http://megastarz.blob.core.windows.net/songs/winnertakesitall.wmv')
GO

