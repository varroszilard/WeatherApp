using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repository.Base
{
    interface IIdentifiableEntity
    {
        Guid Id { get; set; }
    }
}
