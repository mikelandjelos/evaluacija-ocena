using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace OOPR_SoftverskiProj
{
    /// <summary>
    /// Strategija ocenjivanja.
    /// </summary>
    internal class GradingStrategy : IXmlExtractable
    {

        private string name;
        private List<GradeRequirement> gradReqs;

        public GradingStrategy(string _name, GradeRequirement[] _gradReqs = null)
        {
            name = _name;
            gradReqs = new List<GradeRequirement>();
            if (_gradReqs != null)
                gradReqs.AddRange(_gradReqs);
        }

        public GradingStrategy() : this("", null) { }

        #region Class Methods

        /// <summary>
        /// Vrsi evaluaciju i vraca ocenu koja odgovara predatom broju poena.
        /// </summary>
        /// <param name="pts"> Broj ostvarenih poena. </param>
        /// <returns> Ukoliko nema nijednog uslova za ocenjivanje, vraca
        /// Null ocenu. U suprotnom vraca enumeraciju tipa Ocena u 
        /// zavisnosti od broja ostvarenih poena. </returns>
        public Grade GetGrade(int pts)
        {
            if (gradReqs.Count == 0)
                return Grade.Null;
            int? grade;
            foreach (GradeRequirement greq in gradReqs)
                if ((grade = greq.RequirementMet(pts)) != null)
                    return (Grade)grade;
            return Grade.Pet;
        }

        #endregion

        #region Properties

        public GradeRequirement NewRequirement
        {
            set
            {
                if (value == null || gradReqs.Contains(value))
                    return;
                gradReqs.Add(value);
            }
        }

        public string Name
        { get => name; }

        #endregion

        #region IXmlExtractable Members

        public void FromXmlNode(in XmlNode xnd)
        {
            if (xnd == null)
                throw new ArgumentNullException();
            try
            {
                name = xnd.FirstChild.InnerText;
                gradReqs.Clear();
                foreach (XmlNode child in
                    xnd.FirstChild.NextSibling.ChildNodes)
                {
                    GradeRequirement greq = new GradeRequirement();
                    greq.FromXmlNode(child);
                    gradReqs.Add(greq);
                }
            }
            catch (XmlException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        #endregion

        #region Overriden Util

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(name);
            foreach (GradeRequirement greq in gradReqs)
                sb.AppendLine(greq.ToString());
            return sb.ToString();
        }

        public override bool Equals(object obj)
        {
            GradingStrategy other = obj as GradingStrategy;
            if (other == null)
                return false;
            if (!name.Equals(other.name))
                return false;
            if (gradReqs.Count != other.gradReqs.Count)
                return false;
            foreach (GradeRequirement greq in gradReqs)
                if (!other.gradReqs.Contains(greq))
                    return false;
            return true;
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        #endregion

    }
}
