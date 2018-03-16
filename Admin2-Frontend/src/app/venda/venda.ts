import * as moment from 'moment';

import { Cliente } from '../cliente/cliente';
import { Conta, TipoConta } from '../conta/conta';

export class Venda {

    constructor(){
        this.cliente = new Cliente();
        this.itensVenda = new Array<ItemVenda>();
    }

    public id: number;
    public valor: number;
    public cliente: Cliente;
    public dataVenda: moment.Moment;

    public itensVenda: Array<ItemVenda>;
}

export class ItemVenda {

    constructor(){
        this.venda = new Venda();
        this.conta = new Conta();
        this.tipoVenda = new TipoVenda();
        this.tipoConta = new TipoConta();
    }

    public id: number;
    public venda: Venda;
    public conta: Conta;
    public tipoVenda: TipoVenda;
    public valor: number;
    public tipoConta: TipoConta;
    public qtdeDias: number;
    public inicioLocacao: moment.Moment;
    public fimLocacao: moment.Moment;
    public diasGratis: boolean;
    public senha: string;
    public tipoPeriodo: string;
}

export class TipoVenda{
    public id: number;
    public descricao: string;
}

export class VendaFilter{
    public text: string;
    public locacoes: boolean;
}
