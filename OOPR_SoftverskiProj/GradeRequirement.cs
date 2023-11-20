using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace OOPR_SoftverskiProj
{
    /// <summary>
    /// Modeluje uslov za ocenjivanje odredjenom ocenom.
    /// </summary>
    internal class GradeRequirement : IXmlExtractable
    {

        protected Grade ocena;
        protected int minPts;
        protected int maxPts;

        public GradeRequirement(int _ocena, int _minPts, int _maxPts)
        {
            ocena = (Grade)_ocena;
            minPts = _minPts;
            maxPts = _maxPts;
        }

        public GradeRequirement(Grade _grade, int _minPts, int _maxPts) 
            : this ((int)_grade, _minPts, _maxPts) { }

        public GradeRequirement() : this(0, -1, -1) { }

        #region Class Methods

        /// <summary>
        /// Da li je uslov za dobijanje ocene ispunjen.
        /// </summary>
        /// <param name="pts"> Broj ostvarenih poena. </param>
        /// <returns> Vraca ocenu kao celobrojnu vrednost ukoliko
        /// je uslov za njeno dobijanje ispunjen na osnovu predatog broja poena. 
        /// U suprotnom vraca null. </returns>
        public int? RequirementMet(int pts)
        {
            if (ocena == Grade.Null)
                return 0;
            if (pts >= minPts && pts <= maxPts)
                return (int)ocena;
            return null;
        }

        #endregion

        #region IXmlExtractable Members

        public void FromXmlNode(in XmlNode xnd)
        {
            if (xnd == null)
                throw new ArgumentNullException();
            try
            {
                foreach (XmlNode child in xnd.ChildNodes)
                {
                    switch (child.Name)
                    {
                        case "Name":
                            // ukoliko ucitana vrednost nije u opsegu
                            // 5-10 => nevazeci podatak!
                            int temp = Convert.ToByte(child.InnerText);
                            if (temp >= 5 && temp <= 10)
                                ocena = (Grade)temp;
                            else ocena = Grade.Null;
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

        #region Overriden Util

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(((int)ocena).ToString());
            sb.AppendLine(minPts.ToString());
            sb.Append(maxPts.ToString());
            return sb.ToString();
        }

        public override bool Equals(object obj)
        {
            GradeRequirement other = obj as GradeRequirement;
            if (other == null)
                return false;
            return (int)ocena == (int)other.ocena &&
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
