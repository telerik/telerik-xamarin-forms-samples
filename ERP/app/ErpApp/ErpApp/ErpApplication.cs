using MvvmCross.IoC;
using MvvmCross.ViewModels;

namespace ErpApp
{
    public class ErpApplication : MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .InNamespace(typeof(Services.IErpService).Namespace)
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();
                
            RegisterCustomAppStart<AppStart>();
        }
    }
}
