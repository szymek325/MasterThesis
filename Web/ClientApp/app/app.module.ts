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
import { SensorReadingsComponent } from "./components/sensor-readings/sensor-readings.component";
import { ReadingsProviderService } from "./services/readings-provider.service";
import { PeopleComponent } from "./components/people/people.component";
import { PeopleService } from "./services/people.service";
import { NeuralNetworksComponent } from "./components/neural-networks/neural-networks.component";
import { NeuralNetworksService } from "./services/neural-networks.service";
import { NewPersonComponent } from "./components/people/new-person/new-person.component";
import { PersonComponent } from "./components/people/person/person.component";
import { FaceDetectionRequestComponent } from
    "./components/face-detections/face-detection-request/face-detection-request.component";
import { NewFaceDetectionComponent } from
    "./components/face-detections/new-face-detection/new-face-detection.component";
import { NewNeuralNetworkComponent } from './components/neural-networks/new-neural-network/new-neural-network.component';
import { NeuralNetworkComponent } from './components/neural-networks/neural-network/neural-network.component';
import { FaceRecognitionsComponent } from './components/face-recognitions/face-recognitions.component';
import { NewFaceRecognitionComponent } from './components/face-recognitions/new-face-recognition/new-face-recognition.component';
import { FaceRecognitionComponent } from './components/face-recognitions/face-recognition/face-recognition.component';
import { FaceRecognitionService } from './services/face-recognition.service';
import { ReadingsDatesComponent } from './components/readings-dates/readings-dates.component';


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
        NeuralNetworksComponent,
        NewNeuralNetworkComponent,
        NeuralNetworkComponent,
        FaceRecognitionsComponent,
        NewFaceRecognitionComponent,
        FaceRecognitionComponent,
        ReadingsDatesComponent
    ],
    imports: [
        ReactiveFormsModule,
        CommonModule,
        HttpClientModule,
        FormsModule,
        RouterModule.forRoot([
            { path: "", redirectTo: "home", pathMatch: "full" },
            { path: "home", component: HomeComponent },
            { path: "people", component: PeopleComponent },
            { path: "face-detection", component: FaceDetectionsComponent },
            { path: "new-face-detection", component: NewFaceDetectionComponent },
            { path: "face-detection-request/:id", component: FaceDetectionRequestComponent },
            { path: "new-person", component: NewPersonComponent },
            { path: "person/:id", component: PersonComponent },
            { path: "neural-networks", component: NeuralNetworksComponent },
            { path: "new-neural-network", component: NewNeuralNetworkComponent },
            { path: "neural-network/:id", component: NeuralNetworkComponent },
            { path: "face-recognitions", component: FaceRecognitionsComponent },
            { path: "new-face-recognition", component: NewFaceRecognitionComponent },
            { path: "face-recognition/:id", component: FaceRecognitionComponent },
            { path: "readings-dates", component: ReadingsDatesComponent },
            { path: "sensor-readings/:day", component: SensorReadingsComponent }
        ])
    ],
    providers: [
        AlertService, FaceDetectionService, ReadingsProviderService, PeopleService, NeuralNetworksService, FaceRecognitionService
    ],
})
export class AppModuleShared {
}