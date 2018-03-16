import { Injectable } from '@angular/core';

@Injectable()
export class SettingsService {

  constructor() { }

  getLocale() {
    return 'pt-BR';
  }

  getClassSaldo(val) {
    var cssClass = "label label-default";

    if (val > 0)
      cssClass = "label label-success";
    else if (val < 0)
      cssClass = "label label-danger";

    return cssClass;
  }

  gerarSenha(): string{
    let text = "";
    let possible = "qwertyuioplkjhgfdsazxcvbnm0123456789";

    for( var i=0; i < 10; i++ )
        text += possible.charAt(Math.floor(Math.random() * possible.length));

    return text;
  }
}
