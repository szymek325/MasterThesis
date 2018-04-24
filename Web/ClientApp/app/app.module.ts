import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { HttpClientModule } from "@angular/common/http";
import { RouterModule } from "@angular/router";

import { AppComponent } from "./components/app/app.component";
import { NavMenuComponent } from "./components/navmenu/navmenu.component";
import { FetchDataComponent } from "./components/fetchdata/fetchdata.component";
import { SensorTableComponent } from "./components/readings/sensorstable.component";
import { SensorDataComponent } from "./components/readings/sensordata.component";
import { CounterComponent } from "./components/counter/counter.component";
import { AttachmentListComponent } from "./components/attachment-list/attachment-list.component";
import { FormdataUploadComponent } from "./components/formdata-upload/formdata-upload.component";
import { FileUploaderService } from "./services/file-uploader.service";
import { FileDownloaderService } from "./services/file-downloader.service";
import {HomeComponent} from "./components/home/home.component";
import { AlertComponent } from "./components/alert/alert.component";
import { AlertService } from "./services/alert.service";


@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        CounterComponent,
        FetchDataComponent,
        SensorTableComponent,
        SensorDataComponent,
        AttachmentListComponent,
        FormdataUploadComponent,
        HomeComponent,
        AlertComponent
    ],
    imports: [
        ReactiveFormsModule,
        CommonModule,
        HttpClientModule,
        FormsModule,
        RouterModule.forRoot([
            { path: "", redirectTo: "home", pathMatch: "full" },
            { path: "home", component: HomeComponent },
            { path: "counter", component: CounterComponent },
            { path: "fetch-data", component: FetchDataComponent },
            { path: "sensordata", component: SensorDataComponent },
            { path: "attachments", component: AttachmentListComponent },
            { path: "formdata-upload", component: FormdataUploadComponent }
        ])
    ],
    providers: [FileUploaderService, FileDownloaderService, AlertService],
})
export class AppModuleShared {
}