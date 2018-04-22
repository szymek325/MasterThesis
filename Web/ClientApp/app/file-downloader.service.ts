
import { Injectable, Inject } from "@angular/core";
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs/Observable";


@Injectable()
export class FileDownloaderService {
    links: FileLink;


    constructor(private httpClient: HttpClient, @Inject("BASE_URL") private baseUrl: string) {}

    getFile(fileName: string): Observable<Blob> {
        const objectUrl: string = null;
        const params = new HttpParams().set("fileName", fileName);
        return this.httpClient.get(this.baseUrl + "getFileLink", { responseType: "blob", params: params });

        //.subscribe(
        //    m => {
        //        objectUrl = URL.createObjectURL(m);
        //        console.log(objectUrl);
        //    },
        //    error => { console.log(error) });
    }

    getFileNormal(fileName: string) {
        const params = new HttpParams().set("fileName", fileName);
        this.httpClient.get(this.baseUrl + "getFileLink", { params }).subscribe(result => {
                this.links = result as FileLink;
                console.log(this.links);
            },
            error => { console.log(error) });;
    }


    //.subscribe(
    //    m => {
    //        objectUrl = URL.createObjectURL(m);
    //        console.log(objectUrl);
    //    },
    //    error => { console.log(error) });


    //getFile(fileName: string): Observable<Blob> {
    //    const params = new HttpParams().set("fileName", fileName);
    //    return this.httpClient
    //        .get(this.baseUrl + "getFile", {
    //            responseType: "blob",
    //            params:params
    //        });
    //}

    //getFile(fileName: string): Observable<Blob>  {
    //    const params = new HttpParams().set("fileName", fileName);
    //    let objectUrl: string = null;

    //    this.httpClient
    //        .get(this.baseUrl + "getFile",
    //            {
    //                responseType: 'blob',
    //                params: params
    //            })
    //        .subscribe(m => {
    //            objectUrl = URL.createObjectURL(m);
    //        });
    //}
}

interface FileLink {
    url: string;
}