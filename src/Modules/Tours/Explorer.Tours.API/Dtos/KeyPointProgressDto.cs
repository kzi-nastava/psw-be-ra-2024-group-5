﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos {
    public class KeyPointProgressDto {
        public long KeyPointId { get; set; }
        public KeyPointDto KeyPoint { get; set; }
        public DateTime? VisitTime { get; set; }
       
    }
}
