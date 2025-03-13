using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Jvms.Net.Models;

namespace Jvms.Net.Services
{
    public class JdkService
    {
        private readonly ConfigService _configService;
        private readonly HttpClient _httpClient;

        public JdkService(ConfigService configService)
        {
            _configService = configService;
            _httpClient = new HttpClient();
            
            if (!string.IsNullOrEmpty(_configService.ProxyUrl))
            {
                _httpClient.DefaultRequestVersion = new Version(2, 0);
                _httpClient.DefaultRequestHeaders.Add("User-Agent", "JVMS.NET/1.0");
            }
        }

        public async Task<List<JdkVersion>> GetAvailableVersions(bool all = false)
        {
            string json = await _httpClient.GetStringAsync(_configService.OriginalPath);
            var versions = JsonSerializer.Deserialize<List<JdkVersion>>(json) ?? new List<JdkVersion>();
            
            if (!all && versions.Count > 10)
            {
                return versions.GetRange(0, 10);
            }
            
            return versions;
        }

        public async Task<List<JdkVersion>> GetInstalledVersions()
        {
            var result = new List<JdkVersion>();
            string javaHome = _configService.JavaHome;
            
            if (!Directory.Exists(javaHome))
            {
                return result;
            }

            string currentVersion = GetCurrentVersion();
            
            foreach (var dir in Directory.GetDirectories(javaHome))
            {
                string version = Path.GetFileName(dir);
                result.Add(new JdkVersion
                {
                    Version = version,
                    InstallPath = dir,
                    IsInstalled = true,
                    IsActive = version == currentVersion
                });
            }
            
            return result;
        }

        private string GetCurrentVersion()
        {
            try
            {
                string javaHome = Environment.GetEnvironmentVariable("JAVA_HOME") ?? string.Empty;
                if (string.IsNullOrEmpty(javaHome) || !Directory.Exists(javaHome))
                {
                    return string.Empty;
                }
                
                return Path.GetFileName(javaHome);
            }
            catch
            {
                return string.Empty;
            }
        }

        // TODO: Implement install, switch, and remove methods
    }
}
