CREATE LOGIN testAccount   
    WITH PASSWORD = 'drift11';  
GO  

CREATE USER testAccount FOR LOGIN testAccount;  
GO 

EXEC sp_addrolemember'db_datareader',testAccount 
GO

EXEC sp_addrolemember'db_datawriter',testAccount 
GO