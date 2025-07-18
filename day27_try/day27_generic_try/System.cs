namespace booking
{
  class BookingSystem<B> : IBooking<B>
{
  public List<B> bookings = new List<B>();

  public void AddBooking(B booking)
  {
    bookings.Add(booking);
  }
  public List<B> Display()
  {
    return bookings;
  }
}
}