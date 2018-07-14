import { IPerson } from "./person";
export interface INeuralNetwork {
    name: string;
    description: string;
    status: string;
    people: IPerson[];
    creationTime: string;
    completionTime: string;
    files: INeuralNetworkFile[];
}
