import { Injectable } from '@angular/core';
import Swal from 'sweetalert2';

@Injectable({
  providedIn: 'root' 
})
export class ToastService {
  

  private toastMixin = Swal.mixin({
    toast: true,
    position: 'top-end',
    showConfirmButton: false,
    timer: 3000,
    timerProgressBar: true, 
    didOpen: (toast) => {
      toast.onmouseenter = Swal.stopTimer;
      toast.onmouseleave = Swal.resumeTimer;
    }
  });

  constructor() { }

  
  success(title: string) {
    this.toastMixin.fire({
      icon: 'success',
      title: title
    });
  }

  error(title: string) {
    this.toastMixin.fire({
      icon: 'error',
      title: title
    });
  }

  warning(title: string) {
    this.toastMixin.fire({
      icon: 'warning',
      title: title
    });
  }
}