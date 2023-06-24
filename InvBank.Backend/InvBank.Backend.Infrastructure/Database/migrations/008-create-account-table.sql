CREATE TABLE account
(
    iban VARCHAR(35) PRIMARY KEY,
    amount_value NUMERIC(14,2) DEFAULT 0,
    bank VARCHAR(35) REFERENCES bank(iban),
    created_at DATE NOT NULL DEFAULT NOW(),
    updated_at DATE NOT NULL DEFAULT NOW(),
    deleted_at DATE DEFAULT NULL
);