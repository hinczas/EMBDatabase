﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EMBDatabase.Models.API
{
    public class GenericResponse
    {
        public string Type { get; set; }
        public int Code { get; set; }
        public string Message { get; set; }
        public long? ItemId { get; set; }
    }
}