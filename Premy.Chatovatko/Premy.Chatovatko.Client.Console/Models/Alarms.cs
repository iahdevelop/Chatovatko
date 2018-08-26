﻿using System;
using System.Collections.Generic;

namespace Premy.Chatovatko.Client.Console.Models
{
    public partial class Alarms
    {
        public long Id { get; set; }
        public string Time { get; set; }
        public string Text { get; set; }
        public long BlobMessagesId { get; set; }

        public BlobMessages BlobMessages { get; set; }
    }
}
