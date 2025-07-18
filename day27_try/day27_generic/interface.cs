using System;
using System.Collections.Generic;
namespace trying
{
  public interface IRepo<T>
  {
    void Add(T item);
    void Remove(T item);
    List<T> GetList();
  }
}