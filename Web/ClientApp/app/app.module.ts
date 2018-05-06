import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { HttpClientModule } from "@angular/common/http";
import { RouterModule } from "@angular/router";

import { AppComponent } from "./components/app/app.component";
import { NavMenuComponent } from "./components/navmenu/navmenu.component";
import {HomeComponent} from "./components/home/home.component";
import { AlertComponent } from "./components/alert/alert.component";
import { AlertService } from "./services/alert.service";
import { FaceDetectionsComponent } from "./components/face-detections/face-detections.component";
import { FaceDetectionService } from "./services/face-detection.service";
import { NewFaceDetectionComponent } from "./components/face-detections/new-face-detection.component";
import {FaceDetectionRequestComponent} from "./components/face-detections/face-detection-request.component";
import { SensorReadingsComponent } from "./components/sensor-readings/sensor-readings.component";
import { ReadingsProviderService } from "./services/readings-provider.service";
import { PeopleComponent } from "./components/people/people.component";
import { NewPersonComponent } from "./components/new-person/new-person.component";
import { PeopleService } from "./services/people.service";
import { PersonComponent } from "./components/person/person.component";
import { NeuralNetworksComponent } from './components/neural-networks/neural-networks.component';
import { NeuralNetworksService } from './services/neural-networks.service';


@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        HomeComponent,
        AlertComponent,
        FaceDetectionsComponent,
        NewFaceDetectionComponent,
        FaceDetectionRequestComponent,
        SensorReadingsComponent,
        PeopleComponent,
        NewPersonComponent,
        PersonComponent,
        NeuralNetworksComponent
    ],
    imports: [
        ReactiveFormsModule,
        CommonModule,
        HttpClientModule,
        FormsModule,
        RouterModule.forRoot([
            { path: "", redirectTo: "home", pathMatch: "full" },
            { path: "home", component: HomeComponent },
            { path: "face-detection", component: FaceDetectionsComponent },
            { path: "new-face-detection", component: NewFaceDetectionComponent },
            { path: "face-detection-request/:id", component: FaceDetectionRequestComponent },
            { path: "sensor-readings", component: SensorReadingsComponent },
            { path: "people", component: PeopleComponent },
            { path: "new-person", component: NewPersonComponent },
            { path: "person/:id", component: PersonComponent },
            { path: "neural-networks", component: NeuralNetworksComponent }
        ])
    ],
    providers: [
        AlertService, FaceDetectionService, ReadingsProviderService, PeopleService, NeuralNetworksService
    ],
})
export class AppModuleShared {
}