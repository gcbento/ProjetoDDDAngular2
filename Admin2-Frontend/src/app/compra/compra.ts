export class Compra {
    public id: number;
    public descricao: string;
    public valor: number;
    public dataCompra: Date;
}

export class CompraFilter{
    public text: string;
    public dataInicio: Date;
    public dataFim: Date;
}