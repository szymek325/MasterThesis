import { Component, OnInit } from "@angular/core";
import {ReadingsProviderService} from "../../services/readings-provider.service";
import {IReading} from "../../interfaces/reading";
import { ActivatedRoute } from "@angular/router";

@Component({
    selector: "app-sensor-readings",
    templateUrl: "./sensor-readings.component.html",
    styleUrls: ["./sensor-readings.component.css"]
})
export class SensorReadingsComponent implements OnInit {
    readings: IReading[];
    day: string;
    private sub: any;

    constructor(private route: ActivatedRoute, private readingsProvider: ReadingsProviderService) {}

    ngOnInit() {
        this.sub = this.route.params.subscribe(params => {
            this.day = params["day"]; // (+) converts string 'id' to a number

            // In a real app: dispatch action to load the details here.
        });
        this.readingsProvider.getDataPerDay(this.day)
            .subscribe(result => {
                    this.readings = result as IReading[];
                    console.log(this.readings);
                },
                error => { console.log(error) });
    }

}