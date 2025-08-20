using SampleDISoftura.Interface;

namespace SampleDISoftura.Service
{
    public class TransientService : ITransient
    {
        public string Time { get; set; }

        public TransientService()
        {
            Time = DateTime.Now.ToString("HH:mm:ss.ffffff");
        }
    }
}
