import { INeuralNetwork } from "../neuralNetwork/neural-network";
import { IFileLink } from "../file-link";

export interface IFaceRecognition {
    id: number;
    name: string;
    thumbnail: string;
    status: string;
    creationTime: string;
    completionTime: string;
    fileLink: IFileLink;
    results: IRecognitionResult[];
}