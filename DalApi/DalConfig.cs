
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace DALAPI
{

    static class DLConfig
    {
        public class DLPackage
        {
            public string Name;
            public string PkgName;
            public string NameSpace;
            public string ClassName;
        }
        internal static string DLName;
        internal static Dictionary<string, DLPackage> DalPackages;

        /// <summary>
        /// Static constructor extracts Dal packages list and Dal type from
        /// Dal configuration file config.xml
        /// </summary>
        static DLConfig()
        {
            XElement dalConfig = XElement.Load(@"config.xml");
            DLName = dalConfig.Element("dal").Value;
            DalPackages = (from pkg in dalConfig.Element("dal-packages").Elements()
                          let tmp1 = pkg.Attribute("namespace")
                          let nameSpace = tmp1 == null ? "DL" : tmp1.Value
                          let tmp2 = pkg.Attribute("class")
                          let className = tmp2 == null ? pkg.Value : tmp2.Value
                          select new DLPackage()
                          {
                              Name = "" + pkg.Name,
                              PkgName = pkg.Value,
                              NameSpace = nameSpace,
                              ClassName = className
                          })
                           .ToDictionary(p => "" + p.Name, p => p);
        }
    }

    /// <summary>
    /// Represents errors during DalApi initialization
    /// </summary>
    [Serializable]
    public class DLConfigException : Exception
    {
        public DLConfigException(string message) : base(message) { }
        public DLConfigException(string message, Exception inner) : base(message, inner) { }
    }
}










//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Xml.Linq;

//namespace DALAPI
//{
//    /// <summary>
//    /// Class for processing config.xml file and getting from there
//    /// information which is relevant for initialization of DalApi
//    /// </summary>
//    static class DalConfig
//    {
//        internal static string DalName;
//        internal static Dictionary<string, string> DalPackages;

//        /// <summary>
//        /// Static constructor extracts Dal packages list and Dal type from
//        /// Dal configuration file config.xml
//        /// </summary>
//        static DalConfig()
//        {
//            XElement dalConfig = XElement.Load(@"config.xml");
//            DalName = dalConfig.Element("dal").Value;
//            DalPackages = (from pkg in dalConfig.Element("dal-packages").Elements()
//                           select pkg).ToDictionary(p => "" + p.Name, p => p.Value);
//        }
//    }

//    /// <summary>
//    /// Represents errors during DalApi initialization
//    /// </summary>
//    [Serializable]
//    public class DalConfigException : Exception
//    {
//        public DalConfigException(string message) : base(message) { }
//        public DalConfigException(string message, Exception inner) : base(message, inner) { }
//    }
//}
