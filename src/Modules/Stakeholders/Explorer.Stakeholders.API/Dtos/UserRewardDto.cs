using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Dtos
{
    public class UserRewardDto
    {
        public long Id { get; set; }
        public DateTime LastSession { get; set; }
        public int RewardStreak { get; set; }
        public DateTime LastRewardClaimed { get; set; }
        public bool CanBeClaimed { get; set; }
        public UserRewardDto() { }
        public UserRewardDto(long id, DateTime lastSession, int rewardStreak, DateTime lastRewardClaimed)
        {
            Id = id;
            LastSession = lastSession;
            RewardStreak = rewardStreak;
            LastRewardClaimed = lastRewardClaimed;
        }
    }
}
