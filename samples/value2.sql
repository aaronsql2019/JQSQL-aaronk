SELECT 
	UserId, 
	FirstName, 
	LastName, 
	BirthDate, 
	Gender, 
	-- Returns the list of send dates of first message in conversations
	jqsql.getvalue(Conversations, 'Messages[0].SendDate') as SendDates
FROM dbo.Users




