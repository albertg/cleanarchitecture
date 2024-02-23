﻿using Clean.Architecture.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Architecture.Core.Usecase.Interface.External
{
    public interface IParishnerPersistence
    {
        void AddParishner(Parishner parishner, Guid parishId);
        Parishner GetParishner(Guid parishId, Guid parishnerId);
        void UpdateParishner(Parishner parishner);
    }
}
