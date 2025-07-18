using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericINterfacedemokanini
{
    //Generic INterface
    internal interface IRoom<T,K> //T is an Entity , K is a key
    {
       T AddRoom(T room);
       IList<T> GetAllRooms();

       T DeleteRoom(K item);
    }
}
