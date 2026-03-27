INSERT INTO public."AspNetUsers" VALUES ('d3147dce-4e87-456a-8cf2-8b12df5f5508', 'admin', 'admin', 'admin@admin.com', 'ADMIN@ADMIN.COM', 'admin@admin.com', 'ADMIN@ADMIN.COM', false, 'AQAAAAIAAYagAAAAELqoQ8OVv47iCW4KSokdUFZsZ8wawN51btTiZXdvz7zApa4tovTkS8p1m7fmcSdA8Q==', 'H4L5DR62YARAC2BMEKN6TC2A46XCYUXD', 'eb231549-7352-4d09-bc9b-23017114015b', NULL, false, false, NULL, true, 0);
INSERT INTO public."AspNetUsers" VALUES ('5051c1df-d7f7-492d-937b-8a431b704aee', 'approver', 'approver', 'approver@approver.com', 'APPROVER@APPROVER.COM', 'approver@approver.com', 'APPROVER@APPROVER.COM', false, 'AQAAAAIAAYagAAAAEOfZlWcat/w8weDkgQVZBPnVjElK88WkecmEclKP3uu2zmxRKHC7eb08bXNR46Vwxw==', 'DERHB2VPVGH3RW3RVFKLQFJQYGJLXLAF', 'd64accdb-0fc6-40fa-bb01-8133efc96862', NULL, false, false, NULL, true, 0);
INSERT INTO public."AspNetUsers" VALUES ('67485421-f406-4d9f-a823-68fc0a05d7cc', 'user', 'user', 'user@user.com', 'USER@USER.COM', 'user@user.com', 'USER@USER.COM', false, 'AQAAAAIAAYagAAAAEBpHbs89WAFIGhmvGxpaErI37NqmC0PZpGet6+2AGJKp0qOjtly8tEtqWC9V3OCa6Q==', '4U26JV6HG7M7H7Z4JUXZXNY34JV65LY7', 'e4854784-e385-4aa1-9204-be3ff81d38c9', NULL, false, false, NULL, true, 0);


INSERT INTO public."Datasets" VALUES (1, 'COVID-19 Records', 'Rates of COVID-19', 1);
INSERT INTO public."Datasets" VALUES (2, 'Meningitis Records', 'Patient Records for meningitis', 2);
INSERT INTO public."Datasets" VALUES (3, 'Chickenpox Records', 'Patient Records for chickenpox', 2);
INSERT INTO public."Datasets" VALUES (15, 'cquebheb', 'enebnchb', 0);


INSERT INTO public."Requests" VALUES (3, '2026-03-25 10:00:00+00', '67485421-f406-4d9f-a823-68fc0a05d7cc', 1);
INSERT INTO public."Requests" VALUES (4, '2026-03-25 10:30:00+00', '67485421-f406-4d9f-a823-68fc0a05d7cc', 2);
INSERT INTO public."Requests" VALUES (5, '2026-03-25 11:00:00+00', '67485421-f406-4d9f-a823-68fc0a05d7cc', 3);
INSERT INTO public."Requests" VALUES (6, '2026-03-25 11:30:00+00', '67485421-f406-4d9f-a823-68fc0a05d7cc', 1);
INSERT INTO public."Requests" VALUES (7, '2026-03-25 12:00:00+00', '67485421-f406-4d9f-a823-68fc0a05d7cc', 2);
INSERT INTO public."Requests" VALUES (12, '2026-03-25 19:58:07.355553+00', '67485421-f406-4d9f-a823-68fc0a05d7cc', 3);
INSERT INTO public."Requests" VALUES (13, '2026-03-25 20:01:13.988967+00', '67485421-f406-4d9f-a823-68fc0a05d7cc', 2);
INSERT INTO public."Requests" VALUES (14, '2026-03-27 09:27:20.64905+00', '67485421-f406-4d9f-a823-68fc0a05d7cc', 3);

INSERT INTO public."Approvals" VALUES (5, true, '2026-03-25 13:00:00+00', '2026-06-25 13:00:00+00', 'Approved', 3);
INSERT INTO public."Approvals" VALUES (6, false, '2026-03-25 13:10:00+00', '2026-03-25 13:10:00+00', 'Unclear Reasoning', 4);
INSERT INTO public."Approvals" VALUES (7, true, '2026-03-25 13:20:00+00', '2026-06-25 13:20:00+00', 'Approved', 5);
INSERT INTO public."Approvals" VALUES (8, true, '2026-03-25 13:30:00+00', '2026-06-25 13:30:00+00', 'Approved', 6);

INSERT INTO public."AspNetRoles" VALUES ('6717cc1e-9d17-4610-988d-2a437637269b', 'Admin', 'ADMIN', 'f05ea18f-1ade-4162-9b28-e2ebd4b8edf4');
INSERT INTO public."AspNetRoles" VALUES ('be168ae2-05e2-4627-a40d-ae305cb9ab4c', 'Approver', 'APPROVER', '50fded6e-8422-4598-9b73-5f2ca13677e4');
INSERT INTO public."AspNetRoles" VALUES ('1403d009-84d9-43e3-834a-9c83a087c071', 'User', 'USER', '72136c95-8faf-4137-9dea-1f2bb02c27ab');

INSERT INTO public."AspNetUserRoles" VALUES ('d3147dce-4e87-456a-8cf2-8b12df5f5508', '6717cc1e-9d17-4610-988d-2a437637269b');
INSERT INTO public."AspNetUserRoles" VALUES ('5051c1df-d7f7-492d-937b-8a431b704aee', 'be168ae2-05e2-4627-a40d-ae305cb9ab4c');
INSERT INTO public."AspNetUserRoles" VALUES ('67485421-f406-4d9f-a823-68fc0a05d7cc', '1403d009-84d9-43e3-834a-9c83a087c071');

