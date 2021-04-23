using System;

namespace GitApi.Interfaces
{
    public interface IGitCommit
    {
        string Message { get; set;  }

        string Sha { get; set; }

        string Author { get; set; }

        DateTime CreatedAt { get; set; }

        IGitRepository Repository { get; set; }
    }
}