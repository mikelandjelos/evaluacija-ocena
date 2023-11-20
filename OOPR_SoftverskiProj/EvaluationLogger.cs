using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;

namespace OOPR_SoftverskiProj
{
    /// <summary>
    /// Loguje evaluirane rezultate koje su postigli studenti
    /// na ispitima.
    /// </summary>
    internal class EvaluationLogger
    {

        private string logFilePath;

        public EvaluationLogger(string _logFilePath)
        {
            logFilePath = _logFilePath;
        }

        public EvaluationLogger() : this("") { }

        #region Class Methods

        /// <summary>
        /// Na osnovu predatog niza studenata kreira i upisuje
        /// evaluacione stringove u log fajl.
        /// </summary>
        /// <param name="students"> Lista studenata (torka (indeks, ispit)). </param>
        /// <exception cref="ArgumentNullException"> 
        /// Predati niz ne sme da bude null objekat. </exception>
        public void LogEvaluation(Student[] students) 
        {
            if (students == null)
                throw new ArgumentNullException();
            StreamWriter sw = null;
            try
            {

                sw = new StreamWriter(new FileStream(logFilePath, FileMode.OpenOrCreate));
                foreach (Student st in students)
                    sw.WriteLine(st.EvaluationString());

            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            finally
            {
                sw?.Close();
            }
        }

        /// <summary>
        /// Na osnovu predate liste studenata kreira i upisuje
        /// evaluacione stringove u log fajl.
        /// </summary>
        /// <param name="students"> Lista studenata (torka (indeks, ispit)). </param>
        /// <exception cref="ArgumentNullException"> 
        /// Predata lista ne sme da bude null objekat. </exception>
        public void LogEvaluation(List<Student> students)
            => this.LogEvaluation(students.ToArray());

        #endregion

        #region Properties

        public string LogFilePath
        { get => logFilePath; set => logFilePath = value; }

        #endregion

    }
}
