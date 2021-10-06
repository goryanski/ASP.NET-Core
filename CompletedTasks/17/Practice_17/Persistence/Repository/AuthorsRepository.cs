﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entity;

namespace Persistence.Repository
{
    internal sealed class AuthorsRepository : BaseRepository<Guid, Author>

    {
        public AuthorsRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override Task<Author> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public override void Remove(Guid id)
        {
            throw new NotImplementedException();
        }

        public override void Update(Author entity)
        {
            throw new NotImplementedException();
        }
    }
}
