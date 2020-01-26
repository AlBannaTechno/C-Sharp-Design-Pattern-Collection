using System;
using MainProject.StructuralPatterns.Adapter.A2GenericValueAdapter;

/*
 * The Problem here , we need compiler to use deepest method of sub class from the top most parent class
 * and this impossible in C#
 */
namespace MainProject.StructuralPatterns.Adapter
{
  namespace A2GenericValueAdapterWithFullVectorImpHyperPropagation
  {
    #region Aithmatic Helper

    public class ConstrainedValue<T> where T : IComparable, IComparable<T>, IEquatable<T>
    {
      private readonly T _value;

      public ConstrainedValue(T value)
      {
        _value = value;
      }

      public static T operator + (ConstrainedValue<T> x, ConstrainedValue<T> y)
      {
        return (dynamic)x._value + y._value;
      }

      public static T operator - (ConstrainedValue<T> x, ConstrainedValue<T> y)
      {
        return (dynamic)x._value - y._value;
      }

      public static T operator * (ConstrainedValue<T> x, ConstrainedValue<T> y)
      {
        return (dynamic)x._value * y._value;
      }

      public static T operator / (ConstrainedValue<T> x, ConstrainedValue<T> y)
      {
        return (dynamic)x._value / y._value;
      }
    }

    #endregion
    #region Integer Bases

    public interface IInteger
    {
      int Value { get;}
    }

    public abstract class Dimensions
    {
      public  class Two : IInteger
      {
        public int Value => 2;
      }

      public class Three : IInteger
      {
        public int Value => 3;
      }

      public class Four : IInteger
      {
        public int Value => 4;
      }
    }

    #endregion

    #region Vector Base

    public abstract class VectorBase<T, TDim> where TDim : IInteger , new()
    {
      public T[] Vec;
      public int Dim;
      public VectorBase()
      {
        Dim = new TDim().Value;
        Vec = new T[Dim];
      }

      public VectorBase(params T[] values)
      {
        Dim = new TDim().Value;
        Vec = new T[Dim];

        var providedSize = values.Length;

        for (int i = 0 , max = Math.Min(Dim, providedSize); i < max; i++)
        {
          Vec[i] = values[i];
        }
      }


      public T this[int index]
      {
        get
        {
          if (index < Dim)
          {
            return Vec[index];
          }
          else
          {
            throw new IndexOutOfRangeException($"Maximum index of ${Dim} Vector is ${Dim - 1}");
          }
        }

        set
        {
          if (index < Dim)
          {
            Vec[index] = value;
          }
          else
          {
            throw new IndexOutOfRangeException($"Maximum index of ${Dim} Vector is ${Dim - 1}");
          }
        }
      }

      public override string ToString()
      {
        return $"({string.Join(",", Vec)})";
      }

    }

    public abstract class Vector<TSelf, T, TDim, TOperatorReturn> : VectorBase<T, TDim>
    // to force using integer values for dimensions
    where TDim: IInteger, new()
    // to restrict TSelf to be of type Vector
    where TSelf: Vector<TSelf, T, TDim, TOperatorReturn> , new()
    where TOperatorReturn : VectorBase<T, TDim>,new()
    where T : IComparable, IComparable<T>, IEquatable<T>
    {
      protected Vector() // auto call parent constructor
      {
      }

      protected Vector(params T[] values) : base(values)
      {
      }

      #region Arithmatec Operations

      #region Helpers

      // for the same type vec * vec
      private static TOperatorReturn ExecuteArithmeticOperation<TVal>(
        Vector<TSelf, T, TDim, TOperatorReturn> source,
        TSelf dist ,
        Func<ConstrainedValue<T> , ConstrainedValue<T> , T> op)
        where TVal : IComparable, IComparable<TVal>, IEquatable<TVal>
      {

        var result = new TOperatorReturn(){Vec = (T[]) source.Vec.Clone(), Dim = source.Dim};

        for (int i = 0; i < result.Dim; i++)
        {
          var a = new ConstrainedValue<T>(result[i]);
          var b = new ConstrainedValue<T>(dist[i]);
          result[i] = op(a, b);
        }
        return result;
      }

      // for generic vec * 2 , vec * any Type we set the vector as a type of it
      private static TOperatorReturn ExecuteArithmeticOperation<TVal>(
        Vector<TSelf, T, TDim, TOperatorReturn> source,
        T dist ,
        Func<ConstrainedValue<T> , ConstrainedValue<T> , T> op)
        where TVal : IComparable, IComparable<TVal>, IEquatable<TVal>
      {

        var result = new TOperatorReturn(){Vec = (T[]) source.Vec.Clone(), Dim = source.Dim};

        for (int i = 0; i < result.Dim; i++)
        {
          var a = new ConstrainedValue<T>(result[i]);
          result[i] = op(a, new ConstrainedValue<T>(dist));
        }
        return result;
      }

      #endregion

      #region Operators

      public static TOperatorReturn operator + (Vector<TSelf, T, TDim, TOperatorReturn> source, TSelf dist)
      {
        return ExecuteArithmeticOperation<T>(source, dist, (s,d) => s+d);
      }
      public static TOperatorReturn operator + (Vector<TSelf, T, TDim, TOperatorReturn> source, T dist)
      {
        return ExecuteArithmeticOperation<T>(source, dist, (s,d) => s+d);
      }


      public static TOperatorReturn operator - (Vector<TSelf, T, TDim, TOperatorReturn> source, TSelf dist)
      {
        return ExecuteArithmeticOperation<T>(source, dist, (s,d) => s-d);
      }
      public static TOperatorReturn operator - (Vector<TSelf, T, TDim, TOperatorReturn> source, T dist)
      {
        return ExecuteArithmeticOperation<T>(source, dist, (s,d) => s-d);
      }


      public static TOperatorReturn operator * (Vector<TSelf, T, TDim, TOperatorReturn> source, TSelf dist)
      {
        return ExecuteArithmeticOperation<T>(source, dist, (s,d) => s * d);
      }
      public static TOperatorReturn operator * (Vector<TSelf, T, TDim, TOperatorReturn> source, T dist)
      {
        return ExecuteArithmeticOperation<T>(source, dist, (s,d) => s * d);
      }


      public static TOperatorReturn operator / (Vector<TSelf, T, TDim, TOperatorReturn> source, TSelf dist)
      {
        return ExecuteArithmeticOperation<T>(source, dist, (s,d) => s / d);
      }
      public static TOperatorReturn operator / (Vector<TSelf, T, TDim, TOperatorReturn> source, T dist)
      {
        return ExecuteArithmeticOperation<T>(source, dist, (s,d) => s / d);
      }

      #endregion

      #endregion




    }

