using CTAM.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CloudAPI.ApplicationCore.Queries.Users
{
    public class GetNextFreeLoginCodeCommand : IRequest<string>
    {
    }

    public class GetNextFreeLoginCodeHandler : IRequestHandler<GetNextFreeLoginCodeCommand, string>
    {
        private readonly MainDbContext _context;
        private const int MAX_LOGIN_CODE = 999999;
    
        public GetNextFreeLoginCodeHandler(MainDbContext context)
        {
            _context = context;
        }
    
        public async Task<string> Handle(GetNextFreeLoginCodeCommand request, CancellationToken cancellationToken)
        {
            var lastLoginCodeInUsage = await _context.CTAMUser()
                                                    .AsNoTracking()
                                                    .Where(user => !string.IsNullOrEmpty(user.LoginCode))
                                                    .MaxAsync(user => Convert.ToInt32(user.LoginCode), cancellationToken);
    
            int loginCodeNotInUse = -1;
            if (lastLoginCodeInUsage < MAX_LOGIN_CODE)
            {
                loginCodeNotInUse = lastLoginCodeInUsage + 1;
            }
            else if (lastLoginCodeInUsage == MAX_LOGIN_CODE)
            {
                var loginCodesInUsage = await _context.CTAMUser()
                                                      .AsNoTracking()
                                                      .Where(user => !string.IsNullOrEmpty(user.LoginCode))
                                                      .Select(user => Convert.ToInt32(user.LoginCode))
                                                      .OrderBy(code => code)
                                                      .ToListAsync(cancellationToken);
    
                loginCodeNotInUse = Enumerable.Range(1, MAX_LOGIN_CODE)
                                              .FirstOrDefault(code => !loginCodesInUsage.Contains(code));
            }
            else
            {
                throw new InvalidOperationException($"The last login code in use is greater than the maximum allowed code -> {MAX_LOGIN_CODE}");
            }
    
            return loginCodeNotInUse.ToString("000000");
        }
    }
}
