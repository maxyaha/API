//using Microservice.Companion.Entities.Files;
//using Microsoft.Extensions.DependencyInjection;
//using System;

//namespace API.Extensions
//{
//    public static class FileTransfectionServiceCollectionExtensions
//    {
//        public static void AddSftpContext(this IServiceCollection services, string host, string username, string password, int port = 22)
//        {
//            var config = new Config { Host = host, Username = username, Password = password, Port = port };

//            var valid = config?.IsValid() ?? false;

//            if (!valid)
//                throw new Exception($"{nameof(Config)} is invalid");

//            RegisterDependencies(services, config);
//        }

//        private static void RegisterDependencies(IServiceCollection services, Config config)
//        {
//            services.AddSingleton(config);
//        }
//    }
//}