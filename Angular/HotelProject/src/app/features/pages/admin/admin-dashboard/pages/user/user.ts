import { ChangeDetectionStrategy, Component, inject, OnInit, signal } from '@angular/core';
import { CustomerService } from '../../../../../../shared/services/customer.service';
import { Customer } from '../../../../../../shared/types/admin.types';
import { ToastService } from '../../../../../../shared/services/toast.sercie';
import { catchError } from 'rxjs';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faTrash } from '@fortawesome/free-solid-svg-icons';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-user',
  imports: [FontAwesomeModule],
  templateUrl: './user.html',
  styleUrl: './user.scss',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class User implements OnInit {
  private customerService = inject(CustomerService);
  private toast = inject(ToastService);

  faTrash = faTrash;
  customers = signal<Customer[]>([]);
  loading = signal<boolean>(true);

  ngOnInit(): void {
    this.loadCustomers();
  }

  loadCustomers(): void {
    this.loading.set(true);
    this.customerService.getAllCustomers().pipe(
      catchError((error) => {
        console.error('Error fetching customers:', error);
        this.toast.error('Failed to load customers. Please try again.');
        this.loading.set(false);
        throw error;
      })
    ).subscribe({
      next: (customers: Customer[]) => {
        this.customers.set(customers);
        this.loading.set(false);
      }
    });
  }

  deleteCustomer(customer: Customer): void {
    Swal.fire({
      title: 'Are you sure?',
      text: `Do you want to delete ${customer.fullName}? This action cannot be undone.`,
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#ef4444',
      cancelButtonColor: '#6b7280',
      confirmButtonText: 'Yes, delete it!',
      cancelButtonText: 'Cancel'
    }).then((result) => {
      if (result.isConfirmed) {
        this.customerService.deleteCustomer(customer.id).subscribe({
          next: () => {
            this.customers.update(current => current.filter(c => c.id !== customer.id));
            this.toast.success(`Customer ${customer.fullName} deleted successfully!`);
          },
          error: (error) => {
            console.error('Error deleting customer:', error);
            this.toast.error('Failed to delete customer. Please try again.');
          }
        });
      }
    });
  }
}
