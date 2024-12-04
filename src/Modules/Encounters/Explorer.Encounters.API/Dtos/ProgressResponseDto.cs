using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.API.Dtos {
    public class ProgressResponseDto {
        public bool InRange { get; set; }
        public bool Completed { get; set; }

        public ProgressResponseDto() { }
        public ProgressResponseDto(bool inRange, bool completed) {
            InRange = inRange;
            Completed = completed;
        }

        public ProgressResponseDto(bool inRange) {
            InRange = inRange;
            Completed = false;
        }
    }
}
