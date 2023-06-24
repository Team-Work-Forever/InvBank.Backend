insert into bank
values
    (
        'PT50 1234 1785 123456789011 72',
        '914741963',
        '4444-444'
);

insert into auth
    (id, email, user_password, is_enable, user_role)
values
    (
        '2f380755-8ca0-487f-9dc5-95ec28ea449a',
        'admin@gmail.com',
        '$2a$12$FUM1huGu90s2YTiXIJ7UMe0hhHFuCjpnNmchtJDvnWLa6ZAJYscVq',
        TRUE,
        2
    );

insert into profile
    (id, first_name, last_name, birth_date, nif, cc, phone, postal_code)
values
    (
        '2f380755-8ca0-487f-9dc5-95ec28ea449a',
        'Admin',
        'Admin',
        '2003-07-29',
        '123456789',
        '12345678',
        '123456789',
        '4444-444'
    );

insert into auth
    (id, email, user_password, is_enable, user_role)
values
    (
        'c3f48686-0b51-43b7-b8ea-39063b538b95',
        'manager@gmail.com',
        '$2a$12$FUM1huGu90s2YTiXIJ7UMe0hhHFuCjpnNmchtJDvnWLa6ZAJYscVq',
        TRUE,
        1
    );

insert into profile
    (id, first_name, last_name, birth_date, nif, cc, phone, postal_code)
values
    (
        'c3f48686-0b51-43b7-b8ea-39063b538b95',
        'User',
        'Manager',
        '2003-07-29',
        '123456789',
        '12345678',
        '123456789',
        '4444-444'
    );