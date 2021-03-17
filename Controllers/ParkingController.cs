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
        /*
        [HttpPost]
        [Route("KreirajParking")]
        public async Task KreirajParking([FromBody] Parking p)
        {
            var proba=Context.Parkinzi.Add(p);
            await Context.SaveChangesAsync();

            //await KreirajMesta(parking.Ime);

            var parking=Context.Parkinzi.Where(prom => prom.Ime==p.Ime).FirstOrDefault();

            for(var i=0;i<(parking.X*parking.Y);i++)
            {
                var mesto=new Mesto();
                mesto.Broj=i+1;
                mesto.Vozilo=null;
                mesto.Parking=parking;
                Context.Mesta.Add(mesto);
            }
            await Context.SaveChangesAsync();


        }
        */

        [HttpPost]
        [Route("KreirajParking")]
        public async Task KreirajParking([FromBody] Parking parking)
        {
            Context.Parkinzi.Add(parking);
            await Context.SaveChangesAsync();

            for(var i=0;i<(parking.X*parking.Y);i++)
            {
                var mesto=new Mesto();
                mesto.Broj=i+1;
                mesto.Vozilo=null;
                mesto.Parking=parking;
                Context.Mesta.Add(mesto);
                await Context.SaveChangesAsync();

                parking.Mesta.Add(mesto);
            }
            
            Context.Update<Parking>(parking);
            await Context.SaveChangesAsync();
        }

        [HttpDelete]
        [Route("ObrisiParking/{idParkinga}")]
        public async Task ObrisiParking(int idParkinga)
        {
            var parking= await Context.Parkinzi.FindAsync(idParkinga);
            Context.RemoveRange(Context.Mesta.Where(m => m.Parking.ParkingId==idParkinga));
            Context.Remove(parking);
            await Context.SaveChangesAsync();
        }

        [HttpPut]
        [Route("ParkirajVozilo/{idParkinga}/{brojMesta}")]
        public async Task ParkirajVozilo(int idParkinga, int brojMesta, [FromBody] Vozilo vozilo)
        {
            Context.Vozila.Add(vozilo);
            await Context.SaveChangesAsync();

            var parking=await Context.Parkinzi.FindAsync(idParkinga);
            var mesto=parking.Mesta.Where(p => p.Broj==brojMesta).FirstOrDefault();
            mesto.Vozilo=vozilo;

            Context.Update<Mesto>(mesto);
            await Context.SaveChangesAsync();
        }

    }
}
