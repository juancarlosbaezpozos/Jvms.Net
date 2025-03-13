namespace Jvms.Net.Models
{
    public class JdkVersion
    {
        public string Version { get; set; } = string.Empty;
        public string DownloadUrl { get; set; } = string.Empty;
        public string InstallPath { get; set; } = string.Empty;
        public bool IsInstalled { get; set; }
        public bool IsActive { get; set; }

        public override string ToString()
        {
            string status = IsActive ? "* " : "  ";
            return $"{status}{Version}";
        }
    }
}
