import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

import { CustomValidators } from 'ng2-validation';
import { ModalDirective } from 'ng2-bootstrap/ng2-bootstrap';
import { NotificationsService } from 'angular2-notifications';

import { ApiService } from './../services/api.service';
import { GenericResult, GenericSimpleResult } from './../models/models';
import { Cliente, ClienteFilter } from './cliente';

@Component({
  selector: 'app-cliente',
  templateUrl: './cliente.component.html',
  styleUrls: ['./cliente.component.css']
})
export class ClienteComponent implements OnInit {

  constructor(
    private _apiService: ApiService,
    private _notifcations: NotificationsService,
    private _fb: FormBuilder,
    public cliente: Cliente,
    public filter: ClienteFilter,
  ) {
    this.clienteForm = _fb.group({
      'nome': [null, Validators.required],
      'email': [null, Validators.required],
    })
  }

  @ViewChild('clienteModal') public clienteModal: ModalDirective;
  @ViewChild('confirmaDeleteModal') public confirmaDeleteModal: ModalDirective;

  private _baseUrl = "/cliente/";
  clienteForm: FormGroup;
  title: string = "Compras";

  listClientes: Array<Cliente> = [];

  ngOnInit() {
    this._apiService.post(this._baseUrl + "list", this.filter).subscribe(res => this.listClientes = res.result);
  }


  hasError(control): boolean {
    return (!control.valid && !control.untouched);
  }

  showClienteModal(item = null): void {
    this.cliente = new Cliente();

    if (item != null)
      this.cliente = item;

    this.clienteModal.show();
  }

  showConfirmeDeleteModal(id: number): void {
    this.cliente.id = id;
    this.confirmaDeleteModal.show();
  }

  hideModal(): void {
    this.clienteModal.hide();
    this.confirmaDeleteModal.hide();
    this.cliente = new Cliente();
  }

  pesquisa() {
    this._apiService.list(this._baseUrl + "list", this.filter).subscribe(res => this.listClientes = res.result);
  }

  save() {
    var title = "Cadastro";
    var msg = "Cliente cadastrado com sucesso!";

    if (this.cliente.id > 0) {
      title = "Edição";
      msg = "Cliente alterado com sucesso!";
    }

    this._apiService.post(this._baseUrl + "save", this.cliente).subscribe(res => {
      if (res.success) {
        this.cliente = new Cliente();
        this.hideModal();
        this._notifcations.success(title, msg + " #" + res.result.id);
        this.ngOnInit();
      }
      else
        this._notifcations.alert("Cadastro", res.errors.join());
    });
  };

  delete() {
    this._apiService.post(this._baseUrl + "delete", this.cliente.id).subscribe(res => {
      if (res.success) {
        this._notifcations.success("Delete", "Cliente deletada com sucesso! #" + this.cliente.id);
        this.confirmaDeleteModal.hide();
        this.ngOnInit();
        this.cliente = new Cliente();
      }
      else {
        this._notifcations.error("Delete", "Esse Cliente não pode ser deletado. #" + this.cliente.id);
        this.confirmaDeleteModal.hide();
        this.ngOnInit();
        this.cliente = new Cliente();
      }
    });
  };
}
