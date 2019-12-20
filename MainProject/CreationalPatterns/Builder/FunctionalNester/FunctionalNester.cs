using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MainProject.CreationalPatterns.Builder.FunctionalNester
{
    public class FunctionalNester<T>
    {
        public readonly List<FunctionalNester<T>> Children = new List<FunctionalNester<T>>();
        public T Item { get; set; }
        public FunctionalNester<T> Parent { get; set; }
        public bool AllowChildren { get; set; } = true;
        public bool ThrownErrorOnChildrenAdd { get; set; } = true;
        public readonly Func<FunctionalNester<T>, int ,string> ToStringImplementation;

        public FunctionalNester(Func<FunctionalNester<T>, int ,string> toStringImplementation)
        {
            ToStringImplementation = toStringImplementation;
            
        }
        private (bool allowChildrentAttributeDefined, bool allowChildrenValue, bool thrownError) IsAllowingChildren(T item)
        {
            (bool allowChildrentAttributeDefined, bool allowChildrenValue, bool thrownError) result = (false ,false, true);
            // we will use reflection with attributes
            var allowChildrenField = typeof(T).GetProperties().FirstOrDefault(f => CustomAttributeExtensions.IsDefined((MemberInfo) f, typeof(AllowChildrenBinderAttribute)));
            if (allowChildrenField != null)
            {
                result.allowChildrentAttributeDefined = true;
                var allowChildren = allowChildrenField.GetValue(item);
                result.allowChildrenValue = (bool) allowChildren;
                var attr = (AllowChildrenBinderAttribute) allowChildrenField.GetCustomAttribute(typeof(AllowChildrenBinderAttribute));
                result.thrownError = attr.ThrownError;
            }
            return result;
        }
        private bool CheckAddChildrenAvailability()
        {
            // we may allow to not throw an error : just print warning or do nothing
            if (!AllowChildren)
            {
                if (!ThrownErrorOnChildrenAdd)
                {
                    Console.Error.WriteLine("Please review your nester chain : because some " +
                                            "elements will not set because its parent can not have a children");
                    Console.Error.WriteLine("You can Also Enable Thrown Error on AllowChildrenBinder Attribute " +
                                            "To Track This problem");
                    return false;
                }
                throw new Exception("You can not add any child to this element");
            }
            return true;
        }
        
        private (bool allowChildrenDecision, bool thrownError) CheckAddChildrenAllowabilityOfThis(T item, bool allowChildren)
        {
            var acResult = IsAllowingChildren(item);
            // decision
            bool allowChildrenDecision;
            if (acResult.allowChildrentAttributeDefined)
            {
                allowChildrenDecision = acResult.allowChildrenValue;
            }
            else
            {
                allowChildrenDecision = allowChildren;
            }

            return (allowChildrenDecision, acResult.thrownError);
        }
        public FunctionalNester<T> AddChild(T child, bool allowChildren = true)
        {
            if (!CheckAddChildrenAvailability())
            {
                return this;
            }
            var (allowChildrenDecision,thrownError) = CheckAddChildrenAllowabilityOfThis(child, allowChildren);
            var ch = new FunctionalNester<T>(ToStringImplementation)
            {
                Item = child, Parent = this, AllowChildren = allowChildrenDecision, ThrownErrorOnChildrenAdd = thrownError
            };
            Children.Add(ch);
            return this;
        }
        public FunctionalNester<T> AddChild(T child, out FunctionalNester<T> element, bool allowChildren = true)
        {
            if (!CheckAddChildrenAvailability())
            {
                element = null;
                return this;
            }
            var (allowChildrenDecision,thrownError) = CheckAddChildrenAllowabilityOfThis(child, allowChildren);
            var ch = new FunctionalNester<T>(ToStringImplementation)
            {
                Item = child, Parent = this, AllowChildren = allowChildrenDecision, ThrownErrorOnChildrenAdd = thrownError
            };
            Children.Add(ch);
            element = ch;
            return this;
        }
        public FunctionalNester<T> WithChild(T child, bool allowChildren = true)
        {
            if (!CheckAddChildrenAvailability())
            {
                return this;
            }
            var (allowChildrenDecision,thrownError) = CheckAddChildrenAllowabilityOfThis(child, allowChildren);
            var ch = new FunctionalNester<T>(ToStringImplementation)
            {
                Item = child, Parent = this , AllowChildren = allowChildrenDecision, ThrownErrorOnChildrenAdd = thrownError
            };
            Children.Add(ch);
            return ch;
        }
        public FunctionalNester<T> WithChild(T child, out FunctionalNester<T> element, bool allowChildren = true)
        {
            if (!CheckAddChildrenAvailability())
            {
                element = null;
                return this;
            }
            var (allowChildrenDecision,thrownError) = CheckAddChildrenAllowabilityOfThis(child, allowChildren);
            var ch = new FunctionalNester<T>(ToStringImplementation)
            {
                Item = child, Parent = this, AllowChildren = allowChildrenDecision, ThrownErrorOnChildrenAdd = thrownError
            };
            Children.Add(ch);
            element = ch;
            return ch;
        }

        public FunctionalNester<T> ToParent(int upTo = 1)
        {
            if (upTo == 1)
            {
                return Parent;
            }

            return Parent.ToParent(upTo - 1);
        }

        public override string ToString()
        {
            if (ToStringImplementation == null)
            {
                throw new Exception("You must provide ToStringImplementation()");
            }
            return ToStringImplementation(this, 0);
        }
    }
}