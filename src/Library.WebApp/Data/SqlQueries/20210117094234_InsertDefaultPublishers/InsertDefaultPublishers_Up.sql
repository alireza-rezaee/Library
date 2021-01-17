DELETE FROM [Publishers]
DBCC CHECKIDENT ('[Publishers]', RESEED, 0)
GO
SET IDENTITY_INSERT [Publishers] ON
INSERT INTO [Publishers] ([Id], [Title]) VALUES (1, N'کتاب نیستان')
INSERT INTO [Publishers] ([Id], [Title]) VALUES (2, N'عصر داستان')
INSERT INTO [Publishers] ([Id], [Title]) VALUES (3, N'روزبهان')
INSERT INTO [Publishers] ([Id], [Title]) VALUES (4, N'فردیناندپیر بیر، الوود‌راسل جانستون، دیویداف. مازوک')
INSERT INTO [Publishers] ([Id], [Title]) VALUES (5, N'موسسه آموزش علمی کاربردی صنعت آب و برق، مرکز نشر دانشگاهی')
INSERT INTO [Publishers] ([Id], [Title]) VALUES (6, N'عصر زندگی')
INSERT INTO [Publishers] ([Id], [Title]) VALUES (7, N'موجک')
INSERT INTO [Publishers] ([Id], [Title]) VALUES (8, N'پندار پارس')
INSERT INTO [Publishers] ([Id], [Title]) VALUES (9, N'آتی‌نگر')
INSERT INTO [Publishers] ([Id], [Title]) VALUES (10, N'بهار سبز')
INSERT INTO [Publishers] ([Id], [Title]) VALUES (11, N'مرکز اسناد انقلاب اسلامی')
INSERT INTO [Publishers] ([Id], [Title]) VALUES (12, N'Le Robert')
INSERT INTO [Publishers] ([Id], [Title]) VALUES (13, N'larousse')
INSERT INTO [Publishers] ([Id], [Title]) VALUES (14, N'علوم رایانه')
INSERT INTO [Publishers] ([Id], [Title]) VALUES (15, N'راد نواندیش (وابسته به موسسه فرهنگی هنری راد نواندیش)')
SET IDENTITY_INSERT [Publishers] OFF