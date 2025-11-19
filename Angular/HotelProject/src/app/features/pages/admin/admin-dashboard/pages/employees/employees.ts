import { Component, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { EmployeeService, CreateEmployeeCommand, UpdateEmployeeCommand } from '../../../../../../shared/services/employee.services';
import { Employee } from '../../../../../../shared/types/admin.types';
import { ActivatedRoute } from '@angular/router';
import { ToastService } from '../../../../../../shared/services/toast.sercie';
import { ModalComponent } from '../../../../../../shared/components/modal/modal';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faPlus, faEdit, faTrash, faUser, faEnvelope, faBriefcase } from '@fortawesome/free-solid-svg-icons';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-employees',
  imports: [CommonModule, ReactiveFormsModule, ModalComponent, FontAwesomeModule],
  templateUrl: './employees.html',
  styleUrl: './employees.scss',
})
export class Employees {
  // Icons
  faPlus = faPlus;
  faEdit = faEdit;
  faTrash = faTrash;
  faUser = faUser;
  faEnvelope = faEnvelope;
  faBriefcase = faBriefcase;

  employees = signal<Employee[]>([]);
  hotelId = signal<string | null>(null);
  isModalOpen = signal<boolean>(false);
  isEditMode = signal<boolean>(false);
  selectedEmployee = signal<Employee | null>(null);
  employeeForm!: FormGroup;

  constructor(
    private employeeService: EmployeeService,
    private route: ActivatedRoute,
    private toast: ToastService,
    private fb: FormBuilder
  ) {
    this.initForm();
    this.hotelId.set(this.route.snapshot.queryParamMap.get('hotelId'));
    if (this.hotelId()) {
      this.loadEmployees(this.hotelId()!);
    }
  }

  initForm() {
    this.employeeForm = this.fb.group({
      fullName: ['', [Validators.required, Validators.minLength(2)]],
      email: ['', [Validators.required, Validators.email]],
      role: ['', [Validators.required, Validators.minLength(2)]]
    });
  }

  loadEmployees(hotelId: string) {
    this.employeeService.getEmployeesByHotel(hotelId).subscribe({
      next: (employees: Employee[]) => {
        this.employees.set(employees);
        console.log('Loaded employees:', employees);
      },
      error: (error) => {
        console.error('Error fetching employees:', error);
        this.toast.error('Failed to load employees. Please try again later.');
      }
    });
  }

  openAddModal() {
    this.isEditMode.set(false);
    this.selectedEmployee.set(null);
    this.employeeForm.reset();
    this.isModalOpen.set(true);
  }

  openEditModal(employee: Employee) {
    this.isEditMode.set(true);
    this.selectedEmployee.set(employee);
    this.employeeForm.patchValue({
      fullName: employee.fullName,
      email: employee.email,
      role: employee.role
    });
    this.isModalOpen.set(true);
  }

  closeModal() {
    this.isModalOpen.set(false);
    this.employeeForm.reset();
    this.selectedEmployee.set(null);
  }

  onSubmit() {
    if (this.employeeForm.invalid) {
      this.employeeForm.markAllAsTouched();
      return;
    }

    if (this.isEditMode() && this.selectedEmployee()) {
      this.updateEmployee();
    } else {
      this.createEmployee();
    }
  }

  createEmployee() {
    const command: CreateEmployeeCommand = {
      hotelId: this.hotelId()!,
      ...this.employeeForm.value
    };

    this.employeeService.createEmployee(command).subscribe({
      next: (newEmployee) => {
        this.loadEmployees(this.hotelId()!);
        this.toast.success('Employee added successfully!');
        this.closeModal();
      },
      error: (error) => {
        console.error('Error creating employee:', error);
        this.toast.error('Failed to add employee. Please try again.');
      }
    });
  }

  updateEmployee() {
    const command: UpdateEmployeeCommand = {
      id: this.selectedEmployee()!.id,
      ...this.employeeForm.value
    };

    this.employeeService.updateEmployee(command).subscribe({
      next: (updatedEmployee) => {
        this.employees.update(current =>
          current.map(emp => emp.id === updatedEmployee.id ? updatedEmployee : emp)
        );
        this.toast.success('Employee updated successfully!');
        this.closeModal();
      },
      error: (error) => {
        console.error('Error updating employee:', error);
        this.toast.error('Failed to update employee. Please try again.');
      }
    });
  }

  deleteEmployee(employee: Employee) {
    Swal.fire({
      title: 'Are you sure?',
      text: `Do you want to delete ${employee.fullName}?`,
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#ef4444',
      cancelButtonColor: '#6b7280',
      confirmButtonText: 'Yes, delete it!',
      cancelButtonText: 'Cancel'
    }).then((result) => {
      if (result.isConfirmed) {
        this.employeeService.deleteEmployee(employee.id).subscribe({
          next: () => {
            this.employees.update(current => current.filter(emp => emp.id !== employee.id));
            this.toast.success('Employee deleted successfully!');
          },
          error: (error) => {
            console.error('Error deleting employee:', error);
            this.toast.error('Failed to delete employee. Please try again.');
          }
        });
      }
    });
  }

  getFieldError(fieldName: string): string {
    const field = this.employeeForm.get(fieldName);
    if (field?.hasError('required')) {
      return `${fieldName.charAt(0).toUpperCase() + fieldName.slice(1)} is required`;
    }
    if (field?.hasError('email')) {
      return 'Please enter a valid email address';
    }
    if (field?.hasError('minlength')) {
      return `${fieldName.charAt(0).toUpperCase() + fieldName.slice(1)} must be at least ${field.errors?.['minlength'].requiredLength} characters`;
    }
    return '';
  }
} 
