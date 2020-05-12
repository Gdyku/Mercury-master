﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mercury.DAL.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        Task CompleteAsync();
    }
}
