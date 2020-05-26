using System;

[assembly: CLSCompliant(false)]
[assembly: System.Runtime.InteropServices.ComVisible(false)]
namespace Renting.MasterServices.Infraestructure
{
    /// <summary>
    /// Enums
    /// </summary>
    public class Enums
    {
        /// <summary>
        /// enum DataBaseConnection
        /// GestionFlota
        /// Surenting
        /// SurentingTrans
        /// Flota
        /// </summary>
        public enum DataBaseConnection
        {
            GestionFlota,
            Surenting,
            SurentingTrans,
            Flota,
            WebProd,
            GestionFlotaTrans
        }

        public enum AnnouncementAction
        {
            Save,
            Update
        }
    }
}
