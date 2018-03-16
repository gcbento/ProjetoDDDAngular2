import { Component, OnInit, ViewChild } from '@angular/core';

import { ModalDirective } from 'ng2-bootstrap/ng2-bootstrap';
import * as moment from 'moment';

import { ApiService } from './../../services/api.service';
import { SettingsService } from './../../services/settings.service';
import { GenericResult, GenericSimpleResult } from './../../models/models';
import { Saldo, SaldoConta, SaldoFilter, Estatisticas } from './../saldo';
import { Conta } from './../../conta/conta';
import { ItemVenda } from './../../venda/venda';

@Component({
  selector: 'app-saldo-contas',
  templateUrl: './saldo-contas.component.html',
  styleUrls: ['./saldo-contas.component.css']
})
export class SaldoContasComponent implements OnInit {

  constructor(
    private _apiService: ApiService,
    private _settService: SettingsService,
    public conta: Conta,
    public estatisticas: Estatisticas,
    public saldoContas: SaldoConta,
    public filter: SaldoFilter
  ) { }

  @ViewChild('detalhesVendasModal') public detalhesVendasModal: ModalDirective;

  public title: string = "Contas";
  private _baseUrl = "/saldo/";
  public isPanelVendasCollapsed: boolean = true;
  public itensVenda: Array<ItemVenda> = [];

  ngOnInit() {
    this._apiService.list(this._baseUrl + "ListSaldoContas", this.filter).subscribe(res => this.saldoContas = res.result);
  }

  showDetalhesVendasModal(conta: Conta): void {

    this.estatisticas = new Estatisticas();
    this._apiService.get("/venda/GetItensVendaByContaId?contaId=" + conta.id).subscribe(res => {

      res.result.forEach(item => {
        if (item.tipoVenda.id == 1) {
          this.estatisticas.valorVendas = this.estatisticas.valorVendas + item.valor;
          this.estatisticas.qtdeVendas = this.estatisticas.qtdeVendas + 1;
        }
        else if (item.tipoVenda.id == 2) {
          this.estatisticas.valorAlugueis = this.estatisticas.valorAlugueis + item.valor;
          this.estatisticas.qtdeAlugueis = this.estatisticas.qtdeAlugueis + 1;
        };
      });

      this.itensVenda = res.result;
    });

    this.conta = conta;
    this.detalhesVendasModal.show();
  }

  hideModal(): void {
    this.detalhesVendasModal.hide();
    this.estatisticas = new Estatisticas();
  }

  getClassSaldo(val) {
    return this._settService.getClassSaldo(val);
  }

  pesquisa() {
    this._apiService.list(this._baseUrl + "ListSaldoContas", this.filter).subscribe(res => this.saldoContas = res.result);
  }

}

