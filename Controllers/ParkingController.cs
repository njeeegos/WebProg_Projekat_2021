using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebProg_Projekat_2021.Models;

namespace WebProg_Projekat_2021.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ParkingController : ControllerBase
    {
        public Context Context { get; set; }

        public ParkingController(Context context)
        {
            Context=context;
        }

    }
}
