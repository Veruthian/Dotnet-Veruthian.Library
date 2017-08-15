﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Soedeum.Dotnet.Library;
using Soedeum.Dotnet.Library.Data;
using Soedeum.Dotnet.Library.Data.Patterns;
using Soedeum.Dotnet.Library.Data.Readers;
using Soedeum.Dotnet.Library.Data.Ranges;
using Soedeum.Dotnet.Library.Numeric;
using Soedeum.Dotnet.Library.Text.Code;
using Soedeum.Dotnet.Library.Text.Code.Encodings;

namespace _TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Pause();
        }

        static void Pause()
        {
            Console.Write("Press any key to continue...");
            Console.ReadKey(true);
            System.Console.WriteLine();
        }
    }
}