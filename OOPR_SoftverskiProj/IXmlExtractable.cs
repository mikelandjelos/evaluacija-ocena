using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace OOPR_SoftverskiProj
{
    /// <summary>
    /// Interfejs implementira klasa koja se moze
    /// ekstraktovati/ucitati iz objekta tipa XmlNode.
    /// </summary>
    internal interface IXmlExtractable
    {

        /// <summary>
        /// Konverzija/ekstrakcija iz XmlNode objekta u objekat proizvoljnog tipa.
        /// </summary>
        /// <param name="xnd"> Objekat tipa XmlNode; </param>
        void FromXmlNode(in XmlNode xnd);

    }
}
