﻿using System.Reflection;
using System.Runtime.CompilerServices;
using MainProject.CreationalPatterns.Builder;
using MainProject.CreationalPatterns.Builder.FacetedBuilder.FBWorks;
using MainProject.CreationalPatterns.Builder.FunctionalNester.FunctionalBuilderWorks;
using MainProject.CreationalPatterns.Composite.C1GeometricShapes;
using MainProject.CreationalPatterns.Decorator.D2AdapterDecorator_HyperStringBuilder;
using MainProject.CreationalPatterns.Decorator.D5DynamicDecoratorComposition;
using MainProject.CreationalPatterns.Decorator.D6StaticDecoratorComposition;
using MainProject.CreationalPatterns.Decorator.D7StaticDecoratorCompositionWithInternalAccess;
using MainProject.CreationalPatterns.Decorator.D8DecoratorWithDI;
using MainProject.CreationalPatterns.Factories.F7AbstractFactoryWorksOCP;
using MainProject.CreationalPatterns.Prototype.P_1_WithICloneable;
using MainProject.CreationalPatterns.Prototype.P_2_CopyConstructor;
using MainProject.CreationalPatterns.Prototype.P_3_CopyWithSerialization;
using MainProject.CreationalPatterns.Singleton.S_1_SingletonImplementation;
using MainProject.StructuralPatterns.Adapter;
using MainProject.StructuralPatterns.Adapter.A1VectorRasterNoCaching;
using MainProject.StructuralPatterns.Adapter.A2GenericValueAdapter;
using MainProject.StructuralPatterns.Adapter.A2GenericValueAdapterWithFullVectorImpHyperPropagation;
using MainProject.StructuralPatterns.Adapter.A2VectorRasterWithCaching;
using MainProject.StructuralPatterns.Adapter.A6AdapterWithDependencyInjectionAndMetaData;
using MainProject.StructuralPatterns.FlyWight.F2TextFormatter;
using MainProject.StructuralPatterns.Proxy.P1ProtectionProxy;

[assembly: InternalsVisibleTo("Testing")]
namespace MainProject
{
    class Program
    {
        static void Main(string[] args)
        {
            ProtectionProxy.Run();
        }
    }
}
