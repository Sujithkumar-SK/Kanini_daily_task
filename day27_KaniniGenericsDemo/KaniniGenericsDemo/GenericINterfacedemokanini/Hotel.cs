using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericINterfacedemokanini
{
    internal class Hotel : IRoom<RoomAC,int>, IRoom<RoomNonAC,int>
    {
        List<RoomAC> acrooms = new List<RoomAC>();
        
        List<RoomNonAC> nonacrroooms = new List<RoomNonAC>();
        
        public RoomAC AddRoom(RoomAC room)
        {
            acrooms.Add(room);
            return room;
        }

        public RoomNonAC AddRoom(RoomNonAC room)
        {
            nonacrroooms.Add(room);
            return room;
        }

        public RoomAC DeleteRoom(int roomid)
        {
            RoomAC room = acrooms.FirstOrDefault(x=>x.RoomId== roomid);//fetching the room having same roomid as that of the parameter
            acrooms.Remove(room);
            return room;
        }

        public IList<RoomAC> GetAllRooms()
        {
            return acrooms;
        }

        RoomNonAC IRoom<RoomNonAC, int>.DeleteRoom(int item)
        {
            RoomNonAC rm =  nonacrroooms.FirstOrDefault(x => x.RoomId == item);//fetching the room having same roomid as that of the parameter
            nonacrroooms.Remove(rm);
            return rm;
        }

        IList<RoomNonAC> IRoom<RoomNonAC,int>.GetAllRooms()
        {
            return nonacrroooms;
        }

    }
}
