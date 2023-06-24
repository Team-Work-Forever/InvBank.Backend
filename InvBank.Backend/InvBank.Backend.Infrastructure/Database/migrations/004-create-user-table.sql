CREATE TABLE profile
(
    id uuid PRIMARY KEY REFERENCES auth(id),
    first_name VARCHAR(50) NOT NULL,
    last_name VARCHAR(50) NOT NULL,
    birth_date DATE NOT NULL,
    nif VARCHAR(9) NOT NULL,
    cc VARCHAR(8) NOT NULL,
    phone VARCHAR(15) NOT NULL,
    postal_code VARCHAR(15) NOT NULL,
    created_at DATE NOT NULL DEFAULT NOW(),
    updated_at DATE NOT NULL DEFAULT NOW(),
    deleted_at DATE DEFAULT NULL
);