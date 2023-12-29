using System;

namespace Client.Models
{
    public class LotteryEntry
    {
        public DateTime slotTime { get; set; }
        public int betNumber { get; set; }

        public User User { get; set; }
    }

}
