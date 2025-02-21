using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FinancialManagement.Api.Extensions
{
    public class GetUserCurrent
    {
        private readonly IHttpContextAccessor _httpAccessor;
        public GetUserCurrent(IHttpContextAccessor httpAccessor)
        {
            _httpAccessor = httpAccessor;
        }
        public Guid GetUserIdFromToken()
        {
            var user = _httpAccessor.HttpContext!.User;
            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
            return Guid.Parse(userId);
        }
    }
}