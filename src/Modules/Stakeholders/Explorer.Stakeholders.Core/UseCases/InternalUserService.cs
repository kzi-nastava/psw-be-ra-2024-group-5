using Explorer.Stakeholders.API.Internal;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class InternalUserService: IUserService
    {
        private readonly IUserRepository _userRepository;
        public InternalUserService(IUserRepository userRepository) {
            _userRepository = userRepository;
        }
        public bool CheckTouristExists(long touristId)
        {
            var user = _userRepository.Get(touristId);

            if (user == null)
                return false;

            if (user.Role != Domain.UserRole.Tourist)
                return false;

            return true;
        }
    }
}
