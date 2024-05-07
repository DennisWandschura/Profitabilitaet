namespace Profitabilitaet.Config
{
    public class DatabaseSettings
    {
        public const string Name = "DatabaseSettings";

        public string Address { get; set; } = string.Empty;
        public int Port { get; set; }
        public string Database { get; set; } = string.Empty;
        public string User { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
