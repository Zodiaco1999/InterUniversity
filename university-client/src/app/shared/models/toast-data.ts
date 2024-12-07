import { ToastType } from './toast-type.enum';
import { TemplateRef } from '@angular/core';

export class ToastData {
  type!: ToastType;
  text?: string;
  title?: string;
  detail?: string;
  buttonClose!: boolean;
  template?: TemplateRef<any>;
  closeTime?: number;
  viewErrorCode!: boolean;

  get className(): string {
    if (this.type === ToastType.success) {
      return 'toast-success';
    }
    if (this.type === ToastType.warning) {
      return 'toast-warning';
    }
    if (this.type === ToastType.info) {
      return 'toast-info';
    }
    if (this.type === ToastType.danger) {
      return 'toast-danger';
    }
    return 'toast-success';
  }
}
