﻿using CTAM.Core;
using Microsoft.EntityFrameworkCore;

namespace MileageModule.Infrastructure
{
    public class InitService
    {
        private readonly MainDbContext _context;

        public InitService(MainDbContext context)
        {
            _context = context;
        }

        public void Migrate()
        {
            _context.Database.Migrate();
        }
    }
}
