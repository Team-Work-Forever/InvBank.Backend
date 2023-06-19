CREATE TABLE actives_investment_fund
(
    id uuid PRIMARY KEY DEFAULT uuid_generate_v4(),
    invest_name VARCHAR(100) NOT NULL,
    initial_date DATE NOT NULL,
    duration INT NOT NULL,
    invest_value NUMERIC(14,2) NOT NULL,
    tax_percent NUMERIC(5,2) NOT NULL,
    created_at DATE NOT NULL DEFAULT NOW(),
    updated_at DATE NOT NULL DEFAULT NOW(),
    deleted_at DATE DEFAULT NULL
);