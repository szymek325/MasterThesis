import cognitive_face as CF


class CognitiveClient():
    def __init__(self):
        self.client = CF
        # TODO key and url from configuration
        self.client.Key.set("b204cc1207534676b3d77f668405dd95")
        # klucz 2:9a8c664f40b346b7ab1bd3949a3e5f7e
        self.client.BaseUrl.set("https://westcentralus.api.cognitive.microsoft.com/face/v1.0")

    def detect_faces(self, path):
        result = self.client.face.detect(path, True, False)
        return result

    def create_large_group(self, request_id: int, name: str):
        res = CF.large_person_group.create(request_id, name)
        return res

    def create_person_in_large_group(self, large_group_id, person_name):
        res = CF.large_person_group_person.create(large_group_id, person_name)
        return res

    def add_face_to_person_in_large_group(self, person_id, large_group_id, image_path):
        res = CF.large_person_group_person_face.add(image_path, large_group_id, person_id)
        return res

    def train_large_group(self, large_group_id):
        res = CF.large_person_group.train(large_group_id)
        return res

    def identify_faces(self, faces_ids, large_person_group):
        res = CF.face.identify(faces_ids, large_person_group_id=large_person_group)
        return res

    def get_large_group_status(self, large_group_id):
        res = CF.large_person_group.get_status(large_group_id)
        return res
