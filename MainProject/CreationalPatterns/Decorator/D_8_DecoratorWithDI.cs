using Autofac;
using static System.Console;
namespace MainProject.CreationalPatterns.Decorator
{
    namespace D8DecoratorWithDI
    {
        interface IReportingService
        {
            void Report();
        }

        class ReportingService: IReportingService
        {
            public void Report()
            {
                WriteLine("My Report");
            }
        }

        class ReportingServiceLogging: IReportingService
        {
            private readonly IReportingService _decorator;

            public ReportingServiceLogging(IReportingService decorator)
            {
                _decorator = decorator;
            }

            public void Report()
            {
                WriteLine("Start Logging Report");
                _decorator.Report();
                WriteLine("Finish Logging Report");
            }
        }

        public static class DecoratorWithAutofac
        {
            public static void Run()
            {
                var builder = new ContainerBuilder();
                // we want ReportingServiceLogging to be resolved when we request IReportingService
                // but because of ReportingServiceLogging has a constructor with IReportingService parameter
                // we can not allow Autofac to pass ReportingServiceLogging again to itself
                // we need a condition : if IReportingService in the ReportingServiceLogging constructor
                // we must pass ReportingService as a IReportingService Implementation

                builder.RegisterType<ReportingService>().Named<IReportingService>("reporting");
                builder.RegisterDecorator<IReportingService>(
                    (context, service) => new ReportingServiceLogging(service),"reporting");

                using var container = builder.Build();
                var report = container.Resolve<IReportingService>();
                report.Report();
            }
        }

    }
}
