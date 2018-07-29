class ResultsConverter:

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
