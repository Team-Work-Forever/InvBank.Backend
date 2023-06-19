CREATE TABLE actives_deposit_account
(
    id uuid PRIMARY KEY DEFAULT uuid_generate_v4(),
    deposit_name VARCHAR(100) NOT NULL,
    initial_date DATE NOT NULL,
    duration INT NOT NULL,
    tax_percent NUMERIC(5,2) NOT NULL,
    deposit_value NUMERIC(14,2) NOT NULL,
    yearly_tax NUMERIC(5,2) NOT NULL,
    account VARCHAR(35) REFERENCES account(iban),
    created_at DATE NOT NULL DEFAULT NOW(),
    updated_at DATE NOT NULL DEFAULT NOW(),
    deleted_at DATE DEFAULT NULL
);