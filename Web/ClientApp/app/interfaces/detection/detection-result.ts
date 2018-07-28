import { IFileLink } from "../file-link";

export interface IDetectionResult {
    startX: number;
    endX: number;
    startY: number;
    endY: number;
    image: IFileLink;
    detectionTypeName: string;
    faceRectangles: IFaceRectangle[];
}