import {Vozilo} from "./vozilo.js"
import {Mesto} from "./mesto.js"

export class Parking
{
    constructor(id, x, y, ime)
    {
        this.id=id;
        this.x=x;
        this.y=y;
        this.ime=ime;
        this.lista_mesta=new Array(x*y);
    }

    crtaj(host)
    {
        this.container=document.createElement("div");
        this.container.className="container";
        host.appendChild(this.container);
        this.crtajFormu(this.container);
    }

    crtajFormu(host)
    {
        host.innerHTML=" ";

        const forma_div=document.createElement("div");
        forma_div.className="forma_div";
        host.appendChild(forma_div);

        const ime_lab=document.createElement("label");
        ime_lab.innerHTML="\""+this.ime+"\"";
        ime_lab.className="ime_lab";
        forma_div.appendChild(ime_lab);

        //deo forme za unos marke vozila
        const unos_marke_div=document.createElement("div");
        unos_marke_div.className="unos_marke_div";
        forma_div.appendChild(unos_marke_div);

        const marka_lab=document.createElement("label");
        marka_lab.innerHTML="Marka: ";
        unos_marke_div.appendChild(marka_lab);

        const marka_input=document.createElement("input");
        marka_input.type="text";
        marka_input.className="marka_input";
        unos_marke_div.appendChild(marka_input);

        //deo forme za unos modela vozila
        const unos_model_div=document.createElement("div");
        unos_model_div.className="unos_model_div";
        forma_div.appendChild(unos_model_div);

        const model_lab=document.createElement("label");
        model_lab.innerHTML="Model: ";
        unos_model_div.appendChild(model_lab);

        const model_input=document.createElement("input");
        model_input.type="text";
        model_input.className="model_input";
        unos_model_div.appendChild(model_input);

        //deo forme za unos godista vozila
        const unos_godista_div=document.createElement("div");
        unos_godista_div.className="unos_godista_div";
        forma_div.appendChild(unos_godista_div);

        const godiste_lab=document.createElement("label");
        godiste_lab.innerHTML="Godiste: ";
        unos_godista_div.appendChild(godiste_lab);

        const godiste_input=document.createElement("input");
        godiste_input.type="number";
        godiste_input.className="godiste_input";
        unos_godista_div.appendChild(godiste_input); 

        //deo forme za unos tipa vozila (karoserija)
        const unos_tipa_div=document.createElement("div");
        unos_tipa_div.className="unos_tipa_div";
        forma_div.appendChild(unos_tipa_div);

        const labela=document.createElement("label");
        labela.innerHTML="Tip karoserije: ";
        unos_tipa_div.appendChild(labela);

        let tip_lab, tip_input;
        const tipovi_karoserije=["Limuzina", "Hecbek", "Karavan", "Kupe", "Dzip", "Pickup", "Kabriolet"];
        tipovi_karoserije.forEach(tip => {
            tip_input=document.createElement("input");
            tip_input.type="radio";
            tip_input.name="karoserija";
            tip_input.value=tip;
            unos_tipa_div.appendChild(tip_input);

            tip_lab=document.createElement("label");
            tip_lab.innerHTML=tip;
            unos_tipa_div.appendChild(tip_lab);
        })

        //deo forme za unos boje vozila
        const unos_boje_div=document.createElement("div");
        unos_boje_div.className="unos_boje_div";
        forma_div.appendChild(unos_boje_div);

        const boja_lab=document.createElement("label");
        boja_lab.innerHTML="Boja: ";
        unos_boje_div.appendChild(boja_lab);

        const boja_input=document.createElement("input");
        boja_input.type="text";
        boja_input.className="boja_input";
        unos_boje_div.appendChild(boja_input);

        //deo forme za unos broja parking mesta
        const unos_broja_mesta_div=document.createElement("div");
        unos_broja_mesta_div.className="unos_broja_mesta_div";
        forma_div.appendChild(unos_broja_mesta_div);

        const broj_mesta_lab=document.createElement("label");
        broj_mesta_lab.innerHTML="Broj mesta: ";
        unos_broja_mesta_div.appendChild(broj_mesta_lab);

        const broj_mesta_input=document.createElement("input");
        broj_mesta_input.type="number";
        broj_mesta_input.className="broj_mesta_input";
        unos_broja_mesta_div.appendChild(broj_mesta_input); 

        //dugme submit
        const submit_dugme=document.createElement("button");
        submit_dugme.innerHTML="Parkiraj";
        forma_div.appendChild(submit_dugme);

        //dugme isparkiraj
        const isparkiraj_dugme=document.createElement("button");
        isparkiraj_dugme.innerHTML="Isparkiraj";
        forma_div.appendChild(isparkiraj_dugme);
        
        submit_dugme.onclick=(ev)=> {
            this.parkirajVozilo(host);
        }

        isparkiraj_dugme.onclick=(ev)=> {
            this.isparkirajVozilo(host);
        }
        
        this.crtajParking(host);
    }

