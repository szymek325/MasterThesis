import { Injectable, Inject } from "@angular/core";
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs/Observable";

@Injectable()
export class FaceRecognitionService {

    constructor(private httpClient: HttpClient, @Inject("BASE_URL") private baseUrl: string) {}

    createRequest(formData: string): Observable<Object> {
        return this.httpClient.post<{ contents: string }>(this.baseUrl + "api/FaceRecognition/Create", formData);
    };

    getAllFaceRecognitions(): Observable<Object> {
        return this.httpClient.get(this.baseUrl + "api/FaceRecognition/GetAll");
    };

    getFaceRecognition(id: string): Observable<Object> {
        const params = new HttpParams().set("id", id);
        return this.httpClient.get(this.baseUrl + "api/FaceRecognition/GetRequest", { params });
    };

}