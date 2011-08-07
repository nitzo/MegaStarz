INSERT INTO [MegaStarz].[dbo].[Artists]
		   ([Name]
		   ,[InsertDate])
	 VALUES
		   ('ABBA'
		   ,'6/08/2011')
GO

INSERT INTO [MegaStarz].[dbo].[Songs]
		   ([Name]
		   ,[InsertDate]
		   ,[ArtistId]
		   ,[Length]
		   ,[PlayUrl])
	 VALUES
		   ('Dancing Queen'
		   ,'6/8/2001'
		   ,1
		   ,450
		   ,'http://a.b.c.d/bla')
GO

INSERT INTO [MegaStarz].[dbo].[Songs]
		   ([Name]
		   ,[InsertDate]
		   ,[ArtistId]
		   ,[Length]
		   ,[PlayUrl])
	 VALUES
		   ('Waterloo'
		   ,'6/8/2001'
		   ,1
		   ,450
		   ,'http://a.b.c.d/bla')
GO



