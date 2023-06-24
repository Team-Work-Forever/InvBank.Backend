CREATE TABLE auth
(
    id uuid PRIMARY KEY DEFAULT uuid_generate_v4(),
    email VARCHAR(100) NOT NULL,
    user_password VARCHAR(100) NOT NULL,
    is_enable BOOLEAN NOT NULL,
    user_role INT DEFAULT 0,
    created_at DATE NOT NULL DEFAULT NOW(),
    updated_at DATE NOT NULL DEFAULT NOW(),
    deleted_at DATE DEFAULT NULL
);