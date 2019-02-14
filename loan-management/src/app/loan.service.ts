import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of, throwError } from 'rxjs';
import { catchError, tap, map } from 'rxjs/operators';
import { Loan } from './loan';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class LoanService {

  private productsUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getLoans(): Observable<Loan[]> {
    return this.http.get<Loan[]>(this.productsUrl)
      .pipe(
        tap(data => console.log(JSON.stringify(data))),
        catchError(e=>this.handleError(e))
      );
  }

  private handleError(err) {
    let errorMessage: string;
    console.log(JSON.stringify(err));
    errorMessage = `API ${err.name}: ${err.message}`
    console.error(errorMessage);
    return throwError(errorMessage);
  }
}
