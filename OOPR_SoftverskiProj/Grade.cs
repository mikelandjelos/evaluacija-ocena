using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPR_SoftverskiProj
{
    /// <summary>
    /// Enumerisane standardne ispitne ocene, kao i Null ocena koja
    /// ce naznaciti neispravno izracunavanje rezultata zbog 
    /// pogresnih ulaznih podataka;
    /// </summary>
    internal enum Grade : byte
    { 
        Null = 0, Pet = 5, Sest = 6, Sedam = 7, Osam = 8, Devet = 9, Deset = 10
    }

}