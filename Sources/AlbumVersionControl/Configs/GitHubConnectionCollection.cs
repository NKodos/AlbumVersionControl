using System.Configuration;

namespace AlbumVersionControl.Configs
{
    [ConfigurationCollection(typeof(GitHubConnectionElement))]
    public class GitHubConnectionCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new GitHubConnectionElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((GitHubConnectionElement)(element)).Login;
        }

        public GitHubConnectionElement this[int idx]
        {
            get { return (GitHubConnectionElement)BaseGet(idx); }
        }
    }
}