import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class NeuralNetworksService {

    constructor(private httpClient: HttpClient, @Inject("BASE_URL") private baseUrl: string) { }

    createNewNeuralNetwork(formData: string): Observable<Object> {
        return this.httpClient.post<{ contents: string }>(this.baseUrl + "api/NeuralNetwork/Create", formData);
    };

    getAllNeuralNetworks(): Observable<Object> {
        return this.httpClient.get(this.baseUrl + "api/NeuralNetwork/GetAll");
    };

    getNeuralNetwork(id: string): Observable<Object> {
        const params = new HttpParams().set("id", id);
        return this.httpClient.get(this.baseUrl + "api/NeuralNetwork/Get", { params });
    };

    deleteNeuralNetwork(id: string): Observable<Object> {
        const params = new HttpParams().set("id", id);
        return this.httpClient.delete(this.baseUrl + "api/NeuralNetwork/Delete", { params });
    };
}
