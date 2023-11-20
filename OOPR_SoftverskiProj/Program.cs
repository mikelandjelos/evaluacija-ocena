using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace OOPR_SoftverskiProj
{
    internal class Program
    {
        static void Main(string[] args)
        {

            XmlDocument xdoc = new XmlDocument();

            GradeRequirement[] gradeReqsA = {
                new GradeRequirement(6, 55, 64),
                new GradeRequirement(7, 65, 74),
                new GradeRequirement(8, 75, 84),
                new GradeRequirement(9, 85, 98),
                new GradeRequirement(10, 90, 100)
            };

            GradeRequirement[] gradeReqsB = {
                new GradeRequirement(6, 50, 64),
                new GradeRequirement(7, 65, 89),
                new GradeRequirement(8, 90, 98),
                new GradeRequirement(10, 98, 100)
            };

            GradingStrategy[] gradingStrategies = {
                new GradingStrategy("A", gradeReqsA),
                new GradingStrategy("B", gradeReqsB)
            };

            foreach (GradingStrategy gs in gradingStrategies)
                GradingStrategyRefList.Instance.NewElement = gs;

            ExamActivity[] examActivitiesOOPR = { 
                new ExamActivity("Pismeni", (int)(0.5 * 32), 32),
                new ExamActivity("Lab", 0, 20),
            };

            ExamActivity[] examActivitiesGeodezija = {
                new ExamActivity("Projekat a", 6, 10),
                new ExamActivity("Projekat b", 8, 10),
                new ExamActivity("Lab", 8, 20),
                new ExamActivity("Terenski zadatak", 8, 20)
            };

            ExamActivity[] examActivitiesIstSV = {
                new ExamActivity("Istrazivanje", 7, 20)
            };

            ExamActivity[] examActivitiesFarmakologija = { 
                new ExamActivity("Test A", (int)(0.6 * 25), 25),
                new ExamActivity("Test B", (int)(0.6 * 25), 25),
                new ExamActivity("Lab 1", 0, 10),
                new ExamActivity("Lab 2", 2, 10),
                new ExamActivity("Lab 3", 5, 10)
            };

            ExamActivity[] examActivitiesMMS = { 
                new ExamActivity("SW projekat", 0, 30),
                new ExamActivity("MM projekat", 15, 30)
            };

            Exam[] exams = { 
                new Exam("OO Projektovanje", "A", 50, 48, examActivitiesOOPR),
                new Exam("Geodezija", "A", 50, 40, examActivitiesGeodezija),
                new Exam("Istorija starog veka", "A", 60, 80, examActivitiesIstSV),
                new Exam("Farmakologija", "B", 80, 20, examActivitiesFarmakologija),
                new Exam("MMS", "B", 50, 40, examActivitiesMMS),
                new Exam("Rimsko pravo", "B", 55, 100)
            };

            foreach (Exam exam in exams)
                ExamRefList.Instance.NewElement = exam;

            //foreach (Exam ex in ExamRefList.Instance.List)
            //    Console.WriteLine(ex.ToString());

            EvaluationLogger evaluationLogger = new EvaluationLogger();

            try
            {
                List<Student> students = new List<Student>();
                // Harcoded putanja do .xml fajla koji sadrzi evaluacione primere za studente i ocene
                xdoc.Load(@"C:\Users\Mihajlo\Videos\_oopr\patterns\OceneEvaluacioniPrimer.xml");
                foreach (XmlNode xnd in xdoc.FirstChild.ChildNodes)
                {
                    Student student = new Student();
                    student.FromXmlNode(xnd);
                    students.Add(student);
                }
                
                evaluationLogger.LogFilePath = "log_pre_izmena.txt";
                evaluationLogger.LogEvaluation(students);

                // Harcoded putanja do .xml fajla koji sadrzi evaluacioni primer za strategiju C
                // => ucitavanje nove strategije u sistem
                xdoc.Load(@"C:\Users\Mihajlo\Videos\_oopr\patterns\StrategijaEvaluacioniPrimer.xml");
                GradingStrategy stratC = new GradingStrategy();
                stratC.FromXmlNode(xdoc.FirstChild);
                GradingStrategyRefList.Instance.NewElement = stratC;

                // Harcoded putanja do .xml fajla koji sadrzi evaluacioni primer za predmet OOP
                // => ucitavanje novog predmeta u sistem
                xdoc.Load(@"C:\Users\Mihajlo\Videos\_oopr\patterns\PredmetEvaluacioniPrimer.xml");
                Exam examOOP = new Exam();
                examOOP.FromXmlNode(xdoc.FirstChild);
                ExamRefList.Instance.NewElement = examOOP;

                foreach (Exam exam in ExamRefList.Instance.List)
                    exam.GradingStrategy = stratC;

                evaluationLogger.LogFilePath = "log_nakon_izmena.txt";

                evaluationLogger.LogEvaluation(students);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }

        }
    }
}