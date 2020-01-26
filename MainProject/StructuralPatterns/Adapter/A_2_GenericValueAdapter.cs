using System;

namespace MainProject.StructuralPatterns.Adapter
{
  namespace A2GenericValueAdapter
  {
    #region Integer Bases

    public interface IInteger
    {
      int Value { get;}
    }

    public class Dimensions
    {
      public class Two : IInteger
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

    public class Vector<TSelf, T, TDim>
    // to force using integer values for dimensions
    where TDim: IInteger, new()
    // to restrict TSelf to be of type Vector
    where TSelf: Vector<TSelf, T, TDim> , new()
    {
      protected T[] Vec;
      protected int Dim;

      public Vector()
      {
        Dim = new TDim().Value;
        Vec = new T[Dim];
      }

      public Vector(params T[] values)
      {
        Dim = new TDim().Value;
        Vec = new T[Dim];

        var providedSize = values.Length;

        for (int i = 0 , max = Math.Min(Dim, providedSize); i < max; i++)
        {
          Vec[i] = values[i];
        }
      }

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

      // will removed

      public T X
      {
        get => Vec[0];
        set => Vec[0] = value;
      }

    }

    #endregion

    #region Vector Typed Bases

    public class VectorOfInt<TDim> : Vector<VectorOfInt<TDim>, int, TDim>
      where TDim : IInteger, new()
    {
      public VectorOfInt()
      {
      }

      public VectorOfInt(params int[] values) : base(values)
      {
      }

      public static VectorOfInt<TDim> operator + (VectorOfInt<TDim> lhs, VectorOfInt<TDim> rhs)
      {
        var result = new VectorOfInt<TDim>();
        for (int i = 0; i < result.Dim; i++)
        {
          result[i] = lhs[i] + rhs[i];
        }

        return result;
      }
    }

    #endregion

    #region Vector Type Cpacity Bases

    public class Vector2I : VectorOfInt<Dimensions.Two>
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

    public class Vector3I : VectorOfInt<Dimensions.Two>
    {
      public Vector3I()
      {
      }

      public Vector3I(int x, int y, int z) : base(new []{x, y, z})
      {
      }
    }

    #endregion
    public static class GenericValueAdapter
    {
      public static void Run()
      {
        var vec2I = new Vector2I(2,54);
        var vec2I2 = new Vector2I(4,6);
        var vec3 = vec2I + vec2I2;
        Console.WriteLine();

      }
    }
  }
}
