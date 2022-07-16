namespace EnvironmentTests.Exceptions
{
    public class InvalidConfigurationException : Exception
    {
        public string Item { get; }

        public InvalidConfigurationException(string item, string message) : base(message)
        {
            Item = item ?? throw new ArgumentNullException(nameof(item));
        }
    }
}
