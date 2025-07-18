namespace GenericINterfacedemokanini
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Hotel ht = new Hotel();
            ht.AddRoom(new RoomAC() { RoomId = 111, Category = "Classic", Price = 4000 });
     
            ht.AddRoom(new RoomAC() { RoomId = 113, Category = "Premium", Price = 5000 });

            ht.AddRoom(new RoomNonAC() { RoomId = 101, Category = "Classic", Price = 2500 });
            ht.AddRoom(new RoomNonAC() { RoomId = 102, Category = "Premium", Price = 3500 });


          IList<RoomAC> acrooms =  ht.GetAllRooms();

            foreach (RoomAC room in acrooms)
            {
                Console.WriteLine(room.Category + " " + room.Price + " " + room.RoomId);
            }



            IList<RoomNonAC> nonacrooms = ((IRoom<RoomNonAC,int>) ht).GetAllRooms();

            foreach (RoomNonAC room in nonacrooms)
            {
                Console.WriteLine(room.Category + " " + room.Price + " " + room.RoomId);
            }

           RoomAC rm = ht.DeleteRoom(115);
            Console.WriteLine($"{rm.RoomId} got deleted");


        }
    }
}