import { Injectable } from '@angular/core';
import { ToastData } from '../models/toast-data';
import { ToastType } from '../models/toast-type.enum';
import { timer } from 'rxjs';
import { CustomErrorResponse } from '../models/custom-error-response'

@Injectable({
  providedIn: 'root'
})
export class ToastService {
  toasts: ToastData[] = [];
  toastDelaySuccess = 5000;
  toastDelayInfo = 8000;
  toastDelayWarning = 8000;
  toastDelayError = 8000;

  private show(type: ToastType, title: string, text: string, detail: string, buttonClose: boolean, closeTime: number) {
    const toast: ToastData = new ToastData();
    toast.title = title;
    toast.type = type;
    toast.text = text;
    toast.detail = detail;
    toast.buttonClose = buttonClose;
    toast.closeTime = closeTime;

    this.toasts.push(toast);

    const source = timer(closeTime * 1000);

    source.subscribe(_ => this.remove(toast));
  }

  showSuccess(title: string, message: string, closeTime = 0) {
    this.show(ToastType.success, title, message, "", true, closeTime === 0 ? this.toastDelaySuccess : closeTime);
  }

  showInfo(title: string, message: string, closeTime = 0) {
    this.show(ToastType.info, title, message, "", true, closeTime === 0 ? this.toastDelayInfo : closeTime);
  }

  showWarning(title: string, message: string, closeTime = 0) {
    this.show(ToastType.warning, title, message, "", true, closeTime === 0 ? this.toastDelayWarning : closeTime);
  }

  showError(title: string, error: CustomErrorResponse | string, closeTime= 0) {
    console.log(error)
    if (typeof error === 'string') {
      console.log(error)
      this.show(ToastType.danger, title, error, "", true, closeTime === 0 ? this.toastDelayError : closeTime);
    } else {
      console.log("Type")
      const message = typeof error === 'string' ? error : error.message;
      console.log(error)
      this.show(error.isWarning ? ToastType.warning : ToastType.danger, title, message, `${error.typeException}. ${error.detail}`, true, error.isWarning ? closeTime === 0 ? this.toastDelayWarning : closeTime : closeTime === 0 ? this.toastDelayError : closeTime);
    }
  }

  remove(toast: ToastData) {
    this.toasts = this.toasts.filter(t => t !== toast);
  }

}
