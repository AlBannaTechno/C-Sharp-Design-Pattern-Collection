using System;
using MainProject.StructuralPatterns.Adapter.A2GenericValueAdapter;

/*
 * The Problem here , we need compiler to use deepest method of sub class from the top most parent class
 * and this impossible in C#
 */
namespace MainProject.StructuralPatterns.Adapter
{
  namespace A2GenericValueAdapterWithVectorImpHyperPropagation
  {
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
        public int Value => 2;
      }

      public class Four : IInteger
      {
        public int Value => 2;
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
    }

    public abstract class Vector<TSelf, T, TDim, TOperatorReturn> : VectorBase<T, TDim>
    // to force using integer values for dimensions
    where TDim: IInteger, new()
    // to restrict TSelf to be of type Vector
    where TSelf: Vector<TSelf, T, TDim, TOperatorReturn> , new()
    {
      protected Vector()
      {
      }

      protected Vector(params T[] values) : base(values)
      {
      }

      // Factory
      public static TSelf Create(params T[] values)
      {
        var result = new TSelf();

        var providedSize = values.Length;

        for (int i = 0 , max = Math.Min(result.Dim, providedSize); i < max; i++)
        {
          result.Vec[i] = values[i];
        }

        return result;
      }

      // will removed

      public T X
      {
        get => Vec[0];
        set => Vec[0] = value;
      }

      protected abstract TOperatorReturn OperatorAdd(TSelf dist);
      protected abstract TOperatorReturn OperatorAdd(T dist);
      protected abstract TOperatorReturn OperatorSubtract(TSelf dist);
      protected abstract TOperatorReturn OperatorSubtract(T dist);
      protected abstract TOperatorReturn OperatorMultiply(TSelf dist);
      protected abstract TOperatorReturn OperatorMultiply(T dist);

      public static TOperatorReturn operator +(Vector<TSelf, T, TDim, TOperatorReturn> source, TSelf dist)
      {
        return source.OperatorAdd(dist);
      }

      public static TOperatorReturn operator +(Vector<TSelf, T, TDim, TOperatorReturn> source, T dist)
      {
        return source.OperatorAdd(dist);
      }

      public static TOperatorReturn operator -(Vector<TSelf, T, TDim, TOperatorReturn> source, TSelf dist)
      {
        return source.OperatorSubtract(dist);
      }

      public static TOperatorReturn operator -(Vector<TSelf, T, TDim, TOperatorReturn> source, T dist)
      {
        return source.OperatorSubtract(dist);
      }

      public static TOperatorReturn operator *(Vector<TSelf, T, TDim, TOperatorReturn> source, TSelf dist)
      {
        return source.OperatorMultiply(dist);
      }

      public static TOperatorReturn operator *(Vector<TSelf, T, TDim, TOperatorReturn> source, T dist)
      {
        return source.OperatorMultiply(dist);
      }

    }

    #endregion

    #region Vector Typed Bases

    #region Vector Typed Real Number Base

