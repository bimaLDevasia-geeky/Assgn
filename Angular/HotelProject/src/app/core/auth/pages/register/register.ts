import { ChangeDetectionStrategy, Component, DestroyRef, inject, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { Customer } from '../../../../shared/types/admin.types';
import { apiUrl } from '../../../../../environments/developerenvironment';
import { ToastService } from '../../../../shared/services/toast.sercie';

interface RegisterForm {
  fullName: string;
  email: string;
  password: string;
  phoneNumber: string;
  idProofNumber: string;
}

@Component({
  selector: 'app-register',
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './register.html',
  styleUrl: './register.scss',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class Register implements OnInit {
  private fb = inject(FormBuilder);
  private http = inject(HttpClient);
  private router = inject(Router);
  private ref = inject(DestroyRef);
  private toast = inject(ToastService);

  registerForm!: FormGroup;
  error = signal<string | null>(null);
  isSubmitting = signal<boolean>(false);

  ngOnInit(): void {
    this.registerForm = this.fb.group({
      fullName: ['', [Validators.required, Validators.minLength(2)]],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      phoneNumber: ['', [Validators.required, Validators.pattern(/^[0-9]{10,15}$/)]],
      idProofNumber: ['', [Validators.required, Validators.minLength(5)]]
    });
  }

  onSubmit(): void {
    if(this.registerForm.invalid) {
      this.registerForm.markAllAsTouched();
      return;
    }

    this.isSubmitting.set(true);
    const formValue = this.registerForm.value;
    
    // Map password to passwordHash for API
    const registerData = {
      fullName: formValue.fullName,
      email: formValue.email,
      passwordHash: formValue.password,
      phoneNumber: formValue.phoneNumber,
      idProofNumber: formValue.idProofNumber
    };
    
    this.http.post<Customer>(`${apiUrl}/customer`, registerData).pipe(
      takeUntilDestroyed(this.ref)
    ).subscribe({
      next: (response) => {
        console.log('Registration successful:', response);
        this.isSubmitting.set(false);
        this.toast.success('Registration successful! Please login.');
        
        // Navigate to login
        this.router.navigate(['/login']);
      },
      error: (err) => {
        this.isSubmitting.set(false);
        this.error.set('Registration failed. Email might already be in use.');
        this.toast.error('Registration failed. Please try again.');
        console.error('Registration error:', err);
      }
    });
  }

  getField(fieldName: string) {
    return this.registerForm.get(fieldName);
  }
}
