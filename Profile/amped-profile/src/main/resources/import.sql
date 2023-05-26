CREATE TABLE IF NOT EXISTS profile (
   nickName varchar(255),
   bio varchar(255),
   userId varchar(255),
   primary key (userId)
);

MERGE INTO profile (nickName, bio, userId) VALUES ('yurbur', 'This is my message.', 'google-oauth2|106008494241933327801');
MERGE INTO profile (nickName, bio, userId) VALUES ('themokkaman', 'My words. My message.', 'google-oauth2|100000000000000000001');
