using System;
using System.Collections.Generic;

namespace GitApi.Interfaces
{
    public interface IGitRepository
    {
        long Id { get; set; }

        string Name { get; set; }

        string Description { get; set; }

        string Url { get; set; }

        DateTime CreatedAt { get; set; }

        DateTime UpdatedAt { get; set; }

        IGitOwner Owner { get; set; }

        IGitCommit GetCommit(string reference);

        IEnumerable<IGitCommit> GetAllCommits();
    }
}