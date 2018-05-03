import { Component, OnInit } from "@angular/core";
import {ReadingsProviderService} from "../../services/readings-provider.service";
import {IReading} from "../../interfaces/reading";

@Component({
    selector: "app-sensor-readings",
    templateUrl: "./sensor-readings.component.html",
    styleUrls: ["./sensor-readings.component.css"]
})
export class SensorReadingsComponent implements OnInit {
    readings: IReading[];

    constructor(private readingsProvider: ReadingsProviderService) {}

    ngOnInit() {
        this.readingsProvider.getAllReadings()
            .subscribe(result => {
                    this.readings = result as IReading[];
                    console.log(this.readings);
                },
                error => { console.log(error) });
    }

}