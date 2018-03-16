import { Component, ViewContainerRef } from '@angular/core';

import * as moment from 'moment';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  private viewContainerRef: ViewContainerRef;
  isSaldoMenu: boolean = true;

  public constructor(viewContainerRef: ViewContainerRef) {
    
    moment.locale('pt-BR');
    this.viewContainerRef = viewContainerRef;
  }
}
