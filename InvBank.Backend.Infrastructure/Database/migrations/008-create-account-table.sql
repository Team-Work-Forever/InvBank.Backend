CREATE TABLE account
(
    iban VARCHAR(35) PRIMARY KEY,
    bank VARCHAR(35) REFERENCES bank(iban),
    created_at DATE NOT NULL DEFAULT NOW(),
    updated_at DATE NOT NULL DEFAULT NOW(),
    deleted_at DATE DEFAULT NULL
);