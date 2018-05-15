import { Component, OnInit } from "@angular/core";
import { NeuralNetworksService } from "../../../services/neural-networks.service";
import { ActivatedRoute } from "@angular/router";
import { INeuralNetwork } from "../../../interfaces/neural-network";

@Component({
    selector: "app-neural-network",
    templateUrl: "./neural-network.component.html",
    styleUrls: ["./neural-network.component.css"]
})
export class NeuralNetworkComponent implements OnInit {
    neuralNetwork: any;
    id: number;
    private sub: any;

    constructor(private route: ActivatedRoute, private neuralNetworkService: NeuralNetworksService) {}

    ngOnInit() {
        this.sub = this.route.params.subscribe(params => {
            this.id = +params["id"]; // (+) converts string 'id' to a number

            // In a real app: dispatch action to load the details here.
        });
        this.neuralNetworkService.getNeuralNetwork(this.id.toString())
            .subscribe(result => {
                    this.neuralNetwork = result as INeuralNetwork;
                    console.log(this.neuralNetwork);
                },
                error => { console.log(error) });
    }

}