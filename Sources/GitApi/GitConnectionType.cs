namespace GitApi
{
    public enum GitConnectionType
    {
        /// <summary>No credentials provided</summary>
        Guest = 0,
        /// <summary>Username &amp; password</summary>
        Basic = 1,
        /// <summary>Delegated access to a third party</summary>
        Oauth = 2
    }
}