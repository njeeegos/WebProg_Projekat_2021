import { Mesto } from "./mesto.js";
import {Parking} from "./parking.js"
import { Vozilo } from "./vozilo.js";

//const parking = new Parking(5, 5, "Parking");
//parking.crtaj(document.body);

fetch("https://localhost:5001/Parking/PribaviParkinge", {
    method:"GET"
}).then(p => p.json().then(data => {
    data.forEach(parking => {
        const novi_parking=new Parking(parking.parkingId, parking.x, parking.y, parking.ime);
        var parking_mesta=new Array();
        var vozilo=null, i=0;
        parking.mesta.forEach(m => {
            if(m.vozilo!=null)
            {
                vozilo=new Vozilo(m.vozilo.marka, m.vozilo.model, m.vozilo.godiste, m.vozilo.tip, m.vozilo.boja);
            }

            parking_mesta[i]=new Mesto(m.mestoId, m.broj, vozilo)
            i++;
            vozilo=null;
        });

        novi_parking.lista_mesta=parking_mesta;
        novi_parking.crtaj(document.body);
    });
}));