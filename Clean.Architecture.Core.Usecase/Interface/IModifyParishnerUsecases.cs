﻿using Clean.Architecture.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Architecture.Core.Usecase.Interface
{
    public interface IModifyParishnerUsecases
    {
        void PromoteParishnerAsCouncilMember(Guid parishId, Guid parishnerId);
    }
}
