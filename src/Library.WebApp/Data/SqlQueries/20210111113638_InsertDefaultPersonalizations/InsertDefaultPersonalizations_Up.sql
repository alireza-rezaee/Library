INSERT INTO [Personalizations] ([Title], [Value])
VALUES
(N'SiteCoverSrc', N'/uploads/theme/cover.jpg'),
(N'SiteLogoSrc', N'/uploads/theme/avatar-150x.png'),
(N'TextLogoSrc', N'/uploads/theme/logo.png'),
(N'IndexTitle', N'عنوان صفحه نخست سایت را اینجا بنویسید.'),
(N'IndexDescription', N'توضیحات صفحه نخست سایت را اینجا بنویسید.'),
(N'AdditionalTitle', N'عنوان کوتاه سایت را اینجا بنویسید.'),
(N'SiteFootnote',
N'<div class="container">' + CHAR(13) +
	'<div class="row">' + CHAR(13) +
		'<div class="col mx-auto">' + CHAR(13) +
			'<div class="text-center small">' + CHAR(13) +
			N'به این سایت خوش آمدید.' + CHAR(13) +
			'</div>' + CHAR(13) +
		'</div>' + CHAR(13) +
	'</div>' + CHAR(13) +
	'<div class="row">' + CHAR(13) +
		'<div class="col">' + CHAR(13) +
			'<div class="text-center small text-secondary my-3">' + CHAR(13) +
				'<a class"text-secondary" href="https://example.com/">Example.com</a>' + CHAR(13) +
				'e.g. is licensed under a' + CHAR(13) +
				'<a class="text-secondary" href="https://creativecommons.org/licenses/by/4.0/" target="_blank" rel="noopener">' + CHAR(13) +
					'Creative Commons Attribution 4.0 International License' + CHAR(13) +
				'</a>' + CHAR(13) +
			'</div>' + CHAR(13) +
		'</div>' + CHAR(13) +
	'</div>' + CHAR(13) +
'</div>')
