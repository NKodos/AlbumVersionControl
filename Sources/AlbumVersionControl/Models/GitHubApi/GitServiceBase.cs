using GitApi.Interfaces;

namespace AlbumVersionControl.Models.GitHubApi
{
    public abstract class GitServiceBase : GitHubObject, IGitService
    {
        protected GitServiceBase(IGitConnection connection)
        {
            Connection = connection;
        }

        public IGitConnection Connection { get; protected set; }

        public abstract void Connect();

        public abstract IGitOwner GetOwner(string name);

        public abstract IGitRepository GetRepository(long repositoryId);

        public abstract IGitCommit GetCommit(long repositoryId, string reference);
    }
}