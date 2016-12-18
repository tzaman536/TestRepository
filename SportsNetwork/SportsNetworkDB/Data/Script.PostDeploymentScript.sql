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
insert into LeagueLevels(LeagueLevel) values('3.0')
insert into LeagueLevels(LeagueLevel) values('3.5')
insert into LeagueLevels(LeagueLevel) values('4.0')
insert into LeagueLevels(LeagueLevel) values('4.5')
insert into LeagueLevels(LeagueLevel) values('5.0')


/************************
-- League Types
************************/
insert into LeagueTypes(LeagueType) values('Men''s Singles')
insert into LeagueTypes(LeagueType) values('Mens''s Doubles')
insert into LeagueTypes(LeagueType) values('Women''s Singles')
insert into LeagueTypes(LeagueType) values('Women''s Doubles')
insert into LeagueTypes(LeagueType) values('Mixed Singles')
insert into LeagueTypes(LeagueType) values('Mixed Doubles')
