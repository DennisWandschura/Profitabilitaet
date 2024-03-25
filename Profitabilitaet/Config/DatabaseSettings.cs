namespace Profitabilitaet.Config
{
    public class DatabaseSettings
    {
        public const string Name = "DatabaseSettings";

        public string Address { get; set; }
        public int Port { get; set; }
        public string Database { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
    }
}