    #endregion

    #region Vector Typed Bases

    #region Vector Typed Real Number Base

    // this just a class to demonstrate the case when we need to nest inheritance to additional levels

    public class VectorOfReal<TSelf, TDim, TReal, TOperatorReturn> :
      Vector<VectorOfReal<TSelf, TDim, TReal, TOperatorReturn>, TReal, TDim, TOperatorReturn>
      where TDim : IInteger, new()
      where TSelf : VectorOfReal<TSelf, TDim, TReal, TOperatorReturn>, new()
      where TOperatorReturn :  VectorBase<TReal, TDim>, new()
      where TReal : IComparable, IComparable<TReal>, IEquatable<TReal>
    {

    }

    #endregion

    public class VectorOfInt<TSelf, TDim, TOperatorReturn> : Vector<VectorOfInt<TSelf,TDim,TOperatorReturn>, int, TDim, TOperatorReturn>
      where TDim : IInteger, new()
      where TSelf : VectorOfInt<TSelf, TDim, TOperatorReturn>, new()
      where TOperatorReturn : VectorBase<int, TDim>,new()
    {
      public VectorOfInt()
      {
      }

      public VectorOfInt(params int[] values) : base(values)
      {
      }

    }

    #endregion

    #region Vector Type Cpacity Bases

    public class Vector2I : VectorOfInt<Vector2I, Dimensions.Two, Vector2I>
    {
      public Vector2I()
      {
      }

      public Vector2I(int x, int y) : base(x, y)
      {
      }

      public int X
      {
        get => this[0];
        set => this[0] = value;
      }

      public int Y
      {
        get => this[1];
        set => this[1] = value;
      }
    }

    public class Vector3I : VectorOfInt<Vector3I,Dimensions.Three, Vector3I>
    {
      public Vector3I()
      {
      }

      public Vector3I(int x, int y, int z) : base(x, y, z)
      {
      }

      public int X
      {
        get => this[0];
        set => this[0] = value;
      }

      public int Y
      {
        get => this[1];
        set => this[1] = value;
      }

      public int Z
      {
        get => this[2];
        set => this[2] = value;
      }
    }

    #endregion
    public static class GenericValueAdapterWithFullVecImpHyperProp
    {
      public static void Run()
      {
        var vec2I = new Vector2I(2,54);
        var vec2I2 = new Vector2I(4,6);
        var vec3 = vec2I + vec2I2 ;
        Console.WriteLine(vec3);
        Console.WriteLine(vec3.X);

        var vec3I1 = new Vector3I(2,5,4);
        var vec3I2 = new Vector3I(5,4,3);

        var vec3Final = vec3I1 * vec3I2;

        Console.WriteLine(vec3Final);
        Console.WriteLine(vec3Final + 3);
        Console.WriteLine(vec3Final.Z);

      }
    }
  }
}
