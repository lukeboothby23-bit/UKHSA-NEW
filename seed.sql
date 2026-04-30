INSERT INTO public."AspNetUsers"
(
    "Id",
    "UserName",
    "NormalizedUserName",
    "Email",
    "NormalizedEmail",
    "EmailConfirmed",
    "PasswordHash",
    "SecurityStamp",
    "ConcurrencyStamp",
    "PhoneNumber",
    "PhoneNumberConfirmed",
    "TwoFactorEnabled",
    "LockoutEnd",
    "LockoutEnabled",
    "AccessFailedCount",
    "Forename",
    "Surname"
)
VALUES
(
    'd3147dce-4e87-456a-8cf2-8b12df5f5508',
    'admin',
    'ADMIN',
    'admin@admin.com',
    'ADMIN@ADMIN.COM',
    false,
    'AQAAAAIAAYagAAAAELqoQ8OVv47iCW4KSokdUFZsZ8wawN51btTiZXdvz7zApa4tovTkS8p1m7fmcSdA8Q==',
    'H4L5DR62YARAC2BMEKN6TC2A46XCYUXD',
    'eb231549-7352-4d09-bc9b-23017114015b',
    NULL,
    false,
    false,
    NULL,
    true,
    0,
    'Admin',
    'User'
);

INSERT INTO public."AspNetUsers"
(
    "Id","UserName","NormalizedUserName","Email","NormalizedEmail",
    "EmailConfirmed","PasswordHash","SecurityStamp","ConcurrencyStamp",
    "PhoneNumber","PhoneNumberConfirmed","TwoFactorEnabled","LockoutEnd",
    "LockoutEnabled","AccessFailedCount","Forename","Surname"
)
VALUES
(
    '5051c1df-d7f7-492d-937b-8a431b704aee',
    'approver',
    'APPROVER',
    'approver@approver.com',
    'APPROVER@APPROVER.COM',
    false,
    'AQAAAAIAAYagAAAAEOfZlWcat/w8weDkgQVZBPnVjElK88WkecmEclKP3uu2zmxRKHC7eb08bXNR46Vwxw==',
    'DERHB2VPVGH3RW3RVFKLQFJQYGJLXLAF',
    'd64accdb-0fc6-40fa-bb01-8133efc96862',
    NULL,
    false,
    false,
    NULL,
    true,
    0,
    'Approver',
    'User'
);

INSERT INTO public."AspNetUsers"
(
    "Id","UserName","NormalizedUserName","Email","NormalizedEmail",
    "EmailConfirmed","PasswordHash","SecurityStamp","ConcurrencyStamp",
    "PhoneNumber","PhoneNumberConfirmed","TwoFactorEnabled","LockoutEnd",
    "LockoutEnabled","AccessFailedCount","Forename","Surname"
)
VALUES
(
    '67485421-f406-4d9f-a823-68fc0a05d7cc',
    'user',
    'USER',
    'user@user.com',
    'USER@USER.COM',
    false,
    'AQAAAAIAAYagAAAAEBpHbs89WAFIGhmvGxpaErI37NqmC0PZpGet6+2AGJKp0qOjtly8tEtqWC9V3OCa6Q==',
    '4U26JV6HG7M7H7Z4JUXZXNY34JV65LY7',
    'e4854784-e385-4aa1-9204-be3ff81d38c9',
    NULL,
    false,
    false,
    NULL,
    true,
    0,
    'Normal',
    'User'
);

INSERT INTO "AspNetRoles" ("Id", "Name", "NormalizedName", "ConcurrencyStamp")
VALUES
  (gen_random_uuid()::text, 'User', 'USER', gen_random_uuid()::text),
  (gen_random_uuid()::text, 'Approver', 'APPROVER', gen_random_uuid()::text),
  (gen_random_uuid()::text, 'Admin', 'ADMIN', gen_random_uuid()::text);

ALTER DATABASE "UKHSA"
SET log_statement = "all";
SELECT pg_reload_conf();