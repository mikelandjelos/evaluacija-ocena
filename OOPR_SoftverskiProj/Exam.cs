using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace OOPR_SoftverskiProj
{
    /// <summary>
    /// Modeluje ispit/predmet.
    /// </summary>
    internal class Exam : IXmlExtractable
    {

        protected string name;
        protected GradingStrategy gradStrat;
        protected int minUsmPct;
        protected int maxUsmPts;
        protected List<ExamActivity> activities;

        public Exam(string _name, string _gradStrat,
            int _minUsmPct, int _maxUsmPts, ExamActivity[] _activities = null)
        {
            // ako u sistemu nije pronadjena strategija sa datim imenom
            // gradStrat atribut uzima nevazecu vrednost,
            // u suprotnom se referencira na nadjenu strategiju
            gradStrat = GradingStrategyRefList.GetStrategy(_gradStrat)
                ?? new GradingStrategy();
            name = _name;
            minUsmPct = _minUsmPct;
            maxUsmPts = _maxUsmPts;
            activities = new List<ExamActivity>();
            if (_activities != null)
                activities.AddRange(_activities);
        }

        public Exam() : this("", "", -1, -1) { }

        #region Class Methods

        /// <summary>
        /// Evaluira uspeh na datom ispitu.
        /// </summary>
        /// <param name="ptsUsm"></param>
        /// <param name="ptsActivities"></param>
        /// <returns></returns>
        /// <exception cref="PointsOffLimit"> 
        /// Ukoliko je predati broj poena na usmenom 
        /// ili bilo kojoj od ispitnih aktivnosti veci od maksimalnog
        /// moguceg prijavljuje gresku u unesenim podacima. </exception>
        /// <exception cref="PointsNotMatchingActivities">
        /// Predati parametar ne odgovara datoj listi aktivnosti.
        /// </exception>
        public Grade ExamEvaluation(int ptsUsm, int[] ptsActivities)
        {
            int ret = 0;
            if (ptsUsm > maxUsmPts)
                throw new PointsOffLimit();
            if (ptsActivities.Length != activities.Count)
                throw new PointsNotMatchingActivities();
            if (ptsUsm < MinPts)
                return Grade.Pet;
            ret += ptsUsm;
            for (int i = 0; i < activities.Count; ++i)
                if (activities[i].GetPassedStatus(ptsActivities[i]))
                    ret += ptsActivities[i];
                else
                    return Grade.Pet;
            return gradStrat.GetGrade(ret);
        }

        #endregion

        #region Properties

        public double MinPts
        {
            get => (((double)minUsmPct / 100) * (double)maxUsmPts);
        }

        public GradingStrategy GradingStrategy
        {
            get => gradStrat;
            set => gradStrat = value;
        }

        public ExamActivity NewActivity
        {
            set
            {
                if (value != null)
                    activities.Add(value);
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
                XmlNodeList children = xnd.ChildNodes;
                gradStrat =
                    GradingStrategyRefList.GetStrategy(children[0].InnerText) 
                    ?? new GradingStrategy();
                name = children[1].InnerText;
                minUsmPct = Convert.ToInt32(children[2].InnerText);
                maxUsmPts= Convert.ToInt32(children[3].InnerText);
                activities.Clear();
                foreach (XmlNode cnd in children[4].ChildNodes)
                { 
                    ExamActivity act = new ExamActivity();
                    act.FromXmlNode(cnd);
                    activities.Add(act);
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
            sb.AppendLine(name);
            sb.Append(gradStrat.ToString());
            sb.AppendLine(minUsmPct.ToString());
            sb.AppendLine(maxUsmPts.ToString());
            foreach (ExamActivity act in activities)
                sb.AppendLine(act.ToString());
            return sb.ToString();
        }

        public override bool Equals(object obj)
        {
            Exam other = obj as Exam;
            if (other == null)
                return false;
            if (!name.Equals(other.name))
                return false;
            if (!gradStrat.Equals(other.gradStrat))
                return false;
            if (minUsmPct != other.minUsmPct ||
                maxUsmPts != other.maxUsmPts)
                return false;
            foreach (ExamActivity act in activities)
                if (!other.activities.Contains(act))
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
