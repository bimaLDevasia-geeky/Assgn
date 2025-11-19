import { ChangeDetectionStrategy, Component, DestroyRef, inject, OnInit, signal } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
import { LoginForm, LoginRequest } from '../../../../shared/types/login.types';
import { AuthService } from '../../services/auth.service';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { ToastService } from '../../../../shared/services/toast.sercie';

@Component({
  selector: 'app-login',
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './login.html',
  styleUrl: './login.scss',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class Login implements OnInit {
  private fb = inject(FormBuilder);
  private loginservice = inject(AuthService); 
  private ref = inject(DestroyRef);
  private router = inject(Router);
  private route = inject(ActivatedRoute);
  private toast = inject(ToastService);

  loginForm!: FormGroup<LoginForm>;
  error = signal<string | null>(null);
  isSubmitting = signal<boolean>(false);

  ngOnInit(): void {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]]
    });
  }

  onSubmit(): void {
    if(this.loginForm.invalid) {
      this.loginForm.markAllAsTouched();
      return;
    }

    this.isSubmitting.set(true);
    const loginRequest = this.loginForm.value;
    
    this.loginservice.login(loginRequest as LoginRequest).pipe(
      takeUntilDestroyed(this.ref),
    ).subscribe({
      next: (response) => {
        console.log('Login successful:', response); 
        this.isSubmitting.set(false);
        
        const returnUrl = this.route.snapshot.queryParams['returnUrl'];
        
        if (response.role === 'Admin') {
          if (returnUrl && returnUrl.startsWith('/admin')) {
            this.router.navigateByUrl(returnUrl);
          } else {
            this.router.navigate(['/admin/dashboard']);
          }
        } else if (response.role === 'Customer') {
          if (returnUrl && !returnUrl.startsWith('/admin')) {
            this.router.navigateByUrl(returnUrl);
          } else {
            this.router.navigate(['/home']);
          }
        } else {
          this.router.navigate(['/home']);
        }
      },
      error: (err) => {
        this.isSubmitting.set(false);
        this.error.set('Invalid email or password');
        this.toast.error('Login failed. Please check your credentials.');
      }
    });
  }

  get email() {
    return this.loginForm.get('email');
  }

  get password() {
    return this.loginForm.get('password');
  }
}
