using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPR_SoftverskiProj
{

    // za vise detalja zasto je implementirana kao Singleton
    // pogledati klase GradingStrategyRefList i ExamRefList

    /// <summary>
    /// Kolekcija istovrsnih podataka dostupna na referenciranje
    /// drugim klasama (objekat ove klase je dostupan za vreme
    /// pisanja koda).
    /// </summary>
    /// <typeparam name="T"> Parametar proizvoljnog klasnog tipa. </typeparam>
    internal class ReferenceList <T>
        where T : class, new()
    {

        protected static ReferenceList<T> instance;
        protected static readonly object syncLock = new object();

        protected List<T> list;

        protected ReferenceList() 
        {
            list = new List<T>();
        }

        #region Singleton Util

        public static ReferenceList<T> Instance
        {
            get 
            {
                lock (syncLock)
                {
                    if (instance == null)
                        instance = new ReferenceList<T>();
                    return instance;
                }
            }
        }

        #endregion

        #region List Util

        public List<T> List
        {
            get => list;
        }

        public T NewElement
        {
            set => list.Add(value);
        }

        public T this[int index]
        {
            get => list[index];
            set => list[index] = value;
        }

        public void AddRange(T[] arr)
        { 
            if (arr != null)
                list.AddRange(arr);
        }

        public void RemoveAt(int index) => list.RemoveAt(index);

        public bool RemoveElement(T el) => list.Remove(el);

        public void Clear() => list.Clear();

        public int Count => list.Count;

        #endregion

    }
}
