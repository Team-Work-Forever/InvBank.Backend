CREATE TABLE bank
(
    iban VARCHAR(35) PRIMARY KEY,
    phone VARCHAR(15) NOT NULL,
    postal_code VARCHAR(15) NOT NULL,
    created_at DATE NOT NULL DEFAULT NOW(),
    updated_at DATE NOT NULL DEFAULT NOW(),
    deleted_at DATE DEFAULT NULL
);