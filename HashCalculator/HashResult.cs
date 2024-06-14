using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HashCalculator
{
    public class HashResult 
    {
        public string? HashValue { get; set; }
        public string? FilePath { get; set; }
        public HashFunctions HashFunction { get; set; }
    }
}
