CREATE TABLE payment_property
(
    id uuid PRIMARY KEY DEFAULT uuid_generate_v4(),
    ative_id uuid NOT NULL REFERENCES actives_property(id),
    payment_date DATE DEFAULT now(),
    amount NUMERIC(14,2) DEFAULT 0
);