namespace MicroserviceBase
{
    public static class ApplicationInfo
    {
        private static string _serviceName;
        private static string _serviceId;
        private static readonly string _version;
        private const string ApplicationNameEnvironment = "ApplicationName";

        public static void Configure(IConfiguration configuration, string _version)
        {
            var msSettings = configuration.GetSection("MicroserviceSettings");
            if (msSettings is null)
                throw new ArgumentException("Missing MicroserviceSettings configuration section");

            //_version = GitVersion
            _serviceName = msSettings["Name"];
            Environment.SetEnvironmentVariable(ApplicationNameEnvironment, _serviceName);
            _serviceId = $"{_serviceName}-{_version}";
        }

        public static string GetServiceName() => _serviceName;
        public static string GetServiceId() => _serviceId;
        public static string GetVersion() => _version;
    }
}
