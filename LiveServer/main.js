import { Mesto } from "./mesto.js";
import {Parking} from "./parking.js"
import { Vozilo } from "./vozilo.js";

function DodavanjeParkinga()
{
    const container=document.createElement("div");
    container.className="container_div";
    document.body.appendChild(container);

    const ime_div=document.createElement("div");
    ime_div.className="ime_div";
    container.appendChild(ime_div);

    const ime_lab=document.createElement("label");
    ime_lab.innerHTML="Ime parkinga: ";
    ime_div.appendChild(ime_lab);

    const ime_input=document.createElement("input");
    ime_input.type="text";
    ime_input.className="ime_input";
    ime_div.appendChild(ime_input);

    const x_div=document.createElement("div");
    x_div.className="xx_div";
    container.appendChild(x_div);

    const x_lab=document.createElement("label");
    x_lab.innerHTML="Unesite X: ";
    x_div.appendChild(x_lab);

    const x_input=document.createElement("input");
    x_input.type="number";
    x_input.className="x_input";
    x_div.appendChild(x_input);

    const y_div=document.createElement("div");
    y_div.className="yy_div";
    container.appendChild(y_div);

    const y_lab=document.createElement("label");
    y_lab.innerHTML="Unesite Y: ";
    y_div.appendChild(y_lab);

    const y_input=document.createElement("input");
    y_input.type="number";
    y_input.className="y_input";
    y_div.appendChild(y_input);

    const submit_dugme=document.createElement("button");
    submit_dugme.innerHTML="Napravi parking";
    container.appendChild(submit_dugme);

    submit_dugme.onclick=(ev)=> {
        const uneto_ime=container.querySelector(".ime_input").value;
        const uneto_x=parseInt(container.querySelector(".x_input").value);
        const uneto_y=parseInt(container.querySelector(".y_input").value);

        fetch("https://localhost:5001/Parking/KreirajParking", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                "ime": uneto_ime,
                "x": uneto_x,
                "y": uneto_y
            })
        }).then(document.location.reload());
    }

    // const brisi_dugme=document.createElement("button");
    // brisi_dugme.innerHTML="Obrisi parking";
    // container.appendChild(brisi_dugme);    

    // brisi_dugme.onclick=(ev)=> {
    //     const uneto_ime=container.querySelector(".ime_input").value;

    //     fetch("https://localhost:5001/Parking/ObrisiParking/" + uneto_ime, {
    //         method: "DELETE"
    //     }).then(setTimeout(document.location.reload(), 2000));
    // }
}

DodavanjeParkinga();

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