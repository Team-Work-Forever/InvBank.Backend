CREATE TABLE ative_state_property
(
    id uuid PRIMARY KEY DEFAULT uuid_generate_v4(),
    ative_id uuid NOT NULL REFERENCES actives_property(id),
    is_active BOOLEAN DEFAULT TRUE,
    init_date DATE DEFAULT now(),
    end_date DATE DEFAULT NULL
);