﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFTP
{
    interface IServer
    {
        public string List(string path);
        public (long size, byte[] content) Get(string path);
    }
}
