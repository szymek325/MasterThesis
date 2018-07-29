import cognitive_face as CF

from configuration_global.logger_factory import LoggerFactory


class CognitiveClient():
    def __init__(self):
        self.client = CF
        self.client.Key.set("b204cc1207534676b3d77f668405dd95")
        # klucz 2:9a8c664f40b346b7ab1bd3949a3e5f7e
        self.client.BaseUrl.set("https://westcentralus.api.cognitive.microsoft.com/face/v1.0")
        self.logger = LoggerFactory()

    def detect_faces(self, path):
        result = self.client.face.detect(path, False, False)
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

    def identify_faces(self, faces_ids, large_person_group):
        res = CF.face.identify(faces_ids, large_person_group_id=large_person_group)
        return res
    # def detect_face_rectangles(self, path):
    #     try:
    #         result = self.client.face.detect(path, False, False)
    #         faces = [self.resultsConverter.convert_to_coordinates_format(face) for face in result]
    #         return faces
    #     except self.client.CognitiveFaceException as exp:
    #         self.logger.error(f'Exception when sending face recognition request. Response: {exp.code}. {exp.msg}')
    #
    # def detect_face_rectangles(self, path):
    #     try:
    #         result = self.client.face.detect(path, False, False)
    #         faces = [self.resultsConverter.convert_to_coordinates_format(face) for face in result]
    #         return faces
    #     except self.client.CognitiveFaceException as exp:
    #         self.logger.error(f'Exception when sending face recognition request. Response: {exp.code}. {exp.msg}')
    #
    # def create_large_group(self, request_id: int, name: str):
    #     self.logger.info(f"Creating Azure LargePersonGroup with id: {request_id}, name : {name}")
    #     try:
    #         CF.large_person_group.create(request_id)
    #     except Exception as ex:
    #         self.logger.error(f"Exception when creating large group. Ex: {ex}")
    #         raise

    # def create_person_in_large_group(self, large_group_id, person_name):
    #     self.logger.info(f"Creating person {person_name} in large group {large_group_id}")
    #     try:
    #         res = CF.large_person_group_person.create(large_group_id, person_name)
    #         person_id = res['personId']
    #         return person_id
    #     except Exception as ex:
    #         self.logger.error(f"Exception when creating person in large group. Ex: {ex}")
    #         raise

    # def add_face_to_person_in_large_group(self, person_id, large_group_id, image_path):
    #     self.logger.info(f"Adding image {image_path} to person {person_id} in large group {large_group_id}")
    #     try:
    #         CF.large_person_group_person_face.add(image_path, large_group_id, person_id)
    #     except Exception as ex:
    #         self.logger.error(f"Exception when adding face to person in large group. Ex: {ex}")
    #         raise
