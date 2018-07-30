class ResultsConverter:
    def __init__(self):
        # todo need to get this dictionary it somewhere
        self.person_id_names = {}

    def get_face_rectangles_from_result(self, result):
        faces = []
        for entry in result:
            values = entry['faceRectangle']
            left = int(values['left'])
            top = int(values['top'])
            width = int(values['width'])
            height = int(values['height'])
            faces.append([left, top, left + width, top + height])
        return faces

    def get_face_ids_from_result(self, result):
        face_ids = []
        for entry in result:
            face_id = entry['faceId']
            face_ids.append(face_id)
        return face_ids

    def get_face_identities_from_result(self, result):
        face_identities = []
        for entry in result:
            face_id = entry['faceId']
            if entry['candidates']:
                person_id = entry['candidates'][0]['personId']
                face_identities[face_id].set_name(self.person_id_names[person_id])
            else:
                face_identities[face_id].set_name('Unknown')
