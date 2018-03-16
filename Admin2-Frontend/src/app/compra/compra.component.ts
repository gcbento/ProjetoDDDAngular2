import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

import { CustomValidators } from 'ng2-validation';
import { ModalDirective } from 'ng2-bootstrap/ng2-bootstrap';
import { NotificationsService } from 'angular2-notifications';

import { ApiService } from './../services/api.service';
import { GenericResult, GenericSimpleResult } from './../models/models';
import { Compra, CompraFilter } from './compra';

@Component({
  selector: 'compra',
  templateUrl: './compra.component.html',
  styleUrls: ['./compra.component.css']
})
export class CompraComponent implements OnInit {
 constructor(
    private _apiService: ApiService,
    private _notifcations: NotificationsService,
    private _fb: FormBuilder,
    public compra: Compra,
    public filter: CompraFilter,
  ) {
    this.compraForm = _fb.group({
      'descricao': [null, Validators.required],
      'valor': [null, Validators.compose([Validators.required, CustomValidators.number])],
    })
  }

  @ViewChild('compraModal') public compraModal: ModalDirective;
  @ViewChild('confirmaDeleteModal') public confirmaDeleteModal: ModalDirective;

  private _baseUrl = "/compra/";
  compraForm: FormGroup;
  title: string = "Compras";

  listCompra: Array<Compra> = [];

  ngOnInit() {
    this._apiService.list(this._baseUrl + "list", this.filter).subscribe(res => this.listCompra = res.result);
  }

  hasError(control): boolean {
    return (!control.valid && !control.untouched);
  }

  showCompraModal(item = null): void {
    this.compra = new Compra();

    if (item != null)
      this.compra = item;

    this.compraModal.show();
  }

  showConfirmeDeleteModal(id: number): void {
    this.compra.id = id;
    this.confirmaDeleteModal.show();
  }

  hideModal(): void {
    this.compraModal.hide();
    this.confirmaDeleteModal.hide();
    this.compra = new Compra();
  }

  pesquisa() {
    this._apiService.list(this._baseUrl + "list", this.filter).subscribe(res => this.listCompra = res.result);
  }

  save() {
    var title = "Cadastro";
    var msg = "Compra cadastrada com sucesso!";

    if (this.compra.id > 0) {
      title = "Edição";
      msg = "Compra alterada com sucesso!";
    }

    this._apiService.post(this._baseUrl + "save", this.compra).subscribe(res => {
      if (res.success) {
        this.compra = new Compra();
        this.hideModal();
        this._notifcations.success(title, msg + " #" + res.result.id);
        this.ngOnInit();
      }
      else
        this._notifcations.alert("Cadastro", res.errors.join());
    });
  };

  delete() {
    this._apiService.post(this._baseUrl + "delete", this.compra.id).subscribe(res => {
      if (res.success) {
        this._notifcations.success("Delete", "Compra deletada com sucesso! #" + this.compra.id);
        this.confirmaDeleteModal.hide();
        this.ngOnInit();
        this.compra = new Compra();
      }
      else {
        this._notifcations.error("Delete", "Essa compra não pode ser deletada pq já está atrelada a uma conta. #" + this.compra.id);
        this.confirmaDeleteModal.hide();
        this.ngOnInit();
        this.compra = new Compra();
      }
    });
  }
}
