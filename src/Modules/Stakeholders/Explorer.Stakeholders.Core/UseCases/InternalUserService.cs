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

        public bool CheckAuthorExists(long authorId)
        {
            var user = _userRepository.Get(authorId);

            if (user == null)
                return false;

            if (user.Role != Domain.UserRole.Author)
                return false;

            return true;
        }

        public bool UserExists(long userId)
        {
            var user = _userRepository.Get(userId);

            if (user == null)
                return false;

            return true;
        }
    }
}
