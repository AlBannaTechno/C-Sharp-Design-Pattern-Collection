﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace MainProject.CreationalPatterns.Decorator
{
    namespace D2AdapterDecorator_HyperStringBuilder
    {
              /**
               * This Consider a Decorator and Adapter at the same time
               * 1- decorator : because we try to change the state of internal class without touch the class itself
               * 2- adapter   : because we apply interface requirements to allow string instantiation , + operator
               */
              class HyperStringBuilder
              {
                  private StringBuilder _builder = new StringBuilder();

                  public static implicit operator HyperStringBuilder(string s)
                  {
                      var hsb = new HyperStringBuilder();
                      hsb.Append(s);
                      return hsb;
                  }

                  public static HyperStringBuilder operator +(HyperStringBuilder hsb, string s)
                  {
                      hsb.Append(s);
                      return hsb;
                  }
                  public override string ToString()
                  {
                      return _builder.ToString();
                  }

                  #region Delgation

                  public void GetObjectData(SerializationInfo info, StreamingContext context)
                  {
                      ((ISerializable) _builder).GetObjectData(info, context);
                  }

                  public HyperStringBuilder Append(bool value)
                  {
                      _builder.Append(value);
                      return this;
                  }

                  public HyperStringBuilder Append(byte value)
                  {
                      _builder.Append(value);
                      return this;
                  }

                  public HyperStringBuilder Append(char value)
                  {
                      _builder.Append(value);
                      return this;
                  }

                  public unsafe HyperStringBuilder Append(char* value, int valueCount)
                  {
                      _builder.Append(value, valueCount);
                      return this;
                  }

                  public HyperStringBuilder Append(char value, int repeatCount)
                  {
                      _builder.Append(value, repeatCount);
                      return this;
                  }

                  public HyperStringBuilder Append(char[]? value)
                  {
                      _builder.Append(value);
                      return this;
                  }

                  public HyperStringBuilder Append(char[]? value, int startIndex, int charCount)
                  {
                      _builder.Append(value, startIndex, charCount);
                      return this;
                  }

                  public HyperStringBuilder Append(decimal value)
                  {
                      _builder.Append(value);
                      return this;
                  }

                  public HyperStringBuilder Append(double value)
                  {
                      _builder.Append(value);
                      return this;
                  }

                  public HyperStringBuilder Append(short value)
                  {
                      _builder.Append(value);
                      return this;
                  }

                  public HyperStringBuilder Append(int value)
                  {
                      _builder.Append(value);
                      return this;
                  }

                  public HyperStringBuilder Append(long value)
                  {
                      _builder.Append(value);
                      return this;
                  }

                  public HyperStringBuilder Append(object? value)
                  {
                      _builder.Append(value);
                      return this;
                  }

                  public HyperStringBuilder Append(ReadOnlyMemory<char> value)
                  {
                      _builder.Append(value);
                      return this;
                  }

                  public HyperStringBuilder Append(ReadOnlySpan<char> value)
                  {
                      _builder.Append(value);
                      return this;
                  }

                  public HyperStringBuilder Append(sbyte value)
                  {
                      _builder.Append(value);
                      return this;
                  }

                  public HyperStringBuilder Append(float value)
                  {
                      _builder.Append(value);
                      return this;
                  }

                  public HyperStringBuilder Append(string? value)
                  {
                      _builder.Append(value);
                      return this;
                  }

                  public HyperStringBuilder Append(string? value, int startIndex, int count)
                  {
                      _builder.Append(value, startIndex, count);
                      return this;
                  }

                  public HyperStringBuilder Append(HyperStringBuilder? value)
                  {
                      _builder.Append(value);
                      return this;
                  }

                  public HyperStringBuilder Append(HyperStringBuilder? value, int startIndex, int count)
                  {
                      _builder.Append(value._builder, startIndex, count);
                      return this;
                  }

                  public HyperStringBuilder Append(ushort value)
                  {
                      _builder.Append(value);
                      return this;
                  }

                  public HyperStringBuilder Append(uint value)
                  {
                      _builder.Append(value);
                      return this;
                  }

                  public HyperStringBuilder Append(ulong value)
                  {
                      _builder.Append(value);
                      return this;
                  }

                  public HyperStringBuilder AppendFormat(IFormatProvider? provider, string format, object? arg0)
                  {
                      _builder.AppendFormat(provider, format, arg0);
                      return this;
                  }

                  public HyperStringBuilder AppendFormat(IFormatProvider? provider, string format, object? arg0,
                      object? arg1)
                  {
                      _builder.AppendFormat(provider, format, arg0, arg1);
                      return this;
                  }

                  public HyperStringBuilder AppendFormat(IFormatProvider? provider, string format, object? arg0,
                      object? arg1, object? arg2)
                  {
                      _builder.AppendFormat(provider, format, arg0, arg1, arg2);
                      return this;
                  }

                  public HyperStringBuilder AppendFormat(IFormatProvider? provider, string format, params object?[] args)
                  {
                      _builder.AppendFormat(provider, format, args);
                      return this;
                  }

                  public HyperStringBuilder AppendFormat(string format, object? arg0)
                  {
                      _builder.AppendFormat(format, arg0);
                      return this;
                  }

                  public HyperStringBuilder AppendFormat(string format, object? arg0, object? arg1)
                  {
                      _builder.AppendFormat(format, arg0, arg1);
                      return this;
                  }

                  public HyperStringBuilder AppendFormat(string format, object? arg0, object? arg1, object? arg2)
                  {
                      _builder.AppendFormat(format, arg0, arg1, arg2);
                      return this;
                  }

                  public HyperStringBuilder AppendFormat(string format, params object?[] args)
                  {
                      _builder.AppendFormat(format, args);
                      return this;
                  }

                  public HyperStringBuilder AppendJoin(char separator, params object?[] values)
                  {
                      _builder.AppendJoin(separator, values);
                      return this;
                  }

                  public HyperStringBuilder AppendJoin(char separator, params string?[] values)
                  {
                      _builder.AppendJoin(separator, values);
                      return this;
                  }

                  public HyperStringBuilder AppendJoin(string? separator, params object?[] values)
                  {
                      _builder.AppendJoin(separator, values);
                      return this;
                  }

                  public HyperStringBuilder AppendJoin(string? separator, params string?[] values)
                  {
                      _builder.AppendJoin(separator, values);
                      return this;
                  }

                  public HyperStringBuilder AppendJoin<T>(char separator, IEnumerable<T> values)
                  {
                      _builder.AppendJoin(separator, values);
                      return this;
                  }

                  public HyperStringBuilder AppendJoin<T>(string? separator, IEnumerable<T> values)
                  {
                      _builder.AppendJoin(separator, values);
                      return this;
                  }

                  public HyperStringBuilder AppendLine()
                  {
                      _builder.AppendLine();
                      return this;
                  }

                  public HyperStringBuilder AppendLine(string? value)
                  {
                      _builder.AppendLine(value);
                      return this;
                  }

                  public HyperStringBuilder Clear()
                  {
                      _builder.Clear();
                      return this;
                  }

                  public void CopyTo(int sourceIndex, char[] destination, int destinationIndex, int count)
                  {
                      _builder.CopyTo(sourceIndex, destination, destinationIndex, count);
                  }

                  public void CopyTo(int sourceIndex, Span<char> destination, int count)
                  {
                      _builder.CopyTo(sourceIndex, destination, count);
                  }

                  public int EnsureCapacity(int capacity)
                  {
                      return _builder.EnsureCapacity(capacity);
                  }

                  public bool Equals(ReadOnlySpan<char> span)
                  {
                      return _builder.Equals(span);
                  }

                  public bool Equals(HyperStringBuilder? sb)
                  {
                      return _builder.Equals(sb);
                  }

                  public StringBuilder.ChunkEnumerator GetChunks()
                  {
                      return _builder.GetChunks();
                  }

                  public HyperStringBuilder Insert(int index, bool value)
                  {
                      _builder.Insert(index, value);
                      return this;
                  }

                  public HyperStringBuilder Insert(int index, byte value)
                  {
                      _builder.Insert(index, value);
                      return this;
                  }

                  public HyperStringBuilder Insert(int index, char value)
                  {
                      _builder.Insert(index, value);
                      return this;
                  }

                  public HyperStringBuilder Insert(int index, char[]? value)
                  {
                      _builder.Insert(index, value);
                      return this;
                  }

                  public HyperStringBuilder Insert(int index, char[]? value, int startIndex, int charCount)
                  {
                      _builder.Insert(index, value, startIndex, charCount);
                      return this;
                  }

                  public HyperStringBuilder Insert(int index, decimal value)
                  {
                      _builder.Insert(index, value);
                      return this;
                  }

                  public HyperStringBuilder Insert(int index, double value)
                  {
                      _builder.Insert(index, value);
                      return this;
                  }

                  public HyperStringBuilder Insert(int index, short value)
                  {
                      _builder.Insert(index, value);
                      return this;
                  }

                  public HyperStringBuilder Insert(int index, int value)
                  {
                      _builder.Insert(index, value);
                      return this;
                  }

                  public HyperStringBuilder Insert(int index, long value)
                  {
                      _builder.Insert(index, value);
                      return this;
                  }

                  public HyperStringBuilder Insert(int index, object? value)
                  {
                      _builder.Insert(index, value);
                      return this;
                  }

                  public HyperStringBuilder Insert(int index, ReadOnlySpan<char> value)
                  {
                      _builder.Insert(index, value);
                      return this;
                  }

                  public HyperStringBuilder Insert(int index, sbyte value)
                  {
                      _builder.Insert(index, value);
                      return this;
                  }

                  public HyperStringBuilder Insert(int index, float value)
                  {
                      _builder.Insert(index, value);
                      return this;
                  }

                  public HyperStringBuilder Insert(int index, string? value)
                  {
                      _builder.Insert(index, value);
                      return this;
                  }

                  public HyperStringBuilder Insert(int index, string? value, int count)
                  {
                      _builder.Insert(index, value, count);
                      return this;
                  }

                  public HyperStringBuilder Insert(int index, ushort value)
                  {
                      _builder.Insert(index, value);
                      return this;
                  }

                  public HyperStringBuilder Insert(int index, uint value)
                  {
                      _builder.Insert(index, value);
                      return this;
                  }

                  public HyperStringBuilder Insert(int index, ulong value)
                  {
                      _builder.Insert(index, value);
                      return this;
                  }

                  public HyperStringBuilder Remove(int startIndex, int length)
                  {
                      _builder.Remove(startIndex, length);
                      return this;
                  }

                  public HyperStringBuilder Replace(char oldChar, char newChar)
                  {
                      _builder.Replace(oldChar, newChar);
                      return this;
                  }

                  public HyperStringBuilder Replace(char oldChar, char newChar, int startIndex, int count)
                  {
                      _builder.Replace(oldChar, newChar, startIndex, count);
                      return this;
                  }

                  public HyperStringBuilder Replace(string oldValue, string? newValue)
                  {
                      _builder.Replace(oldValue, newValue);
                      return this;
                  }

                  public HyperStringBuilder Replace(string oldValue, string? newValue, int startIndex, int count)
                  {
                      _builder.Replace(oldValue, newValue, startIndex, count);
                      return this;
                  }

                  public string ToString(int startIndex, int length)
                  {
                      return _builder.ToString(startIndex, length);
                  }

                  public int Capacity
                  {
                      get => _builder.Capacity;
                      set => _builder.Capacity = value;
                  }

                  public char this[int index]
                  {
                      get => _builder[index];
                      set => _builder[index] = value;
                  }

                  public int Length
                  {
                      get => _builder.Length;
                      set => _builder.Length = value;
                  }

                  public int MaxCapacity => _builder.MaxCapacity;

                  #endregion

              }

        public static class D2AdapterDecoratorHyperStringBuilder
        {
            public static void Run()
            {
                HyperStringBuilder hsb = "osama";
                hsb += " al banna";
                Console.WriteLine(hsb);
            }
        }
    }
}