    // its very difficult to restrict TReal To int,float,double  in C#
    // so we will depends on developer awareness to do not use this class
    public class VectorOfReal<TSelf, TDim, TReal, TOperatorReturn> :
      Vector<VectorOfReal<TSelf, TDim, TReal, TOperatorReturn>, TReal, TDim, TOperatorReturn>
      where TDim : IInteger, new()
      where TSelf : VectorOfReal<TSelf, TDim, TReal, TOperatorReturn>, new()
    {
      protected override TOperatorReturn OperatorAdd(VectorOfReal<TSelf, TDim, TReal, TOperatorReturn> dist)
      {
        var s = new TSelf {Vec = Vec , Dim = Dim};
        return s.OperatorAdd(dist);
      }

      protected override TOperatorReturn OperatorAdd(TReal dist)
      {
        var s = new TSelf {Vec = Vec , Dim = Dim};
        return s.OperatorAdd(dist);
      }

      protected override TOperatorReturn OperatorSubtract(VectorOfReal<TSelf, TDim, TReal, TOperatorReturn> dist)
      {
        var s = new TSelf {Vec = Vec , Dim = Dim};
        return s.OperatorSubtract(dist);
      }

      protected override TOperatorReturn OperatorSubtract(TReal dist)
      {
        var s = new TSelf {Vec = Vec , Dim = Dim};
        return s.OperatorSubtract(dist);
      }

      protected override TOperatorReturn OperatorMultiply(VectorOfReal<TSelf, TDim, TReal, TOperatorReturn> dist)
      {
        var s = new TSelf {Vec = Vec , Dim = Dim};
        return s.OperatorMultiply(dist);
      }

      protected override TOperatorReturn OperatorMultiply(TReal dist)
      {
        var s = new TSelf {Vec = Vec , Dim = Dim};
        return s.OperatorMultiply(dist);
      }
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

      protected override TOperatorReturn OperatorAdd(VectorOfInt<TSelf, TDim, TOperatorReturn> dist)
      {
        // if we type {Vec = Vec} this will pass Vec value by reference so , but we just need a copy
        // we need to persist the actual value
        var result = new TOperatorReturn(){Vec = (int[]) Vec.Clone(), Dim = Dim};

        for (int i = 0; i < result.Dim; i++)
        {
          result[i] += dist[i];
        }
        return result;
      }

      protected override TOperatorReturn OperatorAdd(int dist)
      {
        var result = new TOperatorReturn(){Vec = (int[]) Vec.Clone(), Dim = Dim};

        for (int i = 0; i < result.Dim; i++)
        {
          result[i] += dist;
        }
        return result;
      }

      // only when we will use the operator we should implement its logic
      protected override TOperatorReturn OperatorSubtract(VectorOfInt<TSelf, TDim, TOperatorReturn> dist)
      {
        var result = new TOperatorReturn(){Vec = (int[]) Vec.Clone(), Dim = Dim};
        for (int i = 0; i < result.Dim; i++)
        {
          result[i] -= dist[i];
        }
        return result;
      }

      protected override TOperatorReturn OperatorSubtract(int dist)
      {
        var result = new TOperatorReturn(){Vec = (int[]) Vec.Clone(), Dim = Dim};
        for (int i = 0; i < result.Dim; i++)
        {
          result[i] -= dist;
        }
        return result;
      }

      protected override TOperatorReturn OperatorMultiply(VectorOfInt<TSelf, TDim, TOperatorReturn> dist)
      {
        var result = new TOperatorReturn(){Vec = (int[]) Vec.Clone(), Dim = Dim};
        for (int i = 0; i < result.Dim; i++)
        {
          result[i] *= dist[i];
        }
        return result;
      }

      protected override TOperatorReturn OperatorMultiply(int dist)
      {
        var result = new TOperatorReturn(){Vec = (int[]) Vec.Clone(), Dim = Dim};
        for (int i = 0; i < result.Dim; i++)
        {
          result[i] *= dist;
        }
        return result;
      }
    }

    #endregion

    #region Vector Type Cpacity Bases

    public class Vector2I : VectorOfInt<Vector2I, Dimensions.Two, Vector2I>
    {
      public Vector2I()
      {
      }

      public Vector2I(int x, int y) : base(new []{x,y})
      {
      }

      public override string ToString()
      {
        return $"{string.Join(",", Vec)}";
      }
    }

    public class Vector3I : VectorOfInt<Vector3I,Dimensions.Three, Vector3I>
    {
      public Vector3I()
      {
      }

      public Vector3I(int x, int y, int z) : base(new []{x, y, z})
      {
      }
    }

    #endregion
    public static class GenericValueAdapterWithVecImp
    {
      public static void Run()
      {
        var vec2I = new Vector2I(2,54);
        var vec2I2 = new Vector2I(4,6);
        var vec3 = vec2I + vec2I2 ;
        var vec4 = vec3 - vec2I;
        Console.WriteLine(vec3);
        var m = vec3 + 4;
        Console.WriteLine(vec4);
        Console.WriteLine(m);
        Console.WriteLine(m - 2);
        Console.WriteLine(m * 5);
      }
    }
  }
}
