import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { HomeComponent } from './home/home.component';
import { CompraComponent } from './compra/compra.component';
import { ContaComponent } from './conta/conta.component';
import { ClienteComponent } from './cliente/cliente.component';
import { VendaComponent } from './venda/venda.component';
import { SaldoValoresComponent } from './saldo/saldo-valores/saldo-valores.component';
import { SaldoContasComponent } from './saldo/saldo-contas/saldo-contas.component';

const routes: Routes = [
  { path: '', redirectTo: '/home', pathMatch: 'full' },
  { path: 'home', component: HomeComponent },
  { path: 'compras', component: CompraComponent },
  { path: 'contas', component: ContaComponent },
  { path: 'clientes', component: ClienteComponent },
  { path: 'vendas', component: VendaComponent },
  { path: 'vendas/:locacoes', component: VendaComponent },
  { path: 'saldo/contas', component: SaldoContasComponent },
  { path: 'saldo/valores', component: SaldoValoresComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
