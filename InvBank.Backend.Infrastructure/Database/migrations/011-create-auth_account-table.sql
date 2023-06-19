CREATE TABLE auth_account
(
    auth uuid REFERENCES auth(id),
    account VARCHAR(35) REFERENCES account(iban),
    join_at DATE NOT NULL DEFAULT NOW(),
    PRIMARY KEY (auth, account)
);