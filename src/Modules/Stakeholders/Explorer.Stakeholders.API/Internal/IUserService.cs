using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Internal
{
    public interface IUserService
    {
        bool CheckTouristExists(long touristId);
        public bool UserExists(long userId);
        public bool CheckAuthorExists(long authorId);

    }
}