    crtajParking(host)
    {
        const parking_div=document.createElement("div");
        parking_div.className="parking_div";
        host.appendChild(parking_div);

        let x_div, mesto;
        for(let i=0;i<this.x;i++)
        {
            x_div=document.createElement("div");
            x_div.className="x_div";
            for(let j=0;j<this.y;j++)
            {
                mesto=this.lista_mesta[j+(this.y*i)]
                mesto.crtaj(x_div);
            }
            parking_div.appendChild(x_div);
        }
    }

    parkirajVozilo(host)
    {
        const uneto_mesto=parseInt(this.container.querySelector(".broj_mesta_input").value);
        if(uneto_mesto>(this.x*this.y) || uneto_mesto<1)
        {
            alert("Uneto mesto je van opsega!")
        }
        else if(this.lista_mesta[uneto_mesto-1].vozilo!=null)
        {
            alert("Mesto je zauzeto!");
        }
        else
        {
            const uneta_marka=this.container.querySelector(".marka_input").value;
            const uneti_model=this.container.querySelector(".model_input").value;
            const uneto_godiste=parseInt(this.container.querySelector(".godiste_input").value);
            const uneti_tip=this.container.querySelector("input[name='karoserija']:checked").value;
            const uneta_boja=this.container.querySelector(".boja_input").value;

            fetch("https://localhost:5001/Parking/ParkirajVozilo/" + this.id + "/" + uneto_mesto, {
                method: "PUT",
                headers: {
                    "Accept": "application/json; charset=utf-8",
                    "Content-Type": "application/json"
                },
                body: JSON.stringify({
                    "marka": uneta_marka,
                    "model": uneti_model,
                    "godiste": uneto_godiste,
                    "tip": uneti_tip,
                    "boja": uneta_boja
                })
            }).then(p => {
                if(p.ok)
                {
                    const vozilo=new Vozilo(uneta_marka, uneti_model, uneto_godiste, uneti_tip, uneta_boja);
                    this.lista_mesta[uneto_mesto-1].izmeniMesto(vozilo);
                    this.crtajFormu(host);
                }
                else if(p.status==406)
                {
                    alert("Mesto je popunjeno!");
                }
            }).catch(p => {
                alert("Greska prilikom upisa!");
            });
        }
    }

    isparkirajVozilo(host)
    {
        const uneto_mesto=parseInt(this.container.querySelector(".broj_mesta_input").value);
        if(uneto_mesto>(this.x*this.y) || uneto_mesto<1)
        {
            alert("Uneto mesto je van opsega!")
        }
        else if(this.lista_mesta[uneto_mesto-1].vozilo==null)
        {
            alert("Mesto je vec prazno!");
        }
        else
        {
            fetch("https://localhost:5001/Parking/IsparkirajVozilo/" + this.id + "/" + uneto_mesto, {
                method: "DELETE"
            }).then(p => {
                if(p.ok) {
                    this.lista_mesta[uneto_mesto-1].isprazniMesto();
                }
                else{
                    alert("Doslo je do gresek!");
                }
            });
        }
    }
}