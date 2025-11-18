import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { apiUrl } from '../../../environments/developerenvironment';

export interface EmailDto {
  to: string;
  subject: string;
  body: string;
}

@Injectable({
  providedIn: 'root'
})
export class EmailService {
  
  private apiUrl = apiUrl + '/email/send';

  constructor(private http: HttpClient) { }

  sendEmail(email: EmailDto): Observable<any> {
    return this.http.post(this.apiUrl, email);
  }
}