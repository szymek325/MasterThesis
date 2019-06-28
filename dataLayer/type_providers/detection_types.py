class DetectionTypes:

    @property
    def haar(self):
        return "haar"

    @property
    def dnn(self):
        return "dnn"

    @property
    def azure(self):
        return "azure"

    @property
    def haar_id(self):
        return 2

    @property
    def dnn_id(self):
        return 1

    @property
    def azure_id(self):
        return 3

    def get_type_id(self, face_detection_type_name: str):
        if face_detection_type_name.lower() == self.haar.lower():
            return self.haar_id
        elif face_detection_type_name.lower() == self.dnn.lower():
            return self.dnn_id
        elif face_detection_type_name.lower() == self.azure.lower():
            return self.azure_id
        else:
            raise Exception(f"Wrong face_detection_type_name. Cant find a match for : {face_detection_type_name}")
