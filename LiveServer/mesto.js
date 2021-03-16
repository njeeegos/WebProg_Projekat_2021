export class Mesto
{
    constructor(broj)
    {
        this.broj=broj;
        this.vozilo=null;
    }

    crtaj(host)
    {
        const mesto_div=document.createElement("div");
        mesto_div.className="mesto_div";
        mesto_div.innerHTML=this.broj;
        host.appendChild(mesto_div);

        mesto_div.onclick=(ev)=> {
            if(this.vozilo==null)
            {
                alert("Ovo mesto je prazno!");
            }
            else
            {
                alert("Marka: "+this.vozilo.marka+"\nModel: "+this.vozilo.model+"\nGodiste: "+
                this.vozilo.godiste+"\nTip karoserije: "+this.vozilo.tip+"\nBoja: "+this.vozilo.boja);
            }
        }
    }

    isprazniMesto()
    {
        this.vozilo=null;
    }

    izmeniMesto(vozilo)
    {
        this.vozilo=vozilo;
    }
}