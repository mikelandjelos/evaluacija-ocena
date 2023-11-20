using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPR_SoftverskiProj
{
    // Singleton pattern dolazi do izrazaja => jedan resurs na koji
    // se referencira ceo sistem po pitanju ispita,
    // i dostupnost instance klase za vreme pisanja koda;

    /// <summary>
    /// Modeluje primitivnu runtime bazu podataka koja cuva
    /// sve ispite trenutno ubacene u sistem.
    /// </summary>
    internal class ExamRefList : ReferenceList<Exam>
    {

        private ExamRefList() : base() { }

        /// <summary>
        /// Trazenje ispita po imenu.
        /// </summary>
        /// <param name="name"> Ime ispita. </param>
        /// <returns> Vraca ispit ukoliko je nadjen u listi, 
        /// null ukoliko nije </returns>
        public static Exam GetExam(string name)
        {
            foreach (Exam exam in Instance.List)
            {
                if (name == exam.Name)
                    return exam;
            }
            return null;
        }

    }
}
