using System;
using System.Runtime.Serialization;

namespace OOPR_SoftverskiProj
{
    /// <summary>
    /// Indikacija da je doslo do problema u ulaznim podacima
    /// vezanim za broj poena na nekom od delova ispita.
    /// </summary>
    [Serializable]
    internal class PointsOffLimit : Exception
    {
        public PointsOffLimit()
        {
        }

        public PointsOffLimit(string message) : base(message)
        {
        }

        public PointsOffLimit(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PointsOffLimit(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}