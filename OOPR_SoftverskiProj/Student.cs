using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace OOPR_SoftverskiProj
{
    /// <summary>
    /// Modeluje uredjeni par (indeks, ispit) sa informacijama
    /// o osvojenim poenima na svim delovima ispita.
    /// </summary>
    internal class Student : IXmlExtractable
    {

        private string index;
        private Exam exam;
        private int usmPts;
        private List<int> activityPts;

        public Student(string _index, string _exam, 
            int _usmPts, int[] _activityPts = null)
        { 
            index = _index;
            exam = ExamRefList.GetExam(_exam) ?? new Exam();
            usmPts = _usmPts;
            activityPts = new List<int>();
            if (_activityPts != null)
                activityPts.AddRange(_activityPts);
        }

        public Student() : this("", "", -1) { }

        #region Class Methods

        /// <summary>
        /// Evaluira uspeh studenta na ispitu i pretvara
        /// rezultat evaluacije u tekstualni format.
        /// </summary>
        /// <returns> 
        /// Formatirani string "<index> | <ime_ispita> | <dobijena_ocena>" 
        /// </returns>
        public string EvaluationString()
        {
            return index + " | " + exam.Name + " | " + Evaluate.ToString();
        }

        #endregion

        #region Properties

        public Grade Evaluate
        {
            get => exam.ExamEvaluation(usmPts, activityPts.ToArray());
        }

        public Exam Exam
        { get => exam; }

        public string Index
        { get => index; set => index = value; }

        #endregion

        #region IXmlExtractable Members

        public void FromXmlNode(in XmlNode xnd)
        {
            if (xnd == null)
                throw new ArgumentNullException();
            try
            {
                XmlNodeList children = xnd.ChildNodes;
                index = children[0].InnerText;
                exam = ExamRefList.GetExam(children[1].InnerText) ?? new Exam();
                usmPts = Convert.ToInt32(children[2].InnerText);
                if (children.Count == 4 && children[3].HasChildNodes)
                    foreach (XmlNode activityPt in children[3].ChildNodes)
                        activityPts.Add(Convert.ToInt32(activityPt.FirstChild.NextSibling.InnerText));
            }
            catch (XmlException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            catch (FormatException fex)
            {
                Console.WriteLine(fex.Message);
                Console.WriteLine(fex.StackTrace);
            }
        }

        #endregion

        #region Overriden Util

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(index);
            sb.Append(exam);
            sb.AppendLine(usmPts.ToString());
            foreach (int act in activityPts)
                sb.AppendLine(act.ToString());
            return sb.ToString();
        }

        public override bool Equals(object obj)
        {
            Student other = obj as Student;
            if (other == null)
                return false;
            if (!index.Equals(other.index))
                return false;
            if (!exam.Equals(other.exam))
                return false;
            if (usmPts != other.usmPts)
                return false;
            foreach (int act in activityPts)
                if (!other.activityPts.Contains(act))
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
