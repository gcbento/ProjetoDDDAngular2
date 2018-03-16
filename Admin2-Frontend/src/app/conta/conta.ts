import * as moment from 'moment';

import { Compra } from '../compra/compra';

export class TipoConta {
    public id: number;
    public descricao: string;
}

export class CodigosConta {
    public id: number;
    public descricao: string;
    public status: boolean;
    public contaId: number;
}

export class Conta {

    constructor() {
        this.tipoConta = new TipoConta();
        this.jogos = new Array<Compra>();
        this.codigos = new Array<CodigosConta>();
    }

    public id: number;
    public email: string;
    public senha: string;
    public idOnline: string;
    public tipoConta: TipoConta;
    public dataNascimento: moment.Moment;
    public status: boolean;
    public ativa: boolean;
    public dataDesativacao: moment.Moment;

    public jogos: Array<Compra>;
    public codigos: Array<CodigosConta>;
}

export class JogosConta {
    public id: number;
    public contaId: number;
    public jogoId: number;
}

export class ContaFilter {
    public text: string;
}