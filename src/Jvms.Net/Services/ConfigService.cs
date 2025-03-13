using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Jvms.Net.Services
{
    public class ConfigService
    {
        private readonly string _configPath;
        private Config _config;

        public ConfigService()
        {
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string jvmsPath = Path.Combine(appDataPath, "jvms");
            
            if (!Directory.Exists(jvmsPath))
            {
                Directory.CreateDirectory(jvmsPath);
            }
            
            _configPath = Path.Combine(jvmsPath, "config.json");
            _config = LoadConfig().GetAwaiter().GetResult();
        }

        public string JavaHome => _config.JavaHome;
        public string OriginalPath => _config.OriginalPath;
        public string ProxyUrl => _config.ProxyUrl;

        private async Task<Config> LoadConfig()
        {
            if (!File.Exists(_configPath))
            {
                return new Config
                {
                    JavaHome = "C:\\Program Files\\jdk",
                    OriginalPath = "https://raw.githubusercontent.com/ystyle/jvms/master/jdk.json"
                };
            }

            string json = await File.ReadAllTextAsync(_configPath);
            return JsonSerializer.Deserialize<Config>(json) ?? new Config();
        }

        public async Task SaveConfig()
        {
            string json = JsonSerializer.Serialize(_config, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(_configPath, json);
        }

        public async Task SetJavaHome(string javaHome)
        {
            _config.JavaHome = javaHome;
            await SaveConfig();
        }

        public async Task SetOriginalPath(string originalPath)
        {
            _config.OriginalPath = originalPath;
            await SaveConfig();
        }

        public async Task SetProxyUrl(string proxyUrl)
        {
            _config.ProxyUrl = proxyUrl;
            await SaveConfig();
        }

        private class Config
        {
            public string JavaHome { get; set; } = string.Empty;
            public string OriginalPath { get; set; } = string.Empty;
            public string ProxyUrl { get; set; } = string.Empty;
        }
    }
}
