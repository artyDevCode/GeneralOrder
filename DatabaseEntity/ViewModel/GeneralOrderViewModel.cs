using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DatabaseEntity
{
    /// <summary>
    /// Represents the Portfolio Model List to be returned to the client. <see cref="GetGeneralOrder"/>
    /// </summary>
    public class GeneralOrderViewModel
    {
        public int GeneralOrderId { get; set; }
        public string GeneralOrderName { get; set; }
    }
}

