import { Component, OnInit } from "@angular/core";
import { PeopleService } from "../../../services/people.service";
import { NeuralNetworksService } from "../../../services/neural-networks.service";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Router } from "@angular/router";
import { AlertService } from "../../../services/alert.service";

@Component({
    selector: "app-new-neural-network",
    templateUrl: "./new-neural-network.component.html",
    styleUrls: ["./new-neural-network.component.css"]
})
export class NewNeuralNetworkComponent implements OnInit {
    formData: any;
    people: IPersonOption[];
    neuralNetworkForm: FormGroup;

    constructor(private alertService: AlertService,
        private peopleService: PeopleService,
        private neuralNetworkService: NeuralNetworksService,
        private formBuilder: FormBuilder,
        private router: Router) {
    }


    onClickSubmit(data) {
        console.log(data);
        var chosenPeople=this.selectedOptions();
        console.log(chosenPeople);
        this.formData = new FormData();
        this.formData.set("name", data.name);
        this.formData.set("people", chosenPeople);
        console.log(this.formData);

        this.neuralNetworkService.createNewNeuralNetwork(this.formData)
            .subscribe(result => {
                    if (result === 0) {
                        alert("Exception occured during request creation");
                    }
                    console.log(result);
                    this.router.navigateByUrl("neural-networks");
                },
                error => {
                    console.log(error.message);
                    this.alertService.error(error.message);
                });
    }

    selectedOptions() { // right now: ['1','3']
        return this.people
            .filter(opt => opt.checked)
            .map(opt => opt.id);
    }

    ngOnInit() {
        this.peopleService.getAllPeople()
            .subscribe(result => {
                    this.people = result as IPersonOption[];
                    console.log(this.people);
                },
                error => { console.log(error) });
        this.neuralNetworkForm = this.formBuilder.group({
            name: ["name", [Validators.required, Validators.minLength(3)]],
            people: ["people"],
        });
    }

}

interface IPersonOption {
    id: number;
    name: string;
    thumbnail: string;
    checked: false;
}