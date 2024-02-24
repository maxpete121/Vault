CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture',
  coverImg varchar(500) DEFAULT 'https://i.redd.it/d2hd73xxwvaa1.jpg' COMMENT 'User CoverImg'
) default charset utf8 COMMENT '';



CREATE TABLE keeps(
  id INT AUTO_INCREMENT PRIMARY KEY,
  creatorId VARCHAR(255),
  name VARCHAR(60) NOT NULL,
  description VARCHAR(400) NOT NULL,
  img VARCHAR(500) NOT NULL,
  views INT,
  kept INT,
  Foreign Key (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';

        INSERT INTO keeps
        ( name, description, img, views)
        VALUES
        ( 'yes', 'wow', 'https:plswork.com', 4);



CREATE TABLE vaults(
  id INT AUTO_INCREMENT PRIMARY KEY,
  creatorId VARCHAR(255),
  name VARCHAR(60) NOT NULL,
  description VARCHAR(400) NOT NULL,
  img VARCHAR(500) NOT NULL,
  isPrivate BOOLEAN,
  Foreign Key (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';

CREATE TABLE vaultkeeps(
  id INT AUTO_INCREMENT PRIMARY KEY,
  creatorId VARCHAR(255),
  vaultId INT NOT NULL,
  keepId INT NOT NULL,
  Foreign Key (creatorId) REFERENCES accounts(id) ON DELETE CASCADE,
  Foreign Key (vaultId) REFERENCES vaults(id) ON DELETE CASCADE,
  Foreign Key (keepId) REFERENCES keeps(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';


CREATE TABLE tags(
  id INT AUTO_INCREMENT PRIMARY KEY,
  creatorId VARCHAR(255),
  keepId INT NOT NULL,
  name VARCHAR(30) NOT NULL,
  Foreign Key (creatorId) REFERENCES accounts(id) ON DELETE CASCADE,
  Foreign Key (keepId) REFERENCES keeps(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';


SELECT MAX(views) AS views,
FROM keeps
WHERE creatorId = '65837617fb48f6cd659dd941'


        INSERT INTO vaultkeeps
        (creatorId, vaultId, keepId)
        VALUES
        ('65837617fb48f6cd659dd941', 2, 1);

        SELECT
        keeps.*,
        vaultkeeps.*,
        accounts.*
        FROM vaultkeeps
        JOIN keeps ON vaultkeeps.keepId = keeps.id
        JOIN accounts ON vaultkeeps.creatorId = accounts.id
        WHERE vaultkeeps.id = 1;
        SELECT
        *
        FROM accounts
        WHERE id = '65837617fb48f6cd659dd941';

        SELECT
        vaults.*,
        accounts.*
        FROM vaults
        JOIN accounts ON vaults.creatorId = accounts.id
        WHERE vaults.creatorId = '65837617fb48f6cd659dd941'

        SELECT
        *
        FROM vaults
        WHERE creatorId = '65837617fb48f6cd659dd941' AND isPrivate = false

            UPDATE accounts SET 
              name = 'Yes',
              picture = 'none',
              coverImg ='none'
            WHERE id = '65837617fb48f6cd659dd941'