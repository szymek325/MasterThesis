import { Component, OnInit } from "@angular/core";
import { NeuralNetworksService } from "../../services/neural-networks.service";
import { Router } from "@angular/router";
import { INeuralNetwork } from "../../interfaces/neural-network";

@Component({
    selector: "app-neural-networks",
    templateUrl: "./neural-networks.component.html",
    styleUrls: ["./neural-networks.component.css"]
})
export class NeuralNetworksComponent implements OnInit {
    neuralNetworks: INeuralNetwork[];

    constructor(private neuralNetworkService: NeuralNetworksService, private router: Router) {}

    newNetwork() {
        this.router.navigateByUrl("/new-neural-network");
    }

    showNetwork(id) {
        this.router.navigateByUrl(`/neural-network/${id}`);
    }

    ngOnInit() {
        this.neuralNetworkService.getAllNeuralNetworks()
            .subscribe(result => {
                    this.neuralNetworks = result as INeuralNetwork[];
                    console.log(this.neuralNetworks);
                },
                error => { console.log(error) });
    }

}