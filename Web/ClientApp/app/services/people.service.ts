import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class PeopleService {

    constructor(private httpClient: HttpClient, @Inject("BASE_URL") private baseUrl: string) { }

    createNewPerson(formData: string): Observable<Object> {
        return this.httpClient.post<{ contents: string }>(this.baseUrl + "api/People/Create", formData);
    };
}
