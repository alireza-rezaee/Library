﻿DELETE FROM [Authors]
DBCC CHECKIDENT ('[Authors]', RESEED, 0)
GO
SET IDENTITY_INSERT [Authors] ON
INSERT INTO [Authors] ([Id], [FullName]) VALUES (1, N'سیدمهدی شجاعی')
INSERT INTO [Authors] ([Id], [FullName]) VALUES (2, N'محمود گلاب دره ایی')
INSERT INTO [Authors] ([Id], [FullName]) VALUES (3, N'نادر ابراهیمی')
INSERT INTO [Authors] ([Id], [FullName]) VALUES (4, N'ابراهیم واحدیان')
INSERT INTO [Authors] ([Id], [FullName]) VALUES (5, N'فرشید واحدیان')
INSERT INTO [Authors] ([Id], [FullName]) VALUES (6, N'فردیناندپیر بیر')
INSERT INTO [Authors] ([Id], [FullName]) VALUES (7, N'الوود‌راسل جانستون')
INSERT INTO [Authors] ([Id], [FullName]) VALUES (8, N'دیویداف')
INSERT INTO [Authors] ([Id], [FullName]) VALUES (9, N'مازوک')
INSERT INTO [Authors] ([Id], [FullName]) VALUES (10, N'عین‌الله جعفرنژادقمی')
INSERT INTO [Authors] ([Id], [FullName]) VALUES (11, N'فریدون شمس‌علیئی')
INSERT INTO [Authors] ([Id], [FullName]) VALUES (12, N'سیامک سیفی')
INSERT INTO [Authors] ([Id], [FullName]) VALUES (13, N'آرش نوربخش')
INSERT INTO [Authors] ([Id], [FullName]) VALUES (14, N'عبدالرحمن عرفان‌نیا')
INSERT INTO [Authors] ([Id], [FullName]) VALUES (15, N'علیرضا فخارزاده')
INSERT INTO [Authors] ([Id], [FullName]) VALUES (16, N'عبدالکریم قناعتیان‌جبذری')
INSERT INTO [Authors] ([Id], [FullName]) VALUES (17, N'علی مصلی‌نژاد')
INSERT INTO [Authors] ([Id], [FullName]) VALUES (18, N'محمد نیک‌نیا')
INSERT INTO [Authors] ([Id], [FullName]) VALUES (19, N'مرضیه گشانی')
INSERT INTO [Authors] ([Id], [FullName]) VALUES (20, N'حامد محمد حسینی')
INSERT INTO [Authors] ([Id], [FullName]) VALUES (21, N'علی‌اکبر ولایتی')
INSERT INTO [Authors] ([Id], [FullName]) VALUES (22, N'حمید ادگی')
INSERT INTO [Authors] ([Id], [FullName]) VALUES (23, N'سیدعباس احمدی')
INSERT INTO [Authors] ([Id], [FullName]) VALUES (24, N'مسعود جاسم‌نژاد')
INSERT INTO [Authors] ([Id], [FullName]) VALUES (25, N'کریس واس و تحل راز')
INSERT INTO [Authors] ([Id], [FullName]) VALUES (26, N'جان گوردون')
INSERT INTO [Authors] ([Id], [FullName]) VALUES (27, N'دن برایتون')
INSERT INTO [Authors] ([Id], [FullName]) VALUES (28, N'جیمی پیج')
INSERT INTO [Authors] ([Id], [FullName]) VALUES (29, N'کارولین وب')
INSERT INTO [Authors] ([Id], [FullName]) VALUES (30, N'Collectif')
INSERT INTO [Authors] ([Id], [FullName]) VALUES (31, N'آلفرد هو')
INSERT INTO [Authors] ([Id], [FullName]) VALUES (32, N'مانیکا لام')
INSERT INTO [Authors] ([Id], [FullName]) VALUES (33, N'راوی ستهی')
INSERT INTO [Authors] ([Id], [FullName]) VALUES (34, N'جفری اولمن')
INSERT INTO [Authors] ([Id], [FullName]) VALUES (35, N'محمود گلابداره‌ای')
INSERT INTO [Authors] ([Id], [FullName]) VALUES (36, N'امیرحسین فاضلی مقدم')
SET IDENTITY_INSERT [Authors] OFF