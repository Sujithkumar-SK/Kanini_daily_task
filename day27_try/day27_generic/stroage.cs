using System;
using System.Collections.Generic;
namespace trying
{
  public class Storage<T>
  {
    private T value;

    public void gdisplay(T va)
    {
      value = va;
    }
    public T pdisplay()
    {
      return value;
    }
  }
}