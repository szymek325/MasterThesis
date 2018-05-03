import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class ReadingsProviderService {

    constructor(private httpClient: HttpClient, @Inject("BASE_URL") private baseUrl: string) { }

    getAllReadings(): Observable<Object> {
        return this.httpClient.get(this.baseUrl + "api/Sensors/GetAll");
    }
}
