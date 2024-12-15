CREATE DATABASE timeServer_db;

USE timeServer_db;

IF NOT EXISTS (SELECT * FROM information_schema.tables WHERE table_name = 'times')
CREATE TABLE times (
    id INT IDENTITY(1,1) PRIMARY KEY,
);