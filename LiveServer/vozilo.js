export class Vozilo
{
    constructor(id, marka, model, godiste, tip, boja)
    {
        this.id=id;
        this.marka=marka;
        this.model=model;
        this.godiste=godiste;
        this.tip=tip;
        this.boja=boja;
    }

    izmeniVozilo(marka, model, godiste, tip, boja)
    {
        this.marka=marka;
        this.model=model;
        this.godiste=godiste;
        this.tip=tip;
        this.boja=boja;
    }
}