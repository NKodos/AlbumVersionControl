using System.Collections.Generic;

namespace GitApi.Interfaces
{
    public interface IGitOwner
    {
        string Name { get; set; }

        IGitConnection Connection { get; set; }

        IGitRepository GetRepository(string name);

        IEnumerable<IGitRepository> GetRepositories();
    }
}