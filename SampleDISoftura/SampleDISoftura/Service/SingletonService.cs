using SampleDISoftura.Interface;

namespace SampleDISoftura.Service
{
    public class SingletonService : ISingletonService
    {
        public string Time { get; set; }

        public SingletonService() 
        {
            Time = DateTime.Now.ToString("HH:mm:ss.ffffff");
        }
    }
}
