using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;

namespace Domain.Entities
{
    public class Sysadmin : Usuario
    {
        public new Userrole Userrole { get; set; } = Userrole.Sysadmin;
    }
}
