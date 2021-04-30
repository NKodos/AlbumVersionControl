using System;
using System.Configuration;

namespace AlbumVersionControl.Configs
{
    public class AppConfiguration
    {
        public string VersionContentFolder { get => GetValue("VersionContentFolder"); }

        public string GeneratedClassesFolder { get => GetValue("GeneratedClassesFolder"); }

        private static string GetValue(string key)
        {
            return ConfigurationManager.AppSettings[key] ?? throw GetConfigurationException(key);
        }

        private static Exception GetConfigurationException(string configKey)
        {
            return new ConfigurationErrorsException(
                $"Не получилось считать значение {configKey} из файла конфигурации App.config");
        }
    }
}