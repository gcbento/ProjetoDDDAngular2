import { BrowserModule } from '@angular/platform-browser';
import { NgModule, LOCALE_ID } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { ModalModule, AlertModule, CollapseModule, TabsModule, TooltipModule } from 'ng2-bootstrap/ng2-bootstrap';
import { DataTableModule } from "angular2-datatable";
import { SimpleNotificationsModule } from 'angular2-notifications';
import { MomentModule } from 'angular2-moment';
import { DatePickerModule } from 'ng2-datepicker';
import { CustomFormsModule } from 'ng2-validation';
import { SelectModule } from 'angular2-select';
import { ChartModule } from 'angular2-highcharts';

import { SettingsService } from './services/settings.service';
import { ApiService } from './services/api.service';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { CompraComponent } from './compra/compra.component';
import { ContaComponent } from './conta/conta.component';
import { ClienteComponent } from './cliente/cliente.component';
import { VendaComponent } from './venda/venda.component';
import { SaldoValoresComponent } from './saldo/saldo-valores/saldo-valores.component';
import { SaldoContasComponent } from './saldo/saldo-contas/saldo-contas.component';
import { AppRoutingModule } from './app-routing.module';
import { Home } from './home/home';
import { Compra, CompraFilter } from './compra/compra';
import { Conta, TipoConta, CodigosConta, ContaFilter, JogosConta } from './conta/conta';
import { Cliente, ClienteFilter } from './cliente/cliente';
import { Venda, ItemVenda, TipoVenda, VendaFilter } from './venda/venda';
import { Saldo, SaldoFilter, Estatisticas, SaldoConta } from './saldo/saldo';
import { NomeImgPipe, AdDatePartPipe, CheckEmptyValuePipe, TamTextPipe } from './pipes/pipes';


@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    CompraComponent,
    ContaComponent,
    ClienteComponent,
    VendaComponent,
    SaldoValoresComponent,
    SaldoContasComponent,
    NomeImgPipe,
    AdDatePartPipe,
    CheckEmptyValuePipe,
    TamTextPipe
  ],
  imports: [
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    HttpModule,
    AppRoutingModule,
    ModalModule,
    AlertModule,
    DataTableModule,
    SimpleNotificationsModule,
    MomentModule,
    CustomFormsModule,
    DatePickerModule,
    CollapseModule,
    SelectModule,
    TabsModule,
    TooltipModule,
    ChartModule
  ],
  providers: [
    SettingsService,
    {
      provide: LOCALE_ID,
      deps: [SettingsService],
      useFactory: (settingsService) => settingsService.getLocale()
    },
    ApiService,
    Home,
    Compra, CompraFilter,
    Conta, TipoConta, CodigosConta, ContaFilter, JogosConta,
    Cliente, ClienteFilter,
    Venda, ItemVenda, TipoVenda, VendaFilter,
    Saldo, SaldoFilter, Estatisticas, SaldoConta
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
