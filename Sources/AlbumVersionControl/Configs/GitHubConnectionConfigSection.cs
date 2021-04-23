using System.Configuration;

namespace AlbumVersionControl.Configs
{
    public class GitHubConnectionConfigSection : ConfigurationSection
    {
        [ConfigurationProperty("Connections")]
        public GitHubConnectionCollection GitHubConnectionItems
        {
            get { return ((GitHubConnectionCollection)(base["Connections"])); }
        }
    }
}