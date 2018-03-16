import { Conta } from '../conta/conta';

export class Saldo {
    constructor() {
        this.estatisticas = new Estatisticas();
        this.saldoContas = new Array<SaldoConta>();
    }

    public estatisticas: Estatisticas;
    public saldoContas: Array<SaldoConta>;
}

export class Estatisticas {
    constructor() {
        this.valorTotalVendas = 0;
        this.qtdeTotalVendas = 0;
        this.valorVendas = 0;
        this.valorAlugueis = 0;
        this.qtdeVendas = 0;
        this.qtdeAlugueis = 0;
    }
    public valorTotalVendas: number;
    public qtdeTotalVendas: number;
    public qtdeVendas: number;
    public valorVendas: number
    public qtdeAlugueis: number;
    public valorAlugueis: number;
    public valorTotalCompras: number;
    public qtdeTotalCompras: number;
}

export class SaldoConta {
    constructor() {
        this.conta = new Conta();
    }

    public conta: Conta;
    public totalVendido: number;
    public saldo: number;
}

export class SaldoFilter {
    public todasContas: boolean;
    public text: string;
}

export class SaldoMensal{
    public numeroMes: number;
    public mes: string;
    public valorVendas: number;
    public valorCompras: number;
    public saldo: number;
}