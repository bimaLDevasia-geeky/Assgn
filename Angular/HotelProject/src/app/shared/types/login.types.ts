import { FormControl } from "@angular/forms";

export interface LoginForm {
    email: FormControl<string | null>;
    password: FormControl<string | null>;
}

export interface LoginRequest {
    email: string | null; 
    password: string | null;
}

export interface AuthResponse {
  id: string;
  token: string;
  email: string;
  role: string;
}

// This is the user data you'll store in your BehaviorSubject
export interface User {
  id: string | null;
  email: string | null;
  role: string | null;
}