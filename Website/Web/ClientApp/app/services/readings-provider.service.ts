import { Injectable, Inject } from "@angular/core";
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs/Observable";

@Injectable()
export class ReadingsProviderService {

    constructor(private httpClient: HttpClient, @Inject("BASE_URL") private baseUrl: string) {}

    getAllReadings(): Observable<Object> {
        return this.httpClient.get(this.baseUrl + "api/Sensors/GetAll");
    }

    getAllDates(): Observable<Object> {
        return this.httpClient.get(this.baseUrl + "api/Sensors/GetAllDates");
    }

    getDataPerDay(date: string): Observable<Object> {
        const params = new HttpParams().set("day", date);
        return this.httpClient.get(this.baseUrl + "api/Sensors/GetDataPerDay", { params });
    }
}