using BrainBoost_API.Models;
using BrainBoost_API.Repositories.Interfaces;
using Microsoft.CodeAnalysis.Operations;

namespace BrainBoost_API.Repositories.Inplementation
{
    public class WhatToLearnRepository:Repository<WhatToLearn>, IWhatToLearnRepository
    {
        private readonly ApplicationDbContext Context;
        public WhatToLearnRepository(ApplicationDbContext context) : base(context)
        {
            this.Context = context;
        }
    }
}
