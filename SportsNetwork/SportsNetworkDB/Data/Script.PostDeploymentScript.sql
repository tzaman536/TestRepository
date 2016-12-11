/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
/************************
-- League Levels
************************/
insert into LeagueLevel(Level) values('3.0')
insert into LeagueLevel(Level) values('3.5')
insert into LeagueLevel(Level) values('4.0')
insert into LeagueLevel(Level) values('4.5')
insert into LeagueLevel(Level) values('5.0')


/************************
-- League Types
************************/
insert into LeagueTypes(Type) values('Men''s Singles')
insert into LeagueTypes(Type) values('Mens''s Doubles')
insert into LeagueTypes(Type) values('Women''s Singles')
insert into LeagueTypes(Type) values('Women''s Doubles')
insert into LeagueTypes(Type) values('Mixed Singles')
insert into LeagueTypes(Type) values('Mixed Doubles')
