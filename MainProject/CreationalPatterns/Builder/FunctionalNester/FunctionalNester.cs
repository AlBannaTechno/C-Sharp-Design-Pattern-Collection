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

        #region Make Next [p,f,...] public / private depends on design descessions , and I will leave all of them public 

        public FunctionalNester<T> Parent { get; set; }
        public bool AllowChildren { get; set; } = true;
        public bool ThrownErrorOnChildrenAdd { get; set; } = true;
        public readonly Func<FunctionalNester<T>, int ,string> ToStringImplementation;

        #endregion

        public FunctionalNester(Func<FunctionalNester<T>, int ,string> toStringImplementation)
        {
            ToStringImplementation = toStringImplementation;
            
        }
        private static (bool allowChildrentAttributeDefined, bool allowChildrenValue, bool thrownError)
            IsAllowingChildren(T item)
        {
            (bool allowChildrentAttributeDefined, bool allowChildrenValue, bool thrownError) 
                result = (false ,false, true);
            var allowChildrenField = typeof(T).GetProperties()
                .FirstOrDefault(f => f.IsDefined(typeof(AllowChildrenBinderAttribute)));
            if (allowChildrenField == null) return result;
            result.allowChildrentAttributeDefined = true;
            var allowChildren = allowChildrenField.GetValue(item);
            result.allowChildrenValue = (bool) allowChildren;
            var attr = (AllowChildrenBinderAttribute) allowChildrenField
                .GetCustomAttribute(typeof(AllowChildrenBinderAttribute));
            result.thrownError = attr.ThrownError;
            return result;
        }
        private bool CheckAddChildrenAvailability()
        {
            if (AllowChildren) return true;
            if (ThrownErrorOnChildrenAdd) throw new Exception("You can not add any child to this element");
            Console.Error.WriteLine("Please review your nester chain : because some " +
                                    "elements will not set because its parent can not have a children");
            Console.Error.WriteLine("You can Also Enable Thrown Error on AllowChildrenBinder Attribute " +
                                    "To Track This problem");
            return false;
        }
        
        private static (bool allowChildrenDecision, bool thrownError) 
            CheckAddChildrenAllowabilityOfThis(T item, bool allowChildren)
        {
            var (allowChildrenAttributeDefined, allowChildrenValue, thrownError) 
                = IsAllowingChildren(item);
            // decision
            var allowChildrenDecision = allowChildrenAttributeDefined ? allowChildrenValue : allowChildren;
            return (allowChildrenDecision, thrownError);
        }
        public FunctionalNester<T> AddChild(T child, bool allowChildren = true)
        {
            if (!CheckAddChildrenAvailability())
            {
                return this;
            }
            var (allowChildrenDecision,thrownError) 
                = CheckAddChildrenAllowabilityOfThis(child, allowChildren);
            var ch = new FunctionalNester<T>(ToStringImplementation)
            {
                Item = child, Parent = this, AllowChildren = allowChildrenDecision,
                ThrownErrorOnChildrenAdd = thrownError
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
            var (allowChildrenDecision,thrownError) = 
                CheckAddChildrenAllowabilityOfThis(child, allowChildren);
            var ch = new FunctionalNester<T>(ToStringImplementation)
            {
                Item = child, Parent = this, AllowChildren = allowChildrenDecision,
                ThrownErrorOnChildrenAdd = thrownError
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
            var (allowChildrenDecision,thrownError) 
                = CheckAddChildrenAllowabilityOfThis(child, allowChildren);
            var ch = new FunctionalNester<T>(ToStringImplementation)
            {
                Item = child, Parent = this , AllowChildren = allowChildrenDecision,
                ThrownErrorOnChildrenAdd = thrownError
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
            var (allowChildrenDecision,thrownError) 
                = CheckAddChildrenAllowabilityOfThis(child, allowChildren);
            var ch = new FunctionalNester<T>(ToStringImplementation)
            {
                Item = child, Parent = this, AllowChildren = allowChildrenDecision,
                ThrownErrorOnChildrenAdd = thrownError
            };
            Children.Add(ch);
            element = ch;
            return ch;
        }

        public FunctionalNester<T> ToParent(int upTo = 1)
        {
            return upTo == 1 ? Parent : Parent.ToParent(upTo - 1);
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