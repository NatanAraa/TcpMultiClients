/*
 *   Copyright (c) 2021 Araa
 *   All rights reserved.
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            //limit msg
            Console.SetIn(new StreamReader(Console.OpenStandardInput(), Encoding.UTF8, false, 1024));
            Client user = new Client();
            user.Connect();
        }
    }
}
