CREATE TABLE postalcode
(
    postal_code VARCHAR(15) PRIMARY KEY,
    street VARCHAR(100) NOT NULL,
    port INT NOT NULL,
    locality VARCHAR(50) NOT NULL,
    created_at DATE NOT NULL DEFAULT NOW(),
    updated_at DATE NOT NULL DEFAULT NOW(),
    deleted_at DATE DEFAULT NULL
);