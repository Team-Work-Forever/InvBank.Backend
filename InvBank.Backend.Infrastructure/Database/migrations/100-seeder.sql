insert into bank
values
    (
        'PT50 1234 1785 123456789011 72',
        '914741963',
        '4444-444'
);

insert into auth
    (email, user_password, is_enable, user_role)
values
    (
        'admin@gmail.com',
        '$2a$12$FUM1huGu90s2YTiXIJ7UMe0hhHFuCjpnNmchtJDvnWLa6ZAJYscVq',
        TRUE,
        2
    );

insert into auth
    (email, user_password, is_enable, user_role)
values
    (
        'manager@gmail.com',
        '$2a$12$FUM1huGu90s2YTiXIJ7UMe0hhHFuCjpnNmchtJDvnWLa6ZAJYscVq',
        TRUE,
        1
    );