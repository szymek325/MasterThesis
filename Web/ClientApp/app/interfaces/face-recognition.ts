import { INeuralNetwork } from "./neuralNetwork/neural-network";
import {IFileLink} from "./file-link";

export interface IFaceRecognition {
    id: number;
    name: string;
    neuralNetwork: INeuralNetwork;
    thumbnail: string;
    status: string;
    creationTime: string;
    completionTime: string;
    fileLinks: IFileLink[];
}
