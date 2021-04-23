namespace GitApi.Interfaces
{
    public interface IGitService
    {
        IGitConnection Connection { get; }

        void Connect();

        IGitOwner GetOwner(string name);

        IGitRepository GetRepository(long repositoryId);

        IGitCommit GetCommit(long repositoryId, string reference);
    }
}