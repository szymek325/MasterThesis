import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class FaceDetectionService {

    constructor(private httpClient: HttpClient, @Inject("BASE_URL") private baseUrl: string) {}

    getAllFaceDetectionRequests(): Observable<Object> {
        return this.httpClient.get(this.baseUrl + "api/FaceDetection/GetAll");
    };

    getRequest(id: string): Observable<Object> {
        const params = new HttpParams().set("id", id);
        return this.httpClient.get(this.baseUrl + "api/FaceDetection/GetRequest", { params });
    };

    createNewRequest(formData: string): Observable<Object> {
        return this.httpClient.post<{ contents: string }>(this.baseUrl + "api/FaceDetection/Create", formData);
    };
}