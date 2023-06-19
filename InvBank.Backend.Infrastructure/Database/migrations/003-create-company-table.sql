CREATE TABLE company
(
     id uuid PRIMARY KEY REFERENCES auth(id),
     company_name VARCHAR(50) NOT NULL,
     nif VARCHAR(9) NOT NULL,
     phone VARCHAR(15) NOT NULL,
     postal_code VARCHAR(15) NOT NULL,
     created_at DATE NOT NULL DEFAULT NOW(),
     updated_at DATE NOT NULL DEFAULT NOW(),
     deleted_at DATE DEFAULT NULL
);