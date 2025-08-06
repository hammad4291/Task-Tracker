-- Users table (auto-increments UserId)
create database Task_tracker
CREATE TABLE Users (
    UserId INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(50) NOT NULL ,
    Password NVARCHAR(100) NOT NULL
);

select * from Users


select * from Tasks

-- First drop the existing empty table
CREATE TABLE Tasks (
    TaskId INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT NOT NULL FOREIGN KEY REFERENCES Users(UserId),
    Title NVARCHAR(100) NOT NULL,
    Description NVARCHAR(MAX),
    Status NVARCHAR(20) DEFAULT 'Pending',
    CreatedDate DATETIME DEFAULT GETDATE(),
    CONSTRAINT FK_Tasks_Users FOREIGN KEY (UserId) REFERENCES Users(UserId)
);