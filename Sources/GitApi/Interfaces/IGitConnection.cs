namespace GitApi.Interfaces
{
    public interface IGitConnection
    {
        string Login { get; set; }

        string Password { get; set; }

        string Token { get; set; }

        GitConnectionType ConnectionType { get; set; }
    }
}