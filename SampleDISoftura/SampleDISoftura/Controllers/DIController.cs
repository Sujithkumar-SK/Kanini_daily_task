using Microsoft.AspNetCore.Mvc;
using SampleDISoftura.Interface;
using SampleDISoftura.Models;

namespace SampleDISoftura.Controllers
{
    public class DIController : Controller
    {
        private readonly ISingletonService _singleton;
        private readonly IScoped _scoped;
        private readonly ITransient _transient;

        public DIController(ISingletonService singleton, IScoped scoped, ITransient transient)
        {
            _singleton = singleton;
            _scoped = scoped;
            _transient = transient;
        }
        //Atribute Routing
        [Route("GetDI")]
        public IActionResult Index()
        {
            var model = new DIModel();
            model.SingletonTime = _singleton.Time;
            model.ScopedTime= _scoped.Time;
            model.TransientTime= _transient.Time;
            return View(model);
        }
    }
}
