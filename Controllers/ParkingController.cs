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

        [HttpDelete]
        [Route("ObrisiParking/{imeParkinga}")]
        public async Task<IActionResult> ObrisiParking(string imeParkinga)
        {
            var parking= Context.Parkinzi.Where(p => p.Ime==imeParkinga).FirstOrDefault();
            if(parking!=null)
            {
                Context.RemoveRange(Context.Mesta.Where(m => m.Parking.ParkingId==parking.ParkingId));
                Context.Remove(parking);
                await Context.SaveChangesAsync();
                return Ok();
            }   
            else
            {
                return StatusCode(406);
            }
        }

        [HttpGet]
        [Route("PribaviParkinge")]
        public async Task<List<Parking>> PribaviParkinge()
        {
            return await Context.Parkinzi.Include("Mesta.Vozilo").ToListAsync();
        }

        [HttpPut]
        [Route("ParkirajVozilo/{idParkinga}/{brojMesta}")]
        public async Task<IActionResult> ParkirajVozilo(int idParkinga, int brojMesta, [FromBody] Vozilo vozilo)
        {
            var mesto=Context.Mesta.Where(m => m.Parking.ParkingId==idParkinga && m.Broj==brojMesta).FirstOrDefault();
            if(mesto!=null)
            {
                Context.Vozila.Add(vozilo);
                await Context.SaveChangesAsync();

                mesto.Vozilo=vozilo;
                Context.Update<Mesto>(mesto);
                await Context.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return StatusCode(406);
            }
        }

        [HttpDelete]
        [Route("IsparkirajVozilo/{idParkinga}/{brojMesta}")]
        public async Task<IActionResult> IsparkirajVozilo(int idParkinga, int brojMesta)
        {
            var mesto=Context.Mesta.Where(m => m.Parking.ParkingId==idParkinga && m.Broj==brojMesta)
                                    .Include(m => m.Vozilo).FirstOrDefault();
            if(mesto!=null)
            {
                var vozilo=mesto.Vozilo;
                Context.Remove(vozilo);
                await Context.SaveChangesAsync();

                return Ok();
            }   
            else
            {
                return StatusCode(406);
            }
        }

        [HttpPut]
        [Route("PromeniVozilo/{idVozila}")]
        public async Task<IActionResult> PromeniVozilo(int idVozila, [FromBody] Vozilo novo_vozilo)
        {
            var vozilo=await Context.Vozila.FindAsync(idVozila);
            
            if(vozilo!=null)
            {
                vozilo.Marka=novo_vozilo.Marka;
                vozilo.Model=novo_vozilo.Model;
                vozilo.Godiste=novo_vozilo.Godiste;
                vozilo.Tip=novo_vozilo.Tip;
                vozilo.Boja=novo_vozilo.Boja;
                Context.Update<Vozilo>(vozilo);
                await Context.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return StatusCode(406);
            }
        }
    }
}
