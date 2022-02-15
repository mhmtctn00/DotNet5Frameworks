using Autofac;
using Core.Utilities.Security.Authorization;
using Core.Utilities.Security.Authorization.JWT;

public class AutofacCoreModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<JwtHelper>().As<ITokenHelper>();
    }
}