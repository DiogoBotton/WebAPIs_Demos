using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Frenet.Domains
{
    public abstract class AbstractDomain
    {
        public virtual int Id { get; protected set; }
    }
}
