import cv2
from datetime import datetime
from configuration_global.logger_factory import LoggerFactory


class ImageEditor():
    def __init__(self):
        self.logger = LoggerFactory()

    def draw_faces(self, source_image, faces):
        new_image = source_image.copy()
        if len(faces) is not 0:
            for face in faces:
                startX, startY, endX, endY = face
                cv2.rectangle(new_image, (startX, startY), (endX, endY), (0, 255, 0), 2)  # green
        return new_image

    def mark_frame(self, frame, stateOfRoom):
        ts = datetime.now().strftime("%A %d %B %Y %I:%M:%S%p")
        cv2.putText(frame, f"Room Status: {stateOfRoom}", (10, 20), cv2.FONT_HERSHEY_SIMPLEX, 0.5, (0, 0, 255), 2)
        cv2.putText(frame, ts, (10, frame.shape[0] - 10), cv2.FONT_HERSHEY_SIMPLEX, 0.35, (0, 0, 255), 1)

    def draw_contour(self, contours, frame):
        for c in contours:
            (x, y, w, h) = cv2.boundingRect(c)
            cv2.rectangle(frame, (x, y), (x + w, y + h), (0, 255, 0), 2)
