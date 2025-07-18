namespace booking
{
  interface IBooking<B>
{
  void AddBooking(B item);
  List<B> Display();
}
}
