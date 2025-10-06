using System.Reflection;
using WebPortal.DbStuff.Repositories;
using WebPortal.DbStuff.Repositories.Interfaces;
using WebPortal.DbStuff.Repositories.Interfaces.Notes;
using WebPortal.DbStuff.Repositories.Notes;

namespace WebPortal.Services.AutoRegistrationInDI
{
    public class AutoRegisterService
    {
        public void RegisterAllRepositories(IServiceCollection services)
        {
            var baseRepository = typeof(BaseRepository<>);
            var assembly = Assembly.GetAssembly(baseRepository);

            var classRepositories = assembly
                .GetTypes()
                .Where(x => x.BaseType != null // BaseRepository<Girl>
                    && x.BaseType.IsGenericType
                    && x.BaseType.GetGenericTypeDefinition() == baseRepository)
                .ToList();

            var interfaceRepositories = assembly
                .GetTypes()
                .Where(x => x.IsInterface
                    && x.GetInterfaces()
                        .Any(i => i.IsGenericType
                            && i.GetGenericTypeDefinition() == typeof(IBaseRepository<>)));


            foreach (var interfaceRepository in interfaceRepositories)
            {
                var classRepository = classRepositories
                    .First(classRepo => classRepo.GetInterfaces().Contains(interfaceRepository));
                services.AddScoped(interfaceRepository, classRepository);
            }
        }

        public void RegisterAllNotesRepositories(IServiceCollection services)
        {
            var baseDbRepository = typeof(BaseDbRepository<>);
            var assembly = Assembly.GetAssembly(baseDbRepository);

            var classNotesRepositories = assembly
                .GetTypes()
                .Where(x => x.BaseType != null 
                            && x.BaseType.IsGenericType 
                            && x.BaseType.GetGenericTypeDefinition() == baseDbRepository)
                .ToList();
            
            var interfaceNotesRepositories = assembly
                .GetTypes()
                .Where(x => x.IsInterface 
                            && x.GetInterfaces()
                                .Any(i => i.IsGenericType 
                                          && i.GetGenericTypeDefinition() == typeof(IBaseDbRepository<>)));

            foreach (var interfaceNotesRepository in interfaceNotesRepositories)
            {
                var classNotesRepository = classNotesRepositories
                    .First(classRepo => classRepo.GetInterfaces().Contains(interfaceNotesRepository));
                services.AddScoped(interfaceNotesRepository, classNotesRepository);
            }
        }

        public void RegisterAllByAttribute(IServiceCollection di)
        {
            RegisterAllByAttributeOnClass(di);
            RegisterAllByAttributeOnConstructor(di);
        }

        private void RegisterAllByAttributeOnConstructor(IServiceCollection di)
        {
            var attributeType = typeof(AutoResolveAttribute);
            var allTypes = Assembly
                .GetAssembly(attributeType)!
                .GetTypes();
            var services = allTypes
                .Where(x => x.IsClass
                    && x.GetConstructors()
                        .Any(c => c.GetCustomAttribute<AutoResolveAttribute>() != null));

            foreach (var serviceClass in services)
            {
                var myInterface = allTypes
                    .FirstOrDefault(x => x.IsInterface
                        && x.Name == "I" + serviceClass.Name); ;
                if (myInterface is not null)
                {
                    di.AddScoped(myInterface, diContainer => CreateObjByType(diContainer, serviceClass));
                }
                else
                {
                    di.AddScoped(serviceClass, diContainer => CreateObjByType(diContainer, serviceClass));
                }
            }
        }

        private object CreateObjByType(IServiceProvider diContainer, Type serviceClass)
        {
            var constructor = serviceClass
                .GetConstructors()
                .First(x => x.GetCustomAttribute<AutoResolveAttribute>() != null);

            var parameters = constructor
                .GetParameters()
                .Select(p => diContainer.GetService(p.ParameterType))
                .ToArray();

            // new SuperService(userRepo, girRe, ConT)
            var obj = constructor.Invoke(parameters);
            return obj;
        }

        private void RegisterAllByAttributeOnClass(IServiceCollection di)
        {
            var attributeType = typeof(AutoResolveAttribute);
            var allTypes = Assembly
                .GetAssembly(attributeType)!
                .GetTypes();
            var services = allTypes
                .Where(x => x.IsClass
                    && x.GetCustomAttribute<AutoResolveAttribute>() != null);

            foreach (var service in services)
            {
                var myInterface = allTypes
                    .FirstOrDefault(x => x.IsInterface
                        && x.Name == "I" + service.Name); ;
                if (myInterface is not null)
                {
                    di.AddScoped(myInterface, service);
                }
                else
                {
                    di.AddScoped(service);
                }
            }
        }
    }
}
