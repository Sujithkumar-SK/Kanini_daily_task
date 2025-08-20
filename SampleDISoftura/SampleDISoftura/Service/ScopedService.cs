using SampleDISoftura.Interface;

namespace SampleDISoftura.Service
{
    public class ScopedService :IScoped
    {
        public string Time { get; set; }

        public ScopedService()
        {
            Time = DateTime.Now.ToString("HH:mm:ss.ffffff");
        }
    }
}
