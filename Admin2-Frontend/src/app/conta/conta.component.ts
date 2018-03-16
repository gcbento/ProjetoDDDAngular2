import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

import { CustomValidators } from 'ng2-validation';
import { ModalDirective } from 'ng2-bootstrap/ng2-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import * as moment from 'moment';

import { ApiService } from './../services/api.service';
import { GenericResult, GenericSimpleResult, SelectModel } from './../models/models';
import { Conta, TipoConta, CodigosConta, ContaFilter, JogosConta } from './conta';
import { Compra } from '../compra/compra';


@Component({
  selector: 'app-conta',
  templateUrl: './conta.component.html',
  styleUrls: ['./conta.component.css']
})
export class ContaComponent implements OnInit {

  constructor(
    private _apiService: ApiService,
    private _notifcations: NotificationsService,
    private _fb: FormBuilder,
    public conta: Conta,
    public tipoConta: TipoConta,
    public codigosConta: CodigosConta,
    public filter: ContaFilter,
    public compra: Compra,
    public jogosConta: JogosConta
  ) {
    this.contaForm = _fb.group({
      'email': [null, Validators.required],
      'senha': [null, Validators.required],
      'idOnline': [null, Validators.required],
      'tipoContaId': [null, Validators.required],
      'dataNascimento': [null, Validators.required],
      'dataDesativacao': [null, Validators.required],
      'codigos': [null],
      'selectJogos': [null],
      'ativa': [null]
    });
  }

  @ViewChild('contaModal') public contaModal: ModalDirective;
  @ViewChild('jogosModal') public jogosModal: ModalDirective;
  @ViewChild('codigosModal') public codigosModal: ModalDirective;
  @ViewChild('confirmaDeleteModal') public confirmaDeleteModal: ModalDirective;

  private _baseUrl = "/conta/";
  listContas: Array<Conta> = [];
  contaForm: FormGroup;
  jogosOptions: Array<SelectModel> = [];
  listJogos: Array<Compra> = [];
  listCodigos: Array<CodigosConta> = [];

  hasError(control): boolean {
    return (!control.valid && !control.untouched);
  }

  ngOnInit() {
    this._apiService.list(this._baseUrl + "list", this.filter).subscribe(res => {
      this.listContas = res.result;
      this._apiService.get(this._baseUrl + "GetSelectJogosConta").subscribe(res => this.jogosOptions = res.result);
    });
  }

  showContaModal(item = null): void {
    this.conta = new Conta();
    this.codigosConta = new CodigosConta();

    this.conta.tipoConta.id = 1;
    this.conta.dataNascimento = moment(new Date("1992/08/29"));

    if (item != null) {
      this.conta = item;
      this.conta.dataNascimento = moment(this.conta.dataNascimento);
      this.conta.dataDesativacao = moment(this.conta.dataDesativacao);
    }

    this.contaModal.show();
  }

  showJogosModal(contaId: number, jogos: Array<any>): void {
    this.jogosConta.contaId = contaId;
    this.listJogos = jogos;
    this.jogosModal.show();
  }

  showCodigosModal(contaId: number, codigos: Array<any>): void {
    this.codigosConta.contaId = contaId;
    this.listCodigos = codigos;
    this.codigosModal.show();
  }

  showConfirmeDeleteModal(id: number): void {
    this.conta.id = id;
    this.confirmaDeleteModal.show();
  }

  hideModal(): void {
    this.contaModal.hide();
    this.confirmaDeleteModal.hide();
    this.jogosModal.hide();
    this.codigosModal.hide();
    this.conta = new Conta();
    this.codigosConta = new CodigosConta();
  }

  bindIconStatus(val) {
    if (val)
      return "<i class='fa fa-check alert-success'></i>";
    else
      return "<i class='fa fa-close alert-danger'></i>";
  }

  pesquisa() {
    this._apiService.list(this._baseUrl + "list", this.filter).subscribe(res => this.listContas = res.result);
  }

  save() {
    var title = "Cadastro";
    var msg = "Conta cadastrada com sucesso!";

    if (this.conta.id > 0) {
      title = "Edição";
      msg = "Conta alterada com sucesso!";
    }

    if (this.codigosConta.descricao != null) {
      let cods: string[] = this.codigosConta.descricao.split('\n');
      for (let cod of cods) {
        this.codigosConta.descricao = cod;
        this.conta.codigos.push(this.codigosConta);
      }
    }

    if (this.compra.id > 0) {
      this.conta.jogos.push(this.compra);
    }

    this._apiService.post(this._baseUrl + "save", this.conta).subscribe(res => {
      if (res.success) {
        this.conta = new Conta();
        this.codigosConta = new CodigosConta();
        this.hideModal();
        this._notifcations.success(title, msg + " #" + res.result.id);
        this.ngOnInit();
      }
      else
        this._notifcations.alert("Cadastro", res.errors.join());
    });
  };

  delete() {
    this._apiService.post(this._baseUrl + "delete", this.conta.id).subscribe(res => {
      if (res.success) {
        this._notifcations.success("Delete", "Conta deletada com sucesso! #" + this.conta.id);
        this.confirmaDeleteModal.hide();
        this.ngOnInit();
        this.conta = new Conta();
      }
      else {
        this._notifcations.error("Delete", "Essa conta não pode ser deletada. #" + this.compra.id);
        this.confirmaDeleteModal.hide();
        this.ngOnInit();
        this.conta = new Conta();
      }
    });
  };

  addJogo() {
    this._apiService.post(this._baseUrl + "addJogo", this.jogosConta).subscribe(res => {
      this._notifcations.success("Add Jogo", "Jogo Adicionado com sucesso! #" + this.jogosConta.jogoId);
      this.hideModal();
      this.jogosConta = new JogosConta();
      this.ngOnInit();
    })
  }

  removerJogo(jogoId: number) {
    this.jogosConta.jogoId = jogoId;
    this._apiService.post(this._baseUrl + "removerJogo", this.jogosConta).subscribe(res => {
      this._notifcations.success("Delete", "Jogo Removido com sucesso! #" + this.jogosConta.jogoId);
      this.hideModal();
      this.jogosConta = new JogosConta();
      this.ngOnInit();
    })
  }

  updateCodigos(codigo: CodigosConta) {
    codigo.status = !codigo.status;
    this._apiService.post(this._baseUrl + "UpdateCodigosConta", codigo);
  }

  deleteCodigos() {
    let contaId = this.listCodigos[0].contaId;
    this._apiService.post(this._baseUrl + "DeleteCodigos", contaId).subscribe(res => {
      this.ngOnInit();
      this.listCodigos = new Array<CodigosConta>();
    });
  }
}
