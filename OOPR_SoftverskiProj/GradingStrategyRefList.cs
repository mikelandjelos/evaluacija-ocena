using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPR_SoftverskiProj
{
    // Singleton pattern dolazi do izrazaja => jedan resurs na koji
    // se referencira ceo sistem po pitanju strategija ocenjivanja,
    // i dostupnost instance klase za vreme pisanja koda;

    /// <summary>
    /// Modeluje primitivnu runtime bazu podataka koja cuva
    /// sve strategije ocenjivanja trenutno ubacene u sistem.
    /// </summary>
    internal class GradingStrategyRefList : ReferenceList<GradingStrategy>
    {

        private GradingStrategyRefList() : base() { }

        /// <summary>
        /// Trazenje strategije ocenjivanja po imenu.
        /// </summary>
        /// <param name="name"> Ime strategije ocenjivanja. </param>
        /// <returns> Vraca strategiju ukoliko je nadjena u listi,
        /// null ukoliko nije. </returns>
        public static GradingStrategy GetStrategy(string name)
        {
            foreach (GradingStrategy gs in Instance.List)
            {
                if (name == gs.Name)
                    return gs;
            }
            return null;
        }

    }
}
