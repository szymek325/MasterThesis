class ImageAttachmentTypes:

    @property
    def detection(self):
        return "Detection"

    @property
    def detection_result(self):
        return "DetectionResult"

    @property
    def recognition(self):
        return "Recognition"

    @property
    def person(self):
        return "Person"

    @property
    def movement(self):
        return "Movement"

    @property
    def detection_id(self):
        return 1

    @property
    def detection_result_id(self):
        return 2

    @property
    def recognition_id(self):
        return 3

    @property
    def person_id(self):
        return 4

    @property
    def movement_id(self):
        return 5

    def get_path_for_id(self, attachment_type_id: int):
        if attachment_type_id is self.detection_id:
            return self.detection
        elif attachment_type_id is self.detection_result_id:
            return self.detection_result
        elif attachment_type_id is self.recognition_id:
            return self.recognition
        elif attachment_type_id is self.person_id:
            return self.person
        elif attachment_type_id is self.movement_id:
            return self.movement
        else:
            raise Exception(f"Wrong attachment_type_id. Cant find a match for : {attachment_type_id}")

