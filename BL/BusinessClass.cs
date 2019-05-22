using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class BusinessClass: IBusinessClass
    {
        private readonly IAppSettings appSettings;

        public BusinessClass(IAppSettings appSettings)
        {
            this.appSettings = appSettings;
        }

        public void BusinessMethod() {

        }
    }
}
