import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';

import { CustomValidators } from 'ng2-validation';
import { ModalDirective } from 'ng2-bootstrap/ng2-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import * as moment from 'moment';

import { SettingsService } from './../services/settings.service';
import { ApiService } from './../services/api.service';
import { GenericResult, GenericSimpleResult, SelectModel } from './../models/models';
import { Venda, VendaFilter, TipoVenda, ItemVenda } from './venda';
import { Conta, TipoConta } from '../conta/conta';
import { Cliente } from '../cliente/cliente';


@Component({
  selector: 'app-venda',
  templateUrl: './venda.component.html',
  styleUrls: ['./venda.component.css']
})
export class VendaComponent implements OnInit {
  constructor(
    private _settService: SettingsService,
    private _apiService: ApiService,
    private _notifcations: NotificationsService,
    private _fb: FormBuilder,
    private _route: ActivatedRoute,
    public venda: Venda,
    public filter: VendaFilter,
    public itemVenda: ItemVenda
  ) {
    this.vendaForm = _fb.group({
      'selectJogos': [null, Validators.required],
      'dataVenda': [null, Validators.required],
      'tipoVendaId': [null, Validators.required],
      'selectClientes': [null, Validators.required],
      'diasGratis': [null],
      'contaId': [null, Validators.required],
      'tipoContaId': [null, Validators.required],
      'valor': [null, Validators.compose([Validators.required, CustomValidators.number])],
      'qtdeDias': [null, Validators.compose([CustomValidators.number])],
      'senha': [null],
      'tipoPeriodo': [null]
    })
  }

  @ViewChild('detalhesVendaModal') public detalhesVendaModal: ModalDirective;
  @ViewChild('vendaModal') public vendaModal: ModalDirective;
  @ViewChild('updateVendaModal') public updateVendaModal: ModalDirective;
  @ViewChild('confirmaDeleteModal') public confirmaDeleteModal: ModalDirective;
  @ViewChild('confirmaFinalizarLocacao') public confirmaFinalizarLocacao: ModalDirective;

  private _baseUrl = "/venda/";
  vendaForm: FormGroup;
  title: string = "Vendas";
  listVendas: Array<Venda> = [];
  jogosOptions: Array<SelectModel> = [];
  clienteOptions: Array<SelectModel> = [];
  contas: Array<Conta> = [];
  isEstenderLocacao: boolean = true;

  ngOnInit() {
    let locacoes = this._route.snapshot.params['locacoes'];
    this.filter.locacoes = false;
    this.itemVenda.tipoPeriodo = 'dias';

    if (locacoes) {
      this.filter.locacoes = locacoes;
    }

    this._apiService.list(this._baseUrl + "list", this.filter).subscribe(res => {
      this.listVendas = res.result;
      this._apiService.get(this._baseUrl + "GetSelectJogosVenda").subscribe(jgos => {
        this.jogosOptions = jgos.result;
        this._apiService.get("/cliente/GetSelectClientes").subscribe(clis => {
          this.clienteOptions = clis.result;
        })
      })
    });
  }

  hasError(control): boolean {
    return (!control.valid && !control.untouched);
  }

  showDetalhesVendaModal(venda: Venda): void {
    this.venda = venda;
    this.venda.dataVenda = moment(venda.dataVenda);
    this.itemVenda.venda.id = venda.id;
    this.itemVenda.id = venda.itensVenda[0].id;
    this.detalhesVendaModal.show();
  }

  showFinalizarLocModal(itemId: number) {
    this.itemVenda.id = itemId;
    this.confirmaFinalizarLocacao.show();
  }

  showVendaModal(): void {
    this.venda = new Venda();
    this.itemVenda = new ItemVenda();

    this.itemVenda.tipoConta.id = 2;
    this.itemVenda.tipoVenda.id = 1;
    this.venda.dataVenda = moment(new Date());

    this.vendaModal.show();
  }

  showUpdateVendaModal(item: Venda): void {
    this.venda = new Venda();
    this.itemVenda = new ItemVenda();

    this.venda.id = item.id;
    this.itemVenda = item.itensVenda[0];
    this.venda.dataVenda = moment(item.dataVenda);

    this.updateVendaModal.show();
  }

  showConfirmeDeleteModal(id: number): void {
    this.venda.id = id;
    this.confirmaDeleteModal.show();
  }

  hideModal(): void {
    this.detalhesVendaModal.hide();
    this.vendaModal.hide();
    this.confirmaDeleteModal.hide();
    this.updateVendaModal.hide();
    this.venda = new Venda();
    this.itemVenda = new ItemVenda();
    this.contas = new Array<Conta>();
  }

  hideFinalizarModal() {
    this.confirmaFinalizarLocacao.hide();
  }

  onSelectJogo(item) {
    this._apiService.get(this._baseUrl + "GetContasByJogo?nomeJogo=" + item.value).subscribe(res => {
      this.contas = res.result;
    })
  }

  checkRadioConta(conta: Conta) {
    this.itemVenda.conta.id = conta.id;
  }

  gerarSenha(){
    this.itemVenda.senha = this._settService.gerarSenha();
  }

  pesquisa() {
    this._apiService.list(this._baseUrl + "list", this.filter).subscribe(res => this.listVendas = res.result);
  }

  save() {
    this.venda.itensVenda.push(this.itemVenda);

    this._apiService.post(this._baseUrl + "save", this.venda).subscribe(res => {
      if (res.success) {
        this.venda = new Venda();
        this.itemVenda = new ItemVenda();
        this.contas = new Array<Conta>();
        this.hideModal();
        this._notifcations.success("Cadastro", "Venda cadastrada com sucesso!" + " #" + res.result.id);
        this.ngOnInit();
      }
      else
        this._notifcations.alert("Cadastro", res.errors.join());
    });
  }

  delete() {
    this._apiService.post(this._baseUrl + "delete", this.venda.id).subscribe(res => {
      if (res.success) {
        this._notifcations.success("Delete", "Venda deletada com sucesso! #" + this.venda.id);
        this.confirmaDeleteModal.hide();
        this.ngOnInit();
        this.venda = new Venda();
        this.itemVenda = new ItemVenda();
        this.contas = new Array<Conta>();
      }
      else {
        this._notifcations.error("Delete", "Essa venda não pode ser deletada. #" + this.venda.id);
        this.confirmaDeleteModal.hide();
        this.ngOnInit();
        this.venda = new Venda();
        this.itemVenda = new ItemVenda();
        this.contas = new Array<Conta>();
      }
    });
  };

  checkDataLocacao(fimLocacao: Date) {
    let fim = moment(fimLocacao);
    let hoje = moment(new Date());
    return fim.isSame(hoje) || fim.isAfter(hoje);
  }

  estenderLocacao() {
    this._apiService.post(this._baseUrl + "estenderLocacao", this.itemVenda).subscribe(res => {
      this._notifcations.success("Locacao", "Locação estendida com sucesso! #" + this.itemVenda.id);
      this.itemVenda = new ItemVenda();
      this.detalhesVendaModal.hide();
      this.ngOnInit();
    })
  };

  finalizarLocacao() {
    this._apiService.post(this._baseUrl + "finalizarLocacao", this.itemVenda.id).subscribe(res => {
      this._notifcations.success("Locacao", "Locação finalizada com sucesso! #" + this.itemVenda.id);
      this.itemVenda = new ItemVenda();
      this.detalhesVendaModal.hide();
      this.confirmaFinalizarLocacao.hide();
      this.ngOnInit();
    })
  }
}
