import { Pipe, PipeTransform } from '@angular/core';

import * as moment from 'moment';

@Pipe({
  name: 'nomeImg'
})
export class NomeImgPipe implements PipeTransform {

  transform(value: any, args?: any): any {
    if (value == null) {
      return null;
    }
    else {
      let values: Array<string> = value.split(' ');
      let v = values.join('_');

      return v;
    }
  }
}

@Pipe({
  name: 'addDatePart'
})
export class AdDatePartPipe implements PipeTransform {

  transform(value: any, args?: any, args2?: any): any {
    if (value == null) {
      return null;
    }
    else {
      let num: number = 0;
      let periodo: any = '';

      if (args != null)
        num = parseInt(args);
      if (args2 != null)
        periodo = args2;

      value = moment(value).add(num, periodo);
      return value;
    }
  }
}

@Pipe({
  name: 'checkEmptyValue'
})
export class CheckEmptyValuePipe implements PipeTransform {

  transform(value: any, args?: any, args2?: any): any {
    if (value == null) {
      return null;
    }
    else {
      if (value == '01/01/0001' || value == '01/01/1900' || value == 0)
      return '-';

     return value;
    }
  }
}

@Pipe({
  name: 'tamText'
})
export class TamTextPipe implements PipeTransform {

  transform(value: string, args?: any): any {
    if (value == null) {
      return null;
    }
    else {
      if (value.length > 15)
        value = value.substring(0,15) + "...";
      

     return value;
    }
  }
}
