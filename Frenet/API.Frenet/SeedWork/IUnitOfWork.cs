﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace API.Frenet.SeedWork
{
    public interface IUnitOfWork
    {
        Task SaveDbChanges(CancellationToken cancellationToken = default(CancellationToken));
    }
}
