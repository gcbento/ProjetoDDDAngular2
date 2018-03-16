import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

import { CustomValidators } from 'ng2-validation';
import { ModalDirective } from 'ng2-bootstrap/ng2-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import * as moment from 'moment';

import { SettingsService } from './../services/settings.service';
import { ApiService } from './../services/api.service';
import { GenericResult, GenericSimpleResult, SelectModel } from './../models/models';
import { Home } from './home';
import { Venda, ItemVenda, TipoVenda, } from '../venda/venda';
import { Conta, TipoConta } from '../conta/conta';
import { Estatisticas } from './../saldo/saldo';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor(
    private _settService: SettingsService,
    private _apiService: ApiService,
    private _notifcations: NotificationsService,
    private _fb: FormBuilder,
    public venda: Venda,
    public itemVenda: ItemVenda,
    public conta: Conta
  ) {
    this.vendaForm = _fb.group({
      'dataVenda': [null, Validators.required],
      'selectClientes': [null],
      'diasGratis': [null],
      'valor': [null, Validators.compose([Validators.required, CustomValidators.number])],
      'qtdeDias': [null, Validators.compose([CustomValidators.number])],
      'cliente': [null],
      'email': [null],
      'senha': [null]
    });


  }

  @ViewChild('locacaoRapidaModal') public locacaoRapidaModal: ModalDirective;
  @ViewChild('estenderLocacaoModal') public estenderLocacaoModal: ModalDirective;
  @ViewChild('finalizarLocacaoModal') public finalizarLocacaoModal: ModalDirective;
  @ViewChild('confirmarDesativacaoModal') public confirmarDesativacaoModal: ModalDirective;

  vendaForm: FormGroup;
  fimLocacoes: Array<ItemVenda> = [];
  contasDisponiveis: Array<ItemVenda> = [];
  contasDesativar: Array<Conta> = [];
  clienteOptions: Array<SelectModel> = [];
  saldoMes: Array<Estatisticas> = [];
  isAddCliente: boolean = true;

  ngOnInit() {
    this._apiService.get("/home/GetListsHome?fimLocacoes=" + true).subscribe(res => {
      this.fimLocacoes = res.result.fimLocacoes;
      this.contasDisponiveis = res.result.contasDisponiveis;
      this.contasDesativar = res.result.contasDesativar;
      this.saldoMes = res.result.saldoMes;

      this._apiService.get("/cliente/GetSelectClientes").subscribe(clis => {
        this.clienteOptions = clis.result;
      })
    })
  }

  getTitleSaldo(index) {
    switch (index) {
      case 0:
        return "Saldo Mês";
      case 1:
        return "Saldo Ano";
    }
  }

  getClassSaldo(val) {
    return this._settService.getClassSaldo(val);
  }

  hasError(control): boolean {
    return (!control.valid && !control.untouched);
  }

  showLocacaoRapidaModal(contaId: number) {
    this.venda = new Venda();
    this.itemVenda = new ItemVenda();
    this.itemVenda.diasGratis = true;
    this.itemVenda.conta.id = contaId;

    this.venda.dataVenda = moment(new Date());
    this.locacaoRapidaModal.show();
  }

  showEstenderLocacaoModal(id: number) {
    this.itemVenda.id = id;
    this.estenderLocacaoModal.show();
  }

  showFinalizarLocacaoModal(id: number) {
    this.itemVenda.id = id;
    this.finalizarLocacaoModal.show();
  }

  showConfirmarDesativacaoModal(item: Conta) {
    this.conta = item;
    this.conta.dataDesativacao = moment(item.dataDesativacao).add(6, 'month');;
    this.confirmarDesativacaoModal.show();
  }

  hideModal() {
    this.itemVenda = new ItemVenda();
    this.conta = new Conta();
    this.estenderLocacaoModal.hide();
    this.locacaoRapidaModal.hide();
    this.finalizarLocacaoModal.hide();
    this.confirmarDesativacaoModal.hide();
  }

  gerarSenha() {
    this.itemVenda.senha = this._settService.gerarSenha();
  }

  estenderLocacao() {
    this._apiService.post("/venda/estenderLocacao", this.itemVenda).subscribe(res => {
      this._notifcations.success("Locacao", "Locação estendida com sucesso! #" + this.itemVenda.id);
      this.itemVenda = new ItemVenda();
      this.estenderLocacaoModal.hide();
      this.ngOnInit();
    })
  };

  checkDataDesativacao(data: Date) {
    let hoje = moment(new Date());
    let dataDesativacao = moment(data).add(6, 'month');

    if (dataDesativacao < hoje)
      return 'danger';
    else
      return '';
  };

  isDesativacao(data: Date) {
    let hoje = moment(new Date());
    let dataDesativacao = moment(data).add(6, 'month');

    return dataDesativacao.isSameOrAfter(hoje);
  }

  finalizarLocacao() {
    this._apiService.post("/venda/finalizarLocacao", this.itemVenda.id).subscribe(res => {
      this._notifcations.success("Locacao", "Locação finalizada com sucesso! #" + this.itemVenda.id);
      this.itemVenda = new ItemVenda();
      this.hideModal();
      this.ngOnInit();
    })
  };

  confirmarDesativacao() {
    this.conta.ativa = false;
    this._apiService.post("/conta/save", this.conta).subscribe(res => {
      this._notifcations.success("Conta", "Conta desativada com sucesso #" + this.itemVenda.id);
      this.conta = new Conta();
      this.hideModal();
      this.ngOnInit();
    })
  };

  saveLocacao() {
    this.itemVenda.tipoVenda.id = 2;
    this.itemVenda.tipoConta.id = 3;
    this.venda.itensVenda.push(this.itemVenda);

    this._apiService.post("/venda/save", this.venda).subscribe(res => {
      if (res.success) {
        this.venda = new Venda();
        this.itemVenda = new ItemVenda();
        this.hideModal();
        this._notifcations.success("Cadastro", "Locação cadastrada com sucesso!" + " #" + res.result.id);
        this.ngOnInit();
      }
      else
        this._notifcations.alert("Cadastro", res.errors.join());
    });
  }
}
