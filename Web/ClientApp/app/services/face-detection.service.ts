import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class FaceDetectionService {

    constructor(private httpClient: HttpClient, @Inject("BASE_URL") private baseUrl: string) { }

    getAllFaceDetectionRequests(): Observable<Object> {
        return this.httpClient.get(this.baseUrl + "api/FaceDetection/GetAll");
    };
}
