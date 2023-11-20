using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace OOPR_SoftverskiProj
{
    /// <summary>
    /// Modeluje ispitnu aktivnost (Pismeni, Lab, ...).
    /// </summary>
    internal class ExamActivity : IXmlExtractable
    {

        protected string name;
        protected int minPts;
        protected int maxPts;

        public ExamActivity(string _ime, int _minPts, int _maxPts)
        { 
            name = _ime;
            minPts = _minPts;
            maxPts = _maxPts;
        }

        public ExamActivity() : this("", -1, -1) { }

        #region Class Methods

        /// <summary>
        /// Na osnovu predatog broja poena vraca da li je
        /// ispitna aktivnost uspesno polozena ili ne.
        /// </summary>
        /// <param name="pts"> Broj ostvarenih poena na aktivnosti. </param>
        /// <returns> Uspesno/neuspesno polozena. </returns>
        /// <exception cref="PointsOffLimit"> 
        /// Gornje prekoracenje opsega poena 
        /// (nemoguce je imati vise od maksimalnog broja bodova). </exception>
        public bool GetPassedStatus(int pts)
        {
            if (pts > maxPts)
                throw new PointsOffLimit();
            return pts >= minPts;
        }

        #endregion

        #region IXmlExtractable Members

        public void FromXmlNode(in XmlNode xnd)
        {
            if (xnd == null)
                throw new ArgumentNullException();
            try
            {
                minPts = 0;
                foreach (XmlNode child in xnd.ChildNodes)
                {
                    switch (child.Name)
                    {
                        case "Name":
                            name = child.InnerText;
                            break;
                        case "Min":
                            minPts = Convert.ToInt32(child.InnerText);
                            break;
                        case "Max":
                            maxPts = Convert.ToInt32(child.InnerText);
                            break;
                    }
                }
            }
            catch (XmlException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        #endregion

        // override-ovane utility funkcije nasledjene iz System.Object
        #region Overriden Util

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(name);
            sb.AppendLine(minPts.ToString());
            sb.Append(maxPts.ToString());
            return sb.ToString();
        }

        public override bool Equals(object obj)
        {
            ExamActivity other = obj as ExamActivity;
            if (other == null)
                return false;
            return name == other.name &&
                minPts == other.minPts &&
                maxPts == other.maxPts;
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        #endregion
    }
}
