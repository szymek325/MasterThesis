class ResultsConverter:

    def convert_to_coordinates_format(self, face):
        values = face['faceRectangle']
        left = int(values['left'])
        top = int(values['top'])
        width = int(values['width'])
        height = int(values['height'])
        return left, top, left + width, top + height
