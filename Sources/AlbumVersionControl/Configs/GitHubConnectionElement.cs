using System.Configuration;

namespace AlbumVersionControl.Configs
{
    public class GitHubConnectionElement : ConfigurationElement
    {
        [ConfigurationProperty("login", DefaultValue = "", IsKey = true, IsRequired = true)]
        public string Login
        {
            get { return ((string)(base["login"])); }
            set { base["login"] = value; }
        }

        [ConfigurationProperty("password", DefaultValue = "", IsKey = false, IsRequired = false)]
        public string Password
        {
            get { return ((string)(base["password"])); }
            set { base["password"] = value; }
        }

        [ConfigurationProperty("token", DefaultValue = "", IsKey = false, IsRequired = false)]
        public string Token
        {
            get { return ((string)(base["token"])); }
            set { base["token"] = value; }
        }
    }
}