using System;

namespace MainProject.CreationalPatterns.Builder.FunctionalNester
{
    [AttributeUsage(AttributeTargets.Property)]
    public class AllowChildrenBinderAttribute : Attribute
    {
        // if this is false : nester will just continue without any work on the passed element
        public bool ThrownError { get; set; } = true;
    }
}