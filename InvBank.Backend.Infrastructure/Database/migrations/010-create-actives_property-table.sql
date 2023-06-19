CREATE TABLE actives_property
(
    id uuid PRIMARY KEY DEFAULT uuid_generate_v4(),
    property_name VARCHAR(100) NOT NULL,
    initial_date DATE NOT NULL,
    duration INT NOT NULL,
    tax_percent NUMERIC(5,2) NOT NULL,
    designation VARCHAR(100) NOT NULL,
    postal_code VARCHAR(15) NOT NULL,
    property_value NUMERIC(14,2) NOT NULL,
    rent_value NUMERIC(9,2) NOT NULL,
    month_value NUMERIC(9,2) NOT NULL,
    yearly_value NUMERIC(5,2) NOT NULL,
    account VARCHAR(35) REFERENCES account(iban),
    created_at DATE NOT NULL DEFAULT NOW(),
    updated_at DATE NOT NULL DEFAULT NOW(),
    deleted_at DATE DEFAULT NULL
);