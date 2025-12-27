-- Criando tipos de usuários
INSERT INTO TypeUsers VALUES
('Admin'),
('Cliente');

-- Visualizando os tipos de usuários
SELECT * FROM TypeUsers;

-- Criando usuários
INSERT INTO Users (Name, Email, TypeUserId) VALUES
('Vanessa', 'vanessa@email.com', 1),
('Dioga', 'diogo@email.com', 2);

-- Visualizando usuários
SELECT * FROM Users;

-- Atualizando um usuário
UPDATE Users
SET Name = 'Diogo'
WHERE Email = 'diogo@email.com';

-- Deletando um usuário
DELETE FROM Users
WHERE Id = 5;

-------------------------------------------
-- Criando uma situação embaraçosa, com On delete Cascade não da erro

INSERT INTO Orders VALUES
(2);

SELECT * FROM Orders;

DELETE FROM Users
WHERE Id = 2;