﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mohkazv.Library.WebApp.Helpers
{
    public interface IFileManager
    {
        Task SaveFile(IFormFile file, string path);

        void DeleteFile(string path);
    }
}
