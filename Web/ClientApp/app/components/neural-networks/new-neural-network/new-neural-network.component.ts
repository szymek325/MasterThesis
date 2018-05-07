import { Component, OnInit } from "@angular/core";
import { PeopleService } from "../../../services/people.service";
import { NeuralNetworksService } from "../../../services/neural-networks.service";
import { IPerson } from "../../../interfaces/person";
import { FormBuilder, FormGroup, Validators  } from "@angular/forms";

@Component({
    selector: "app-new-neural-network",
    templateUrl: "./new-neural-network.component.html",
    styleUrls: ["./new-neural-network.component.css"]
})
export class NewNeuralNetworkComponent implements OnInit {
    people: IPerson[];
    neuralNetworkForm:FormGroup;

    constructor(private peopleService: PeopleService, private neuralNetworkService: NeuralNetworksService, private formBuilder: FormBuilder) {}

    ngOnInit() {
        this.peopleService.getAllPeople()
            .subscribe(result => {
                    this.people = result as IPerson[];
                    console.log(this.people);
                },
            error => { console.log(error) });
        this.neuralNetworkForm = this.formBuilder.group({
            name: ["name", [Validators.required, Validators.minLength(3)]],
            people:["people"],
        });
    }

}