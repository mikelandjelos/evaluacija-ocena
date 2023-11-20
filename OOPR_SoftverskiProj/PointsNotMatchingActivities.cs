using System;
using System.Runtime.Serialization;

namespace OOPR_SoftverskiProj
{
    /// <summary>
    /// Indikacija da je kao parametar pri evaluaciji
    /// ocene studenta na ispitu predat kraci/duzi niz poena
    /// od ocekivanog.
    /// </summary>
    [Serializable]
    internal class PointsNotMatchingActivities : Exception
    {
        public PointsNotMatchingActivities()
        {
        }

        public PointsNotMatchingActivities(string message) : base(message)
        {
        }

        public PointsNotMatchingActivities(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PointsNotMatchingActivities(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}