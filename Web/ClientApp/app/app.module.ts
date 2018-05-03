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
import { FileUploaderService } from "./services/file-uploader.service";
import { FileDownloaderService } from "./services/file-downloader.service";
import {HomeComponent} from "./components/home/home.component";
import { AlertComponent } from "./components/alert/alert.component";
import { AlertService } from "./services/alert.service";
import { DropboxPicturesExampleComponent } from
    "./components/dropbox-pictures-example/dropbox-pictures-example.component";
import { FaceDetectionsComponent } from "./components/face-detections/face-detections.component";
import { FaceDetectionService } from "./services/face-detection.service";
import { NewFaceDetectionComponent } from "./components/face-detections/new-face-detection.component";
import {FaceDetectionRequestComponent} from "./components/face-detections/face-detection-request.component";
import { SensorReadingsComponent } from "./components/sensor-readings/sensor-readings.component";
import { ReadingsProviderService } from "./services/readings-provider.service";


@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        CounterComponent,
        FetchDataComponent,
        SensorTableComponent,
        SensorDataComponent,
        AttachmentListComponent,
        HomeComponent,
        AlertComponent,
        DropboxPicturesExampleComponent,
        FaceDetectionsComponent,
        NewFaceDetectionComponent,
        FaceDetectionRequestComponent,
        SensorReadingsComponent
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
            { path: "dropboxpictures", component: DropboxPicturesExampleComponent },
            { path: "face-detection", component: FaceDetectionsComponent },
            { path: "new-face-detection", component: NewFaceDetectionComponent },
            { path: "face-detection-request/:id", component: FaceDetectionRequestComponent },
            { path: "sensor-readings", component: SensorReadingsComponent }
        ])
    ],
    providers: [
        FileUploaderService, FileDownloaderService, AlertService, FaceDetectionService, ReadingsProviderService
    ],
})
export class AppModuleShared {
}