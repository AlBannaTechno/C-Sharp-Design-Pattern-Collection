using System.Reflection;
using System.Runtime.CompilerServices;
using MainProject.CreationalPatterns.Builder;
using MainProject.CreationalPatterns.Builder.FacetedBuilder.FBWorks;
using MainProject.CreationalPatterns.Builder.FunctionalNester.FunctionalBuilderWorks;
using MainProject.CreationalPatterns.Factories.F7AbstractFactoryWorksOCP;
using MainProject.CreationalPatterns.Prototype.P_1_WithICloneable;
using MainProject.CreationalPatterns.Prototype.P_2_CopyConstructor;
using MainProject.CreationalPatterns.Prototype.P_3_CopyWithSerialization;
using MainProject.CreationalPatterns.Singleton.S_1_SingletonImplementation;

[assembly: InternalsVisibleTo("Testing")]
namespace MainProject
{
    class Program
    {
        static void Main(string[] args)
        {
            SingletonImplementation.Run();
        }
    }
}
