import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, EMPTY } from 'rxjs';
import { ProcedureMetaData } from '../model/Procedure-metadata';
import { catchError, tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ProcedureService {

  constructor(
    @Inject('API_URL') private primaryUrl: string,
    @Inject('ANOTHER_URL') private secondaryUrl: string,
    private http: HttpClient) {
  }

  public LoadStoreProcedureMetaData(): Observable<ProcedureMetaData> {
    const primaryUrl = `${this.primaryUrl}/Procedure/procedure-meta-data`;
    const secondaryUrl = 'Procedure/procedure-meta-data'; // Fallback URL

    return this.http.get<ProcedureMetaData>(primaryUrl).pipe(
      // If the primary request fails, catch the error and switch to the secondary URL
      catchError(error => {
        console.error('Primary URL failed, trying secondary URL:', error);
        // Return the Observable from the secondary URL request
        return this.http.get<ProcedureMetaData>(secondaryUrl);
      }),
      // Handle errors from the secondary request if it also fails
      catchError(secondaryError => {
        this.handleLoadError(secondaryError);
        // Return an empty Observable or handle it as necessary
        return EMPTY; // or throwError if you want to propagate the error
      }),
      tap((result) => this.getProcedureMetaData(result)) // Process the result
    );
  }

  handleLoadError(secondaryError: any): void {
    throw new Error("Method not implemented.");
  }
  getProcedureMetaData(result: ProcedureMetaData): void {
    throw new Error("Method not implemented.");
  }

}
