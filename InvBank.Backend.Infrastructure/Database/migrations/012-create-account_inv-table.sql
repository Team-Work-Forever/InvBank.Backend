CREATE TABLE account_inv
(
    id uuid REFERENCES actives_investment_fund(id),
    account VARCHAR(35) REFERENCES account(iban),
    account_value NUMERIC(14,2) NOT NULL,
    join_at DATE NOT NULL DEFAULT NOW(),
    PRIMARY KEY (id, account)
);