import { Component, OnInit, ViewChild } from '@angular/core';

import { ModalDirective } from 'ng2-bootstrap/ng2-bootstrap';
import * as moment from 'moment';

import { ApiService } from './../../services/api.service';
import { SettingsService } from './../../services/settings.service';
import { GenericResult, GenericSimpleResult } from './../../models/models';
import { Saldo, SaldoConta, SaldoFilter, Estatisticas, SaldoMensal } from './../saldo';
import { Conta } from './../../conta/conta';
import { ItemVenda } from './../../venda/venda';

@Component({
  selector: 'app-saldo-valores',
  templateUrl: './saldo-valores.component.html',
  styleUrls: ['./saldo-valores.component.css']
})
export class SaldoValoresComponent implements OnInit {

  constructor(
    private _apiService: ApiService,
    private _settService: SettingsService,
    public estatisticas: Estatisticas,
    public filter: SaldoFilter) {

    this._apiService.get(this._baseUrl + "LisSaldoMensal?ano=2017").subscribe(res => {

      this.saldoMensal = res.result;
      this.meses = res.result.map(s => s.mes);
      this.ano = moment().year();

      this.options = {
        chart: { type: 'column' },
        title: {
          text: this.ano
        },
        xAxis: {
          categories: res.result.map(s => s.mes)
        },
        credits: {
          enabled: false
        },
        series: [{
          name: 'Lucro',
          data: res.result.map(s => s.saldo)
        }]
      };
    });
  };

  @ViewChild('detalhesSaldoMensalModal') public detalhesSaldoMensalModal: ModalDirective;

  public options: any = {};
  public title: string = "Valores";
  private _baseUrl = "/saldo/";
  public isPanelVendasCollapsed: boolean = true;
  public graficoCategorias: Array<any> = [];
  public graficoValores: Array<number> = [];
  public saldoMensal: Array<Estatisticas> = [];
  public vendasMensal: Array<ItemVenda> = [];
  public meses: Array<string> = [];
  public ano: number = 0;

  ngOnInit() {
    this._apiService.list(this._baseUrl + "GetEstatisticas", this.filter).subscribe(res => {
      this.estatisticas = res.result;
    });
  }

  showDetalhesSaldoMensalModal(mes: string): void {

    var numeroMes = moment().month(mes).format("M");

    this._apiService.get('/venda/GetItensByMes?ano=' + this.ano + '&mes=' + numeroMes).subscribe(res => {
      this.vendasMensal = res.result;
    })

    this.detalhesSaldoMensalModal.show();
  }

  hideModal(): void {
    this.detalhesSaldoMensalModal.hide();
  }

  getClassSaldo(val) {
    return this._settService.getClassSaldo(val);
  }

  pesquisa(ano) {
    this._apiService.get(this._baseUrl + "LisSaldoMensal?ano=" + ano).subscribe(res => {

      this.ano = ano;
      this.saldoMensal = res.result;
      this.meses = res.result.map(s => s.mes);

      this.options = {
        title: {
          text: ano
        },
        xAxis: {
          categories: res.result.map(s => s.mes)
        },
        credits: {
          enabled: false
        },
        series: [{
          name: 'Lucro',
          data: res.result.map(s => s.saldo)
        }]
      };
    });
  }

}
