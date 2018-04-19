import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from "./components/app/app.component";
import { NavMenuComponent } from "./components/navmenu/navmenu.component";
import { FetchDataComponent } from "./components/fetchdata/fetchdata.component";
import { HomeComponent } from "./components/home/home.component";
import { ActivitiesComponent } from "./components/activities/activities.component";
import { AccessControlComponent } from "./components/accesscontrol/accesscontrol.component";
import { HomeControlComponent } from "./components/control/homecontrol.component";
import { SensorTableComponent } from "./components/readings/sensorstable.component";
import { SensorDataComponent } from "./components/readings/sensordata.component";
import { CounterComponent } from "./components/counter/counter.component";
import { AttachmentListComponent } from "./components/attachment-list/attachment-list.component";
import { FormdataUploadComponent } from "./components/formdata-upload/formdata-upload.component";

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        CounterComponent,
        FetchDataComponent,
        HomeComponent,
        ActivitiesComponent,
        AccessControlComponent,
        HomeControlComponent,
        SensorTableComponent,
        SensorDataComponent,
        AttachmentListComponent,
        FormdataUploadComponent
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
            { path: "activities", component: ActivitiesComponent },
            { path: "accesscontrol", component: AccessControlComponent },
            { path: "homecontrol", component: HomeControlComponent },
            { path: "sensordata", component: SensorDataComponent },
            { path: "attachments", component: AttachmentListComponent },
            { path: "formdata-upload", component: FormdataUploadComponent }
        ])
    ],
})
export class AppModuleShared {
}
