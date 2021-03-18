using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebProg_Projekat_2021.Models;
using Microsoft.EntityFrameworkCore;

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

                parking.Mesta.ToList().Add(mesto);
            }

            Context.Update<Parking>(parking);
            await Context.SaveChangesAsync();
        }

        /*
        [HttpGet]
        [Route("PribaviMesta/{idParkinga}")]
        public async Task<int> PribaviMesta(int idParkinga)
        {
            var parkinzi=await Context.Parkinzi.Include(p=>p.Mesta).ToListAsync();
            var parking=parkinzi.Find(p => p.ParkingId==idParkinga);
            return parking.Mesta.Count();
        }
        */

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

            // var parkinzi=await Context.Parkinzi.Include(p=>p.Mesta).ToListAsync();
            // var parking=parkinzi.Find(p => p.ParkingId==idParkinga);
            // var mesto=parking.Mesta.Where(p => p.Broj==brojMesta).FirstOrDefault();
            // mesto.Vozilo=vozilo;

            var mesto=Context.Mesta.Where(m => m.Parking.ParkingId==idParkinga && m.Broj==brojMesta).FirstOrDefault();
            mesto.Vozilo=vozilo;

            Context.Update<Mesto>(mesto);
            await Context.SaveChangesAsync();
        }

        [HttpDelete]
        [Route("IsparkirajVozilo/{idParkinga}/{brojMesta}")]
        public async Task IsparkirajVozilo(int idParkinga, int brojMesta)
        {
            var mesto=Context.Mesta.Where(m => m.Parking.ParkingId==idParkinga && m.Broj==brojMesta)
                                    .Include(m => m.Vozilo).FirstOrDefault();
            var vozilo=mesto.Vozilo;

            Context.Remove(vozilo);
            await Context.SaveChangesAsync();
        }

        
    }
}
